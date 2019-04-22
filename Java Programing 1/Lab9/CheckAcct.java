/* Chapter
Programmer: Anthony Goh
Date: 4/8/19
Filename:CheckAccount.java
Purpose: External class to accesss and use a Checking Account in the Driver Demo class.

 */

import java.util.Date;

public class CheckAcct {

    private int id;
    private double balance;
    private static double annualInterestRate;
    // This means this variable will be the same across all instances of
    // this class - every new checking account will have this same Interest Rate
    // and if it is changed with the below mutator, it will change for every
    // use of this class in that program.
    private Date dateCreated = new Date();

    public CheckAcct(int newID, double initialBalance)
    {
        id = newID;
        balance = initialBalance;
    }

    public int getId()
    {
        return id;
    }

    public double getBalance()
    {
        return balance;
    }

    public static double getAnnualInterestRate()
    {
        return annualInterestRate;
    }

    public Date getDateCreated()
    {
        return dateCreated;
    }

    public static void setAnnualInterestRate(double newInterestRate)
    {
        annualInterestRate = newInterestRate;
    }

    public double getMonthlyInterest()
    {
        return (balance * annualInterestRate)/12;
        // one month worth of interest - 1/12th of the annual interest earned on that balance.
    }

    public void withdraw(double withdrawAmt)
    {
        balance -= withdrawAmt;
    }

    public void deposit(double depositAmt)
    {
        balance += depositAmt;
    }

}
