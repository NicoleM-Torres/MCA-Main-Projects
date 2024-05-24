using System;
using System.Diagnostics;
using System.Drawing;

namespace Kiosk_Project_1
{
    class Kiosk
    {
        #region FIELDS
        public static int transactionNum = 1; //TRANSACTION # FOR RECEIPT
        public static string date = ""; //DATE FOR RECEIPT
        public static decimal cashAmount = 0;//
        public static string cardVendor = "no card vendor";//
        public static decimal cardAmount = 0;//
        public static decimal changeDue = 0;
        public static int[] bank = new int[12];
        public static decimal[] moneyValues = new decimal[] { 100, 50, 20, 10, 5, 2, 1, .50m, .25m, .10m, .05m, .01m };
        public static int[] userMoney = new int[12];
        #endregion
        static void Main(string[] args)
        {
            
            #region CUSTOMER QUESTIONS

            string customerChoice = "";
            decimal answer = 0;

            for (int index = 0; index < bank.Length; index++)
            {
                bank[index] = 5;
            }

            DateTime dateTime = DateTime.Now;
            date = dateTime.ToString("MMM-dd-yyyy,HH:mm");

            while (true)
            {
                Console.WriteLine("Welcome to NHS Self-Help Kiosk");
                Console.WriteLine("***************************************");
                Console.WriteLine("Please start by entering in the prices for the items");
                Console.WriteLine("Press enter in a empty space when you are finishes scanning the products)");
                Console.WriteLine("***************************************");

                //call the function to get the total cost
                decimal totalCost = InputValidation();

                Console.WriteLine("***************************************");
                Console.WriteLine("The total cost of the items is {0}", totalCost);
                Console.WriteLine("***************************************");

                //ask if the user would like to use cash or card
                do
                {
                    Console.WriteLine("***************************************");
                    customerChoice = Prompt("Will you be paying with Cash or Card?");
                    customerChoice = customerChoice.ToLower();
                    if (customerChoice != "cash" && customerChoice != "card") Console.WriteLine("Enter the word cash or card to choose payment method.");
                } while (customerChoice != "cash" && customerChoice != "card");


                //if the user choose cash then the program goes here
                if (customerChoice == "cash")
                {
                    CashPayCheck.CashPay(totalCost);
                    answer = -1;
                }

                do
                {
                    //if they choose card the code brings them here
                    if (customerChoice == "card")
                    {
                        answer = CardPayCheck.CardPay(totalCost);
                        if (answer == -3)
                        {
                            answer = CardPayCheck.CardPay(totalCost);
                        }
                        else if (answer == -2)
                        {
                            CashPayCheck.CashPay(totalCost);
                        }
                        else if (answer == -1)
                        {
                            Console.WriteLine("Thank you for choosing a NHS Self-Help Kiosk, your reciept will display in a few seconds.");
                        }
                        else if (answer < -3)
                        {
                            answer = CardPayCheck.CardPay(answer * -1);
                        }
                        else if (answer > 0)
                        {
                            CashPayCheck.CashPay(answer);
                        }
                    }
                } while (answer != -1);
                Console.WriteLine("***************************************");
                Console.WriteLine();
                RecieptLog.transactionLogging();
                transactionNum++;
            }
        }
            #endregion
        
        
    #region Input Validation
        static decimal InputValidation
            ()
        {
            #region Input Validation Variables

            string userInput = " ";
            decimal totalCost = 0;
            int count = 1;
            bool realNumCheck = true;
            decimal numberCheck = 0;
            int counter = 0;
            bool checker = true;
            #endregion

            //INPUT ITEMS $$
            do
            {
                //IF THEY ENTER ANYTHING OTHER THAN A NUMBER, THE PROGRAM WILL REQUEST THEY ENTER A 'VALID AMOUNT'
                if (realNumCheck == false) Console.WriteLine("(Enter a valid amount.)");

                //REQUEST USER INPUT FOR COST ITEM               
                Console.Write("Please enter the cost for item {0}:   $", count);
                Console.ForegroundColor = ConsoleColor.Cyan;
                
                userInput = Console.ReadLine();
                counter = 0;

                for (int index = 0; index < userInput.Length; index++)
                {
                    if (userInput[index] == '.')
                    {
                        for (int incrementer = index + 1; incrementer < userInput.Length; incrementer++)
                        {
                            counter++;
                        }
                    }
                }

                if (counter > 2) checker = false;
                else checker = true;

                //WILL CHECK IF THE USERINPUT IS A VALID NUMBER, IF IT IS, IT WILL BE ASSIGNED TO THE 'NUMBERCHECK' VARIABLE
                realNumCheck = decimal.TryParse(userInput, out numberCheck);


                //CHECK NUMBER TO ENSURE IS NOT A NEGATIVE VALUE, IF IT'S A VALID NUMBER THE AMOUNT WILL BE ADDED TO THE VARIABLE 'TOTALCOST'
                if (realNumCheck == true && numberCheck > 0 && checker == true)
                {
                    count++;
                    totalCost += numberCheck;
                }

                //IF THE PRICE OF ITEM HAS TWO OR MORE DECIMAL PLACES IT WILL PROMPT USER TO RE-ENTER THE PRICE
                else if (checker == false) Console.WriteLine("(You cant have more than two decimal places)");

                //IF THE NUMBER IS NOT ABOVE 0, THE USER WILL BE P[ROMPTED TO RE-ENTER
                else if (numberCheck < 0) Console.WriteLine("(Please enter a number above 0)");

            } while (userInput != "");

            return totalCost;
        }
        #endregion  


    #region PROMPT FUNCTIONS
            static string Prompt(string dataRequest)
        {
            //CREATE VARIABLE TO STORE THE USER RESPONSE
            string userResponse = "";

            //WRITE THE REQUEST TO THE SCREEN FOR USER TO READ
            Console.WriteLine(dataRequest);

            //RECEIVE BACK USER RESPONSE AND STORE INTO VARIABLE
            userResponse = Console.ReadLine();

            //RETURN THE REQUESTED DATA BACK TO THE CALLING CODE-BLOCK
            return userResponse;
        }//end function

        static int PromptInt(string dataRequest)
        {
            //CREATE VARIABLE TO STORE THE USER RESPONSE
            int userResponse = 0;

            //REQUEST AND RECEIVE BACK USER RESPONSE AND STORE INTO VARIABLE
            userResponse = int.Parse(Prompt(dataRequest));

            //RETURN THE REQUESTED DATA BACK TO THE CALLING CODE-BLOCK
            return userResponse;
        }//end function

        static double PromptDouble(string dataRequest)
        {
            //CREATE VARIABLE TO STORE THE USER RESPONSE
            double userResponse = 0;

            //REQUEST AND RECEIVE BACK USER RESPONSE AND STORE INTO VARIABLE
            userResponse = double.Parse(Prompt(dataRequest));

            //RETURN THE REQUESTED DATA BACK TO THE CALLING CODE-BLOCK
            return userResponse;
        }//end function

        #endregion

    } //END CLASS
} //END NAMESPACE
