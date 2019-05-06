import java.text.DecimalFormat;
import java.util.Scanner;

/* Chapter
Programmer: Anthony Goh
Date: 2/4/19
Filename: lab3a.java
Purpose: - Take inputs of Name, hours worked, rate, federal and state tax rate, and calculate a paystub

No real error checking. Displaying use and knowledge of Formatting.
 */


public class lab3a {

    public static void main(String[] args)
    {
        DecimalFormat currency = new DecimalFormat("$#,###.00");
        DecimalFormat percentage = new DecimalFormat("<##.0%>");
        //format masks

        Scanner input = new Scanner(System.in);
        //initializing the scanner

        //variable declarations
        String employeeName;
        int hours;
        double payRate, federalTax, stateTax, grossPay, fedWithheld, stateWithheld, netPay, totalDeduction;

        System.out.println("          Payroll Record\r\n");

        System.out.print("Enter the employees Name: ");
        employeeName = input.nextLine();

        System.out.print("Enter the number of hours worked in a week: ");
        hours = input.nextInt();

        System.out.print("Enter the hourly pay rate: ");
        payRate = input.nextInt();

        System.out.print("Enter the Federal Tax withholding Rate (as a decimal): ");
        federalTax = input.nextDouble();

        System.out.print("Enter the State Tax withholding Rate (as a decimal): ");
        stateTax = input.nextDouble();

        //Calculations

        grossPay = payRate*hours;
        fedWithheld = grossPay*federalTax;
        stateWithheld = grossPay*stateTax;
        totalDeduction = fedWithheld+stateWithheld;
        netPay = grossPay - totalDeduction;

        /* This is assuming that State and Federal taxes are taking out of your total gross (which is usually the case)
        No indication for other items to be taken out before taxes, or if they get taken out one after another.
         */


        System.out.println("\r\nEmployee Name: " +employeeName);
        System.out.println("Hours Worked:  " +hours);
        System.out.println("Pay Rate:      " +currency.format(payRate));
        System.out.println("Gross Pay:     " +currency.format(grossPay));
        System.out.println("Deductions: ");
        System.out.println("\tFederal Withholding " +percentage.format(federalTax)+": " +currency.format(fedWithheld));
        System.out.println("\tState Withholding " +percentage.format(stateTax)+": " +currency.format(stateWithheld));
        System.out.println("\tTotal Deduction: " + currency.format(totalDeduction));
        System.out.println("Net Pay: "+currency.format(netPay));




    }
}
