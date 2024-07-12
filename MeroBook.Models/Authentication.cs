using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeroBook.Models
{
    public class Authentication
    {
        [Key]
        public int userId { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; }
        public string address { get; set; }
        public int phone { get; set; }
        public int telePhone { get; set; }
        public string webSite { get; set; }
        public int panNo { get; set; }
        public bool isDeleted { get; set; }
        public string deletedBy { get; set; }
        public DateTime deletedOn { get; set; }
    }
}
