using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_Reader_1.Models
{
    public  class TestDatabase
    {

        public  string Semester { get; set; }
        public  string Subject { get; set; } // refered to as Department in the database
        public  string CourseNum { get; set; }
        public  string Section { get; set; }
        public  string SynonymNum { get; set; }
        public  string ActiveStudents { get; set; } 
        public  string Capacity { get; set; }// max Students in database
        public  string PercentFull { get; set; } // Not stored in Database
        public  string MinStudents { get; set; } 
        public  string Status { get; set; }
        public  string StartDate { get; set; }
        public  string EndDate { get; set; }
        public  string ClassType { get; set; }
        public  string Location { get; set; }
        public  string Faculity { get; set; }
        public  string Building { get; set; }
        public  string Room { get; set; }
        public  string StartTime { get; set; }
        public  string EndTime { get; set; }
        public  string Frequency { get; set; }
        public  string Mon { get; set; }
        public  string Tues { get; set; }
        public  string Weds { get; set; }
        public  string Thurs { get; set; }
        public  string Fri { get; set; }
        public  string Sat { get; set; }
        public  string Sun { get; set; }

        


        public Professor GetProf()
        {
            string profEmail = "";
            var replace = new char[] { '-', '.', ' ', '"' };

            if (Faculity != "To be Announced"  && !Faculity.Contains(","))
            { //Makes sure the Professor isn't To be Announced, and isnt Multiple Professors (Which will start with ")
                
                //ugh. There has got to be a better, more effecient way
                profEmail = Faculity.ToLower().Replace("-", "").Replace(".","").Replace(" ","").Replace("\"","") +"@cscc.edu";
                
            }else if (Faculity.Contains(","))
            { // If Multiple Professors, split them on the comma (which we had to deal with in the upload) and then make a combined email list) 
                string[] temp = Faculity.Split(",");
                string x = "";
                foreach (var name in temp)
                {
                    profEmail = Faculity.ToLower().Replace("-", "").Replace(".", "").Replace(" ", "").Replace("\"", "") + "@cscc.edu \n";


                }
                if (x.Length>2)
                { // just a an error check to make sure we don't get an out of index ereror
                    profEmail = profEmail.Substring(0, x.Length - 2);
                }

                
            }
            else 
            {
                profEmail = "Not Applicable";
            }
            


            var prof = new Professor()
            {
                Name = Faculity,
                Department = Subject,
                Email = profEmail
            };

            return prof;
            

        }

        public Section GetSect()
        {
            int.TryParse(SynonymNum, out int Syn);
            int.TryParse(ActiveStudents, out int Active);
            int.TryParse(Capacity, out int Max);
            int.TryParse(Room, out int Rm);
            int.TryParse(MinStudents, out int Min);

            string weekDays = "";

            if (Mon == "Y")
            {
                weekDays += "M, ";
            }
            if (Tues == "Y")
            {
                weekDays += "Tu, ";
            }
            if (Weds == "Y")
            {
                weekDays += "W, ";
            }
            if (Thurs == "Y")
            {
                weekDays += "Th, ";
            }
            if (Fri == "Y")
            {
                weekDays += "F, ";
            }
            if (Sat == "Y")
            {
                weekDays += "Sa, ";
            }
            if (Sun == "Y")
            {
                weekDays += "Su, ";
            }

            if(weekDays.Length != 0)
            {
                weekDays = weekDays.Remove(weekDays.Length - 2, 2);
            } else
            {
                weekDays = "Online Class";
            }
            

            string duration = StartTime +" - " + EndTime;

            var sect = new Section()
            {
                SectionNum = Section,
                Synonym = Syn,
                ActiveStudents = Active,
                MaxStudents = Max,
                StartDate = StartDate,
                EndDate = EndDate,
                Building = Building,
                Room = Rm,
                Time = duration,
                Days = weekDays,
                Type = ClassType,
                MinEnrollment = Min

            };
            return sect;


        }

        public Course GetCor()
        {
            int.TryParse(CourseNum, out int CNum);
            var Cor = new Course()
            {
                CourseNumber = CNum,
                Department = Subject,
                Semester = Semester,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc fermentum nibh eget scelerisque condimentum. Mauris a velit iaculis, ultricies elit at, posuere nunc. Praesent consequat porta nibh sit amet ornare. Aliquam tincidunt neque eget nunc cursus feugiat. Donec vitae elit vel lectus iaculis fringilla quis id nulla. Suspendisse potenti. Nullam semper lorem lorem, sit amet dictum quam consectetur et. Ut volutpat tortor tincidunt, volutpat lectus ac, euismod nunc. Proin ultricies, sapien sit amet viverra laoreet, eros felis ornare dui, non laoreet magna eros id orci. Aenean vel tristique mi."
            };
            // cheating on Description :P
            return Cor;
        }

    }
}
