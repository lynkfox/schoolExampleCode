/* Chapter
Programmer: Anthony Goh
Date: 4/1/19
Filename: CarDemo.java , Car.java
Purpose: Uses a Class called car and creates a car object that speeds up and brakes, showing speed along the way

 */
import java.util.Scanner;

public class CarDemo
{
    public static void main(String[] args)
    {

        System.out.println("\tDemonstration of the Car Class");
	    Car carDemo = new Car(getInputMake(), getInputYear());
	    // using Methods for error checking, by instructions given in class.

	    System.out.println("Demo Car");
	    System.out.println("\tModel Year : " +carDemo.getYearModel());
	    System.out.println("\tMake       : " +carDemo.getMake());
	    System.out.println("\tSpeed      : " +carDemo.getSpeed());

	    System.out.println("\r\nSPEED UP!");

	    for(int i = 0; i < 5; i++)
        {
            carDemo.accelerate();
            System.out.println("Demo Car's Speed: " + carDemo.getSpeed());
        }

        System.out.println("\r\nSLOW DOWN!");

        for(int i = 0; i < 5; i++)
        {
            carDemo.brake();
            System.out.println("Demo Car's Speed: " + carDemo.getSpeed());
        }

        System.out.println("\r\nEnd of the Road for this car demonstration...");
    }


    static int getInputYear()
    {
        Scanner input = new Scanner(System.in);
        //Error checking method. Makes sure the year is between 1940 and 2016


        System.out.print("\r\nEnter the year of the car: ");
        int carYear=input.nextInt();
        while (carYear < 1940 || carYear > 2016)
        {
            System.out.println("\r\nInput Error - enter a year between 1940 and 2016");
            System.out.print("\r\nEnter the year of the car: ");
            carYear=input.nextInt();
        }

        return carYear;
    }

    static String getInputMake()
    {
        Scanner input = new Scanner(System.in);
        //Error checking method. Makes sure the Make of the car is not an empty string


        System.out.print("\r\nEnter the make of the car: ");
        String carMake = input.nextLine();

        while(carMake.isEmpty())
        {
            System.out.println("\r\nInput Error - cannot be empty.");
            System.out.print("Enter the make of the car: ");
            carMake = input.nextLine();
        }

        return carMake;
    }
}

