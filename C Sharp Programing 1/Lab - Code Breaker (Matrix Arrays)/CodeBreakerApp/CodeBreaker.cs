using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeBreakerApp
{
    public partial class CodeBreaker : Form
    {

        //declare variables outside of the buttons so they can be used by multiple buttons
        string magicWord;
        char[,] playFairArray = new char[5, 5];
        int[] cords = new int[2];

        string inputString;

        string outputString = null;




        public CodeBreaker()
        {
            InitializeComponent();
        }
        // This Method creates the Play Fair Array

        public void CreatePLayFairArray()
        {

            magicWord = Convert.ToString(magicWord_txt.Text);
            magicWord = magicWord.ToUpper();
            magicWord_txt.Text = string.Format(magicWord);

            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int z = 0; // count for getting through the list

            bool checkLetter = false;

            List<char> playFairStart = new List<char>();
            List<char> alphabetList = new List<char>();

            //make sure these two lists are empty for reusing this method
            playFairStart.Clear();
            alphabetList.Clear();

            // sets the alphabet array into a list - an array is quicker to type and I'm lazy, and didnt want to type 26 .Add statements.
            // but a list is important, because it guarntees we don't duplicate letters in the final matrix
            foreach (char letter in alphabet)
            {
                alphabetList.Add(letter);
            }

            foreach (char letter in magicWord)
            {
                // this foreach loop will take each letter out of the key phrase and, first check if its been already added
                // then add it to an list - a list again here because it can be dynamically updated.

                if (char.IsWhiteSpace(letter))
                {

                }
                else
                {
                    char storeLetter = letter;

                    for (int i = 0; i < playFairStart.Count; i++)
                    {
                        if (storeLetter == playFairStart[i])
                        {
                            checkLetter = true;

                        }
                    }

                    if (!checkLetter)
                    {
                        // error check to remove J's and replace with I
                        if (letter == 'J')
                        {
                            playFairStart.Add('I');

                        }
                        else
                        {
                            playFairStart.Add(letter);
                        }
                    }
                    else
                    {
                        checkLetter = false;
                    }

                }
            }



            z = 0;

            // this double four statement moves through the matrix across each row, then incriments up to the next row.
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (z < playFairStart.Count)
                    {
                        playFairArray[x, y] = playFairStart[z];
                        z++;
                    }
                    else
                    {
                        if (CheckIfDuplicate(alphabetList[0], playFairStart))
                        {

                            alphabetList.RemoveAt(0);
                            x--;

                            // we have to set the x value back one because we didn't add a letter
                            // (tried just reversing the else branch here, remove then add instead of add then remove
                            // ended up with duplicate letters. whoops.  This works.

                        }
                        else
                        {



                            playFairArray[x, y] = alphabetList[0];
                            alphabetList.RemoveAt(0);


                        }


                    }
                }
            }


        }




        // This Method will check for a letter in a list - the reason we use a list in this function is so that we can remove that letter from the list and not have to worry about it accidently getting printed.
        // Outputs True if the letter is found in the list
        // Outputs False if the letter is NOT found in the list
        public static bool CheckIfDuplicate(char checkLetter, List<char> charArray)
        {

            bool isDuplicate = false;
            foreach (char letter in charArray)
            {
                if (letter == checkLetter)
                {
                    isDuplicate = true;

                    break;
                    // this breaks us out of the foreach loop as well as the if statement - because we found a match, dont need to go further
                    // break is not the best way to do thing, but using a return here can cause it to still continue the foreach loop.
                }

                else
                {
                    isDuplicate = false;

                }
            }

            return isDuplicate;

        }




        //This Method is for comparing a letter and finding it in the PlayFair Matrix
        //Outputs an array: 
        //Index 0 is the x coord in the matrix
        //Index 1 is the y coord in the matrix
        public static int[] FindCords(char letter, char[,] matrix)
        {
            int[] cords = new int[2];

            if (letter == 'J' || letter == 'j')
            {
                letter = 'I';
                // J/I substitution check
            }

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {

                    if (letter == matrix[x, y])
                    {
                        cords[0] = x;
                        cords[1] = y;
                    }
                }
            }

            return cords;
        }

        private void translate_btn_Click(object sender, EventArgs e)
        {
            
            

            if (String.IsNullOrEmpty(startPhrase_txt.Text) || String.IsNullOrEmpty(magicWord_txt.Text))
            {
                //error checking, making sure there is input in the Key Phrase and the input phrase to be able to create the array
                MessageBox.Show("Please make sure Key Word and Input Phrase are filled", "No Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                CreatePLayFairArray();

                endPhrase_txt.Text = null; //clear the output box of previous text
                outputString = null; //clear the storage string of text to start a new one.

                inputString = Convert.ToString(startPhrase_txt.Text);
                inputString = inputString.ToUpper();


                foreach (char letter in inputString)
                {
                    if (char.IsWhiteSpace(letter))
                    {
                        outputString = outputString + " ";
                    }
                    else
                    {
                        cords = FindCords(letter, playFairArray);
                        outputString = outputString + Convert.ToString(playFairArray[cords[1], cords[0]]);
                    }
                }

                endPhrase_txt.Text = string.Format(outputString);

            }

        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            magicWord_txt.Text = null;
            startPhrase_txt.Text = null;
            endPhrase_txt.Text = null;
        }

        private void Close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowKey_btn_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(magicWord_txt.Text))
            {
                MessageBox.Show("Need a Magic Word", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string keyString = "Play Fair Key for this Key Word";

                CreatePLayFairArray();

                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (x == 0)
                        {
                            keyString = keyString + "\r\n" + playFairArray[x, y];
                        }
                        else
                        {
                            keyString = keyString + ", " + playFairArray[x, y];

                        }
                    }
                }

                endPhrase_txt.Text = string.Format(keyString);
            }
        }
    }
    

}
