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
        int pictureIndex = 999999; // pulled into a global variable, so that every time New Flash card is picked it doesnt reset to 1,
        // set at artbitrarily high value for future proofing - so if you hit New Flash Card before Start, it wont give the error message that 
        // nothing is in the answer box
        int flashcardNumber = 1; // For Fun: used for the Flashcard # of 5 label on the gui. Seperate value so pictureIndex can be respected as unique and used for check statements without affecting gui.
        int numberCorrect = 0; // For Fun: for a 'You Got X out of 5 Correct message when finished
        bool checkAnswer; // For Fun: Make sure the user selected 'Show Answer' before continuing, to ensure proper scorekeeping


        Random random = new Random();

        public Flashcards()
        {
            InitializeComponent();
        }



        private void Flashcards_Load(object sender, EventArgs e)
        {



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

            //Images loaded in On Load so that everytime the Start Flashcards button is hit it doesnt reload.
        }

        private void button_StartButton_Click(object sender, EventArgs e)
        {
            /* Note:
             *  Here I use a Fisher Yates Shuffle to shuffle the Pictures and the answers (in the same shuffled order)
             *  IN this method I use a List to dynamically a new array of pictures and answers in a random order, rather than actually creating just a 
             *  list of indexes
             *  I do this becaues it makes the New Flash Card button much more simple - just call the new image array and the new answer array.
             *  Alternatively:
             *  Instead of storing the images in a new, now shuffled order, array, we could leave them in order and just shuffle the numbers
             *  Then we use the new int array of shuffled numbers as indexs to birdNames and pictures - but it i feel that it takes the same amount of 
             *  work to do this as the above method, and this method means a much simplier 'next image' button.
             *  */
            

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
            flashcardNumberLabel.Text = Convert.ToString(1);
            flashcardNumber = 1;
            
        }
        

        private void button_NewFlashcard_Click(object sender, EventArgs e)
        {
            
            if ((String.IsNullOrEmpty(answerBoxText.Text) && pictureIndex < 5) && String.IsNullOrEmpty(solutionBoxText.Text))
            {
                // This if branch chcks first to make sure they have put an answer in the box in some way. (If somehow an answer is in the
                // solution box it will pass this as well, just as a precaution
                MessageBox.Show("You have to guess before you can proceede.", "Guess!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!checkAnswer)
            {
                // checkAnswer is a simple Bool set in the 'Show Answer' button to make sure they actually hit the show answer button, for scoring!
                MessageBox.Show("Please hit the 'Show Answer' button to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {

                if (pictureIndex > pictures.Length-1)
                {
                    // Once all 5 images have been displayed, sets the picture box to null, displays a message box showing the score out of 5.
                    birdImage.Image = null;
                    string message = "You got " + numberCorrect + " out of "+pictures.Length+" correct.";
                    MessageBox.Show(message, "Finished", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    //Each hit of the Next Flash Card button will incriment the index by 1, allowing us to move in order through the shuffled array
                    // of images and answers
                    birdImage.Image = pictureShuffled[pictureIndex];
                    birdAnswer = Convert.ToString(birdShuffled[pictureIndex]);
                    ++pictureIndex;
                    ++flashcardNumber; // for the X of 5 flashcard label
                    answerBoxText.Text = null;
                    solutionBoxText.Text = null;
                }
            }


            // Output flashcard # (for # of 5 label)
            // Note - this is the only 5 that is not calculated by pictures.length so if more pictures added to the flashcards, then
            // this label will need to be updated in gui editor.

            flashcardNumberLabel.Text = Convert.ToString(flashcardNumber);

            

        }

        private void button_ShowAnswer_Click(object sender, EventArgs e)
        {
           

            string answerText = Convert.ToString(answerBoxText.Text);

           
         // These if branches set a lable to inform the user if their guess was correct or incorrect

            if(answerText.Equals(birdAnswer))
            {
                lableAnswerCorrect.Text = string.Format("Correct!");
                lableAnswerCorrect.ForeColor = Color.Green;
                solutionBoxText.Text = birdAnswer;
                ++numberCorrect;
                

            }
            else
            {
                lableAnswerCorrect.Text = string.Format("Incorrect...");
                lableAnswerCorrect.ForeColor = Color.Red;
                solutionBoxText.Text = birdAnswer;
                
            }

            // simple bool to make sure the user hits Show Answer before continuing to next card.

            checkAnswer = true;

        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
