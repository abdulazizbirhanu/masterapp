using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAPI.Config
{
    public class DataConfig
    {
        public InternalApiAccessParam EmailConfig { get; set; }

    }
    public class ApplicationInsightsOption
    {
        public string InstrumentationKey { get; set; }
    }
}
