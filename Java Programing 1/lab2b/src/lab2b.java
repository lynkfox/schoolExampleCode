import java.util.Scanner;

/* Chapter 2
Programmer: Anthony Goh
Date: 1/28/19
Filename: lab2b.java
Purpose:

Display a selection menu to convert USD to Pounds, Euros, or Rubles.

Requirements: Use a Switch method with current exchange rates.

Very limited error checking performed for Menu option.
 */

public class lab2b {

    public static void main(String[] args)
    {
	    Scanner input=new Scanner(System.in);
	    double currencyUSD,convertedCurrency;
	    int choice;

        double poundExchange = .64;
	    double euroExchange = .91;
	    double rubleExchange = 61.73;

	    //values provided in class
        //Placing them out here allows them to be easily found and updated. Not sure what const is in Java yet,
        //so leaving them a is for now. Assuming we'll get to that in class at some point.

        System.out.print("Enter the US Dollar amount: ");
        currencyUSD = input.nextDouble();


        System.out.println("\n\r\tSelect 1 to convert to Pounds.");
        System.out.println("\tSelect 2 to convert to Euros.");
        System.out.println("\tSelect 3 to convert to Rubles.");

        System.out.print("\n\rPlease select what currency to convert to: ");
        choice = input.nextInt();

        switch(choice)
        {
            case 1:
                convertedCurrency = currencyUSD*poundExchange;
                System.out.println("\n\rThe amount of $" + currencyUSD + " is " +convertedCurrency + " Pounds.");
                break;
            case 2:
                convertedCurrency = currencyUSD*euroExchange;
                System.out.println("\n\rThe amount of $" + currencyUSD + " is  " +convertedCurrency + " Euros.");
                break;
            case 3:
                convertedCurrency = currencyUSD*rubleExchange;
                System.out.println("\n\rThe amount of $" + currencyUSD + " is " +convertedCurrency + " Rubles.");
                break;
            default:
                //error checking can go here for not a valid option. Good place for a bool to break a while loop. For now...
                System.out.println("\n\rWhoops. Not a valid choice.");
                break;
        }

    /*
    would prefer a single print statement at the end: The amount of +currencyUSD+ is +convertedCurrency+ moneyName
    but need a loop error check for the default so a to not get a duplication of statements.

    just my style of coding, I like to keep duplicates down as much as possible.
     */

    }
}
