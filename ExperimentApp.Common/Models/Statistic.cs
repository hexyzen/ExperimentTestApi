using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApp.Common.Models
{
    public class ExperimentStatistics
    {
        public string Key { get; set; }
        public int DeviceCount { get; set; }
        public string Value { get; set; }
        public int OptionCount { get; set; }
    }

}
