using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeWeather1.Models
{
    public class Zip
    {
        [Required]
        [Display(Name = "Enter your Zip Code:")]
        public int zipCode { get; set; }
    }
}
