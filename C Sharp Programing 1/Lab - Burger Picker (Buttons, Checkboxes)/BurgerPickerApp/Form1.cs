using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BurgerPickerApp
{
    public partial class CreateABurger : Form
    {



        //Create the instantiation of the Order Class
        Order customersOrder = new Order();
        

        public CreateABurger()
        {
            InitializeComponent();
            //Set the default of the dropdown box when the program opens
            Patty_ComboBox.SelectedIndex = 0;
            //Set the radio buttons to checked, default no Toppings

            NoCheese_Radio.Checked = true;
            NoToppings_radio.Checked = true;
            NoExtra_radio.Checked = true;
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            //Error Handling - check to see if the Name is filled in or empty, and how many cheeses are selected
            int number = customersOrder.TotalCheeses();
            
           
            bool errorCaught = false;
            try
            {
                IsFieldEmpty(Name_textBox.Text, "Name");
                
            }
            catch (OrdersException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                errorCaught = true;
            }

        

            try
            {
                TooManyItems(number);
            }
            catch (OrdersException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                errorCaught = true;
            }

            if (!errorCaught)
            {
                customersOrder.Name = Name_textBox.Text;
                
               


                string pattySelection = Patty_ComboBox.Items[Patty_ComboBox.SelectedIndex].ToString();
                customersOrder.SetPatty(pattySelection);

                //Generate the Reciept
                Reciept_textBox.Text = customersOrder.CreateReceipt();
            }
        }

        private void Combo_btn_Click(object sender, EventArgs e)
        {
            if(customersOrder.IsCombo)
            {
                Combo_btn.Text = "Make it a Combo";
                customersOrder.IsCombo = false;
            }
            else
            {
                Combo_btn.Text = "Remove Combo from Order";
                customersOrder.IsCombo = true;
            }
           
        }


        private void Close_btn_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Reciept_textBox.Text))
            {
                MessageBox.Show("Hit 'See Total' and verify your order is correct before finalizing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Close();
            }
        }

        private void Reset_btn_Click(object sender, EventArgs e)
        {
            
            if(customersOrder.IsCombo)
            {
                Combo_btn.Text = "Make it a Combo";
            }
            Reciept_textBox.Text = null;
            Name_textBox.Text = null;
            Patty_ComboBox.SelectedIndex = 0;
            customersOrder.ResetOrder();
            NoCheese_Radio.Checked = true;
            NoToppings_radio.Checked = true;
            NoExtra_radio.Checked = true;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Aaaand... All the bloody checkboxes.

        private void American_check_CheckedChanged(object sender, EventArgs e)
        {
            if (American_check.Checked)
            {
                customersOrder.AddCheese("AMERICAN");
                NoCheese_Radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveCheese("AMERICAN");
            }
            

        }

        private void PepperJack_check_CheckedChanged(object sender, EventArgs e)
        {
            if (PepperJack_check.Checked)
            {
                customersOrder.AddCheese("PEPPER JACK");
                NoCheese_Radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveCheese("PEPPER JACK");
            }
        }

        private void Cheddar_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Cheddar_check.Checked)
            {
                customersOrder.AddCheese("CHEDDAR");
                NoCheese_Radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveCheese("CHEDDAR");
            }
        }

        private void Swiss_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Swiss_check.Checked)
            {
                customersOrder.AddCheese("SWISS");
                NoCheese_Radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveCheese("SWISS");
            }
        }

        private void NoCheese_Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (NoCheese_Radio.Checked)
            {
                Swiss_check.Checked = false;
                American_check.Checked = false;
                PepperJack_check.Checked = false;
                Cheddar_check.Checked = false;
            }
        }

        private void Ketchup_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Ketchup_check.Checked)
            {
                customersOrder.AddTopping("KETCHUP");
                NoToppings_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveTopping("KETCHUP");
            }
        }

        private void Mustard_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Mustard_check.Checked)
            {
                customersOrder.AddTopping("MUSTARD");
                NoToppings_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveTopping("MUSTARD");
            }
        }

        private void Mayo_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Mayo_check.Checked)
            {
                customersOrder.AddTopping("MAYO");
                NoToppings_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveTopping("MAYO");
            }
        }

        private void GarlicAoili_check_CheckedChanged(object sender, EventArgs e)
        {
            if (GarlicAoili_check.Checked)
            {
                customersOrder.AddTopping("GARLIC AOILI");
                NoToppings_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveTopping("GARLIC AOILI");
            }
        }

        private void Lettuce_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Lettuce_check.Checked)
            {
                customersOrder.AddTopping("LETTUCE");
                NoToppings_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveTopping("LETTUCE");
            }
        }

        private void Onion_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Onion_check.Checked)
            {
                customersOrder.AddTopping("RED ONION");
                NoToppings_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveTopping("RED ONION");
            }
        }

        private void Tomato_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Tomato_check.Checked)
            {
                customersOrder.AddTopping("TOMATO");
                NoToppings_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveTopping("TOMATO");
            }
        }

        private void Pickles_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Pickles_check.Checked)
            {
                customersOrder.AddTopping("PICKLES");
                NoToppings_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveTopping("PICKLES");
            }
        }

        private void NoToppings_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (NoToppings_radio.Checked)
            {
                Ketchup_check.Checked = false;
                Mustard_check.Checked = false;
                Mayo_check.Checked = false;
                GarlicAoili_check.Checked = false;
                Lettuce_check.Checked = false;
                Tomato_check.Checked = false;
                Onion_check.Checked = false;
                Pickles_check.Checked = false;
            }
        }

        private void Bacon_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Bacon_check.Checked)
            {
                customersOrder.AddExtra("BACON");
                NoExtra_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveExtra("BACON");
            }
        }

        private void Jalapenos_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Jalapenos_check.Checked)
            {
                customersOrder.AddExtra("JALAPENO");
                NoExtra_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveExtra("JALAPENO");
            }
        }

        private void FriedEgg_check_CheckedChanged(object sender, EventArgs e)
        {
            if (FriedEgg_check.Checked)
            {
                customersOrder.AddExtra("FRIED EGG");
                NoExtra_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveExtra("FRIED EGG");
            }
        }

        private void Guacamole_check_CheckedChanged(object sender, EventArgs e)
        {
            if (Guacamole_check.Checked)
            {
                customersOrder.AddExtra("GUACAMOLE");
                NoExtra_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveExtra("GUACAMOLE");
            }
        }

        private void CarmalizedOnions_check_CheckedChanged(object sender, EventArgs e)
        {
            if (CarmalizedOnions_check.Checked)
            {
                customersOrder.AddExtra("C.ONIONS");
                NoExtra_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveExtra("C.ONIONS");
            }
        }

        private void OnionRings_check_CheckedChanged(object sender, EventArgs e)
        {
            if (OnionRings_check.Checked)
            {
                customersOrder.AddExtra("O-RINGS");
                NoExtra_radio.Checked = false;
            }
            else
            {
                customersOrder.RemoveExtra("O-RINGS");
            }
        }

        private void NoExtra_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (NoExtra_radio.Checked)
            {
                Bacon_check.Checked = false;
                Jalapenos_check.Checked = false;
                FriedEgg_check.Checked = false;
                Guacamole_check.Checked = false;
                CarmalizedOnions_check.Checked = false;
                OnionRings_check.Checked = false;
            }
        }



        // Error Check Methods
        private void IsFieldEmpty(string test, string fieldname)
        {
            if (String.IsNullOrEmpty(test))
            {
                throw new OrdersException(String.Format("The {0} field must be filled out", fieldname));
            }
        }

        private void TooManyItems(int number)
        {
            if (number > 3)
            {
                throw new OrdersException("Only three cheeses per burger. Please uncheck some and resubmit");
            }
        }


    }
}





