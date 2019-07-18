namespace CcnSession
{
    /* This custom class is for holding the data for the Accounting Reports from the General Ledger
     * 
     */
    public class PLState : SQL
    {
        public double Sales { get; set; }
        public double Utilities { get; set; }
        public double Inventory { get; set; }
        public double Expenses { get; set; }
        public double Payroll { get; set; }
        public double Marketing { get; set; }
        public double Vendor { get; set; }
        public double Receivable { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string LongMonth { get; set; }
        public PLState()
        {
            Sales = 0;
            Utilities = 0;
            Inventory = 0;
            Expenses = 0;
            Payroll = 0;
            Year = 0;
            Month = 0;
            LongMonth = "";
            Marketing = 0;
            Vendor = 0;
            Receivable = 0;
        }
        public void SetMonth(int m)
        {
            switch (m)
            {
                case 1:
                    LongMonth = "Januaray";
                    Month = m;
                    break;
                case 2:
                    LongMonth = "Feburary";
                    Month = m;
                    break;
                case 3:
                    LongMonth = "March";
                    Month = m;
                    break;
                case 4:
                    LongMonth = "April";
                    Month = m;
                    break;
                case 5:
                    LongMonth = "May";
                    Month = m;
                    break;
                case 6:
                    LongMonth = "June";
                    Month = m;
                    break;
                case 7:
                    LongMonth = "July";
                    Month = m;
                    break;
                case 8:
                    LongMonth = "August";
                    Month = m;
                    break;
                case 9:
                    LongMonth = "September";
                    Month = m;
                    break;
                case 10:
                    LongMonth = "October";
                    Month = m;
                    break;
                case 11:
                    LongMonth = "November";
                    Month = m;
                    break;
                case 12:
                    LongMonth = "December";
                    Month = m;
                    break;
                default:
                    LongMonth = "";
                    Month = 0;
                    break;
            }
        }
    }
}
