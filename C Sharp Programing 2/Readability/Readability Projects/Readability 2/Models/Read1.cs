
using System.ComponentModel.DataAnnotations;

namespace Readability_2.Models
{
    public class Read1
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Input Text to be Parsed:")]
        public string inputText { get; set; }

    }
}
