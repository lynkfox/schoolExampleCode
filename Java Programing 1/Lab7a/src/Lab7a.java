/* Chapter
Programmer: Anthony Goh
Date: 3/18/19
Filename: Lab7a.java
Purpose: Uses an overloaded method to do  average 10 numbers inputted into an array.

 */

import java.util.Scanner;

public class Lab7a
{

    public static void main(String[] args)
    {

        Scanner input = new Scanner(System.in);

        int[] list1 = new int[10];
        double[] list2 = new double[10];


        System.out.println("\t\tAverage Arrays");

        System.out.print("Input ten integers: ");
        for (int i = 0; i < list1.length; i++)
        {
            list1[i] = input.nextInt();

        }
        // in order not to create another variable, just putting the method call directly in the print
        System.out.println("The average of the ten numbers is: " + average(list1));


        System.out.print("Input ten Doubles: ");
        for (int i = 0; i < list2.length; i++)
        {
            list2[i] = input.nextDouble();

        }
        System.out.println("The average of the ten numbers is: " + average(list2));

    }

    //overload version 1 - int arrays
    public static int average(int[] array)
    {
        int avg = 0;
        for(int e: array)
        {
            //using the avg variable here so we don't have to declare a separate variable.
            avg += e;
        }

        // again, saving time by simply returning the sum / length to get the actual average.
        return avg/array.length;
    }

    //overload vers 2 - double arrays.
    public static double average(double[] array)
    {
        double avg = 0;
        for(double e: array)
        {
            //using the avg variable here so we don't have to declare a separate variable.
            avg += e;
        }

        return avg/array.length;
    }

}
