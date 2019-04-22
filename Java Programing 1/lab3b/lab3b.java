
import java.util.Scanner;

/* Chapter
Programmer: Anthony Goh
Date: 2/4/19
Filename: lab3b.java
Purpose: - User enters 3 cities, and puts them out in alphabetical order using a simple bubble sort

no error checking

 */


public class lab3b {

    public static void main(String[] args)
    {
        Scanner input = new Scanner(System.in); //Scanner Initialization.
        String cityOne, cityTwo, cityThree, temp;

        System.out.println("\t\t\t\t\tAscending Order");

        System.out.print("\r\nEnter the first city: ");
        cityOne = input.nextLine();

        System.out.print("\r\nEnter the second city: ");
        cityTwo = input.nextLine();

        System.out.print("\r\nEnter the third city: ");
        cityThree = input.nextLine();

        //basic Bubble sort
        if(cityOne.compareToIgnoreCase(cityTwo) > 0)
        {
            temp = cityTwo;
            cityTwo = cityOne;
            cityOne = temp;
        }
        if (cityTwo.compareToIgnoreCase(cityThree) > 0)
        {
            temp = cityThree;
            cityThree = cityTwo;
            cityTwo = temp;
        }
        if (cityOne.compareToIgnoreCase(cityTwo) > 0 )
        {
            temp = cityTwo;
            cityTwo = cityOne;
            cityOne = temp;
        }

        System.out.print("The cities in alphabetical order are: " + cityOne +", " +cityTwo+ ", " +cityThree);

    }
}
