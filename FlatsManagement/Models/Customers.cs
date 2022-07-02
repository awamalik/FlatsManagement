using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FlatsManagement.Models
{
    public class Customers
    {
        [Display(Name = "Customer ID")]
        public int c_id { get; set; }

        [Display(Name = "Name")]
        public string c_name { get; set; }

        [Display(Name = "CNIC")]
        public string c_cnic { get; set; }

        [Display(Name = "Phone No")]
        public string c_phone { get; set; }

        [Display(Name = "Flat ID")]
        public int f_id { get; set; }

        [Display(Name = "Start Month")]
        public string c_sMonth { get; set; }

        [Display(Name = "Rent Status")]
        public string c_rentStatus { get; set; }

    }
}