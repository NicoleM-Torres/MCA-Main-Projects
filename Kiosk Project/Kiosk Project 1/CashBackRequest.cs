using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk_Project_1
{
    internal class CashBackRequest
    {
        #region Cash back
        public static decimal CashBack()
        {
            string cashBack = "";
            decimal counter = 0;
            decimal numberCheck = 0;
            bool checker = true;
            bool numericCheck = true;

            //ask them how much cash they want back
            do
            {
                Console.Write("How much cash do you want back: $");
                cashBack = Console.ReadLine();

                counter = 0;

                //check to make sure it only has one decimal and only 2 places after the decimal
                for (int index = 0; index < cashBack.Length; index++)
                {
                    if (cashBack[index] == '.')
                    {
                        for (int incrementer = index + 1; incrementer < cashBack.Length; incrementer++)
                        {
                            counter++;
                        }
                    }
                }

                if (counter > 2) checker = false;
                else checker = true;

                //this checks that it is actually a number and if it is it will put into the numbercheck variable
                numericCheck = decimal.TryParse(cashBack, out numberCheck);

                // if it has more than two decimal places then it tells them to re-enter a number
                if (checker == false) Console.WriteLine("(You cant have more than two decimal places)");

                //if it is not a number above 0 it will tell the user to re-enter a number
                else if (numberCheck < 0) Console.WriteLine("(Please enter a number above 0)");

                //it is not actually a number It will tell them to re-renter a number
                else if (numericCheck == false) Console.WriteLine("(Please enter a number)");

            } while (checker == false || numberCheck < 0 || numericCheck == false);

            //make sure there is enough money in the bank
            bool check = Bank.CheckBankCash(numberCheck, true);

            // return the answers
            if (check == true) return numberCheck;
            else return -1;
        } //END CASHBACK METHOD
#endregion
    } //END CASHBACK REQUEST CLASS
} //END NAMESPACE