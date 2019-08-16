using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule_Reader_1.Models;
using System.Text.RegularExpressions;

namespace Schedule_Reader_1.Controllers
{
    public class HomeController : Controller
    {
        IEnumerable<TestDatabase> parsedData = new List<TestDatabase>();
        RowCount rowCount = new RowCount();

        private readonly DatabaseContext _context;
        

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot","Uploads",
                        file.FileName);


            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            else
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            parsedData = ParseFile(path);

            await UpdateAsync(parsedData);

            //cleanup the file afterwards
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return View("Uploaded",rowCount);
        }


        

        public async Task UpdateAsync(IEnumerable<TestDatabase> dataSet)
        {

            

            foreach (var entry in dataSet)
            {
                int profID = 1, courseID = 1;
                var prof = entry.GetProf();
                var course = entry.GetCor();
                var sect = entry.GetSect();
                int z = 0;

                /*Check to see if the data already exists (same name) in the professor table. 
                 * 
                 * The data in Professors isnt going to change in the csv file, because its an aggregate list of what
                 * currently exists. Safe to check this way.
                 */
                var profExists = _context.Professors.Where(u => u.Name == prof.Name).FirstOrDefault();
                if (profExists is null)
                {
                    _context.Add(prof);
                    //if it doesn't exist, we're going to grab the next number that will come in.

                    profID = prof.ProfessorID;

                    rowCount.rowsProfessor++;
                    

                    //unfortunately, because I'm checking to see if they exist  before adding
                }
                else
                {
                    // if it already exists, then store the data in prof
                    prof = profExists;
                    profID = prof.ProfessorID;
                }





                /* Similar with Professors, the Course data isn't going to change as long as the Semester/CourseNumber is the same
                 * so we can check for it to exist already as its an aggregate of the file
                 */
                var courExist = _context.Courses.Where(x => x.CourseNumber == course.CourseNumber && x.Semester == course.Semester).FirstOrDefault();
                if (courExist is null)
                {
                    _context.Add(course);
                    courseID = course.CourseID;
                    rowCount.rowsCourses++;
                } else
                {
                    course = courExist;
                    courseID = course.CourseID;

                }

                //Add the ProfessorID, and the CourseID to the Section for the Fkey
                //sect.ProfessorID = profID;
                //sect.CourseID = courseID;


                sect.Professor = prof;
                sect.Course = course;

                /* Only duplicate Synonyms would be Lecture + Lab  - when we were reading the data in ParseData
                 * we did not add that to the list, dealing with it there. So we can search by Synonym here.
                 */
                // Get the data in the table to be able to check what changed. 
                var curData = _context.Sections.Where(x => x.Synonym == sect.Synonym).FirstOrDefault();
                bool changed = false; // if changed is true, then a change was made in the data)
                if (curData is null)
                {
                    _context.Add(sect);
                    rowCount.rowsSections++;
                }
                else if(sect != curData)
                {


                    PropertyInfo[] properties = typeof(Section).GetProperties();

                    foreach(PropertyInfo prop in properties)
                    {
                        if (prop.Name !="Id" && prop.Name != "CourseID") // these two values cannot and willnot change. so we will not even attempt to.
                        {
                            var newValue = typeof(Section).GetProperty(prop.Name).GetValue(sect);
                            var oldValue = typeof(Section).GetProperty(prop.Name).GetValue(curData);

                            if (newValue != oldValue)
                            {
                                typeof(Section).GetProperty(prop.Name).SetValue(curData, newValue);
                                changed = true;
                            }
                        }
                        
                    }
                    if(changed)
                    {
                        rowCount.rowsUpdated++;
                    }
               
                }
                await _context.SaveChangesAsync();

            }

            // this will update the professors Active Class Ammount
            await UpdateProfClasses();
           
        }

        //Read and Parse the file into the the Database

        public async Task UpdateProfClasses()
        {
            var profList = _context.Professors;

            foreach(var prof in profList)
            {
                int id = prof.ProfessorID;
                /* Section.ProfessorID is a int? because it could potentially be null in the database (it SHOULDNT be but... well mistakes happen)
                 * so we are delibertly storing it to prevent issues
                 */
                prof.ActiveCourseAmount = _context.Sections.Where(x => x.ProfessorID == id).Count();
            }
            await _context.SaveChangesAsync();

        }

        public IEnumerable<TestDatabase> ParseFile(string path)
        {
            var testDataList = new List<TestDatabase>();
            
            int i = 0; //count
            int y = 0; // testing value
            bool first = true;

            string[] ColNames = new string[27] {"Semester", "Subject", "CourseNum", "Section", "SynonymNum", "ActiveStudents",
                "Capacity", "PercentFull", "MinStudents", "Status", "StartDate", "EndDate", "ClassType", "Location",
                "Faculity", "Building", "Room", "StartTime", "EndTime", "Frequency", "Mon", "Tues", "Weds", "Thurs", "Fri", "Sat", "Sun" };

            if (System.IO.File.Exists(path))
            {
                string[] rows = System.IO.File.ReadAllLines(path);
                foreach (var row in rows)
                {
                    string[] cols;
                    bool duplicate = false;
                    


                    var testData = new TestDatabase();
                    i = 0; // gotta reset i for each row
                           // stupid. one friggin entry has a " data , data" approach so... have to check for it
                    cols = Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");


                    if (cols.Length !=27) // checking for bad lines)
                    {
                        if (rowCount.rowsCounted ==2) // second line contains the execution time of the file. We want to speed this up, so we can use this to see if the file is already the current version on the server
                        {
                            var uploads = _context.LastUpdates;
                            var newUpload = new LastUpdate();
                            
                            if (!uploads.Any())
                            {
                                // for the first upload...

                                // record the new file execution date and continue
                                newUpload.FileGeneratedDate = cols[0];
                                newUpload.DateUploaded = DateTime.Now.ToString(@"M/d/yyyy h:mm:ss tt");
                                uploads.Add(newUpload);
                                _context.SaveChanges(); // doesn't need to be async, cause we won't be worrying about it being finished before the rest of this is.

                            }
                            else
                            {
                                var lastID = uploads.Max(u => u.Id);

                                //ok. Theyre stored as strings, cause DateTime is annoying. But here we go...
                                var last = DateTime.ParseExact(uploads.Find(lastID).FileGeneratedDate.ToString(), @"M/d/yyyy h:mm:ss tt", null);
                                var current = DateTime.ParseExact(cols[0], @"M/d/yyyy h:mm:ss tt", null);
                                if (current <= last)
                                {
                                    rowCount.NotChanged = true;
                                    break;

                                }
                                else // current file execution date > last update
                                {
                                    // record the new file execution date and continue
                                    rowCount.NotChanged = false;
                                    newUpload.FileGeneratedDate = cols[0];
                                    newUpload.DateUploaded = DateTime.Now.ToString(@"M/d/yyyy h:mm:ss tt");
                                    uploads.Add(newUpload);
                                    _context.SaveChanges(); // doesn't need to be async, cause we won't be worrying about it being finished before the rest of this is.

                                }
                            }
                            


                        }
                        rowCount.rowsCounted++;
                    }
                    else
                    {
                        if(first)
                        {
                            // the first row with 27 col is the header row. we want to skip that row.
                            first = false;
                            continue;
                        }
                        
                        /* Because we don't care to have two seperate lines for Lecture and Lab, if the 13th (0-12) col
                         * == Lab, then we are just going to hit the last entry (the Lecture) and adjust the EndTime to be the end of the Lab
                         * 
                         * we're also going to change the ClassType to LectureLab
                         */
                        
                            if (cols[12] == "Lab" || cols[12] == "Other Technology")
                            {
                                /* These are often duplicate lines that have Lab or Web Component of a Blended class
                                 * So if they have the same Synonym we're going to ignore the line because it's not important for
                                 * how we are going to display the data. But we will take some of the information and make use of it.
                                 */
                                if(cols[4] == testDataList.Last().SynonymNum)
                                {

                                    /* So if the Synonmym number is the same, then its part of the same class and we can safely ignore
                                     * it and use its data that we choose to add to testDataList.Last(). We also set duplicate = true so we can
                                     * skip the add to the list in the next if
                                     */

                                    duplicate = true;

                                    /* If the EndTime actually has a value that isn't just "" then we want to change the last
                                     * one's end time to match the Lab
                                     */
                                    if (cols[18] != "")
                                    {
                                        testDataList.Last().EndTime = cols[18];
                                        // because we have an End Time, then we know it is an In Person Lecture and Lab so we update the ClassType
                                        testDataList.Last().ClassType += " and " +cols[12]; // Should say "Lecture and Lab"
                                    }else // if there is no value in EndTime, then it must be online right?
                                    {
                                        testDataList.Last().ClassType = "Blended";
                                    }

                                }
                                
                                
                                Debug.WriteLine("Line: " + (y + 1) + " is a lab or Blended part of a class, skipping.");
                                rowCount.rowLabs++;
                                rowCount.rowsCounted++;
                            } 
                            if(!duplicate) // if it does not have a duplicate synonym then we want to add it to the list.
                            {
                                foreach (var col in cols)
                                {

                                    // playing with Reflection for dynamic property names!
                                    /*
                                     * I don't want to type out a gain 27 phase switch property here, or anything like the that.
                                     * so created an arry of strings that has all of the Property names of the TestDatabase Class.
                                     * 
                                     * 
                                     * 
                                     * then use GetType().GetProperty(ColNames[i]).SetValue to set that into the property!
                                     * 
                                     * then add it to the List we created as this needs to be an Ienumberable to display everything properly.
                                     */

                                    testData.GetType().GetProperty(ColNames[i]).SetValue(testData, col);
                                    i++;

                                }

                                // Couple of Cleanup issues
                                if (testData.ClassType == "Other Technology")
                                    testData.ClassType = "Online Course";

                                if (testData.Location == "" || testData.Location == null)
                                {
                                    testData.Location = "Main Campus";
                                }

                                testDataList.Add(testData);
                                rowCount.rowsCounted++;
                                rowCount.rowsImported++;
                            }

                            y++;
                            Debug.WriteLine("Line: " + y + " processed.");
                        
                        

                    }

                }
                return testDataList;

            }
            else
            {
                return null;
            }

        }

    }
}
