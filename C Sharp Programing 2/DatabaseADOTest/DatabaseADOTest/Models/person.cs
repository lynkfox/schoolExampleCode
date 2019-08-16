using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseADOTest.Models
{
    public class person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public int LastName { get; set; }

        public virtual ICollection<animal> Animals { get; set; }
        // the other side of a foreign key to Animals. This table does not have a FK to Animals
        // ICollection is for a 1(person) to many(animal) relationship. Hence, collections.
    }
}
