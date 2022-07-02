using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FlatsManagement.Models
{
    public class Flats
    {
        [Display(Name = "Flat ID")]
        public int f_id { get; set; }

        [Display(Name = "Flat Type")]
        public string f_type { get; set; }

        [Display(Name = "Total Rooms")]
        public int f_rooms { get; set; }

        [Display(Name = "Total Washrooms")]
        public int f_baths { get; set; }

        [Display(Name = "Total Halls")]
        public int f_hall { get; set; }

        [Display(Name = "Total Kitchens")]
        public int f_kitchens { get; set; }

        [Display(Name = "Address")]
        public string f_location { get; set; }
        public HttpPostedFileBase imageFile { get; set; }

        [Display(Name = "Image")]
        public string f_image { get; set; }

        [Display(Name = "Flat Status")]
        public string f_status { get; set; }

    }
}