using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Readability_3.Models
{
    public class ReadingInput
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Input Text to be Parsed:")]
        public string inputText { get; set; }
    }
}
