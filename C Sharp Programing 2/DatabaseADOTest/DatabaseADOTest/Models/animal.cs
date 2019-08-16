using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseADOTest.Models
{
    public class animal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public person Person { get; set; }
        // using a class here acts as a Foreign Key to the person. This should be the name of the foreig key as it would be in the SQL create statement
        // like person_idfk

        // this is the FK. It is a single of a 1 to many. This class only has 1 person  - 1 (person) to many (animals)

    }
}
