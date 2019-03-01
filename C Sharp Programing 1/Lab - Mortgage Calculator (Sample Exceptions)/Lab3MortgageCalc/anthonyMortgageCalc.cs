using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Anthony Goh, Lab 3 - Mortgage Calc w/Gui
 * 
 * A Windows Forms GUI will be developed.
 * Required user input fields (text boxes) on this form include: Applicant Name, Home Purchase Price, Down Payment Amount, 
 *    Interest Rate (annual), and Loan Term (months). 
 * Each of the required fields must be validated to ensure they are not empty after the user clicks the Calculate button.
 * Each of the required numeric fields must be validated to ensure correct numeric entry.
 * In addition to the required fields there should be two additional fields with their read only properties to to true: Amount to 
 *    Finance and Monthly Payment.
 * The Amount to Finance field will be filled with the result of the Down Payment Amount subtracted from the Home Purchase Price. 
 * The Monthly Payment will be calculated by this application.
 * Two buttons will be included on this form: Calculate and Close. When the user clicks Calculate, the form's fields will be 
 *       validated and, if valid, calculate the monthly mortgage payment. The Close button will cause the application to end.
 *       
 *       */

    /* Lab 8-9 - Adding Exception Class to this program
     * 
     * three exception methods created as part of custome exception class
     * 
     * These methods are used for the following exceptions:
     * 
     * Any numerical field contains a negative
     * 
     * Loan Term (Months) less than 12 or greater than 720
     * 
     * Down Payment less than 1% of the total.
     */

namespace Lab3MortgageCalc

   
{
    
    public partial class Form1 : Form
    {

        double purchasePrice = 0;
        double downPayment = 0;
        double annualInterest = 0;
        double loanTerm = 0;
        double monthlyPayment = 0;
        double monthlyInterest = 0;

        public Form1()

           
        {
            InitializeComponent();
        }

