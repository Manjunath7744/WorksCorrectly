using System.Web;
using System.Web.Optimization;

namespace AutoSherpa_project
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/public/javascripts/jquery.js",
                  "~/public/javascripts/jquery-ui.min.js",
                  "~/public/javascripts/bootstrap.min.js",
                  "~/Content/master/dist/js/master.js",
                  "~/public/javascripts/jquery.blockUI.js",
                  "~/public/javascripts/multiselect.js",
                  "~/public/javascripts/Chart.min.js",
                  "~/public/javascripts/fastclick.min.js",
                  "~/public/javascripts/lobibox.js",
                  "~/public/javascripts/datatables.min.js",
                  "~/public/javascripts/jquery.ui.position.min.js",
                  "~/public/javascripts/jquery.ui.timepicker.js",
                  "~/public/javascripts/jquery.bootstrap.wizard.js",
                  "~/public/javascripts/wizard.js",
                  "~/public/javascripts/jquery.datetimepicker.full.js",
                  "~/public/javascripts/jquery.datetimepicker.js",
                   "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                  "~/public/css/jquery-ui.min.css",
                  "~/public/css/bootstrap.min.css",
                  "~/Content/master/dist/css/skin-blue.css",
                  "~/public/css/AdminLTE.min.css",
                  "~/public/css/dataTables.bootstrap.css",
                  "~/public/css/dataTables.responsive.css",
                  "~/public/css/font-awesome.min.css",
                  "~/public/css/CutomizedNewIconCSS.css",
                       "~/public/css/ionicons.min.css",
                       "~/public/css/animate.css",
                        "~/public/css/lobibox.min.css",
                        "~/public/css/Wyz_css.css",
                        "~/public/css/multiselect.css",
                        "~/public/css/jquery.datetimepicker.css"
                 ));


            bundles.Add(new Bundle("~/bundles/liveRptJqury").IncludeDirectory(@"~/public/liveReportjs", "*.js"));

            bundles.Add(new StyleBundle("~/bundles/CallLoggingCSS").Include(
                    "~/public/css/cre.css",
                    "~/public/PullOuts/psfPullOut.css",
                    "~/public/CallLogFiles/jquery-confirm.min.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/CallLoggingJS").Include(
                "~/public/CallLogFiles/commondisposition.js",
                "~/public/CallLogFiles/insuranceOutBounddisposition.js",
                "~/public/CallLogFiles/insuranceQuote.js",
                "~/public/CallLogFiles/ajaxCallScripts.js",
                    "~/public/CallLogFiles/popupValidations.js",
                    "~/public/CallLogFiles/jquery-confirm.min.js",
                    "~/public/CallLogFiles/callLoggingMain.js"//,
                    //"~/public/CallLogFiles/ABTC2C.js" // Newly Added
                ));

            bundles.Add(new ScriptBundle("~/bundles/BlueIMPFiles").Include(
                "~/public/BlueIMPFiles/tmpl.min.js",
                "~/public/BlueIMPFiles/load-image.all.min.js",
                "~/public/BlueIMPFiles/canvas-to-blob.min.js",
                "~/public/BlueIMPFiles/jquery.blueimp-gallery.min.js",
                "~/public/js/jquery.iframe-transport.js",
                "~/public/js/vendor/jquery.ui.widget.js",
                "~/public/js/jquery.fileupload.js",
                "~/public/js/jquery.fileupload-process.js",
                "~/public/js/jquery.fileupload-image.js",
                "~/public/js/jquery.fileupload-audio.js",
                "~/public/js/jquery.fileupload-video.js",
                "~/public/js/jquery.fileupload-validate.js",
                "~/public/js/jquery.fileupload-ui.js",
                "~/public/js/main.js"
                ));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js",
            //            "~/Scripts/jquery.validate.js",
            //            "~/Scripts/jquery.validate.min.js",
            //             "~/sweetAlerts/sweetalert2.min.js",
            //             "~/Content/master/bootstrap-select-1.13.9/dist/js/bootstrap-select.js",
            //            "~/Scripts/jquery.validate.unobtrusive.min.js",
            //            "~/Content/DataTable/dataTables.bootstrap.min.js",
            //            "~/Content/DataTable/dataTables.buttons.min.js",
            //            "~/Content/DataTable/dataTables.min.js",
            //            "~/Content/DataTable/dataTables.responsive.min.js",
            //             "~/Content/master/master/bower_components/jquery/dist/jquery.min.js",
            //            "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/master").Include(
            //      "~/Content/master/dist/js/master.js",
            //         "~/public/javascripts/multiselect.js",
            //         "~/public/javascripts/lobibox.js",
            //             "~/public/javascripts/jquery-ui.min.js"
            //   //"~/Content/master/bower_components/jquery-ui/jquery-ui.min.js"

            //   ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //      "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //             "~/Content/master/bower_components/bootstrap/dist/js/bootstrap.min.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //      "~/Content/master/bower_components/bootstrap/dist/css/bootstrap.min.css",
            //      "~/public/css/jquery-ui.min.css",
            //           "~/Content/master/dist/css/skin-blue.css",
            //          "~/Content/site.css",
            //          // "~/Content/roboto.css",
            //    //       "~/sweetAlerts/sweetalert2.min.css",
            //           "~/Content/master/bootstrap-select-1.13.9/dist/css/bootstrap-select.css",
            //              "~/public/css/AdminLTE.min.css",
            //              "~/public/css/font-awesome.min.css",
            //              "~/public/css/CutomizedNewIconCSS.css",
            //                "~/public/css/ionicons.min.css",
            //                 "~/public/css/animate.css",
            //            "~/public/css/lobibox.min.css",
            //            "~/public/css/Wyz_css.css",
            //            "~/public/css/multiselect.css",                      
            //        //   "~/Content/master/bower_components/font-awesome/css/font-awesome.min.css",
            //   //   "~/Content/master/bower_components/Ionicons/css/ionicons.min.css",
            //    //    "~/Content/master/dist/css/master.css",                   
            //          //   "~/Content/animate3.7.2.css",
            //          "~/Content/DataTable/datatables.bootstrap.css",
            //          "~/Content/DataTable/datatables.responsive.css"));

            //bundles.Add(new StyleBundle("~/Content/card").Include(
            //    "~/Content/card.css"));
            //bundles.Add(new StyleBundle("~/Content/tablecss").Include(
            //   "~/Content/tablecss.css"));
            //bundles.Add(new StyleBundle("~/Content/formcss").Include(
            //    "~/Content/formcss.css"));
            //bundles.Add(new ScriptBundle("~/bundles/Custom").Include("~/Scripts/Custom/BOM.js"));

            //bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(

            //     "~/public/javascripts/jquery.ui.timepicker.js",             
            //     "~/public/css/jquery.datetimepicker.css",
            //     "~/public/javascripts/jquery.datetimepicker.js"
            //     //"~/Content/master/datepicker/jquery-ui.css",
            //     //"~/Content/master/datepicker/jquery-ui.js",
            //     //"~/Content/master/datepicker/jquery.min.js"
            //     ));
        }

    }
}
