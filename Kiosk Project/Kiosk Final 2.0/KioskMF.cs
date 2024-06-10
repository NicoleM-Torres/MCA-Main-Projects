using NHSKiosk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NHSKiosk.Program;

namespace NHSKiosk
{

    public class KioskMF
    {

        decimal changeOwed = 0;

        //nk kiosk;

        public DateTime dtmTime;
        dtmTime = DateTime.Now;

                    //Declaring arrays
                    /*The CurrencyName array holds the names of different currency.
                      The CurrencyAmount array stores the quantity of each currency.
                      The CurrencyValue array contains the monetary value of each currency.
                      The UserPayment array tracks the user's payment in each currency.*/
           Bank MyBank = new Bank {

               CurrencyName = new string[] { "Hundreds", "Fifties", "Twenties", "Tens", "Fives", "Twos",
                                                  "Ones", "Half Dollar", "Quarters", "Dimes", "Nickels", "Pennies" };
      CurrencyAmount = new int[12];
            CurrencyValue = new decimal[] { 100, 50, 20, 10, 5, 2, 1, .50m, .25m, .10m, .05m, .01m };
    UserPayment = new int[12];

            };
        //Setting Variables
        //Initializes a transaction 'trans' with default values.
        trans.transactionNum = 1;
        trans.transactionDate = dtmTime.ToString("MMM-dd-yyyy,HH:mm"); //The date and time of the transaction.
        trans.cashAmount = 0;
        trans.cardVendor = "No Vendor Given";
        trans.cardAmount = 0;
        trans.changeGiven = 0;



    //--------------------------------------------VAULT--------------------------------------------
    //Filling the "Bank" with bills/coins
    /*sets each element of the kiosk.3CurrencyAmount array to 5.
    This means that, regardless of the change owed, the kiosk
    will always return 5 units of each currency denomination.*/
    for (int index = 0; index <CurrencyAmount.Length; index++)
    {
        CurrencyAmount[index] = 5;
    } //END FOR

//Loop that will run the kiosk forever.
while (true)
{
    //Displaying a welcome message and how to use the kiosk                
    Console.WriteLine("Welcome to NHS Self-Help Kiosk");
    Console.WriteLine("***************************************");
    Console.WriteLine("Please start by entering in the prices for the items.");
    Console.WriteLine("Press enter in a empty space when you are finished scanning the products.");
    Console.WriteLine("***************************************");


    /*method to input item prices and calculate the total cost
     Calculates the total cost of input item prices.
     Returns the total cost of the input item prices as a decimal value.*/
    decimal totalCost = InputItemPrices();


    /*Handle user input for the prices of items.After 
     * calculating the total cost, displaying it in the console*/
    Console.WriteLine("***************************************");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Total\t\t$" + totalCost);
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("***************************************");


    /*Calling 'UsingCard' method to see if the user is using a card 
      and if so the function calls for method to be used.
      Updates the total cost using a card at the kiosk.
      It will return the total cost after entering card # */
    totalCost = UsingCard(kiosk, totalCost);
    Console.WriteLine();

    //--------------------------------------------Rounds up totalCost to two decimal points.--------------------------------------------
    /*Converts a decimal number to a string representation and finds the index of the decimal point.
     And will return string representation of the decimal number*/
    string decimalString = totalCost.ToString(); //Decimal number that needs to be converted to a string.
    int decimalIndex = 0;

    //for loop to find the "." in the string version of the totalCost
    /*Finds the index of the decimal point in a decimal string 'decimalString'.
      And returns index of the decimal point in the decimal string.*/
    for (int index = 0; index < decimalString.Length; index++)
    {
        if (decimalString[index] == '.') decimalIndex = index; //Setting decimalIndex to the place of the "."
    }

    /*Shortens decimal part of the string if the 'totalCost' is greater than 0 and the decimal part length is greater than 3.
     it will return the shortened decimal string if conditions are met; otherwise,the original decimal string.*/
    if (totalCost > 0 && decimalString.Length > 3)
    {
        //Making sure there are no numbers more than 2 places after the "."
        //extrac substring from = index where to start from (index 0 , variable + 3) from user input string
        decimalString = decimalString.Substring(0, decimalIndex + 3);
    }

    //reverting the string back into a decimal
    totalCost = decimal.Parse(decimalString);

    //if the total price is still greater than 0 and the user is not using a card
    //the user will be prompted to pay the remainder in cash
    if (totalCost > 0)
    {

        //Running method to prompt user to input payment
        changeOwed = CashPayment(kiosk, totalCost);
        Console.WriteLine("---------------------------------------");

        //Prints 'chagedOwed' to console
        Console.WriteLine("Change {0:C}", changeOwed);

        //if the kiosk owes the user change this section will run.
        //and see if the bank has enough change to dispense. 
        //If the bank does not have enough cash for change the user is refunded.
        if (changeOwed > 0)
        {
            Console.WriteLine();
            //runs method to see if the bank has enough $$$ change
            bool canPay = CheckBank(changeOwed, kiosk);

            //runs method to dispense change
            DispenseChange(changeOwed, kiosk, canPay);
        }

        //Prints 'Thank You' message to console
        Console.WriteLine("---------------------------------------");
        Console.WriteLine("Thank you for choosing a NHS Self-Help Kiosk, your reciept will display in a few seconds.");

    } //END IF
    /*method being called, records the details of the current transaction*/
    TransLog.Logging();
    /*increments the transactionNum property of a trans object by 1.
     * It tracks the number of transactions processed by the kiosk*/
    trans.transactionNum += 1;

    //Call Kiosk Transaction Reset Function to reset the kiosk for the next transaction
    KioskTransactionReset(kiosk);

}//ND CLASS KIOSK MF   
}
}
//end namespace
