using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityMeterAutoTest.Entity.Models
{
    public class ObisCode
    {
        public ObisCode(string name,string mode, string code, string parameter)
        {
            Name = name;
            Mode = mode;
            Code = code;
            Parameter = parameter;
        }

        public string Name { get; set; }
        public string Mode { get; set; }
        public string Code { get; set; }
        public string Parameter { get; set; }
    }
}
