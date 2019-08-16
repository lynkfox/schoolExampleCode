using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Readability_3.Models;

namespace Readability_3.Controllers
{
    public class ReadCalcController : Controller
    {
        // GET: ReadCalc
        public ActionResult Index()
        {
            return View();
        }
        // This method is only called when the CALCULATE button is clicked on the ReadingScore view
        // - when a Post is called (returning the data to the server)
        [HttpPost]
        public IActionResult Index(ReadingInput data)
        {
            var sylDictionary = new SyllableDictionary();

            //get the data from the Post
            string inputText = data.inputText;

            var matches = Regex.Matches(inputText, @"\s+[^.!?]*[.!?]");
            int sentances = matches.Count;

            matches = Regex.Matches(inputText, @"\b[^\s]+\b");
            int words = matches.Count;


            /* using the sylDictionary dictionary, and we already have a match collection of all the words
             * thanks to the above regex, so we can just foreach through that and get the syllables of each
             * word and add them to a total count
             */

            int syl = 0;
            foreach (var word in matches)
            {
                int count = sylDictionary.GetSyllables(word.ToString());
                if (count == 0)
                {
                    matches = Regex.Matches(word.ToString(), @"[aeiouy]+?\w*?[^e]");
                    syl += matches.Count;
                }
                else
                    syl += count;
                
            }
            /*
            matches = Regex.Matches(inputText, @"[aeiouy]+?\w*?[^e]");
            int syl = matches.Count;
            */

            //quick setup of the results Model
            var viewModel = new ReadResults
            {
                SentCount = sentances.ToString(),
                SylCount = syl.ToString(),
                WordCount = words.ToString(),
                inputText = data.inputText
            };

            viewModel.GradeLevel = CalculateGradeLevel(words, sentances, syl).ToString();






            return View("ReadingScoreResults", viewModel);
        }


        //User Methods


        public double CalculateGradeLevel(int word, int sent, int syl)
        {
            double gLevel = ((0.39 * (word / sent)) + (11.8 * (syl / word))) - 15.59;

            return gLevel;
        }




    }
}