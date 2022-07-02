using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FlatsManagement.Models
{
    public class RMC
    {
        [Display(Name = "Flat ID")]
        public int f_id { get; set; }

        [Display(Name = "Customer ID")]
        public int c_id { get; set; }

        [Display(Name = "Customer Name")]
        public string c_name { get; set; }

        [Display(Name = "Contact")]
        public string c_phone { get; set; }

        [Display(Name = "Rent Status")]
        public string r_status { get; set; }
        public string r_stValue { get; set; }

        

        [Required]
        public int inc { get; set; }

    }
}