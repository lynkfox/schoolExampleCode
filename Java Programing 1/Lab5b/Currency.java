/*
	Chapter :  Chapter5
	Programmer:  Anthony Goh
	Date:
	Filename:	 Currency.java
	Purpose:	 converts currency using swing boxes
				 */

import javax.swing.JOptionPane;
import java.text.DecimalFormat;

public class Currency {

    public static void main(String[] args)
    {
        //declare and construct variables
         double currency;
         double pound, ruble, euro;
         /*would be better to declare these as the values of the currency exchange, rather than use them as the
         variable to hold the new value, but beyond scope.
          */
         String answer;
        DecimalFormat money = new DecimalFormat("$####.00");

         //Print Prompts and get Output

        System.out.println("/tThe Currency Calculator");
        answer = JOptionPane.showInputDialog(null, "Enter your Dollar amount:");
        if (answer == null) System.exit(0); // empty string catch.
        currency = Double.parseDouble(answer);

        pound = currency*.64;
        euro = currency*.91;
        ruble = currency*61.73;

        JOptionPane.showMessageDialog(null, "YOUR DOLLAR AMOUNT OF  " + money.format(currency) +
                "\nis equal to " +money.format(pound) + " pound, \n" + money.format(euro) + " euros,\nand "
                +money.format(ruble)+ " rubles.","Currency Calculator",JOptionPane.PLAIN_MESSAGE);

        System.exit(0);
    }
}
