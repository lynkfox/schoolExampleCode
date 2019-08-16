using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Readability_2.Models;
using System.Text.RegularExpressions;

namespace Readability_2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult ReadingScore()
        {

            return View();
        }

        // This method is only called when the CALCULATE button is clicked on the ReadingScore view
        // - when a Post is called (returning the data to the server)
        [HttpPost]
        public IActionResult ReadingScore(Read1 data)
        {
            //get the data from the Post
            string inputText = data.inputText;

            var matches = Regex.Matches(inputText, @"\s+[^.!?]*[.!?]");
            int sentances = matches.Count;

            matches = Regex.Matches(inputText, @"\b[^\s]+\b");
            int words = matches.Count;

            matches = Regex.Matches(inputText, @"[aeiouy]+?\w*?[^e]");
            int syl = matches.Count;

            //quick setup of the results Model
            var viewModel = new results
            {
                SentCount = sentances.ToString(),
                SylCount = syl.ToString(),
                WordCount = words.ToString(),
                inputText = data.inputText
            };

            viewModel.GradeLevel = CalculateGradeLevel(words, sentances, syl).ToString();






            return View("ReadingScoreResults", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // Pulled it out into a method because this is how I believe it should be.

            // future thoughts? Should this be a controller of its own?
            // probably not - its part of this 'controller'

            // future thoughts - this would be best to put both ReadingScore and ReadingScoreResults into
            // its own controller
        public double CalculateGradeLevel(int word, int sent, int syl)
        {
            double gLevel = ((0.39 * (word / sent)) + (11.8 * (syl / word))) - 15.59;

            return gLevel;
        }
    }
}
