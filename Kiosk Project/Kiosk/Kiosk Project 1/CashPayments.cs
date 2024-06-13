using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk_Project_1
{
        #region CashPay
    internal class CashPayments ()
    {
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
    }//END CLASS CASHPAYMENTS
        #endregion


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
                    //IF USERINPUT IS NOT A VALID NUMBER THEY WILL BE PROMPTER TO RE-ENTER
                    if (numericCheck == false) Console.WriteLine("(Please enter a number)");

                    //USERINPUT PAYMENT #'S (DOLLAR BILL VALUE -- 1,2,5,10,20,50,100)
                    Console.Write("Payment {0}:   $", count);
                    userInput = Console.ReadLine();

                    //CHECKS IF USERINPUR IS A #, IF IT IS A VALID #, IT WILL BE STORED TO 'numberCheck' VARIABLE
                    numericCheck = decimal.TryParse(userInput, out numbercheck);


                    //IF IT'S A VALID #, IT IS ADDED TO THE CHANGE AMOUNT AND 
                    if (numericCheck == true && numbercheck > 0)
                    {
                        billNumber = CashPayments.CheckBill(numbercheck);
                        if (billNumber < 12)
                        {
                            count++;
                            totalInput += numbercheck;
                            Kiosk.cashAmount += totalInput;
                            changeLeft = totalCost - totalInput;
                            Kiosk.bank[billNumber]++;
                            Kiosk.userMoney[billNumber]++;
                            if (totalInput < totalCost) Console.WriteLine("Remaining ${0}", changeLeft);
                        }
                        else if (billNumber >= 12) Console.WriteLine("Please enter a valid dollar bill");
                    }

                    //IF NOT A # ABOVE 0, IT WILL REQUIRE USERINPUT TO BE RE-ENTERED
                    else if (numbercheck < 0) Console.WriteLine("(Enter a number above 0)");



                } while (totalInput < totalCost);

                //this puts the changeLeft into the totalLeft and checks to see if it is a negative number
                //ADDS 'changeLeft' VALUES INTO 'totalLeft' VARIABLE AND VERIFIES IF THE #'S ARE VALID (NOT NEGATIVE OR < 0)
                if (changeLeft < 0) totalChange = changeLeft * -1;
                else totalChange = changeLeft;

                //RETURN THE 'totalChange' VALUES
                return totalChange;
            }
            #endregion

        #region Dispense change
            public static void dispenseChange(decimal totalChange)
            {
                //DISPENSE CHANGE TO USER
                for (int index = 0; totalChange > 0; index++)
                {
                    if (totalChange >= Kiosk.moneyValues[index] && Kiosk.bank[index] > 0)
                    {
                        Kiosk.bank[index]--;
                        totalChange -= Kiosk.moneyValues[index];
                        Console.WriteLine("We dispensed {0:C} change.", Kiosk.moneyValues[index]);
                        Kiosk.changeDue += Kiosk.moneyValues[index];
                        index = 0;
                    }
                }
                Console.WriteLine("***************************************");
                Console.WriteLine("We have processed your payment.");

            }
            #endregion

    } //END CASHCHANGE CLASS
}//END NAMESPACE
