using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Common
{
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly Func<Task<string>> getToken;

        public AuthenticatedHttpClientHandler(Func<Task<string>> getToken)
        {
            if (getToken == null) throw new ArgumentNullException("getToken");
            this.getToken = getToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //TODO: make it with polly
            int tryCount = 0;
            while (tryCount < 2)
            {
                try
                {
                    tryCount++;

                    // Add header for display language
                    request.Headers.Add("DisplayLanguage", SessionHelper.CurrentLanguageTwoChar);

                    var auth = request.Headers.Authorization;
                    if (auth != null && auth.Parameter !=null)
                    {
                        var token = SessionHelper.CurrentUser.UserToken;
                        request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
                    }

                    // U can use for testing
                    //if (request.Method == HttpMethod.Post && request.Content != null)
                    //{
                    //    var rawMessage = await request.Content.ReadAsStringAsync();
                    //}

                    var result = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                    switch (result.StatusCode)
                    {
                        case System.Net.HttpStatusCode.RequestTimeout:
                            await Task.Delay(500);
                            break;

                        case System.Net.HttpStatusCode.Unauthorized:
                            // TODO unauthorized
                            break;
                    }

                    return result;
                }
                catch (ApiException ex)
                {
                    switch (ex.StatusCode)
                    {
                        case System.Net.HttpStatusCode.RequestTimeout:
                            await Task.Delay(500);
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                            // TODO unauthorized
                            break;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Teknik bir sorun oluştu!");
                }
            }

            return null;

        }
    }
}
