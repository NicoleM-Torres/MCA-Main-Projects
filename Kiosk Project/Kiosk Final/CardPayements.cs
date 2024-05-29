using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk_Final
{
    #region CardPayments
    internal class CardPayments
    {

        public static decimal CardPay(decimal totalCost)
        {
            //this tells
            Console.WriteLine("***************************************");
            Console.WriteLine("We only accept Visa, Mastercard, Discover, and American Express");
            string creditCard = "";
            bool isGood = true;
            string answer = "";
            decimal cashBack = 0;
            decimal newTotal = 0;
            bool getCashBack = true;


            bool bankHasMoney = true;
            decimal numberCheck = 0;

            //PROMPTS USER TO ENTER CC NUMBER -- IF THE NUMBER IS NOT 15-16 DIGITS IN LENGH THE PROGRAM WILL PRINT ERROR MESSAGE
            
            do
            {
                //creditCard = prompt("Enter a card number:");
                Console.WriteLine("Enter a card number:");
                creditCard = Console.ReadLine();
                if (creditCard.Length != 16)
                    Console.WriteLine("(ERROR: Please enter a valid card number)");
            } while (creditCard.Length != 16);
            /*creditCard = prompt("Enter a card number:");                
                if (creditCard.Length < 15 || creditCard.Length > 16)
                    Console.WriteLine("(ERROR: Please enter a valid card number)");
            } while (creditCard.Length < 15 || creditCard.Length > 16);*/
                

            //CHECKS FOR MACHING CC COMPANY
            string vendor = CCVendorCheck.ValidateVendor(creditCard);
            Kiosk.cardVendor = vendor;

            //IF IT'S MATCHED TO A VALID CC COMPANY IT WILL BE CLEARED AS VALID
            if (vendor != "ERROR: Invalid Card -- TRY AGAIN")
            {
                isGood = CardCheckVal.ValidateCard(creditCard);

                //IF THE CARD WAS INVALID, USER WILL BE PROMTER TO RE-ENTER
                if (isGood == false)
                {
                    Console.WriteLine("***************************************");
                    do
                    {
                        Console.Write("Card was invalid, would you like to enter a new one? (y/n):");
                        answer = Console.ReadLine();
                        answer = answer.ToLower().Substring(0, 1);
                        if (answer != "y" && answer != "n") Console.WriteLine("(Enter 'y' or 'n')");
                    } while (answer != "y" && answer != "n");
                    if (answer == "y") return -3;
                }
                //IF THE CC VENDOR/NUM WAS INVALID, THEY WILL BE ASKED IF THEY WANT TO ENTER A NEW CC NUMBER
            }
            else
            {
                Console.WriteLine("***************************************");
                do
                {
                    Console.Write("ERROR: Card was invalid, would you like to enter a new one?(y/n):");
                    answer = Console.ReadLine();
                    answer = answer.ToLower().Substring(0, 1);
                    if (answer != "y" && answer != "n") Console.WriteLine("(please enter y or n)");
                } while (answer != "y" && answer != "n");

                //IT 'YES', THEY WILL BE PROPTED AGAIN TO ENTER A CC #
                if (answer == "y") return -3;

                //IF 'NO', THE USER WILL BE ASKED IF THEY WISH TO PAY CASH INSTEAD 
                else
                {
                    Console.WriteLine("***************************************");
                    do
                    {
                        Console.Write("Would you like to pay with cash instead (y/n):");
                        answer = Console.ReadLine();
                        answer = answer.ToLower().Substring(0, 1);
                        if (answer != "y" && answer != "n") Console.WriteLine("(Enter 'y' or 'n')");
                    } while (answer != "y" && answer != "n");

                    // IF USER INPUT IS 'YES' TO CASHPAYMENT, THE CASH PAYMENT METHOD WILL START 
                    if (answer == "y")
                    {
                        return -2;

                        //ENDS PROGRAM
                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            //IF CARD WAS INVALID, THEY WILL BE PROMPED FOR CASHPAYMENT OPTION
            if (isGood == false)
            {
                do
                {
                    Console.Write("Would you like to pay with cash instead? (y/n):");
                    answer = Console.ReadLine();
                    answer = answer.ToLower().Substring(0, 1);
                    if (answer != "y" && answer != "n") Console.WriteLine("(Enter 'y' or 'n':)");
                } while (answer != "y" && answer != "n");

                //IF 'YES', THEY WILL BE REDIRECTED TO CASH PAYMENT
                if (answer == "y") return -2;

                //ENDS PROGRAM
                else return -1;

                //IF CARD WAS VALID THEY WILL BE PROPMTER FOR CASHBACK
            }
            else
            {
                Console.WriteLine("***************************************");
                do
                {
                    Console.Write("Do you want Cash back? (y/n)");
                    answer = Console.ReadLine();
                    answer = answer.ToLower().Substring(0, 1);
                    if (answer != "y" && answer != "n") Console.WriteLine("(Enter 'y' or 'n')");
                } while (answer != "y" && answer != "n");
                //IF 'YES' USER WILL BE RETURNED TO CASHBACK METHOD
                if (answer == "y")
                {
                    cashBack = CashBackRequest.CashBack();
                    getCashBack = true;
                    //IF CASH BACK IS < 0, IT WILL BE ADDED TO 'totalCost'
                    if (cashBack > 0)
                    {
                        newTotal += cashBack;
                        totalCost += newTotal;

                        //IF # RETURNED IS < 0, THE BANK DOES NOT HAVE MONEY FOR CASH BACK
                    }
                    else if (cashBack < 0)
                    {
                        Console.WriteLine("We're sorry! :( \nThe NHS Kiosk does not have sufficient funds to dispense the cashback requested.");
                        getCashBack = false;
                    }
                    //IF THEY CHOOSE 'NO' CASHBACK, THE MSG WILL BE PRINTED TO THE CONSOLE
                }
                else
                {
                    Console.WriteLine("NO CASHBACK");
                    getCashBack = false;
                }

                //STARTS CHECKING THE USERS BANK BALANCE TO WITHDRAW PAYMENT
                /*string[] availability = MoneyReq.MoneyRequest(creditCard, totalCost);
                string checkPayment = availability[1];
                bankHasMoney = decimal.TryParse(checkPayment, out numberCheck); */

                bankHasMoney = true;
                numberCheck = totalCost;

                //CHECKS IF PAYMENT WAS SUCESSFUL
                if (bankHasMoney == true)
                {

                    //IF PAYMENT WAS SUCESSFUL
                    if (numberCheck == totalCost)
                    {
                        Console.WriteLine("***************************************");
                        if (getCashBack == true) Console.WriteLine("Your payment was APPROVED and {0:C} cashback was dispensed.", cashBack);
                        else Console.WriteLine("Your payment was APPROVED.");
                        Kiosk.cardAmount = totalCost + cashBack;
                        Kiosk.changeDue = cashBack;
                        return -1;
                        //IF USER CARD CAN ONLY PAY FOR A %, THEY WILL BE PROMPTED FOR CASH PAYMENT OR NEW CARD
                    }

                    else
                    {

                        Console.WriteLine("***************************************");
                        do
                        {
                            string toString = numberCheck.ToString();
                            int decimalCheck = 0;

                            //USERINPUT CHECK TO ENSURE THERE ARE NO MORE THAN 2 DECIMAL PLACES
                            for (int index = 0; index < toString.Length; index++)
                            {
                                if (toString[index] == '.') decimalCheck = index;
                            }
                            toString = toString.Substring(0, decimalCheck + 3);

                            numberCheck = decimal.Parse(toString);

                            Console.Write("Your card vendor only approved {0:C} towards your amount due. Would you like to enter a new card?(y/n) ", numberCheck);
                            answer = Console.ReadLine();
                            answer = answer.ToLower().Substring(0, 1);
                            if (answer != "y" && answer != "n") Console.WriteLine("(Enter 'y' or 'n')");
                        } while (answer != "y" && answer != "n");
                        //IF 'YES' IT RETURNS LEFT OVER AMOUNT 
                        if (answer == "y") return numberCheck * -1;

                        // ELSE ASKS USER IF THEY WANT TO PAY IN CASH
                        else
                        {
                            do
                            {
                                Console.Write("Would you like to pay with cash instead? (y/n):");
                                answer = Console.ReadLine();
                                answer = answer.ToLower().Substring(0, 1);
                                if (answer != "y" && answer != "n") Console.WriteLine("(Enter 'y' or 'n')");
                            } while (answer != "y" && answer != "n");

                            //IF YES, RETURNS AMOUNT AND IT GOES TO CASH
                            if (answer == "y") return numberCheck;

                            //ELSE IF ENDS THE PROGRAM
                            else return -1;
                        }
                    }
                    //IF CARD WAS DECLINES IT WILL ASK IF YOU WANT TO PURCHASE ANOTHER ITEM INSTEAD?
                }
                else
                {
                    Console.WriteLine("***************************************");
                    do
                    {
                        Console.Write("Card DECLINED -- Insufficient funds.\n Would you like to try another card?");
                        answer = Console.ReadLine();
                        answer = answer.ToLower().Substring(0, 1);
                        if (answer != "y" && answer != "n") Console.WriteLine("(Enter 'y' or 'n')");
                    } while (answer != "y" && answer != "n");

                    //if yes it sends them back to the start of the project
                    //IF 'YES', IT RE-STARTS PROGRAM AGAIN
                    if (answer == "y") return -3;

                    //ELSE -- ASKS USER IF THEY WANT TO PAY CASH 
                    else
                    {
                        Console.WriteLine("***************************************");
                        do
                        {
                            Console.Write("Would you like to pay with cash instead (y/n):");
                            answer = Console.ReadLine();
                            answer = answer.ToLower().Substring(0, 1);
                            if (answer != "y" && answer != "n") Console.WriteLine("(Enter 'y' or 'n')");
                        } while (answer != "y" && answer != "n");

                        //IF 'YES' -- SENDS USER TO CASH PAYMENT METHOD
                        if (answer == "y") return -2;

                        //ELSE ENDS PROGRAM
                        else return -1;
                    }
                }
            }
        }
    }
    #endregion

    #region CardValidation
    internal class CardCheckVal
    {
        public static bool ValidateCard(string creditCard)
        {
            //VARIABLES -- 
            int[] array = new int[creditCard.Length];
            bool shouldDouble = true;
            char[] word = creditCard.ToCharArray();
            int sum = 0;
            int doubled = 0;
            string number = "";

            //STORES CC NUMBER INTO ARRAY
            for (int index = 0; index < creditCard.Length; index++)
            {
                number = word[index].ToString();
                array[index] = int.Parse(number);
            }

            //DOUBLES EVERY OTHER NUM STARTING FROM RIGHT
            for (int index = creditCard.Length - 2; index >= 0; index--)
            {
                if (shouldDouble)
                {
                    doubled = array[index] * 2;
                    if (doubled >= 10) sum += doubled - 9;
                    else sum += doubled;
                }
                else sum += array[index];
                shouldDouble = !shouldDouble;
            }
            //RETURNS LAST NUMBER OF THE CREDIT CARD NUMBER
            sum += array[15];

            //IF SUM US NOT EVENLY DIVISIBLE BY TEN THEN IT RETURNS TRUE OR FALSE
            return (sum % 10 == 0);
        }
    }
    #endregion

    #region Cash back
    internal class CashBackRequest
    {
        public static decimal CashBack()
        {
            string cashBack = "";
            decimal counter = 0;
            decimal numberCheck = 0;
            bool checker = true;
            bool numericCheck = true;

            //USERINPUT HOW MUCH CASH BACK THEY WANT TO RETRIEVE
            do
            {
                Console.Write("Enter cashback amount: $");
                cashBack = Console.ReadLine();

                counter = 0;

                //INPUT VALIDATION --CHECK FOR ONE DECIMAL POINT AND 2 PLACES AFTER THE DECIMAL
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

                //CHECKS USERINPUT IS A NUMBER AND WILL SORE TO 'NUMBERCHECK' VARIABLE
                numericCheck = decimal.TryParse(cashBack, out numberCheck);

                //IF IT HAS MORE THAT TWO DECIMAL PLACES PROMPTS USER TO RE-ENTER AMOUNT
                if (checker == false) Console.WriteLine("(You cant have more than two decimal places)");

                //IF IT IS NOT A NUMBER > 0, PROMPTS USER TO RE-ENTER AMOUNT
                else if (numberCheck < 0) Console.WriteLine("(Please enter a number above 0)");
                //IF NOT AN ACUTAL #, WILL PROMPT USER TO RE-ENTER AMOUNT
                else if (numericCheck == false) Console.WriteLine("(Please enter a number)");

            } while (checker == false || numberCheck < 0 || numericCheck == false);
            //CHECKS IF THERE IS ENOUGH MONEY IN THE BANK
            bool check = Bank.CheckBankCash(numberCheck, true);

            //RETURNS 'CHECK BANK CASH' & RETURNS VALUE
            if (check == true) return numberCheck;
            else return -1;
        } //END CASHBACK METHOD
    } //END CASHBACK REQUEST CLASS
    #endregion

    #region Check CC -- Mastercard-Visa-AE- Discover
    internal class CCVendorCheck
    {
        //IDENTIFYING THE CARD VENDOR
        public static string ValidateVendor(string creditCard)
        {
            //VISA CC
            if (creditCard[0] == '4') return "Visa";
            //MASTERCARD CC
            else if (creditCard[0] == '5' && creditCard[1] == '1') return "Mastercard";
            else if (creditCard[0] == '5' && creditCard[1] == '2') return "Mastercard";
            else if (creditCard[0] == '5' && creditCard[1] == '3') return "Mastercard";
            else if (creditCard[0] == '5' && creditCard[1] == '4') return "Mastercard";
            else if (creditCard[0] == '5' && creditCard[1] == '5') return "Mastercard";
            //DISCOVER CC
            else if (creditCard[0] == '6' && creditCard[1] == '0' && creditCard[2] == '1' && creditCard[3] == '1') return "Discover";
            else if (creditCard[0] == '6' && creditCard[1] == '4' && creditCard[2] == '4') return "Discover";
            else if (creditCard[0] == '6' && creditCard[1] == '5') return "Discover";
            //AMERICAN EXPRESS CC
            else if (creditCard[0] == '3' && creditCard[1] == '4') return "American Express";
            else if (creditCard[0] == '3' && creditCard[1] == '7') return "American Express";
            //CARD NOT VALID
            else return "CARD NOT VALID";
        } //END VALIDATE CC METHOD
    }//END CC VENDOR CLASS  
    #endregion

    
}
