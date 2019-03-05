import java.util.Scanner;
import java.text.DecimalFormat;
/* Chapter 6
Programmer: Anthony Goh
Date: 3/4/19
Filename: lab6a.java
Purpose: a program to convert C to F or F to C using methods.

 */

public class lab6a {

    public static void main(String[] args) {


        DecimalFormat oneDec = new DecimalFormat("##.0 degrees");

        Scanner input = new Scanner(System.in);
        int iteration = 0, count = 0; // initialze these to 'safe' numbers we know are outside the do while loops, just in case.
        double celsius, fahrenheit;

        System.out.println("\t\tTemperature Converter");
        System.out.println("------------------------------------------------------");




        do
        {
            System.out.println("How many conversions would you like to make?");
            System.out.print("Enter a number between 3 and 6: ");
            iteration = input.nextInt();
        }
        while ( iteration < 3 || iteration > 6);

        for(int i = 1; i <= iteration; i++) // <= so we can use i = 1 to start the loop, and so that we just use i in the indicator
        {
            do {
                System.out.println("\n\rConversion #" + i );
                System.out.println("\n\rTo convert from celsius to fahrenheit type 1,");
                System.out.print("  To convert from fahrenheit to celsius type 2: ");
                count = input.nextInt();
            } while (count != 1 && count != 2); // we only have 2 options. We could do  < 1 and > 2 - but this works too!



            if( count == 1)
            {
                System.out.print("\n\rEnter a celsius temperature: ");
                celsius = input.nextDouble();
                fahrenheit = celsiusToFahrenheit(celsius);
                System.out.print("\nThe celsius temperature of " + oneDec.format(celsius) + " celsius is equal to " + oneDec.format(fahrenheit) + " fahrenheit.");
            } else // implicit that count will equal 2 because of above do while
            {
                System.out.print("\n\rEnter a fahrenheit temperature: ");
                fahrenheit = input.nextDouble();
                celsius = fahrenheitToCelsius(fahrenheit);
                System.out.print("\nThe fahrenheit temperature of " + oneDec.format(fahrenheit) + " fahrenheit is equal to " + oneDec.format(celsius) + " celsius.");

            }


        }
    }

    public static double celsiusToFahrenheit(double cels)
    {
        // your notes in class said that all we should do is a return. Just as a thought, I think the 'Enter a x Temp'
        // and the output lines should all be in this method, so that the if else is just the method calls. Of course these would then be
        // void instead of return, so different  lesson I suppose.
        return (cels *(9.0/5.0)+32);
    }

    public static double fahrenheitToCelsius(double fahr)
    {
        return (fahr-32)*(5.0/9.0);
    }
}


