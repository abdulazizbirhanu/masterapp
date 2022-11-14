using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAPI.Config;

namespace DataAPI.Caller
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetSabreAsync<TReturn>(HttpContext request, string baseAddress, string ApiAddress, string AuthenticationHeaderType, string Token, string MediaType, Dictionary<string, string> cookies, Dictionary<string, string> headers, string requestSource);
        Task<HttpResponseMessage> PostSabreAsync<ThttpResponseMessage, Tpara>(HttpContext request, string baseAddress, string ApiAddress, string AuthenticationHeaderType, string Token, Tpara para,  string MediaType, Dictionary<string, string> cookie, Dictionary<string, string> headers, string requestSource);
        Task<HttpResponseMessage> PutSabreAsync<TreturnHttpResponseMessage, Tpara>(HttpContext request, string baseAddress, string ApiAddress, string AuthenticationHeaderType, string Token, Tpara para,  string MediaType, Dictionary<string, string> cookies, Dictionary<string, string> headers, string requestSource);
        Task<HttpResponseMessage> DeleteSabreAsync<TreturnHttpResponseMessage>(HttpContext request, string baseAddress, string ApiAddress, string AuthenticationHeaderType, string Token, string MediaType, Dictionary<string, string> cookies, Dictionary<string, string> headers, string requestSource);
    }
}
