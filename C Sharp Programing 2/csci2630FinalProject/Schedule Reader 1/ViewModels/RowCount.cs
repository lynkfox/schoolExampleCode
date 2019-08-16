using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_Reader_1.Models
{
    // Just a fun for nothing Model to add interesting things to the View of 'Successfull Upload'
    public class RowCount
    {
        public int rowLabs { get; set; }
        public int rowsCounted { get; set; }
        public int rowsProfessor { get; set; }
        public int rowsCourses { get; set; }
        public int rowsSections { get; set; }
        public int rowsImported { get; set; }
        public int rowsUpdated { get; set; }
        public bool NotChanged { get; set; }

        public RowCount()
        {
            rowLabs = 0;
            rowsCounted = 1;
            rowsProfessor = 0;
            rowsCourses = 0;
            rowsImported = 0;
            rowsUpdated = 0;
            NotChanged = false;
        }

    }

}
