/*
   CSCI2467        Final
   Programmer:	   Keyser Soze (Anthony Goh)
   Date: 5/6/19
   Program Name:   TestMultiply.java


   NOTES: This was provided as a pre made code, with errors. The test was to debug the code, add exception handling,
   and  compile and run. So some choices in how it is coded is not necessarily my own choices, but due to the limitation
   of working with what was provided.
*/

//removed unnecessary import

// added proper import for scanner/exception handling.
import java.util.*;

//corrected Class name to match file name.
public class TestMultiply
{

    public static void main(String[] args )
    {
        //Declaring variables
        int multiplier;
        double correct;
        // why is this a double? This very well could and probably should be an int, but the
        // example window does show it as a double at the end. So i left it as one.




        //Opening messages
        // fixed typo (Println)
        System.out.println("\t\tWelcome to the Multiplication Quiz");
        System.out.println("");

        //Calling the user-defined methods
        multiplier = getNumber();
        //fixed typo (corect)
        correct = takeQuiz(multiplier);
        System.out.println("\t\tYou got "+correct+ " correct!" );

    }

    public static int getNumber()
    {
        //Declaring variables

        // added System.in to fix the scanner object.
        Scanner input = new Scanner(System.in);
        int multiplier = -1; //default just in case the try catch block fails.
        boolean done=false;


        //Get a value from user
        while(!done)
        {
            try
            {
                System.out.print("Enter the multiplication table you wish to practice:  ");

                multiplier = input.nextInt();

                if(multiplier < 1)
                {
                    // Multiple tables being negative are supposedly possible, but not the intention of this
                    // program, so we check for them here. 0 would also be a broken table.
                    throw new InputMismatchException();
                }

                System.out.println();

                done = true;



            } catch (InputMismatchException e)
            {
                System.out.println("Error. Enter a Positive Integer.");
                input.nextLine(); // don't forget to clear for strings!
            }
        }

        //Return a value to main
        return multiplier;

    }

    public static int takeQuiz(int multiplier)
    {
        //Declaring variables

        int answer=0;

        // doubles are possible inputs, even if they would always be incorrect in the possible questions. However, UI shouldn't
        // stop someone from entering the wrong answer, only stop them from entering answers that are not possible (ie,
        // strings). This was provided as an int by the final, so I left it as is.

        // also,initialized for safety sake.
        int count = 0;
        int correct = 0;
        boolean done = false; // added for exception handling
        Scanner input = new Scanner(System.in);


        // fixed a typo (cout)
        while (count <= 12)
        {

            done = false; // have to make sure that done is false before entering the try catch while, otherwise it will
            // skip over the while for every subsequent question and never ask them.
            // This could also be put in the If/Else choice below, but i find it better to place at the top, as a reminder
            // that there is a flag being cleared.


            //exception handling for input errors added here:
            while(!done)
            {
                try
                {
                    //Display question and get answer
                    System.out.println( "What is "+count + " times " + multiplier +"?" );

                    // You stressed very often that this is not good form and you don't want to see println on inputs.
                    // However, example sheet has it remaining a new line after each input request, so I left it.

                    answer =input.nextInt();

                    if(answer <= 0)
                    {
                        throw new InputMismatchException();
                        // this isn't strictly necessary, but I believe what you want to see as part of showing what to
                        // do for exception handling
                        // negative numbers aren't strictly incorrect in this situation, as they are possible answers,
                        // but to show understanding of the the code needed, I put this in here.
                    }

                    System.out.println();

                    done = true; // exit out of the try/catch loop



                } catch (InputMismatchException e)
                {
                    System.out.println("Error. Enter a Positive Integer.");
                    input.nextLine(); // don't forget to clear for strings!
                }
            }


            if (answer == count * multiplier)
            {
                System.out.println("\tCorrect!");
                correct = correct + 1;
                // could correct to other syntax (correct+=1;), but still technically correct so does not need to be debugged.
            }

            else
            {
                System.out.println("\tIncorrect");
            }

            // fixed incorrect syntax for adding 1 to an int.
            count += 1;




        }

        return correct;
    }
}

