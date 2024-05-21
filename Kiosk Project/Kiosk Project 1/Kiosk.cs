using System.Diagnostics;

namespace Kiosk_Project_1
{
    class Kiosk
    {
        #region FIELDS
        public static int transactionNum = 1;//
        public static string date = ""; //
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
                    if (customerChoice != "cash" && customerChoice != "card") Console.WriteLine("Please enter the word cash or card");
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
                            Console.WriteLine("Thank you for choosing a NHS Self-Help kiosk, your reciept will display in a few seconds.");
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
                Console.WriteLine("---------------------------------------");
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
            #region variables
            string userInput = " ";
            decimal totalcost = 0;
            int count = 1;
            bool numericCheck = true;
            decimal numbercheck = 0;
            int counter = 0;
            bool checker = true;
            #endregion

            //start a loop to input your items
            do
            {
                //if what they enter is not a number this will tell them to re-enter it
                if (numericCheck == false) Console.WriteLine("(Please enter a number)");

                //this Is where they input there number
                Console.Write("Please enter the cost for item {0}:   $", count);
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

                //this checks that it is actually a number and if it is it will put into the numbercheck variable
                numericCheck = decimal.TryParse(userInput, out numbercheck);



                //if it is a number it and it is not a negative number then put it into the total cost
                if (numericCheck == true && numbercheck > 0 && checker == true)
                {
                    count++;
                    totalcost += numbercheck;
                }

                // if it has more than two decimal places then it tells them to re-enter a number
                else if (checker == false) Console.WriteLine("(You cant have more than two decimal places)");

                //if it is not a number above 0 it will tell the user to re-enter a number
                else if (numbercheck < 0) Console.WriteLine("(Please enter a number above 0)");

            } while (userInput != "");

            return totalcost;
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
