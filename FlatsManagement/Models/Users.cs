using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FlatsManagement.Models
{
    public class Users
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter User ID!")]
        [Display(Name = "User ID")]
        public string u_id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Your Full Name!")]
        [Display(Name = "Full Name")]
        public string u_name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please Enter Your Email Address!")]
        [Display(Name = "Email")]
        public string u_email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password!")]
        [Display(Name = "Password")]
        public string u_password { get; set; }
        public bool online { get; set; }
    }
}