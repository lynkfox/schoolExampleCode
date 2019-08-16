using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_Reader_1.ViewModels
{
    public class ProfessorVM
    {
        [Display(Name = "Name")]
        [MaxLength(30, ErrorMessage = "Name must be no more than 30 characters.")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Email must be no more than 50 characters.")]
        public string Email{ get; set; }

        [Display(Name = "People Selection List")]
        public List<ProfName> ProfList { get; set; }
        [Display(Name = "Department")]
        public List<string> Departments { get; set; }
        public int SelectProfId { get; set; }
        public string Department { get; set; }

    }

    public class ProfName
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
