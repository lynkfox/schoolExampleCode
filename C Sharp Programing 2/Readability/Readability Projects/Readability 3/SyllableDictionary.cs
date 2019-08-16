using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;    //reminder  - this is for loading data from a file
using static System.Net.Mime.MediaTypeNames;

namespace Readability_3
{
    public class SyllableDictionary
    {
        private SortedDictionary<string, int> _dictionary;

        public SyllableDictionary()
        {
            _dictionary = new SortedDictionary<string, int>();

            /* trial concept program, expediancey: no error checking for if a file exists
             * 
             * if (File.Exists("path")
             */

            string[] rows = File.ReadAllLines(@"C:\Users\lynkf\source\repos\C#2\Readability\Readability Projects\dictionary.txt");

            foreach (var row in rows)
            {
                //Split function takes a char as an argument (ie, a single quote with a single item as a delmiter
                string[] cols = row.Split(',');
                int.TryParse(cols[1], out int numSyl);

                /* note - try parse will set a 0 if the second part of the dictionary file is not a number.
                 * so it will set 0 in the dictionary.
                 * and we are using 0 to deterimine if we need to use the regex instead of the dictionar
                 */

                _dictionary.Add(cols[0], numSyl);
            }

        }

        public int GetSyllables(string word)
        {
            _dictionary.TryGetValue(word.ToUpper(), out int count);

            return count;
        }
    }
}
