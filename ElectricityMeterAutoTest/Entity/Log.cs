using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityMeterAutoTest.Entity
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string LogType { get; set; }
        public string Path { get; set; }
        public string Tittle { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string Request { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
