/*
	Chapter 6:	Traffic Ticket
	Programmer: Anthony Goh
	Date:3/4/19
	Filename:	Trafic.java
	Purpose:	This program calculates a traffic ticket and court costs.
*/

import javax.swing.JOptionPane;
import java.text.DecimalFormat;

public class Traffic
{
    public static void main(String[] args)
    {
        //declare class variables
        double fine, courtCosts, ticket;
        int speedLimit, offenderSpeed, previousTickets, overLimit;
        System.out.println("       *****Ticket Calculator*****");



        //call methods

        speedLimit = getLimit();
        offenderSpeed = getDriverSpeed();
        if (offenderSpeed <= speedLimit)
        {
            JOptionPane.showMessageDialog(null,"No violation", "Traffic Ticket",JOptionPane.INFORMATION_MESSAGE);
            finish();
        }


        previousTickets = getTickets();
        overLimit = offenderSpeed - speedLimit;
        fine = overLimit * 20.00;
        courtCosts = getCosts(previousTickets);
        ticket = fine + courtCosts;
        output(fine, courtCosts, ticket);
        finish();
    }




    //The getLimit() method asks the user to input a the speed limit.
    public static int getLimit()
    {
        //declare method variables
        int limit = 0;
        String answer;

        do {
            answer = JOptionPane.showInputDialog(null, "Enter the speed limit:");
            if(answer == null) finish();
            limit = Integer.parseInt(answer);
            if (limit <=0) JOptionPane.showMessageDialog(null, "Speed limit must be above 0.");

        } while(limit <=0);

        return limit;
    }

    //The getDriverSpeed() method asks the user to input the offender's speed.
    public static int getDriverSpeed()
    {
        //declare method variables
        int speed = 0;
        String answer;
        do {
            answer = JOptionPane.showInputDialog(null, "Enter the the speed the driver was going:");
            if(answer == null) finish();
            speed = Integer.parseInt(answer);
            if (speed <=0) JOptionPane.showMessageDialog(null, "Drivers Speed must be above 0.");

        } while(speed <=0);




        return speed;
    }

    //The getTickets() method retrieves number of tickets.
    public static int getTickets()
    {
        //declare method variables
        int tickets = 0;
        String answer;
        do {
            answer = JOptionPane.showInputDialog(null, "Enter the number of previous tickets: ");
            if(answer == null) finish();
            tickets = Integer.parseInt(answer);
            if (tickets <0) JOptionPane.showMessageDialog(null, "Speed limit must be 0 or positive.");

        } while(tickets <0); // 0 is an acceptable number. Negative amount of tickets is not.


        return tickets;
    }

    //The getCosts() method returns the court costs.
    public static double getCosts(int tickets)
    {
        double costs = 0.0;

        if( tickets <=1 ) // we are error checking above for negative, so this is OK... but it would be probably be
            //safer and more modual to use tickets == 1 || tickets == 0
        {
            costs = 74.80;
        } else if (tickets ==2 )
        {
            costs = 99.80;
        } else // implicit - not <=1 and not 2, must be 3 or greater
        {
            costs = 124.80;
        }


        // perhaps beyond the scope of this lab, but it would have been better to return the cost value in each if (or switch)
        // statement = return 74.80 would work just as well.
        return costs;
    }

    //The output() method displays the cost of the ticket.
    public static void output(double dFine, double dCost, double dTicket)
    {

        DecimalFormat currency = new DecimalFormat("#,###.00");
        JOptionPane.showMessageDialog(null, "Your fine of " + currency.format(dFine) + " plus your court costs of " + currency.format(dCost) + " is " + currency.format(dTicket));


    }

    //The finish() method ends the program.
    public static void finish()
    {
        System.exit(0);
    }
}