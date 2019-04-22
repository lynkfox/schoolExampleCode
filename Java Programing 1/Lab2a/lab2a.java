import java.util.Scanner;

/* Chapter 2
Programmer: Anthony Goh
Date: 1/28/19
Filename: lab2a.java
Purpose:

Simulate a shipping company requesting information on a package, and output the cost to ship depending on weight

Requirements: use a nested iff statement

Very limited error checking.

 */

public class lab2a {

    public static void main(String[] args)
    {

        double weight;
        Scanner input = new Scanner(System.in);

        System.out.print("Please enter the weight of the package you are shipping: ");
        weight = input.nextDouble();

        // start the heaviest possible weight (too heavy to ship) because it is the easiest to eliminate (most possible numbers
        // and because as you go down, it MUST be false if the weight is lower than the min for that class of shipping.


        double cost = 0;

        if(weight > 20 )
            System.out.println("\n\rApologies, this packages is too heavy for us to ship."); // to heavy
        else if(weight > 10 )
            cost = 10.5; // weight >10 <=20)
        else if(weight > 3 )
            cost = 8.5; // weight >3 <=10)
        else if(weight > 1)
            cost = 5.5; // weight >1 <=3)
        else if(weight > 0)
            cost = 3.5;  // weight >0 <=1
        else
            System.out.println("\n\rNot a valid weight;"); // error check for possible negative number

        if(cost == 0)
        {   // because we don't change cost in the above nested if statements, we don't need anything here.
            // the error message are already in the if statements. We also initialized cost as 0 at the start
            // giving us a good way to check if it had been changed.
        } else
        {
            System.out.println("\n\rThe cost of your package to ship is $" + cost +"0");
        }

        /*

        the point is to demonstrate nested if, so here we are!

        as you said we'll be doing number formatting next week, so I went with the above (the below was my original code
        without the number formatting. I dislike that man print statements, which are basically repeating the same information)

        if(weight > 20 )
            System.out.println("\n\rApologies, this packages is too heavy for us to ship.");
        else if(weight > 10 )
            System.out.println("\n\rThe cost of your package to ship is $10.50.");
        else if(weight > 3 )
            System.out.println("\n\rThe cost of your package to ship is $8.50.");
        else if(weight > 1)
            System.out.println("\n\rThe cost of your package to ship is $5.50.");
        else if(weight > 0)
            System.out.println("\n\rThe cost of your package to ship is $3.50.");
        else
            System.out.println("\n\rNot a valid weight;"); // error check for possible negative number








         */

    }
}
