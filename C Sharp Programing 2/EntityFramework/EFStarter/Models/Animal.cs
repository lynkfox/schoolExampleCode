using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFStarter.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }


    }
}
