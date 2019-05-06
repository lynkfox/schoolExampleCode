/* Final, Programing
Programmer: Anthony Goh
Date: 5/6/19
Filename: FinalATG.java
Purpose: a console program. Accepts two >0 integers from methods, with exception handling and a return integer. Includes an output method.


Note: This was a final exam, and as such I had to follow parameters provided. I would have not wanted to have two separate
getNum methods, rather combining them into one, either with a counter argument for the method, or another option. I also
would have put them both in output rather than as their own methods, but again - must abide by the parameters of the assignment.
 */
import java.util.*;

public class FinalATG
{

    public static void main(String[] args)
    {
        //variable declaration
        int number1, number2;

        System.out.println("*************************************************");
        System.out.println("*            Final Program, Anthony Goh         *");
        System.out.println("*************************************************");

        System.out.println("***           Integer arithmetic tests        ***");

        number1 = getNum1();
        number2 = getNum2();

        output(number1, number2);


    }

    public static int getNum1()
    {
        Scanner input = new Scanner(System.in);
        int num1=0;
        boolean done = false;

        while(!done)
        {
            try
            {
                System.out.print("Enter the first integer to test: ");
                num1 = input.nextInt(); // will cover strings and doubles

                if (num1 < 1) // for covering any 0 or negative number
                {
                    throw new InputMismatchException();
                }
                done = true; // end the loop
            } catch (InputMismatchException e)
            {
                System.out.println("Error - enter an integer greater than 0 for the first number");
                input.nextLine(); // string clear
            }
        }
        return num1;
    }

    public static int getNum2()
    {
        Scanner input = new Scanner(System.in);
        int num2=0;
        boolean done = false;

        while(!done)
        {
            try
            {
                System.out.print("Enter the second integer to test: ");
                num2 = input.nextInt(); // will cover strings and doubles

                if (num2 < 1) // for covering any 0 or negative number
                {
                    throw new InputMismatchException();
                }
                done = true; // end the loop
            } catch (InputMismatchException e)
            {
                System.out.println("Error - enter an integer greater than 0 for the second number");
                input.nextLine(); // string clear
            }
        }
        return num2;
    }

    public static void output(int n1, int n2)
    {
        System.out.println("\r\n" + n1 + " + " + n2 + " = " +(n1+n2) );
        System.out.println(+ n1 + " - " + n2 + " = " +(n1-n2));
        System.out.println(+ n1 + " * " + n2 + " = " +(n1*n2));
        System.out.println(+ n1 + " / " + n2 + " = " +(n1/n2));
        System.out.println(+ n1 + " % " + n2 + " = " +(n1%n2));

        // Note to self: % is tricky. Example is n1 = 5, n2 = 13. 5%13 is 5 (because 13 divides into 5 0 times, and there is 5 left over)
        // but if you were to switch them  (13%5) you get 3 (5x2 = 10, 3 left over in 13). Keep this in  mind in the future!
    }
}
