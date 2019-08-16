using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_Reader_1.Models
{
    public class Professor
    {
        [Key][Display(Name="ID")]
        public int ProfessorID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        [Display(Name = "# of courses Teaching")]
        public int? ActiveCourseAmount { get; set; }
        public ICollection<Section> Section { get; set; }




    }
}
