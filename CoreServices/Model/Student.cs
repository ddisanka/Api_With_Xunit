using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Model
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public int phone {get;set;}
        public DateTime DOB {get;set;}

    }
}
