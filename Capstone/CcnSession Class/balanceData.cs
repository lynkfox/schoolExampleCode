namespace CcnSession
{
    public class balanceData : SQL
    {
        public double Cash { get; set; }
        public double Receivable { get; set; }
        public double Payable { get; set; }
        public double Inventory { get; set; }
        public double Trademark { get; set; }
        public double Taxes { get; set; }
        public double Payroll { get; set; }
        public double Insurance { get; set; }
        public double Other { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string LongMonth { get; set; }
        public balanceData()
        {
            Cash = 0;
            Receivable = 0;
            Payable = 0;
            Inventory = 0;
            Trademark = 0;
            Taxes = 0;
            Payroll = 0;
            Insurance = 0;
            Other = 0;
            Year = 0;
            Month = 0;
            LongMonth = "";
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
        public double TotalIntang()
        {
            return Trademark;
        }
        public double TotalPhysAsset()
        {
            return Cash + Inventory + Insurance;
        }
        public double TotalAsset()
        {
            return TotalIntang() + TotalPhysAsset() + Other;
        }
        public double CurrLiab()
        {
            return Payroll + Receivable + Payable + Taxes;
        }
    }
}
