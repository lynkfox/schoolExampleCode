/* Chapter
Programmer: Anthony Goh
Date:  3/18/19
Filename: Lab7b.java
Purpose: Perform a sequence search for the smallest element of an array, using a method. Without a Sort

 */

import java.util.Scanner;

public class Lab7b
{

    public static void main(String[] args)
    {

        Scanner input = new Scanner(System.in);

        double[] list = new double[10];

        System.out.println("\t\tFind Min Value");

        System.out.print("Input ten Doubles: ");
        for (int i = 0; i < list.length; i++)
        {
            list[i] = input.nextDouble();

        }
        // saving a variable by just outputting the return from the method
        System.out.println("The average of the ten numbers is: " + min(list));

    }

    public static double min(double[] array)
    {
        double min = array[0]; // initialize min to the first value of the array in order to make sure that it
        // is accurately searching for a min within what is in the array. Prevents some large numbers from
        // disrupting the search.

        for(double e: array)
        {
            // a for array loop should work just fine, it goes through each value and sees if the next
            // position in the array is smaller than any of the previous, if so it saves it and uses
            // that value for the new smaller than comparison
            if(e < min)
            {
                min = e;
            }
        }
        return min;
    }

}
