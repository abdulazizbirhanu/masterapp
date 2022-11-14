using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DataAPI.Config
{

    public class ApiAccessParam
    {
        HttpContext _request;
        bool isProd = true;
        public ApiAccessParam(HttpContext request)
        {
            _request = request;
            isProd = (_request?.Request.Headers["IsDevelopment"].SingleOrDefault()?.ToLower()) == "true" ? false : true;            
        }

        public ApiAccessParam()
        {

        }

        public string BaseURI
        {
            get
            {
                if (isProd)
                {
                    //  For PROD and Clear for CERT, to minimize inconsistency
                    //DevBaseURI = null;
                    return ProdBaseURI;
                }
                else
                {
                    //  For CERT and Clear for PROD, to minimize inconsistency
                    //ProdBaseURI = null;
                    return DevBaseURI;
                }
            }
            set { }
        }

        public string _DevBaseURI { get; set; }
       
        public string DevBaseURI
        {
            get
            {
                return _DevBaseURI;
            }
            set
            {
                _DevBaseURI = value == null ? null : value;
            }
        }
        public string ProdBaseURI { get; set; }
        public string _RelativeApiPath { get; set; }
        public string RelativeApiPath
        {
            get
            {
                return _RelativeApiPath;
            }
            set
            {
                _RelativeApiPath = value == null ? null : value;
            }
        }
        
    }

    public class InternalApiAccessParam
    {
        public string URI { get; set; }
        public string RelativeApiPath { get; set; }
    }
}
