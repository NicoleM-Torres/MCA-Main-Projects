using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kiosk_Final
{
    internal class Kiosk
    {
        #region FIELDS
        public static int transactionNum = 1; //TRANSACTION # FOR RECEIPT
        public static string date = ""; //DATE FOR RECEIPT
        public static decimal cashAmount = 0;//
        public static string cardVendor = "*NO CARD VENDOR*";//
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

            //LOOPS THROUGH ELEMENTS IN 'BANK' ARRAY AND SETS VALUE TO 5
            for (int index = 0; index < bank.Length; index++)
            {
                bank[index] = 5;
            }

            DateTime dateTime = DateTime.Now; // GETS THE CURRENT DATE AND TIME
            date = dateTime.ToString("MMM-dd-yyyy,HH:mm"); // DATE & TIME FORMAT -- "MMM-dd-yyyy,HH:mm"


            while (true)
            {
                Console.WriteLine("Welcome to NHS Self-Help Kiosk");
                Console.WriteLine("***************************************");
                Console.WriteLine("Please start by entering in the prices for the items.");
                Console.WriteLine("Press enter in a empty space when you are finished scanning the products.");
                Console.WriteLine("***************************************");

                //CALL THE METHOD TO GET TOTAL COST
                decimal totalCost = InputValidation();

                Console.WriteLine("***************************************");
                Console.WriteLine("Your purchase total is ${0}", totalCost);
                Console.WriteLine("***************************************");

                //ASKS USER IF THEY WOULD LIKE TO PAY WITH CASH OR CARD
                do
                {
                    Console.WriteLine("***************************************");
                    customerChoice = Prompt("Will you be paying with Cash or Card?");
                    customerChoice = customerChoice.ToLower();
                    if (customerChoice != "cash" && customerChoice != "card") Console.WriteLine("Enter the word cash or card to choose payment method.");
                } while (customerChoice != "cash" && customerChoice != "card");


                //IF THE USER CHOOSES CASH --
                if (customerChoice == "cash")
                {
                    CashPayments.CashPay(totalCost); // Call the CashPay method from CashPayments class with totalCost as parameter
                    answer = -1;
                }

                do
                {
                    //IF USER CHOOSES CARD --
                    if (customerChoice == "card")
                    {
                        answer = CardPayments.CardPay(totalCost);
                        if (answer == -3)
                        {
                            answer = CardPayments.CardPay(totalCost);
                        }
                        else if (answer == -2)
                        {
                            CashPayments.CashPay(totalCost);
                        }
                        else if (answer == -1)
                        {
                            Console.WriteLine("Thank you for choosing a NHS Self-Help Kiosk, your reciept will display in a few seconds.");
                        }
                        else if (answer < -3)
                        {
                            answer = CardPayments.CardPay(answer * -1);
                        }
                        else if (answer > 0)
                        {
                            CashPayments.CashPay(answer);
                        }
                    }
                } while (answer != -1);
                Console.WriteLine("***************************************");
                Console.WriteLine();
                RecieptLog.transactionLogging();
                transactionNum++;
            }
            #endregion   

        }//end main

        #region Input Validation
        static decimal InputValidation
            ()
        {
            #region Input Validation Variables

            string userResponse = " ";
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
                Console.Write("Item {0} cost:   $", count);
                userResponse = Console.ReadLine();
                counter = 0;

                for (int index = 0; index < userResponse.Length; index++)
                {
                    if (userResponse[index] == '.')
                    {
                        for (int incrementer = index + 1; incrementer < userResponse.Length; incrementer++)
                        {
                            counter++;
                        }
                    }
                }

                if (counter > 2) checker = false;
                else checker = true;

                //WILL CHECK IF THE USERINPUT IS A VALID NUMBER, IF IT IS, IT WILL BE ASSIGNED TO THE 'NUMBERCHECK' VARIABLE
                realNumCheck = decimal.TryParse(userResponse, out numberCheck);


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

            } while (userResponse != "");

            return totalCost;
        }
        #endregion

        #region Receipt
        internal class RecieptLog
        {

            public static void transactionLogging()
            {
                string vendor = Kiosk.cardVendor.Replace(' ', '`');
                string arguments = Kiosk.transactionNum.ToString() + "," + Kiosk.date + ",$" + Kiosk.cashAmount.ToString() + "," + vendor + ",$" + Kiosk.cardAmount.ToString() + ",$" + Kiosk.changeDue.ToString();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "Kiosk Final.exe";
                startInfo.Arguments = arguments;
                Process.Start(startInfo);


            } //END TRANSACTION LOG METHOD


        } //END RECIEPT LOG CLASS
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
    }//end class
}//end namespace
