using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk_Project_1
{
    internal class CardPayCheck
    {

        #region card pay
        public static decimal CardPay(decimal totalCost)
        {
            //this tells
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("We only accept Visa, Mastercard, Discover, and American Express");
            string creditCard = "";
            bool isGood = true;
            string answer = "";
            decimal cashBack = 0;
            decimal newTotal = 0;
            bool getCashBack = true;

            bool numericCheck = true;
            decimal numberCheck = 0;

            //Validates if it is the actual length of a card
            do
            {
                creditCard = Prompt("Please enter a card number");         
                if (creditCard.Length != 16) Console.WriteLine("(Please enter a valid card number)");
            } while (creditCard.Length != 16);

            //sees if its actually a vendor
            string vendor = VendorCheck.ValidateVendor(creditCard);
            Kiosk.cardVendor = vendor;

            //if it is a good vendor it then checks to make sure its a valid card
            if (vendor != "Non valid card")
            {
                isGood = CardCheckVal.ValidateCard(creditCard);

                //if the card was invalid it then asks them if they want to enter a new one
                if (isGood == false)
                {
                    Console.WriteLine("---------------------------------------");
                    do
                    {
                        Console.Write("Card was invalid, would you like to enter a new one?(y/n):");
                        answer = Console.ReadLine();
                        answer = answer.ToLower().Substring(0, 1);
                        if (answer != "y" && answer != "n") Console.WriteLine("(please enter y or n)");
                    } while (answer != "y" && answer != "n");
                    if (answer == "y") return -3;
                }
                //if the card vendor was not valid it asks if they want to enter new one
            }
            else
            {
                Console.WriteLine("---------------------------------------");
                do
                {
                    Console.Write("Card was invalid, would you like to enter a new one?(y/n):");
                    answer = Console.ReadLine();
                    answer = answer.ToLower().Substring(0, 1);
                    if (answer != "y" && answer != "n") Console.WriteLine("(please enter y or n)");
                } while (answer != "y" && answer != "n");

                //if they enter yes then they go here
                if (answer == "y") return -3;

                //else you ask them if they wanna pay with cash instead
                else
                {
                    Console.WriteLine("---------------------------------------");
                    do
                    {
                        Console.Write("Would you like to pay with cash instead (y/n):");
                        answer = Console.ReadLine();
                        answer = answer.ToLower().Substring(0, 1);
                        if (answer != "y" && answer != "n") Console.WriteLine("(please enter y or n)");
                    } while (answer != "y" && answer != "n");

                    //if its yes then it sends them to the cash payment
                    if (answer == "y")
                    {
                        return -2;

                        //else it ends the program
                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            //if the card was invalid it asks if they want to pay with cash instead
            if (isGood == false)
            {
                do
                {
                    Console.Write("Would you like to pay with cash instead? (y/n):");
                    answer = Console.ReadLine();
                    answer = answer.ToLower().Substring(0, 1);
                    if (answer != "y" && answer != "n") Console.WriteLine("(Please enter 'y' or 'n':)");
                } while (answer != "y" && answer != "n");

                //if its yes then it sends them to the cash payment
                if (answer == "y") return -2;

                //else it ends the program
                else return -1;

                //else if the card didnt fail it asks if they want cash back
            }
            else
            {
                Console.WriteLine("---------------------------------------");
                do
                {
                    Console.Write("Do you want Cash back?(y/n)");
                    answer = Console.ReadLine();
                    answer = answer.ToLower().Substring(0, 1);
                    if (answer != "y" && answer != "n") Console.WriteLine("(please enter y or n)");
                } while (answer != "y" && answer != "n");
                //if yes it sends them to the cashback function
                if (answer == "y")
                {
                    cashBack = CashBackRequest.CashBack();
                    getCashBack = true;
                    //if cash back is greater than zero it adds it to totalCost
                    if (cashBack > 0)
                    {
                        newTotal += cashBack;
                        totalCost += newTotal;

                        //if the number returned is less than zero the bank does not have money to give cash back 
                    }
                    else if (cashBack < 0)
                    {
                        Console.WriteLine("This machine does not have the funds required to give cash back");
                        getCashBack = false;
                    }
                    //if they chose no cash back it prints this message
                }
                else
                {
                    Console.WriteLine("You chose no cash Back");
                    getCashBack = false;
                }

                //start checking the users bank to see if they have enough money to pay for it
                string[] availability = MoneyReq.MoneyRequest(creditCard, totalCost);
                string checkPayment = availability[1];

                numericCheck = decimal.TryParse(checkPayment, out numberCheck);

                numericCheck = true;
                numberCheck = totalCost;
                //check to see if the payment went through
                if (numericCheck == true)
                {
                    //if it went through congratulations
                    if (numberCheck == totalCost)
                    {
                        Console.WriteLine("---------------------------------------");
                        if (getCashBack == true) Console.WriteLine("Congrats your payment went through and {0:C} was dispensed to you", cashBack);
                        else Console.WriteLine("Congrats your payment went through");
                        Kiosk.cardAmount = totalCost + cashBack;
                        Kiosk.changeDue = cashBack;
                        return -1;
                        //else if your card only can only pay for part of it then you ask if they want to pay the rest with a new card
                    }
                    else
                    {
                        Console.WriteLine("---------------------------------------");
                        do
                        {
                            string toString = numberCheck.ToString();
                            int decimalCheck = 0;

                            //this loop checks to make sure you dont have more than two decimal places.
                            for (int index = 0; index < toString.Length; index++)
                            {
                                if (toString[index] == '.') decimalCheck = index;
                            }
                            toString = toString.Substring(0, decimalCheck + 3);

                            numberCheck = decimal.Parse(toString);

                            Console.Write("Your Card Could only pay {0:C} Would you like to enter a new card? ", numberCheck);
                            answer = Console.ReadLine();
                            answer = answer.ToLower().Substring(0, 1);
                            if (answer != "y" && answer != "n") Console.WriteLine("(please enter y or n)");
                        } while (answer != "y" && answer != "n");
                        // if yes it returns what they have left to pay
                        if (answer == "y") return numberCheck * -1;

                        //else it asks if they want to pay with cash
                        else
                        {
                            do
                            {
                                Console.Write("Would you like to pay with cash instead (y/n):");
                                answer = Console.ReadLine();
                                answer = answer.ToLower().Substring(0, 1);
                                if (answer != "y" && answer != "n") Console.WriteLine("(please enter y or n)");
                            } while (answer != "y" && answer != "n");

                            //if yes it returns the amount and goes to cash
                            if (answer == "y") return numberCheck;

                            //else it ends the program
                            else return -1;
                        }
                    }
                    //if your card was declined it will ask you if you want to try another cost
                }
                else
                {
                    Console.WriteLine("---------------------------------------");
                    do
                    {
                        Console.Write("your card was declined because you didnt have enough in your bank would you like to try another card?");
                        answer = Console.ReadLine();
                        answer = answer.ToLower().Substring(0, 1);
                        if (answer != "y" && answer != "n") Console.WriteLine("(please enter y or n)");
                    } while (answer != "y" && answer != "n");

                    //if yes it sends them back to the start of the project
                    if (answer == "y") return -3;

                    //else it asks if they wanna pay with cash instead
                    else
                    {
                        Console.WriteLine("---------------------------------------");
                        do
                        {
                            Console.Write("Would you like to pay with cash instead (y/n):");
                            answer = Console.ReadLine();
                            answer = answer.ToLower().Substring(0, 1);
                            if (answer != "y" && answer != "n") Console.WriteLine("(please enter y or n)");
                        } while (answer != "y" && answer != "n");

                        //if yes it sends them to the cash payment
                        if (answer == "y") return -2;

                        //else it ends the program
                        else return -1;
                    }
                }
            }
        }
        #endregion

    }
}
