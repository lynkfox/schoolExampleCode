/* Chapter
Programmer: Anthony Goh
Date: 2/11/19
Filename: lab4b.java
Purpose: The program will request a variable number of integers, finding which is the highest, and recording the
number of times that high number is entered.  0 will end the program.

 */

import java.util.Scanner;

public class lab4b {

    public static void main(String[] args)
    {
	Scanner input = new Scanner(System.in);

	int max=0, temp, count=0;

	System.out.print("Enter an integer (0 to end): ");
	temp= input.nextInt();

		if(temp ==0 )
		{
			System.out.println("No numbers were entered except 0.");
			System.exit(0);

		}

	while ( temp != 0 ) {
		if (temp > max)
		{
			max = temp;
			count = 1; // new max value, so set count back to 1 ( cause there is 1 instance)

		} else if (temp == max) {
			count++; // Max is the same as being held
		}
		// else do nothing!

		System.out.print("Enter an integer (0 to end): ");
		temp = input.nextInt();
	}


	System.out.println("The maximum number entered is: " +max);
	System.out.println("The count for the max number is: " +count);





	}
}