        private void button_Calculate_Click(object sender, EventArgs e)
        {

           
            if (!String.IsNullOrEmpty(applicantNameText.Text))
            {

            }
            else
            {
                MessageBox.Show("Applicant Name is a required field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                applicantNameText.BackColor = Color.MistyRose;
            }

            


            /* We aren't using Applicant Name for anything, so the check function just checks to see if its empty or not. No reason 
             * to check anything beyond that - if someone wants to be named 32, let them!
             */

            /* Old Error Checking for Home Purchase Price, before Exception Lab
            if (!String.IsNullOrEmpty(homePurchasePriceText.Text))
            {
                double value;

                if (Double.TryParse(homePurchasePriceText.Text, out value))
                {
                    purchasePrice = Convert.ToDouble(value);
                }

                else
                {
                    MessageBox.Show("Purchase price should be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    homePurchasePriceText.Text = string.Empty;
                    homePurchasePriceText.BackColor = Color.MistyRose;

                }

            } else
            {
                MessageBox.Show("Purchase Price cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                homePurchasePriceText.BackColor = Color.MistyRose;
            }
            */
            try
            {
                IsNegativeOrZeroNumberOrNumberValidation(homePurchasePriceText.Text, "Home Purchase Price");
                double.TryParse(homePurchasePriceText.Text, out purchasePrice);
            }
            catch(MorgtageIncorrectFieldsException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                homePurchasePriceText.BackColor = Color.MistyRose;
            }


            /* The above is repeated for each field. It first checks to see if its empty or not. If its empty, it goes to the 'Can't be empty
             * error message. If its not empty, then it checks to see if it is a valid int32 
             * this is repeated for every field
             * 
             * the color change is just for fun.
             */

            /*
            if (!String.IsNullOrEmpty(downPaymentText.Text))
            {
                double value;
                if (Double.TryParse(downPaymentText.Text, out value))
                {
                    downPayment = Convert.ToDouble(value);
                }

                else
                {
                    MessageBox.Show("Down Payment should be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    downPaymentText.Text = string.Empty;
                    downPaymentText.BackColor = Color.MistyRose;

                }

            } else
            {
                MessageBox.Show("Down Payment cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                downPaymentText.BackColor = Color.MistyRose;
            }

            if (!String.IsNullOrEmpty(interestRateText.Text))
            {
                double value;
                if (Double.TryParse(interestRateText.Text, out value))
                {
                    annualInterest = Convert.ToDouble(value);
                    monthlyInterest = annualInterest / 12;

                    // convert annual interest rate to the monthly value for the final calculations.
                }

                else
                {
                    MessageBox.Show("Interest Rate should be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    interestRateText.Text = string.Empty;
                    interestRateText.BackColor = Color.MistyRose;

                }

            } else
            {
                MessageBox.Show("Interest Rate cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                interestRateText.BackColor = Color.MistyRose;
            }

            if (!String.IsNullOrEmpty(loanTermText.Text))
            {
                int value;
                if (Int32.TryParse(loanTermText.Text, out value))
                {
                    loanTerm = Convert.ToInt32(value);
                }

                else
                {
                    MessageBox.Show("Loan Term should be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    loanTermText.Text = string.Empty;
                    loanTermText.BackColor = Color.MistyRose;

                }

            } else
            {
                MessageBox.Show("Loan Term cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                loanTermText.BackColor = Color.MistyRose;
            }

            /* We use an IF statement of && to make sure all of the fields above are actually filled out - if they arent, then it throws an error
             * instead of doing the calculations.
             */

            // Loan Term Testing
            try
            {
                IsNegativeOrZeroNumberOrNumberValidation(loanTermText.Text, "Loan Term");
                Double.TryParse(loanTermText.Text, out loanTerm);
                LoanTermsLengthValidation(loanTerm);
            }
            catch (MorgtageIncorrectFieldsException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                loanTermText.BackColor = Color.MistyRose;
            }
           
            


            //Interest Rate Testing
            try
            {
                IsNegativeOrZeroNumberOrNumberValidation(interestRateText.Text, "Interest Rate");
                double.TryParse(interestRateText.Text, out annualInterest);
                monthlyInterest = annualInterest / 12;

                // convert annual interest rate to the monthly value for the final calculations.

            }
            catch (MorgtageIncorrectFieldsException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                interestRateText.BackColor = Color.MistyRose;
            }

            // Down Payment Amount Testing
            try
            {
                IsNegativeOrZeroNumberOrNumberValidation(downPaymentText.Text, "Down Payment");
                double.TryParse(downPaymentText.Text, out downPayment);
                DownPaymentValidation(downPayment, purchasePrice);

            }
            catch (MorgtageIncorrectFieldsException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                downPaymentText.BackColor = Color.MistyRose;
            }
           


            // back to the old programing... if it aint broke...

            if (!String.IsNullOrEmpty(applicantNameText.Text) && !String.IsNullOrEmpty(homePurchasePriceText.Text) && !String.IsNullOrEmpty(downPaymentText.Text) && !String.IsNullOrEmpty(interestRateText.Text) && !String.IsNullOrEmpty(loanTermText.Text))
            {
                //Lab 8/9 - This error checkabove may seem overly needed, but it prevents any information from being placed in the calculated fields if any exceptions were thrown.
                monthlyPayment = ((purchasePrice - downPayment) * monthlyInterest) * ((Math.Pow((1 + monthlyInterest), loanTerm)) / (Math.Pow((1 + monthlyInterest), loanTerm) - 1));

                monthlyPaymentText.Text = string.Format("{0:C}", monthlyPayment);

                totalLoanText.Text = string.Format("{0:C}", purchasePrice - downPayment);

                applicantNameText.BackColor = Color.White;
                homePurchasePriceText.BackColor = Color.White;
                loanTermText.BackColor = Color.White;
                interestRateText.BackColor = Color.White;
                downPaymentText.BackColor = Color.White;

                // The calculations are done. Reset the backcolor to show the values were correct now that you've gone and put them in right.

            } else
            {
               
               

            }

        }


        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void IsNegativeOrZeroNumberOrNumberValidation(string test, string fieldName)
        
        {
            if (String.IsNullOrEmpty(test))
            {
                throw new MorgtageIncorrectFieldsException(String.Format("There must be an entry in {0}", fieldName));
            }
            else
            {
                if (double.TryParse(test, out double number))
                {
                    if (number <= 0)
                    {
                        throw new MorgtageIncorrectFieldsException(String.Format("The value of {0} must be greater than or equal to 0.", fieldName));
                    }
                }
                else
                {
                    throw new MorgtageIncorrectFieldsException(String.Format("The value of {0} must be a number.", fieldName));

                }
            }
            
            
        }

        private void LoanTermsLengthValidation(double number)
        {
            if (number <12 || number > 720)
            {
                throw new MorgtageIncorrectFieldsException("Loan term must be between 12 and 720 months");
            }
        }

        private void DownPaymentValidation(double downpayment, double houseValue)
        {
            double checkPercentage = (downpayment / houseValue) * 100;

            if (checkPercentage < 1)
            {
                downPayment = houseValue / 100;
                throw new MorgtageIncorrectFieldsException(String.Format("The Downpayemt must be at least 1%, or {0}", downPayment));
            }
        }
    }
}

//Custom Exception Class, the very simple way to do it, with custom exception. - the quick and dirty way, if you will.
// Sorry if this wasnt the idea of the lab, but this is what I found to work and how I could wrap my brain around it

public class MorgtageIncorrectFieldsException : Exception

{

    public MorgtageIncorrectFieldsException()
    {

    }
    
    public MorgtageIncorrectFieldsException(string message) : base(message)
    {
        
    }
    
}