using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashCards
{
    public partial class Flashcards : Form
    {
        Image[] pictures = new Image[5];
        Image[] pictureShuffled = new Image[5];
        string[] birdName = new string[5];
        string[] birdShuffled = new string[5];
        string birdAnswer;
        List<int> unusedNumbers = new List<int>();
        int listCount = 0;
        int randomNumber = 0; // holds the random number generated in the shuffle to be used mulitple times
        int newIndex = 0; // finds random index on pictures[] and birdNames[] that will be inserted into the next space when shuffled
        int pictureIndex = 5; // pulled into a global variable, so that every time New Flash card is picked it doesnt reset to 1
        // set at 5 - so if you hit New Flash Card before Start, it wint give the error message that nothing is in the answer box
        int flashcardNumber = 1; // used for the Flashcard # of 5 label on the gui. Seperate value so pictureIndex can be respected as unique and used for check statements without affecting gui.



        Random random = new Random();

        public Flashcards()
        {
            InitializeComponent();
        }



        private void Flashcards_Load(object sender, EventArgs e)
        {
           

        }

        private void button_StartButton_Click(object sender, EventArgs e)
        {
            /* --- Start Button doesn't need this. Because project Docs specify that Start Button initilizes.
            randomPicture = random.Next(0, 4);
            birdImage.Image = pictures[randomPicture];
            birdAnswer = birdName[randomPicture];
            */

            // Ugh. Ugh. Despite all my google foo, I cannot find a way to use the Resource pack to get a variable in 
            // the image name. I think 'ForEach' might work, but I am not sure of the syntax/use of ForEach yet.
            // Apparently, some other google fu, has indicated that is is not possible in C# to do a variable
            // variable...

            pictures[0] = Properties.Resources.image0;
            pictures[1] = Properties.Resources.image1;
            pictures[2] = Properties.Resources.image2;
            pictures[3] = Properties.Resources.image3;
            pictures[4] = Properties.Resources.image4;
            birdName[0] = "Mew Gull";
            birdName[1] = "Great Blue Heron";
            birdName[2] = "Moorhen";
            birdName[3] = "Northern Cardinal";
            birdName[4] = "Turtle-Dove Spotted";

            // Futureproofing : set up list of numbers to use for Fisher Yates shuffle - if more images are added, list automatically is bigger
            for (int i = 0; i < pictures.Length; i++)
            {
                unusedNumbers.Add(i);
                listCount++;
            }

            // Fisher Yates shuffle, using Lists, to show the images ONLY once (lists because RemoveAt() is much easier than trying to mess with an array copy here. Sorry !
            for (int i = 0; i < pictures.Length; ++i)
            {

                randomNumber = random.Next(0, listCount);
                newIndex = unusedNumbers[randomNumber];
                pictureShuffled[i] = pictures[newIndex];
                birdShuffled[i] = birdName[newIndex];
                unusedNumbers.RemoveAt(randomNumber);
                --listCount;
            }

            // Sets the first image onto the box, and only the first image.
            birdImage.Image = pictureShuffled[0];
            // records the answer for later
            birdAnswer = Convert.ToString(birdShuffled[0]);
            pictureIndex = 1;
            flashcardNumberLabel.Text = Convert.ToString(flashcardNumber);
            
        }
        

        private void button_NewFlashcard_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(answerBoxText.Text) && pictureIndex < 5)
            {
                MessageBox.Show("You have to guess before you can proceede.", "Guess!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (pictureIndex > 4)
                {
                    birdImage.Image = null;
                }
                else
                {
                    birdImage.Image = pictureShuffled[pictureIndex];
                    birdAnswer = Convert.ToString(birdShuffled[pictureIndex]);
                    ++pictureIndex;
                    ++flashcardNumber;
                    answerBoxText.Text = null;
                }
            }


            // Output flashcard # (for # of 5 label)

            flashcardNumberLabel.Text = Convert.ToString(flashcardNumber);

            

        }

        private void button_ShowAnswer_Click(object sender, EventArgs e)
        {
            string answerText = Convert.ToString(answerBoxText);
            
            bool answerCorrect = answerText.Equals(birdAnswer);
            // I don't know why, but I can't get the If statement below to go downt he true path with answerText == birdAnswer, or putting the string.Equals in the if statement


            /*debug
            if(answerText == birdAnswer)
            {
                testLabel.BackColor = Color.Green;
                testLabel.Text = string.Format("True");

            } else
            {
                testLabel.BackColor = Color.Red;
                testLabel.Text = string.Format("False");

            }
            */


            if(answerCorrect)
            {
                lableAnswerCorrect.Text = string.Format("Correct!");
                lableAnswerCorrect.ForeColor = Color.Green;
                solutionBoxText.Text = birdAnswer;
            }
            else
            {
                lableAnswerCorrect.Text = string.Format("Incorrect...");
                lableAnswerCorrect.ForeColor = Color.Red;
                solutionBoxText.Text = birdAnswer;
            }
                

            
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
