using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_Reader_1.Models
{
    public class LastUpdate
    {
        [Key]
        public int Id { get; set; }
        public string DateUploaded { get; set; }
        public string FileGeneratedDate { get; set; }

    }
}
