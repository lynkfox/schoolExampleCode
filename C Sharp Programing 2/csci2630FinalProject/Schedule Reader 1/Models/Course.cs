using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_Reader_1.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        
        public int? CourseNumber { get; set; }
        public string Department { get; set; }
        public string Semester { get; set; }
        public string Description { get; set; }

        public ICollection<Section> Section { get; set; }


    }
}
