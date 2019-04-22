
import java.util.Date;

/* Chapter 1 and 2, Lab 1
Programmer: Anthony Goh
Date: 1/14/19
Filename: Lab1Part2.java
Purpose:: This program outputs a header for a console app, plus the current date.

No error checking has been implimented in this lab (none is needed).
 */


public class Lab1Part2
{

    public static void main(String[] args)
    {
    	//Setting up the date
		Date currentDate = new Date();

		//And.... lots of *'s.
		System.out.println("************************************************************");
		System.out.println("*                                                          *");
		System.out.println("*                                                          *");
		System.out.println("*                                                          *");
		System.out.println("*                                                          *");
		System.out.println("*      *****  *****  *****   Anthony T. Goh                *");
		System.out.println("*      *   *    *    *       123 Anystreet                 *");
		System.out.println("*      *****    *    *  **   Anytown, AZ, USA              *");
		System.out.println("*      *   *    *    *   *   agoh@student.cscc.edu         *");
		System.out.println("*      *   *    *    *****                                 *");
		System.out.println("*                                                          *");
		System.out.println("*      Today's Date is: " +currentDate+"       *");
		System.out.println("************************************************************");


    }
}
