

import java.util.Scanner;

/* Chapter 1 and 2, Lab 1
Programmer: Anthony Goh
Date: 1/14/19
Filename: Lab1Part1.java
Purpose:: This program will use the scanner input method and do several math problems on inputed integrals, then again on inputed reals

No error checking has been implimented in this lab.
 */

public class Lab1Part1
{

    public static void main(String[] args)
    {
        double firstD, secondD;
        int firstI, secondI;

        Scanner input = new Scanner(System.in);

        System.out.println("************************************************");
        System.out.println("*   This is my ... not first Java program...   *");
        System.out.println("************************************************");

        System.out.println("\r\n** Integer arithmetic tests **");

        System.out.print("\r\nEnter the first integer to test: ");
        firstI = input.nextInt();
        System.out.print("Enter the second integer to test: ");
        secondI = input.nextInt();

        //output the Int calculations
        System.out.println(+firstI+ " + " +secondI+ " = " +(firstI+secondI));
        System.out.println(+firstI+ " - " +secondI+ " = " +(firstI-secondI));
        System.out.println(+firstI+ " * " +secondI+ " = " +(firstI*secondI));
        System.out.println(+firstI+ " / " +secondI+ " = " +(firstI/secondI));
        System.out.println(+firstI+ " % " +secondI+ " = " +(firstI%secondI));

        System.out.println("\r\n** Real artithmetic tests");

        System.out.print("Enter the first real to test: ");
        firstD = input.nextDouble();
        System.out.print("Enter the second real to test: ");
        secondD = input.nextDouble();

        //Output the Real (Double) calculations.
        System.out.println(+firstD+ " + " +secondD+ " = " +(firstD+secondD));
        System.out.println(+firstD+ " - " +secondD+ " = " +(firstD-secondD));
        System.out.println(+firstD+ " * " +secondD+ " = " +(firstD*secondD));
        System.out.println(+firstD+ " / " +secondD+ " = " +(firstD/secondD));

        System.out.println("Press any key to continue...");
        // I threw this in here cause its in the picture. But Press Any key is not good Java. Should be Enter
        // and a try/catch error handling, but that seems beyond this current class.

    }
}
