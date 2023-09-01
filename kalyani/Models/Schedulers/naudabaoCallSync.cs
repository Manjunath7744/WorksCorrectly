using MySql.Data.MySqlClient;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.Schedulers
{
    [DisallowConcurrentExecution]
    public class naudabaoCallSync :schedulerCommonFunction, IJob
    {
        Logger logger = LogManager.GetLogger("apkRegLogger");
        DataTable dt = new DataTable();
        public async Task Execute(IJobExecutionContext context)
        {
            string siteRoot = HttpRuntime.AppDomainAppVirtualPath;
            if (siteRoot != "/")
            {
                List<naudabaoCredintials> credintialsDetails = new List<naudabaoCredintials>();
                logger.Info("\n\n -------- Pulling data from naubadao DB to autosherpa -------------AT  : " + DateTime.Now);
                try
                {
                    using (var db = new AutoSherDBContext())
                    {
                        schedulers schedulerDetails = db.schedulers.FirstOrDefault(m => m.scheduler_name == "naubadaocallsynch");

                        if (schedulerDetails != null && schedulerDetails.isActive == true && schedulerDetails.IsItRunning == false)
                        {
                            startScheduler("naubadaocallsynch");

                            credintialsDetails = db.NaudabaoCredintials.ToList();
                            foreach (var naudaboCred in credintialsDetails)
                            {

                                long totalCount = counttotalRecords(naudaboCred.naudabaouip, naudaboCred.naudabaodb, naudaboCred.naudabaouid, naudaboCred.naudabaopassword, naudaboCred.naudabaoport);
                                logger.Info("\n\n -------- Total Records in nau_cdr_sync_manual_{0} - {1}", naudaboCred.synctype, totalCount);

                                for (int i = 0; i <= totalCount; i = i + 50000)
                                {
                                    try
                                    {
                                        dt = new DataTable();
                                        fetchmySqlRead(0, 50000, naudaboCred.naudabaouip, naudaboCred.naudabaodb, naudaboCred.naudabaouid, naudaboCred.naudabaopassword, naudaboCred.naudabaoport);
                                        insrtMysql(naudaboCred.synctype);
                                    }
                                    catch (Exception ex)
                                    {
                                        string exception = "";
                                        if (ex.Message.Contains("inner exception"))
                                        {
                                            exception = ex.InnerException.Message;
                                        }
                                        else
                                        {
                                            exception = ex.Message;
                                        }
                                        logger.Info("\n\n --------  Extraction Error Inside Loop{0} Naudabao  Breaking loop-------------: ", exception);
                                    }
                                }
                            }

                            if (credintialsDetails != null)
                            {
                               
                                    logger.Info("\n\n -------- EVENT_Callsync Procedure Called -------------AT  : ");

                                    db.Database.ExecuteSqlCommandAsync("CALL EVENT_Callsync()");
                            }

                            stopScheduler("naubadaocallsynch");
                        }
                        else
                        {
                            logger.Info("\n naubadaocallsynch Inactive / Not Exist / Already Running");
                        }
                    }
                }
                catch (Exception ex)
                {
                    stopScheduler("naubadaocallsynch");

                    string exception = "";
                    if (ex.Message.Contains("inner exception"))
                    {
                        exception = ex.InnerException.Message;
                    }
                    else
                    {
                        exception = ex.Message;
                    }
                    logger.Info("\n\n --------  Extraction Error Outer Exeption{0} Naudabao -------------: ", exception);
                }
                logger.Info("\n\n -------- Pulling data from Naudabao to autosherpa Ended -------------AT  : " + DateTime.Now);

            }
        }
        #region Get Total Count
        public long counttotalRecords(string ip, string db, string uid, string pass,string port)
        {
            long totalCount = 0;
            string constring = @"Server=" + ip + ";Database=" + db + ";Uid=" + uid + ";Pwd=" + pass + ";port=" + port +";integrated security=True";
            using (MySqlConnection con = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand("select  count(*) from nau_cdr_sync_manual where date(calldate) = curdate()", con))
                {
                    con.Open();
                    totalCount = (int)(long)cmd.ExecuteScalar();
                    con.Close();
                }
            }
            return totalCount;
        }
        #endregion

        #region Get Records
        public DataTable fetchmySqlRead(int fromlimit, int tolimit, string ip, string db, string uid, string pass,string port)
        {
            logger.Info("\n\n -------- Data Extraction Started from Naudabo  From Limit-{0} - To Limit {1}-------------  : ", fromlimit, tolimit);

            string constring = @"Server=" + ip + ";Database=" + db + ";Uid=" + uid + ";Pwd=" + pass + ";port=" + port + ";integrated security=True";
            using (MySqlConnection con = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from nau_cdr_sync_manual where date(calldate) = curdate()  order by id  limit " + fromlimit + "," + tolimit, con))
                {
                    cmd.CommandTimeout = 300;
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            logger.Info("\n\n -------- Data Extraction Ended from Naudabo  From Limit-{0} - To Limit {1}-------------  : ", fromlimit, tolimit);

            return dt;
        }
        #endregion

        #region Creating Multiple Insert Query
        public String BulkInsert(ref DataTable table, String table_name)
        {
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                DateTime dt;

                queryBuilder.AppendFormat("INSERT INTO `{0}` (", table_name);

                // more than 1 column required and 1 or more rows
                if (table.Columns.Count > 1 && table.Rows.Count > 0)
                {
                    // build all columns
                    queryBuilder.AppendFormat("`{0}`", table.Columns[0].ColumnName);

                    if (table.Columns.Count > 1)
                    {
                        for (int i = 1; i < table.Columns.Count; i++)
                        {
                            queryBuilder.AppendFormat(", `{0}` ", table.Columns[i].ColumnName);
                        }
                    }

                    queryBuilder.AppendFormat(") VALUES (");

                    // build all values for the first row
                    // escape String & Datetime values!
                    if (table.Columns[0].DataType == typeof(String))
                    {
                        queryBuilder.AppendFormat("'{0}'", MySqlHelper.EscapeString(table.Rows[0][table.Columns[0].ColumnName].ToString()));
                    }
                    else if (table.Columns[0].DataType == typeof(DateTime))
                    {
                        if (table.Rows[0][table.Columns[0].ColumnName].ToString() != "" && table.Rows[0][table.Columns[0].ColumnName] != DBNull.Value)
                        {
                            dt = (DateTime)table.Rows[0][table.Columns[0].ColumnName];
                            queryBuilder.AppendFormat("'{0}'", dt.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            queryBuilder.AppendFormat("null");

                        }
                    }
                    else if (table.Columns[0].DataType == typeof(Int32))
                    {
                        queryBuilder.AppendFormat("{0}", table.Rows[0].Field<Int32?>(table.Columns[0].ColumnName) ?? 0);
                    }
                    else if (table.Columns[0].DataType == typeof(Int64))
                    {
                        queryBuilder.AppendFormat("{0}", table.Rows[0].Field<Int64?>(table.Columns[0].ColumnName) ?? 0);
                    }
                    else if (table.Columns[0].DataType == typeof(decimal))
                    {
                        queryBuilder.AppendFormat("{0}", table.Rows[0].Field<decimal?>(table.Columns[0].ColumnName) ?? 0);
                    }
                    else
                    {
                        queryBuilder.AppendFormat("{0}", table.Rows[0][table.Columns[0].ColumnName].ToString());
                    }

                    for (int i = 1; i < table.Columns.Count; i++)
                    {
                        // escape String & Datetime values!
                        if (table.Columns[i].DataType == typeof(String))
                        {
                            queryBuilder.AppendFormat(", '{0}'", MySqlHelper.EscapeString(table.Rows[0][table.Columns[i].ColumnName].ToString()));
                        }
                        else if (table.Columns[i].DataType == typeof(DateTime))
                        {
                            if (table.Rows[0][table.Columns[i].ColumnName].ToString() != "" && table.Rows[0][table.Columns[i].ColumnName] != DBNull.Value)
                            {
                                dt = (DateTime)table.Rows[0][table.Columns[i].ColumnName];
                                queryBuilder.AppendFormat(", '{0}'", dt.ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                queryBuilder.AppendFormat(", null");
                            }

                        }
                        else if (table.Columns[i].DataType == typeof(Int64))
                        {
                            queryBuilder.AppendFormat(", {0}", table.Rows[0].Field<Int64?>(table.Columns[i].ColumnName) ?? 0);
                        }
                        else if (table.Columns[i].DataType == typeof(Int32))
                        {
                            queryBuilder.AppendFormat(", {0}", table.Rows[0].Field<Int32?>(table.Columns[i].ColumnName) ?? 0);
                        }
                        else if (table.Columns[i].DataType == typeof(decimal))
                        {
                            queryBuilder.AppendFormat(", {0}", table.Rows[0].Field<decimal?>(table.Columns[i].ColumnName) ?? 0);
                        }
                        else
                        {
                            queryBuilder.AppendFormat(", {0}", table.Rows[0][table.Columns[i].ColumnName].ToString());
                        }
                    }

                    queryBuilder.Append(")");
                    queryBuilder.AppendLine();

                    // build all values all remaining rows
                    if (table.Rows.Count > 1)
                    {
                        // iterate over the rows
                        for (int row = 1; row < table.Rows.Count; row++)
                        {
                            // open value block
                            queryBuilder.Append(", (");

                            // escape String & Datetime values!
                            if (table.Columns[0].DataType == typeof(String))
                            {
                                queryBuilder.AppendFormat("'{0}'", MySqlHelper.EscapeString(table.Rows[row][table.Columns[0].ColumnName].ToString()));
                            }
                            else if (table.Columns[0].DataType == typeof(DateTime))
                            {
                                if (table.Rows[row][table.Columns[0].ColumnName].ToString() != "" && table.Rows[row][table.Columns[0].ColumnName] != DBNull.Value)
                                {
                                    dt = (DateTime)table.Rows[row][table.Columns[0].ColumnName];
                                    queryBuilder.AppendFormat("'{0}'", dt.ToString("yyyy-MM-dd"));
                                }
                                else
                                {
                                    queryBuilder.AppendFormat(", null");
                                }
                            }
                            else if (table.Columns[0].DataType == typeof(Int32))
                            {
                                queryBuilder.AppendFormat("{0}", table.Rows[row].Field<Int32?>(table.Columns[0].ColumnName) ?? 0);
                            }
                            else if (table.Columns[0].DataType == typeof(Int64))
                            {
                                queryBuilder.AppendFormat("{0}", table.Rows[row].Field<Int64?>(table.Columns[0].ColumnName) ?? 0);
                            }
                            else if (table.Columns[0].DataType == typeof(decimal))
                            {
                                queryBuilder.AppendFormat("{0}", table.Rows[row].Field<decimal?>(table.Columns[0].ColumnName) ?? 0);
                            }
                            else
                            {
                                queryBuilder.AppendFormat(", {0}", table.Rows[row][table.Columns[0].ColumnName].ToString());
                            }

                            for (int col = 1; col < table.Columns.Count; col++)
                            {
                                // escape String & Datetime values!
                                if (table.Columns[col].DataType == typeof(String))
                                {
                                    queryBuilder.AppendFormat(", '{0}'", MySqlHelper.EscapeString(table.Rows[row][table.Columns[col].ColumnName].ToString()));
                                }
                                else if (table.Columns[col].DataType == typeof(DateTime))
                                {
                                    if (table.Rows[row][table.Columns[col].ColumnName].ToString() != "" && table.Rows[row][table.Columns[col].ColumnName] != DBNull.Value)
                                    {
                                        dt = (DateTime)table.Rows[row][table.Columns[col].ColumnName];
                                        queryBuilder.AppendFormat(", '{0}'", dt.ToString("yyyy-MM-dd"));
                                    }
                                    else
                                    {
                                        queryBuilder.AppendFormat(", null");

                                    }
                                }
                                else if (table.Columns[col].DataType == typeof(Int32))
                                {
                                    queryBuilder.AppendFormat(", {0}", table.Rows[row].Field<Int32?>(table.Columns[col].ColumnName) ?? 0);
                                }
                                else if (table.Columns[col].DataType == typeof(Int64))
                                {
                                    queryBuilder.AppendFormat(", {0}", table.Rows[row].Field<Int64?>(table.Columns[col].ColumnName) ?? 0);
                                }
                                else if (table.Columns[col].DataType == typeof(decimal))
                                {
                                    queryBuilder.AppendFormat(", {0}", table.Rows[row].Field<decimal?>(table.Columns[col].ColumnName) ?? 0);
                                }
                                else
                                {
                                    queryBuilder.AppendFormat(", {0}", table.Rows[row][table.Columns[col].ColumnName].ToString());
                                }
                            } // end for (int i = 1; i < table.Columns.Count; i++)

                            // close value block
                            queryBuilder.Append(")");
                            queryBuilder.AppendLine();

                        } // end for (int r = 1; r < table.Rows.Count; r++)

                        // sql delimiter =)
                        queryBuilder.Append(";");

                    } // end if (table.Rows.Count > 1)

                    return queryBuilder.ToString();
                }
                else
                {
                    return "";
                } // end if(table.Columns.Count > 1 && table.Rows.Count > 0)
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        #endregion

        #region Inserting To Table
        public void insrtMysql(string tblname)
        {
            string firstCellValue = dt.Rows[0][0].ToString();
            string lastCellValue = dt.Rows[dt.Rows.Count - 1][0].ToString();
            logger.Info("\n\n -------- Inserting to MYSQl  From Index {0}  To Index  {1} -------------: ", firstCellValue, lastCellValue);
            using (var db = new AutoSherDBContext())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE nau_cdr_sync_manual_" + tblname.ToLower());

            }
            string rows = BulkInsert(ref dt, "nau_cdr_sync_manual_" + tblname.ToLower());
            string conStr = ConfigurationManager.ConnectionStrings["AutoSherContext"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                connection.Open();
                MySqlCommand myCmd = new MySqlCommand(rows, connection);
                myCmd.ExecuteNonQuery();

                connection.Close();
            }
            logger.Info("\n\n -------- Done Inserting to MYSQl  From Index {0} To Index {1}-------------: ", firstCellValue, lastCellValue);
        }
        #endregion
    }
}