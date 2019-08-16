using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Readability_2.Models
{
    public class results : Read1
    {
        [Display(Name = "Grade Level")]
        public string GradeLevel { get; set; }

        [Display(Name = "Word Count")]
        public string WordCount { get; set; }
        [Display(Name = "Sentance Count")]
        public string SentCount { get; set; }
        [Display(Name = "Syllable Count")]
        public string SylCount { get; set; }
    }
}
