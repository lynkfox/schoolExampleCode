/*
	Chapter :  Chapter5
	Programmer:  Anthony Goh
	Date:
	Filename:	 KilowattApplet.class , KilowattApplet.html
	Purpose:	 Calculates the cost of an appliance based on KwH's used and cost per KhW
				 */

import java.applet.*;
import java.awt.*;
import java.awt.event.*;
import java.text.DecimalFormat;

public class KilowattApplet extends Applet implements ActionListener {

    //Declare Variables

    double costPKW, kwHours, monthlyCost;
    DecimalFormat currency = new DecimalFormat("$####.00");

    //construct components

    Label appTitle = new Label("Welcome to the Appliance Energy Calculator");
    Label costLabel = new Label("Please enter the cost per kilowatt-hour: ");
    TextField costField = new TextField(10);
    Label hoursLabel = new Label("Please enter number of kilowatt-hours: ");
    TextField hoursField = new TextField(10);
    Button calcButton = new Button("Calculate");
    Label outputLabel = new Label("Click the Calculate button to display the monthly cost.");

    public void init()
    {
        setBackground(Color.pink);
        setForeground(Color.DARK_GRAY);
        add(appTitle);
        add(costLabel);
        add(costField);
        add(hoursLabel);
        add(hoursField);
        add(calcButton);
        calcButton.addActionListener(this);
        add(outputLabel);
    }

    public void actionPerformed(ActionEvent e)
            // remember this is required for any applet class using 'implements ActionListener'
    {
        costPKW = Double.parseDouble(costField.getText());
        kwHours = Double.parseDouble(hoursField.getText());

        monthlyCost = kwHours*costPKW;
        outputLabel.setText("The monthly cost to operate this appliance is " + currency.format(monthlyCost) + ".");
    }
}
