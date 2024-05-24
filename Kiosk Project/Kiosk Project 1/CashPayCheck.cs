using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk_Project_1
{
    internal class CashPayCheck ()
    {
        #region CashPay
        public static void CashPay(decimal totalCost)
        {
            CashChange.CalculateChange(totalCost);
            //CALLS THE METHOD TO GET CHANGE AMOUNT
            decimal totalChange = CashChange.CalculateChange(totalCost);

            Console.WriteLine("***************************************");
            Console.WriteLine("Your total change is ${0}", totalChange);

            //CALLS METHOD TO CHECK KIOSK BANK
            bool payGood = Bank.CheckBankCard(totalChange);

            //CHECKS IF KIOSK VAULT HAS ENOUGH $$, IF IT IS, IT WILL DISPENSE CHANGE
            if (payGood == true) CashChange.dispenseChange(totalChange);

            Console.WriteLine("***************************************");
            Console.WriteLine("Thank you for using a NHS Kiosk");

        } //END CASHPAY METHOD
        #endregion 

        #region CheckDollarBills
        public static int CheckBill(decimal numbercheck)
        {
            if (numbercheck == 100) return 0;
            else if (numbercheck == 50) return 1;
            else if (numbercheck == 20) return 2;
            else if (numbercheck == 10) return 3;
            else if (numbercheck == 5) return 4;
            else if (numbercheck == 2) return 5;
            else if (numbercheck == 1) return 6;
            else if (numbercheck == .50m) return 7;
            else if (numbercheck == .25m) return 8;
            else if (numbercheck == .10m) return 9;
            else if (numbercheck == .05m) return 10;
            else if (numbercheck == .01m) return 11;
            else return 12;
        }
#endregion


    }//END CLASS CASHPAYMENTS
}
