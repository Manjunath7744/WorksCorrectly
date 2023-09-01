using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AutoSherpa_project.Models.APIKey_Handler
{
    public class APIkkeyHandler: DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.PathAndQuery.Contains("/ServiceDetailsConf/serviceDueDetailsAPI") || request.RequestUri.PathAndQuery.Contains("/knwolaritycallDetails/knwolarityCallRecordingAPI"))
            {
                string ApiKey = string.Empty;
                using (var db = new AutoSherDBContext())
                {
                    ApiKey = db.dealers.FirstOrDefault().authorizationkey;
                }

                bool isValidAPIKey = false;
                IEnumerable<string> lsHeaders;

                if (ApiKey != null)
                {
                    var checkApiKeyExists = request.Headers.TryGetValues("Authorization", out lsHeaders);

                    if (checkApiKeyExists)
                    {
                        if (lsHeaders.FirstOrDefault().Equals(ApiKey))
                        {
                            isValidAPIKey = true;
                        }
                    }
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.Forbidden, "API KEY NOT PRESENT IN DATABASE");
                }

                //If the key is not valid, return an http status code.
                if (!isValidAPIKey)
                    return request.CreateResponse(HttpStatusCode.Forbidden, "Invalid API Key");

                //Allow the request to process further down the pipeline
                var response = await base.SendAsync(request, cancellationToken);

                //Return the response back up the chain
                return response;
            }
            else
            {
                var response = await base.SendAsync(request, cancellationToken);
                return response;

            }

        }
    }
}