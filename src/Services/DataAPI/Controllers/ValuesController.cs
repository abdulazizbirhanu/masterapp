using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        [Route("List")]
        public IEnumerable<string> GetList()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
