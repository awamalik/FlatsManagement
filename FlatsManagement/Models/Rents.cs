using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FlatsManagement.Models
{
    public class Rents
    {
        [Display(Name = "Rent ID")]
        public int r_id { get; set; }
        [Display(Name = "Customer ID")]
        public int c_id { get; set; }
        [Display(Name = "Flat ID")]
        public int f_id { get; set; }
        [Display(Name = "Month")]
        public string r_month { get; set; }

    }
}