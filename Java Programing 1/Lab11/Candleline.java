/*
	Chapter 13
	Programmer: Anthony Goh
	Date: 4/29/19
	Filename:	Candleline.java
	Purpose:	Exception Handling in a candle shipping purchasing console program.
*/

import java.util.*;
import java.text.DecimalFormat;

public class Candleline {

    public static void main(String[] args) {
        double candleCost, shippingCost;
        int shippingType;

        System.out.println("\t\t***CandleLine - Candles Online***\r\n");


        //call methods
        candleCost = getCandlecost();
        shippingType = getShippingType();
        shippingCost = getShippingCost(candleCost, shippingType);
        output(candleCost, shippingCost);



    }

    public static double getCandlecost()
    {
        Scanner input = new Scanner(System.in);
        boolean done = false;
        double cost = 0.0;

        //exception handling

        while(!done)
        {
            try
            {
                System.out.print("Enter the Cost of the candle order: ");
                cost = input.nextDouble();
                if (cost <=0)
                {
                    throw new InputMismatchException();
                }

                done = true;
            }
            catch (InputMismatchException e)
            {
                System.out.println("Error, enter a dollar amount greater than 0.");
                input.nextLine(); // to catch and deal with strings, to prevent infinite loop

            }
        }

        return cost;
    }

    public static int getShippingType()
    {
        Scanner input = new Scanner(System.in);
        boolean done = false;
        int type = 0;

        //exception handling

        while(!done)
        {
            try
            {
                System.out.print("\r\nEnter the type of shipping:\r\n  (1): Priority (Overnight)\r\n  " +
                        "(2): Express (2 Business Days\r\n  (3): Standard (3-7 Business Days)\r\nPlease Choose an Option: ");
                type = input.nextInt();
                if (type < 1 || type > 3 ) // range of numbers that are unacceptable for type of shipping
                {
                    throw new InputMismatchException();
                }

                done = true;
            }
            catch (InputMismatchException e)
            {
                System.out.println("\r\n\r\nError, please select a viable option.\r\n");
                input.nextLine(); // to catch and deal with strings, to prevent infinite loop

            }
        }
        return type;
    }

    /* There are three types of shipping:

    1=Priority (16.95),
    2=Express (13.95) and
    3=Standard (7.95).

    Orders of 100.00 and above and who choose standard shipping will receive free shipping.


     */
    public static double getShippingCost(double c, int t)
    {


        switch(t)
        {
            case 1:
                return 16.95;

            case 2:
                return 13.95;

            case 3:
                //standard shipping, check if cost is greater than or equal to 100, to receive free shipping
                if (c>=100)
                    return 0;
                else
                    return 7.95;

        }
        return 7.95; // end return -- could be used for additional error checking (ie: return -1!) but currently set to default to Standard in case something went wrong.

    }

    public static void output(double c, double s)
    {

        DecimalFormat currency = new DecimalFormat("&#,###.00");

        System.out.println("The candle cost of " + currency.format(c) + " plus shipping costs of "+ currency.format(s) + " comes out to a total of " +currency.format(c+s) + ".");

    }

}