/* This is a Class for an individuals Order. It will contain the methods for adjusting the total cost of 
* an order, adding the components onto the burger order sheet, and printing out the receipt
*/

public class Order
{
    private double cost;

    // Code costs as variables to make changes in future easier to adjust.
    private const double BASICCOST = 5.99;
    private const double DOUBLEPATTY = 1.99;
    private const double EXTRACOST = .50;
    private const double COMBOCOST = 3.5;
    private const double NOPATTY = -2.00;

    //Lists of toppings (in order to make them easy to dynamically update)
    private List<string> cheese = new List<string>();
    private List<string> toppings = new List<string>();
    private List<string> extras = new List<string>();
    private string patty = "BEEF";
    public bool IsCombo
    { get; set; }

    //Count to keep them under 3 slices of cheese and warn if above
    private int totalCheeseSlices = 0;

    //Count to know how much to add for price
    private int totalExtrasCount = 0;



    public string Name
    { get; set; }




    public void ResetOrder()
    {
        cheese.Clear();
        toppings.Clear();
        extras.Clear();
        Name = null;
        cost = 0;
        totalCheeseSlices = 0;
        totalExtrasCount = 0;
        IsCombo = false;

    }
    

    //For making it look Pretty (if no checkboxes selected, set the Radio Button to Checked)

