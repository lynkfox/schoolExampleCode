using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Schedule_Reader_1.Models
{

    public class Section
    {
        [Key]
        public int Id { get; set; }
        public string SectionNum { get; set; }
        public int? Synonym { get; set; }
        public int? ActiveStudents { get; set; }
        public int? MaxStudents { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? ProfessorID { get; set; }
        public string Building { get; set; }
        public int? Room { get; set; }
        public string Time { get; set; }
        public string Days { get; set; }
        public int? CourseID { get; set; }
        public string Type { get; set; }
        public int MinEnrollment { get; set; }

        public Professor Professor { get; set; }
        public Course Course { get; set; }





    }
}
