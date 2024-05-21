using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk_Project_1
{
     internal class CashChange
    {
        #region CalculateChange
        public static decimal CalculateChange(decimal totalCost)
        {
            
            #region variables
            string userInput = "";
            bool numericCheck = true;
            decimal numbercheck = 0;
            int count = 1;
            decimal totalInput = 0;
            decimal changeLeft = 0;
            int billNumber = 0;
            decimal totalChange = 0;
            #endregion

            do
            {
                //if what they enter is not a number this will tell them to re-enter it
                if (numericCheck == false) Console.WriteLine("(Please enter a number)");

                //this Is where they input there number
                Console.Write("Please enter the payment {0}:   $", count);
                userInput = Console.ReadLine();

                //this checks that it is actually a number and if it is it will put into the numbercheck variable
                numericCheck = decimal.TryParse(userInput, out numbercheck);


                //if it is a number it and it is not a negative number then put it into the change left and fill the bank
                if (numericCheck == true && numbercheck > 0)
                {
                    billNumber = CashPayCheck.CheckBill(numbercheck);
                    if (billNumber < 12)
                    {
                        count++;
                        totalInput += numbercheck;
                        Kiosk.cashAmount += totalInput;
                        changeLeft = totalCost - totalInput;
                        Kiosk.bank[billNumber]++;
                        Kiosk.userMoney[billNumber]++;
                        if (totalInput < totalCost) Console.WriteLine("You still owe ${0}", changeLeft);
                    }
                    else if (billNumber >= 12) Console.WriteLine("Please enter a valid bill");
                }

                //if it is not a number above 0 it will tell the user to re-enter a number
                else if (numbercheck < 0) Console.WriteLine("(Please enter a number above 0)");



            } while (totalInput < totalCost);

            //this puts the changeLeft into the totalLeft and checks to see if it is a negative number
            if (changeLeft < 0) totalChange = changeLeft * -1;
            else totalChange = changeLeft;

            //return the totalchange
            return totalChange;
        }
#endregion

        #region Dispense change
        public static void dispenseChange(decimal totalChange)
        {
            //start dispencing the change to the user
            for (int index = 0; totalChange > 0; index++)
            {
                if (totalChange >= Kiosk.moneyValues[index] && Kiosk.bank[index] > 0)
                {
                    Kiosk.bank[index]--;
                    totalChange -= Kiosk.moneyValues[index];
                    Console.WriteLine("We dispensed {0:C} change", Kiosk.moneyValues[index]);
                    Kiosk.changeDue += Kiosk.moneyValues[index];
                    index = 0;
                }
            }
            Console.WriteLine("***************************************");
            Console.WriteLine("We have completed your payment");

        }
        #endregion

    } //END CASHCHANGE CLASS
}