    public int TotalToppings()
    {
        int total = toppings.Count;
        return total;
    }

    public int TotalExtras()
    {
        int total = totalExtrasCount;
        return total;
    }
    //For error Checking as well as making look pretty
    public int TotalCheeses()
    {
        int y = totalCheeseSlices;
        return y;
    }


    public void AddCheese(string a)
    {
        cheese.Add(a);
        totalCheeseSlices++;
    }

    public void RemoveCheese(string a)
    {
        cheese.Remove(a);
        totalCheeseSlices--;
    }

    public void AddTopping(string a)
    {
        toppings.Add(a);
    }

    public void RemoveTopping(string a)
    {
        toppings.Remove(a);
    }

    public void AddExtra(string a)
    {
        extras.Add(a);
        totalExtrasCount++;
    }

    public void RemoveExtra(string a)
    {
        extras.Remove(a);
        totalExtrasCount--;
    }

    public void SetPatty(string a)
    {
        patty = a;
    }

    public void CalculateCost()
    {
        cost = BASICCOST + (totalExtrasCount * EXTRACOST);

        if (IsCombo)
        {
            cost = cost + COMBOCOST;
        } 
        if (patty == "BEEF (Double)")
        {
            cost = cost + DOUBLEPATTY;
        }
        else if (patty == "NO PATTY")
        {
            cost = cost + NOPATTY;
        }
        
    }


    public string CreateReceipt()
    {
        CalculateCost();

        string reciept = "Verify your Order is Correct.\r\n\r\n**********\r\n\r\nOrder for: ";

        reciept = reciept + Name + ".\r\n\t" + patty;

        for(int i=0; i<cheese.Count; i++)
        {
            reciept = reciept + "\r\n\t" + cheese[i];
        }

        for(int x=0; x < toppings.Count; x++)
        {
            reciept = reciept + "\r\n\t" + toppings[x];
        }

        for (int y=0; y < extras.Count; y++)
        {
            reciept = reciept + "\r\n\t" + extras[y] + " " + "*upcharge*";
        }

        if(IsCombo)
        {
            reciept = reciept + "\r\n\r\nCOMBO ORDER";
        }

        reciept = reciept + "\r\n\r\n**********\r\n\r\nTOTAL COST: " + cost + "\r\n\r\nHIT SUBMIT AND EXIT TO FINALIZE YOUR ORDER";

        return reciept;
    }
}


    //Custom Exception Class (not required for project, but for practice!

    public class OrdersException : Exception

    {

        public OrdersException()
        {

        }

        public OrdersException(string message) : base(message)
        {

        }

        
    }
