/*
	Chapter 12:	Exception Handling
	Programmer: Anthony Goh
	Date: 4/22/19
	Filename:	TravelCommission.java
	Purpose:	Using Exception Handling in a swing applet to handle errors
*/

import javax.swing.JOptionPane;
import java.text.DecimalFormat;

public class TravelCommission
{
    public static void main(String[] args)
    {

        //provided code by project
        double dollars, answer;
        int empCode;
        System.out.println("\t*****Commission Calculator*****");
        //call methods
        dollars = getSales();
        empCode = getCode();
        answer = getComm(dollars,empCode);
        output(dollars, answer);
        finish();

    }

    // This Method gets the Sales amount from the user, checks to make sure it is a double (or an int that can be parsed into a double), not a string, and >0

    public static double getSales()
    {
        double amount =0;
        boolean done = false;
        String answer;

        while(!done)
        {
            try
            {
                answer = JOptionPane.showInputDialog(null,
                        "Enter the Sales Amount:\r\n(Do not use comma's  or dollar signs.)\r\nCancel to exit.");
                if(answer == null) finish(); // cancel clicked, just exit
                amount = Double.parseDouble(answer); // try to put the input into a double. Will throw exceptions to be caught if not possible

                //User Input error check (commons sense check)

                if (amount <= 0) throw new NumberFormatException();

                done = true;

            }
            catch(NumberFormatException e)
            {
                //error message box
                JOptionPane.showMessageDialog(null,
                        "Your entry should be a number greater than zero. No words please.",
                        "Error", JOptionPane.PLAIN_MESSAGE);

            }

        }
        return amount;
    }

    //This method looks for the sales code of the sale made, then checks to make sure it is a valid code (1, 2, 3)
    public static int getCode()
    {
        int code = 0;
        boolean done=false;
        String answer;
        while(!done)
        {
            try
            {
                answer = JOptionPane.showInputDialog(null,
                        "Enter the sales Code:\r\n1. Telephone Sales\r\n2. In-Store sales\r\n3. Outside Sales\r\nCancel to exit.");
                if(answer == null) finish(); // cancel clicked, just exit
                code = Integer.parseInt(answer);

                if(code <1 || code > 3) // if the code is not 1, 2 or 3
                {
                    throw new NumberFormatException();
                }
                done = true;
            }
            catch(NumberFormatException e)
            {
                //error message box
                JOptionPane.showMessageDialog(null,
                        "Your entry needs to be 1, 2 or 3..", "Error", JOptionPane.PLAIN_MESSAGE);

            }
        }

        return code;
    }

    /* This method takes the code of the type of sale, and the amount of the sale, and calculates and returns the
    commission

    Sale Type 1: Telephone sales earn a 10% commission,
    Sale Type 2: in-store earns 14%,
    Sale Type 3: outside sales earn 18%.
     */

    public static double getComm(double sale, int code)
    {
        double commission = 0;

        switch(code)
        {
            case 1:
                commission = sale*.10;
                break;
            case 2:
                commission = sale*.14;
                break;
            case 3:
                commission = sale*.18;
                break;
        }

        return commission;
    }

    public static void output(double amount, double commission)
    {

        DecimalFormat currency = new DecimalFormat("#,###.00");
        // we're only using this in this method, so it is safe to be only in here

        JOptionPane.showMessageDialog(null,
                "Your Commission on a sale of " + currency.format(amount) + " will be " + currency.format(commission),
                "Commission", JOptionPane.PLAIN_MESSAGE);
    }

    public static void finish()
    {
        //Exit method, to close out with a safe exception.
        System.exit(0);
    }

}