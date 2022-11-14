using DataAPI.Config;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAPI.Caller
{
    public class ApiClient : IApiClient
    {
        private int LoopCount = 0;
        //  Sabre
        public async Task<HttpResponseMessage> GetSabreAsync<TReturn>(HttpContext request, string baseAddress, string ApiAddress, string AuthenticationHeaderType, string Token, string MediaType, Dictionary<string, string> cookies, Dictionary<string, string> headers, string requestSource)
        {
            try
            {
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                using (var client = new HttpClient(handler))
                {
                    //Add Cookie Values
                    if (cookies != null)
                    {
                        foreach (var cookie in cookies)
                        {
                            if (cookie.Key != null && cookie.Value != null)
                            {
                                cookieContainer.Add(new Uri(baseAddress), new Cookie(cookie.Key, cookie.Value));
                            }
                        }
                    }

                    ////Trust all certificates
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                    if (AuthenticationHeaderType != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationHeaderType, Token);
                    }
                    //Add Header Values
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            try
                            {
                                if (!client.DefaultRequestHeaders.Contains(header.Key))
                                {
                                    if (!String.IsNullOrEmpty(header.Value))
                                    {
                                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    client.Timeout = TimeSpan.FromMinutes(60);

                    HttpResponseMessage response = await client.GetAsync(ApiAddress);

                    return response;

                }

            }
            catch (Exception ex)
            {
                //await new LogStatus(request).TrackTrace("GetSabreAsync Method Exception has occured." + LoopCount, ex, "requestSource=" + requestSource + ";baseAddress=" + baseAddress + ";ApiAddress=" + ApiAddress + ";AuthenticationHeaderType" + AuthenticationHeaderType + ";Token=" + Token + ";MediaType=" + MediaType + ";HasValue:" + request.Request.Host.HasValue + ";Host:" + request.Request.Host.Host + ";Port:" + request.Request.Host.Port + ";Value:" + request.Request.Host.Value, null, null, this.GetType().Namespace + "." + this.GetType().Name);

                string exceptionMessage = (ex.InnerException != null ?
                    ex.InnerException.InnerException != null ?
                    ex.InnerException.InnerException.InnerException != null ?
                    ex.InnerException.InnerException.InnerException.InnerException != null ?
                    ex.InnerException.InnerException.InnerException.InnerException.Message
                    : ex.InnerException.InnerException.InnerException.Message
                    : ex.InnerException.InnerException.Message
                    : ex.InnerException.Message
                    : ex.Message)?.Trim()?.ToLower();

                return new HttpResponseMessage();
            }
        }
        public async Task<HttpResponseMessage> PostSabreAsync<ThttpResponseMessage, Tpara>(HttpContext request, string baseAddress, string ApiAddress, string AuthenticationHeaderType, string Token, Tpara para, string MediaType, Dictionary<string, string> cookies, Dictionary<string, string> headers, string requestSource)
        {
            try
            {
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                using (var client = new HttpClient(handler))
                {
                    //Add Cookie Values
                    if (cookies != null)
                    {
                        foreach (var cookie in cookies)
                        {
                            if (!String.IsNullOrEmpty(cookie.Value))
                            {
                                cookieContainer.Add(new Uri(baseAddress), new Cookie(cookie.Key, cookie.Value));
                            }

                        }
                    }


                    //////Trust all certificates
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                    if (AuthenticationHeaderType != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationHeaderType, Token);
                    }
                    //Add Header Values
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            try
                            {
                                if (!client.DefaultRequestHeaders.Contains(header.Key))
                                {
                                    if (!String.IsNullOrEmpty(header.Value))
                                    {
                                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    client.Timeout = TimeSpan.FromMinutes(60);
                    HttpResponseMessage response = null;

                    response = await client.PostAsync(ApiAddress,
                  new StringContent(JsonConvert.SerializeObject(para,
                            Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }).ToString(),
                  Encoding.UTF8, MediaType));

                    return response;
                }

            }
            catch (Exception ex)
            {
                //await new LogStatus(request).TrackTrace("PostSabreAsync Method Exception has occured." + LoopCount, ex, "requestSource=" + requestSource + ";baseAddress=" + baseAddress + ";ApiAddress=" + ApiAddress + ";AuthenticationHeaderType" + AuthenticationHeaderType + ";Token=" + Token + ";MediaType=" + MediaType + ";HasValue:" + request.Request.Host.HasValue + ";Host:" + request.Request.Host.Host + ";Port:" + request.Request.Host.Port + ";Value:" + request.Request.Host.Value, null, null, this.GetType().Namespace + "." + this.GetType().Name);

                string exceptionMessage = (ex.InnerException != null ?
                  ex.InnerException.InnerException != null ?
                  ex.InnerException.InnerException.InnerException != null ?
                  ex.InnerException.InnerException.InnerException.InnerException != null ?
                  ex.InnerException.InnerException.InnerException.InnerException.Message
                  : ex.InnerException.InnerException.InnerException.Message
                  : ex.InnerException.InnerException.Message
                  : ex.InnerException.Message
                  : ex.Message)?.Trim()?.ToLower();

                return new HttpResponseMessage();
            }
        }
        public async Task<HttpResponseMessage> PutSabreAsync<TreturnHttpResponseMessage, Tpara>(HttpContext request, string baseAddress, string ApiAddress, string AuthenticationHeaderType, string Token, Tpara para, string MediaType, Dictionary<string, string> cookies, Dictionary<string, string> headers, string requestSource)
        {
            try
            {
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                using (var client = new HttpClient(handler))
                {
                    //Add Cookie Values
                    if (cookies != null)
                    {
                        foreach (var cookie in cookies)
                        {
                            cookieContainer.Add(new Uri(baseAddress), new Cookie(cookie.Key, cookie.Value));
                        }
                    }

                    ////Trust all certificates
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationHeaderType, Token);
                    //Add Header Values
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            try
                            {
                                if (!client.DefaultRequestHeaders.Contains(header.Key))
                                {
                                    if (!String.IsNullOrEmpty(header.Value))
                                    {
                                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    client.Timeout = TimeSpan.FromMinutes(60);

                    HttpResponseMessage response = null;

                    response = await client.PutAsync(ApiAddress, new StringContent(JsonConvert.SerializeObject(para).ToString(), Encoding.UTF8, MediaType));

                    return response;
                }

            }
            catch (Exception ex)
            {
                //await new LogStatus(request).TrackTrace("PutSabreAsync Method Exception has occured." + LoopCount, ex, "requestSource=" + requestSource + ";baseAddress=" + baseAddress + ";ApiAddress=" + ApiAddress + ";AuthenticationHeaderType" + AuthenticationHeaderType + ";Token=" + Token + ";MediaType=" + MediaType + ";HasValue:" + request.Request.Host.HasValue + ";Host:" + request.Request.Host.Host + ";Port:" + request.Request.Host.Port + ";Value:" + request.Request.Host.Value, null, null, this.GetType().Namespace + "." + this.GetType().Name);

                string exceptionMessage = (ex.InnerException != null ?
                   ex.InnerException.InnerException != null ?
                   ex.InnerException.InnerException.InnerException != null ?
                   ex.InnerException.InnerException.InnerException.InnerException != null ?
                   ex.InnerException.InnerException.InnerException.InnerException.Message
                   : ex.InnerException.InnerException.InnerException.Message
                   : ex.InnerException.InnerException.Message
                   : ex.InnerException.Message
                   : ex.Message)?.Trim()?.ToLower();

                return new HttpResponseMessage();
            }
        }
        public async Task<HttpResponseMessage> DeleteSabreAsync<TreturnHttpResponseMessage>(HttpContext request, string baseAddress, string ApiAddress, string AuthenticationHeaderType, string Token, string MediaType, Dictionary<string, string> cookies, Dictionary<string, string> headers, string requestSource)
        {
            try
            {
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                using (var client = new HttpClient(handler))
                {
                    //Add Cookie Values
                    if (cookies != null)
                    {
                        foreach (var cookie in cookies)
                        {
                            if (cookie.Value != null)
                                cookieContainer.Add(new Uri(baseAddress), new Cookie(cookie.Key, cookie.Value));
                        }
                    }

                    ////Trust all certificates
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationHeaderType, Token);
                    //Add Header Values
                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            try
                            {
                                if (!client.DefaultRequestHeaders.Contains(header.Key))
                                {
                                    if (!String.IsNullOrEmpty(header.Value))
                                    {
                                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    client.Timeout = TimeSpan.FromMinutes(60);

                    HttpResponseMessage response = await client.DeleteAsync(ApiAddress);

                    string responseData = response?.Content?.ReadAsStringAsync()?.Result;
                    if (!string.IsNullOrWhiteSpace(responseData) && !string.IsNullOrEmpty(responseData))
                    {
                        if (!response.IsSuccessStatusCode)
                        {

                        }
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                //await new LogStatus(request).TrackTrace("DeleteSabreAsync Method Exception has occured." + LoopCount, ex, "requestSource=" + requestSource + ";baseAddress=" + baseAddress + ";ApiAddress=" + ApiAddress + ";AuthenticationHeaderType" + AuthenticationHeaderType + ";Token=" + Token + ";MediaType=" + MediaType + ";HasValue:" + request.Request.Host.HasValue + ";Host:" + request.Request.Host.Host + ";Port:" + request.Request.Host.Port + ";Value:" + request.Request.Host.Value, null, null, this.GetType().Namespace + "." + this.GetType().Name);

                string exceptionMessage = (ex.InnerException != null ?
                  ex.InnerException.InnerException != null ?
                  ex.InnerException.InnerException.InnerException != null ?
                  ex.InnerException.InnerException.InnerException.InnerException != null ?
                  ex.InnerException.InnerException.InnerException.InnerException.Message
                  : ex.InnerException.InnerException.InnerException.Message
                  : ex.InnerException.InnerException.Message
                  : ex.InnerException.Message
                  : ex.Message)?.Trim()?.ToLower();

                return new HttpResponseMessage();
            }
        }
    }
}

