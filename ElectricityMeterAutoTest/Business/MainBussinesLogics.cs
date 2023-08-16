using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ElectricityMeterAutoTest.Entity;


namespace ElectricityMeterAutoTest.Business
{
    public class MainBussinesLogics
    {
         public MainBussinesLogics() {
            Context c = new Context();
            if (!c.Database.Exists())
            {
                c.Database.Create();
            }
        }    
    }
}
