using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schedule_Reader_1.Models;

namespace Schedule_Reader_1.Controllers
{
    public class ClassListController : Controller
    {
        private static DateTime LastGenerated = new DateTime();
        

        private  static IEnumerable<ClassList> storedList = new List<ClassList>();

        private readonly DatabaseContext _context;

        public ClassListController(DatabaseContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            if(LastGenerated < DateTime.Now.AddHours(-1)) // more than 1 hour ago
            {
                storedList = GetCourseSections().OrderBy(x => x.Course.Semester).ThenBy(x=> x.Course.CourseNumber).ThenBy(x=> x.Course.Department);
            }
            ViewBag.Time = LastGenerated.ToString("M/dd/yyy h:mm tt");

            ViewBag.Updated = _context.LastUpdates.OrderByDescending(u => u.Id).FirstOrDefault().DateUploaded;


            return View(storedList);
        }

        


        public IEnumerable<ClassList> GetCourseSections()
        {

            /* Each ClassList is composed of 1 Course and a ienum of Sections that are in that course.
             * The goal is to be able to create a list where we show each course, then when you click on the list
             * some javascript will drop down and reveal more table with the sections in it.
             */
            var dbCourses = _context.Courses;
            List<ClassList> ComboSections = new List<ClassList>();

            foreach(var course in dbCourses)
            {
                var secList = new ClassList();
                var profList = new List<string>();

                secList.Course = course;

                int id = course.CourseID; // section.CourseID is int? so this just bypasses some issues
                
                // add an Ienum of the sections for each class
                secList.Sections = _context.Sections.Where(x => x.CourseID == id).ToList<Section>();
                foreach(var section in secList.Sections)
                {
                    /* This feels like a dirty brute force way to do this. I am confident
                     * that I could eventually come up with a proper join, but I am not certain
                     * if the above line where we add all the Sections would be as clean with a join
                     * and would probably need some sort of work to split the two and place the sections
                     * int secList and the professor name seperate.
                     * 
                     * And with any luck the Professor List index should be equal to the secList Index...
                     */
                    profList.Add(_context.Professors.Find(section.ProfessorID).Name);
                }
                secList.Professors = profList;
                ComboSections.Add(secList);

            }

            LastGenerated = DateTime.Now;
            return ComboSections;

        }
    }
}