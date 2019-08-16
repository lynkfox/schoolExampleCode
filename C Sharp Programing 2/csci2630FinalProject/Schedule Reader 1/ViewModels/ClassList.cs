using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_Reader_1.Models
{
    public class ClassList
    {
        public Course Course { get; set; }
        public List<Section> Sections { get; set; }
        public List<string> Professors { get; set; }
    }
}
