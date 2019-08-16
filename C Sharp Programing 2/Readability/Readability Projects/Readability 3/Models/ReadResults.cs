using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Readability_3.Models
{
    public class ReadResults : ReadingInput
    {
        [Display(Name = "Grade Level")]
        [Editable(false)]
        public string GradeLevel { get; set; }

        [Display(Name = "Word Count")]
        [Editable(false)]
        public string WordCount { get; set; }

        [Display(Name = "Sentance Count")]
        [Editable(false)]
        public string SentCount { get; set; }

        [Display(Name = "Syllable Count")]
        [Editable(false)]
        public string SylCount { get; set; }
    }
}
