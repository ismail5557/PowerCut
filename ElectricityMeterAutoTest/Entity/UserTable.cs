using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityMeterAutoTest.Entity
{
    public class UserTable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
        public Guid Guid { get; set; }
        public int RoleId { get; set; }
        public int LanguageId { get; set; }
        public string PhoneNumber { get; set; }
        public string RolePassword { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int ActiveStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public string CustomerProducts { get; set; }
        public string CustomerSubscriptions { get; set; }
    }
}
