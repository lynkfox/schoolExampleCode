/* Chapter
Programmer: Anthony Goh
Date: 2/11/19
Filename: lab4a.java
Purpose: ask for a value between 5 and 10, then ask for that many inputs: each input will record if the value
is positive, negative, or zero.

error checking includes: the initial value between 5 and 10.

 */

import java.util.Scanner;

public class lab4a {

    public static void main(String[] args) {
	int positive=0, negative=0, zero=0,number, count =1, temp;

	// we're starting count at 1, for ease with the output for asking for numbers in the second loop.
	Scanner input = new Scanner(System.in);
	System.out.println("\t\t\tPositive and Negative Numbers");

	// do while loop, because it must run at least once (posttest) and we want to make sure its between 5 and 10
        // error checking
	do {
	    System.out.print("Enter the total number of integers to be requested, between  5 and 10: ");
	    number = input.nextInt();

	    if(number <5 || number > 10)
        {
            System.out.println("Number must be between 5 and 10.");
        }

    } while(number <5 || number > 10);

	// while loop to go through, asking for the number of values and counting if they are +/-/0
	while( count <= number)
    {

        // This is why we started at count = 1 - so we can just use count to show what number we are requesting.
        // its also why we need <= number above, because otherwise we go with 1 too few numbers.
        System.out.print("Enter number " +count + ": ");
        temp = input.nextInt();

        if( temp < 0)
        { // I know brackets aren't needed here, but I always use them because it is clearer, and safer for if I
            // go back to add more code to this if branch
            negative++;

        } else if( temp == 0)
        {
            zero++;

        } else
        {
            positive++;
        }
        count++;

    }

	System.out.println("The total number of negative numbers is: " + negative);
	System.out.println("The total number of zeros is: " + zero);
	System.out.println("The total number of positives is: " +positive);
    }
}
