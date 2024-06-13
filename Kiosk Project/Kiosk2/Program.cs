using System.Diagnostics;

namespace Kiosk2
{
        class Program
        {
            //Declaring our Global Bank Struct
            //BANK STRUCT HOLDS INFO ABOUT CURRENCY DENOMINATIONS AND AMOUNTS
            public struct Bank
            {
                public int[] CurrencyAmount;
                public string[] CurrencyName;
                public decimal[] CurrencyValue;
                public int[] UserPayment;
            }
            //End of Global Bank Struct

            //Creating a new struct for Transaction
            // TRANSACTION STRUCT TRACKS INDIVIDUAL TRANSACTIONS
            public struct Transaction
            {
                public int transactionNum;
                public string transactionDate;
                public decimal cashAmount;
                public string cardVendor;
                public decimal cardAmount;
                public decimal changeGiven;
            }

            public static Transaction trans;

            static void Main(string[] args)
            {
                //card test value = 4452720412345678   Visa
                //card test value = 3792720412345679   American Express
                //card test value = 6552720412365878   Discover
                //card test value = 5152720412345678   MasterCard

                //Declaring variables and instance of Bank struct
                decimal changeOwed = 0;
                Bank kiosk;

                DateTime dtmTime;
                dtmTime = DateTime.Now;

                //Declaring arrays
                /*The CurrencyName array holds the names of different currency.
                  The CurrencyAmount array stores the quantity of each currency.
                  The CurrencyValue array contains the monetary value of each currency.
                  The UserPayment array tracks the user's payment in each currency.*/
                kiosk.CurrencyName = new string[] { "Hundreds", "Fifties", "Twenties", "Tens", "Fives", "Twos",
                                                "Ones", "Half Dollar", "Quarters", "Dimes", "Nickels", "Pennies" };
                kiosk.CurrencyAmount = new int[12];
                kiosk.CurrencyValue = new decimal[] { 100, 50, 20, 10, 5, 2, 1, .50m, .25m, .10m, .05m, .01m };
                kiosk.UserPayment = new int[12];

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
                for (int index = 0; index < kiosk.CurrencyAmount.Length; index++)
                {
                    kiosk.CurrencyAmount[index] = 5;
                }

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

                    }
                    /*method being called, records the details of the current transaction*/
                    Logging();
                    /*increments the transactionNum property of a trans object by 1.
                     * It tracks the number of transactions processed by the kiosk*/
                    trans.transactionNum += 1;

                    //Call Kiosk Transaction Reset Function to reset the kiosk for the next transaction
                    KioskTransactionReset(kiosk);
                }
            }//End of Main

            #region InputOfItemPrices
            static decimal InputItemPrices() //Start of Input Item Prices
            {
                //Declaring variables
                string inputItem = ""; //user input item in transaction
                int itemCount = 1; //The number of the item being input.
                decimal total = 0; //represents total amount per item
                bool realAmount = true; //checks value for validation
                int checkDecimal = 0;

                //Do while loop for input validation
                /*Reads user input for item prices and calculates the total amount.*/
                do
                {

                    //User enters an item price
                    /*Prompts the user to input an item with a corresponding price.    
                      User enters an item price and returns the user input for the 'inputItem' variable*/
                    Console.Write("Item {0}:\t$", itemCount);
                    inputItem = Console.ReadLine();

                    //Check to make sure the input does not have more than 2 decimal points
                    /*Counts the number of characters after '.' in the input string 'inputItem'
                     and returns the total count of characters after each dot in the input string*/
                    checkDecimal = 0; //sets 'checkDecimal' to 0
                    for (int index = 0; index < inputItem.Length; index++)
                    {
                        if (inputItem[index] == '.')
                        {
                            for (int count = index + 1; count < inputItem.Length; count++)
                            {
                                checkDecimal++;
                            }
                        }
                    }

                    //setting varibale to T/F depending if the value is real or not
                    /*Determines the value of 'realAmount' based on the value of the int 'checkDecimal'.
                     *uses 'checkDecimal' value to determine if 'realAmount has more than 2 decimal places.
                     *'realAmount' will reflect true or false based on checkDecimal.*/
                    if (checkDecimal > 2) realAmount = false;
                    else realAmount = true;


                    //Checking to see if the user inputed a real price that is not negative
                    /*Validates the 'inputItem' as a decimal, checks if it is greater than 0,
                     * and increments the total and item count if conditions are met. 
                     * If the input is not a valid decimal (realAmount -- if it has 2 ot more decimal places)
                     * and not an empty string, it prompts the user to enter a valid amount.*/
                    if (decimal.TryParse(inputItem, out _) && decimal.Parse(inputItem) > 0 && realAmount == true)
                    {
                        //adding the item price to the total price
                        total = total + decimal.Parse(inputItem);
                        itemCount++;
                    }
                    //Checks if the input item is not an empty string and prints a message if it is.
                    else if (inputItem != "") Console.WriteLine("Enter a valid amount.");

                    //Continues a loop until the input item is an empty string.
                } while (inputItem != "");

                //Returning the total price
                return total;

            }//End of Input Item Prices
            #endregion

            #region CashPayment
            //Processes a cash payment transaction with the bank.Returns remaining balance after the cash payment.
            static decimal CashPayment(Bank bank, decimal total) //bank - contains currency info // 'total' - amount to be paid 
            {
                //Declaring Variables
                int paymentCount = 1; //payment number
                string payment = ""; //stores payment amount
                bool validPayment = false; //checks 'payment' amount is valid

                //Print msg to screen 
                Console.WriteLine("Insert cash payment now.");
                Console.WriteLine();

                //Do while loop for validation
                do
                {
                    //Prompts the user to enter a payment amount.
                    Console.Write("Payment {0}:\t$", paymentCount);
                    payment = Console.ReadLine();


                    //Validating the payment input to make sure it's a positive number
                    /*Checks if the payment is a valid positive decimal value.
                     *True if the payment is a valid positive decimal value, false otherwise. */
                    if (decimal.TryParse(payment, out _) && decimal.Parse(payment) > 0)
                    {

                        //loop through the array of values to test if the payment is a real bill or coin
                        for (int index = 0; index < bank.CurrencyValue.Length; index++)
                        {
                            //checking if the payment is a valid bill/coin
                            /*Checks if a payment matches the currency value and updates the bank accordingly.
                             *returns true if the payment is valid and the bank is updated, false otherwise*/
                            if (decimal.Parse(payment) == bank.CurrencyValue[index])
                            {
                                validPayment = true;
                                bank.UserPayment[index]++;
                                bank.CurrencyAmount[index]++;
                                break;
                            }
                            else if (decimal.Parse(payment) < 0.01m && decimal.Parse(payment) > 0)
                            {
                                validPayment = true;
                                bank.UserPayment[11]++;
                                bank.CurrencyAmount[11]++;
                                break;
                            }
                            else validPayment = false;
                        }
                    }

                    //if the payment is valid then it is subtracted from the
                    /*Process a valid payment. Updates the cash amount, total amount,
                     * and payment count based on the valid payment.*/
                    if (validPayment == true) //boolean indicating if the payment is valid
                    {
                        trans.cashAmount += decimal.Parse(payment); //Updates the cash amount in a transaction by adding the parsed payment amount.
                        total = total - decimal.Parse(payment); //Subtracts a payment amount from the total.
                        paymentCount++;//ncrement the payment count by one
                    }

                    //Tells the user their payment is invalid
                    else Console.WriteLine("Enter a a valid US currency amount.");

                    //Displays the remaining value if there is any
                    if (total > 0) Console.WriteLine("Remaining:\t{0:C}", total);
                    Console.WriteLine();

                } while (total > 0); // Executes a block of code repeatedly as long as a total is > than 0.

                //returns what change needs to be provided
                return total * -1; //Returns the negation of the input value.

            }//end of Cash Payment
            #endregion

            #region CheckBank
            //Checks if a bank has enough funds to cover a given amount of change owed.
            static bool CheckBank(decimal changeOwed, Bank bank) //Start of Check Bank
            {
                /*Declaring Variable and Array. Initialize an array with 12 
                 * elements and assign the value of changeOwed to the variable owed.*/
                decimal owed = changeOwed;
                int[] array = new int[12];


                //Copies currency amounts from a bank to an array.
                for (int index = 0; index < bank.CurrencyAmount.Length; index++)
                {
                    array[index] = bank.CurrencyAmount[index];
                }

                //Loop to see if the bank has enough money for the transaction
                int newIndex = 0;
                while (owed > 0)
                {
                    /*Update the array and owed amount based on the currency value and index.
                     *if statement that checks which bills can be used and if that bill is available */
                    if (owed >= bank.CurrencyValue[newIndex] && array[newIndex] > 0)
                    {
                        array[newIndex]--; //Decrements the value at the specified index in the array by 1.
                        owed -= bank.CurrencyValue[newIndex]; //Subtracts a specified currency value from the owed amount.
                        newIndex = 0; // Set the newIndex variable to 0.

                    }
                    /*If the kiosk does not have enough change
                     *if 'owed' is > 0 & array (and index in array) = 0.
                     *returns true if the condition is met, false otherwise.*/
                    else if (owed > 0 && array[newIndex] == 0)
                    {
                        //Prints msg to console
                        Console.WriteLine("The Kiosk does not have enough cash to dispense your change. ");
                        Console.WriteLine();

                        /*Loop to refund the user's money
                         *Dispenses currency based on user payments.
                         *method iterates through the 'userPayments' in the 'bank' and dispenses currency.
                         *It decrements the user payment count, currency amount, and resets the iteration 'count' when currency is dispensed.*/
                        for (int count = 0; count < bank.UserPayment.Length; count++)
                        {
                            if (bank.UserPayment[count] > 0)
                            {
                                //Money is refunded and taken back out of the bank
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("{0:C} dispensed", bank.CurrencyValue[count]);
                                Console.ForegroundColor = ConsoleColor.White;
                                bank.UserPayment[count]--;
                                bank.CurrencyAmount[count]--;
                                count = 0;
                            }
                        }
                        Console.WriteLine("");

                        /*Validation loop for paying with a card
                         *Prompts the user to choose a payment method.</ summary >
                        * true if the user chooses to pay using a card, false otherwise.*/
                        string response = "";
                        do
                        {
                            Console.Write("Would you like to pay using a card? (y/n) ");
                            response = Console.ReadLine();
                            /* Checks if the first character of a given response is neither 'y' nor 'n'.
                             *True if the first character of the response is neither 'y' nor 'n'; otherwise, false and prints ERROR to console.*/
                            if (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n")
                            {
                                Console.WriteLine("Enter valid response: (y/n) ");
                            }
                            //Repeatedly prompts the user for input until a valid response is received.
                        } while (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n");

                        //If paying with a card, run the UsingCard function
                        if (response == "y")
                        {
                            UsingCard(bank, owed);
                        }
                        break;
                    }
                    //Increment the value of newIndex by 1.
                    newIndex++;
                }

                //If the amount owed is still greater than zero then function returns false
                if (owed > 0) return false;

                //return true if the bank has enough money
                return true;
            }//End of Check Bank
            #endregion

            #region DispenseChange
            /*Dispenses change from the bank based on the specified amount owed.
             *If the application can pay the change owed, this method dispenses the change using the available bank currency.
             *It iterates through the bank's currency values and amounts to calculate and dispense the change. */
            static void DispenseChange(decimal changeOwed, Bank bank, bool canPay) //Start of Dispense Change
            {
                //Will only run if the bank has enough money to pay
                if (canPay == true)
                {
                    //Loop that will run until the change has been dispensed
                    int index = 0;
                    trans.changeGiven = changeOwed;
                    while (changeOwed > 0)
                    {
                        //Checks to see if the bank has enough cash to dispense change
                        if (changeOwed >= bank.CurrencyValue[index] && bank.CurrencyAmount[index] > 0)
                        {
                            //Dispenses specific currency (bills and coins)
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("{0:C} dispensed", bank.CurrencyValue[index]);
                            Console.ForegroundColor = ConsoleColor.White;
                            //Takes bill/coin out of the bank
                            bank.CurrencyAmount[index]--;
                            //Subtracts amount from change owed
                            changeOwed -= bank.CurrencyValue[index];
                            //starts back at the top of the list of bills/coins
                            index = 0;
                        }
                        index++;
                    }
                }
            }//End of Dispense Change
            #endregion

            #region CardMerchantCheck
            static string CardMerchantCheck(string cardNum) //Start of Card Name Check
            {
                char[] chrArray = cardNum.ToCharArray();

                //Identify the name of the card vendor
                if ((chrArray[0] == '3' && chrArray[1] == '4') || (chrArray[0] == '3' && chrArray[1] == '7'))
                    return "American Express";
                else if ((chrArray[0] == '6' && chrArray[1] == '0') || (chrArray[0] == '6' && chrArray[1] == '5'))
                    return "Discover";
                else if ((chrArray[0] == '5' && ((int)chrArray[1] < '6' && (int)chrArray[1] > '0')))
                    return "MasterCard";
                else if (chrArray[0] == '4')
                    return "Visa";
                else return "We only accept Visa, Mastercard, Discover, and American Express";
            } //End of Card Name Check
            #endregion

            #region LuhnCheck
            static bool LuhnCheck(string cardNum) //Start of Luhn Check
            {
                //Declaring variables
                int[] array = new int[cardNum.Length];
                bool isDouble = true;
                char[] chrArray = cardNum.ToCharArray();
                int sum = 0;
                int doubled = 0;
                string arrayConvert;

                //Loop that takes the char array and converts to an int array
                for (int index = 0; index < cardNum.Length; index++)
                {
                    arrayConvert = chrArray[index].ToString();

                    array[index] = int.Parse(arrayConvert);
                }

                /*
                Loop to run Luhn Algorithm. Starting from the rightmost digit,
                every other digit is doubled. If a doubled digit is > 9
                then 9 is subtracted from the doubled digit and then added to the sum. 
                The digits that were not doubled are immediately added to the sum.
                If the sum is evenly divisible by 10 then the card has passed the Luhn Algorithm.
                */
                for (int index = cardNum.Length - 2; index >= 0; index--)
                {
                    //Checks and runs on every other number
                    if (isDouble)
                    {
                        //doubles
                        doubled = array[index] * 2;

                        //if > 9, 9 is subtracted then number is added to sum
                        if (doubled > 9)
                        {
                            sum += doubled - 9;
                        }
                        //if < 9 number is added to sum
                        else sum += doubled;
                    }
                    //if not a number that gets doubled it is added to sum
                    else sum += array[index];

                    //changes to false on every other number
                    isDouble = !isDouble;
                }

                //adding the far right number to the sum
                sum += array[15];

                //if eveny divisible by 10, returns true. if not returns false
                return (sum % 10 == 0);

            } //End of Luhn Check
            #endregion

            #region CashBack
            static void CashBack(Bank bank) //Start of CashBack 
            {
                //Declaring Variables
                string response = "";
                int checker = 0;
                bool realAmount = false;

                //Validation loop for receive cashback option
                do
                {
                    Console.Write("Do you want cash back? (y/n) ");
                    response = Console.ReadLine();

                    if (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n")
                    {
                        Console.WriteLine("Enter valid response: (y/n) ");
                    }
                } while (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n");

                //If they want cashback
                if (response.ToLower().Substring(0, 1) == "y")
                {
                    bool validAmount = false;
                    bool canPay = false;

                    //Validation loop for valid amount of cashback
                    do
                    {
                        //Message to User
                        Console.Write("Enter cashback amount: $");
                        string amount = Console.ReadLine();

                        checker = 0;

                        //Loop to make sure there are no more than 2 decimal places
                        for (int index = 0; index < amount.Length; index++)
                        {
                            if (amount[index] == '.')
                            {

                                for (int count = index + 1; count < amount.Length; count++)
                                {
                                    checker++;
                                }
                            }
                        }

                        if (checker > 2) realAmount = false;
                        else realAmount = true;


                        //Checking to see if the user inputed a real number that is not negative
                        if (decimal.TryParse(amount, out _) && decimal.Parse(amount) > 0 && realAmount)
                        {
                            //Seeing if the bank has enough money to dispense cashback 
                            canPay = CheckCashBack(decimal.Parse(amount), bank);
                            Console.WriteLine();
                            Console.WriteLine("-----------------------------------------------");

                            Console.WriteLine();

                            //attempting to dispense cashback
                            DispenseChange(decimal.Parse(amount), bank, canPay);

                            validAmount = true;
                        }
                        else Console.WriteLine("Enter a valid amount.");

                    } while (validAmount == false || canPay == false);
                }
            } //End of CashBack 
            #endregion

            #region CheckCashBack
            static bool CheckCashBack(decimal amount, Bank bank) //Start of Check Cash Back
            {
                //Declaring Variables
                decimal owed = amount;
                int[] array = new int[12];

                //Filling the new array with the amount of bills and coins
                for (int index = 0; index < bank.CurrencyAmount.Length; index++)
                {
                    array[index] = bank.CurrencyAmount[index];
                }

                //Loop to see if the bank has enough money for the transaction
                for (int index = 0; owed > 0; index++)
                {
                    if (owed >= bank.CurrencyValue[index] && array[index] > 0)
                    {
                        array[index]--;
                        owed -= bank.CurrencyValue[index];
                        index = 0;
                    }
                    //If the amount owed is still greater than 0 and the bank does not have enough funds this will run
                    else if (owed > 0 && array[index] == 0)
                    {
                        Console.WriteLine("We're sorry! :( \nThe NHS Kiosk does not have sufficient funds to dispense the cashback requested.");
                        break;
                    }
                }

                //If the amount owed is still > 0 the function returns false
                if (owed > 0) return false;

                //return true if the bank has enough money
                return true;

            }//End of Check Cash Back
            #endregion

            #region MoneyRequest
            static string[] MoneyRequest(string account_number, decimal amount) //Start of Money Request
            {
                Random rnd = new Random();
                //50% CHANCE TRANSACTION PASSES OR FAILS
                bool pass = rnd.Next(100) < 50;
                //50% CHANCE THAT A FAILED TRANSACTION IS DECLINED
                bool declined = rnd.Next(100) < 50;
                if (pass)
                {
                    return new string[] { account_number, amount.ToString() };
                }
                else
                {
                    if (!declined)
                    {
                        return new string[] { account_number, (amount / rnd.Next(2, 6)).ToString() };
                    }
                    else
                    {
                        return new string[] { account_number, "declined" };
                    }
                }
            }//End of Money Request
            #endregion

            #region UsingCard
            static decimal UsingCard(Bank bank, decimal amount) //Start of Using Card
            {
                //Declaring Variables
                string response = "";
                string cardNum = "";

                //Validation loop for using a card
                do
                {
                    Console.Write("Would you like to pay using a card? (y/n) ");
                    response = Console.ReadLine();
                    //Substring is set to only check first character of userInput. If character is 'y' it returns yes. if 'n' it returns 'no'.
                    if (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n")
                    {
                        Console.WriteLine("Please enter (y/n): ");
                    }
                } while (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n");

                //If using a card
                if (response.ToLower().Substring(0, 1) == "y")
                {
                    //Validation loop for entering card number
                    do
                    {
                        Console.WriteLine();
                        Console.Write("Enter your card number: ");
                        cardNum = Console.ReadLine();

                        //Simulate card read error
                        while (cardNum.Length != 16)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Card was invalid, try again.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Enter your card number: ");
                            cardNum = Console.ReadLine();
                        }
                        Console.WriteLine();

                        //Checking the Card Vendor Name
                        string cardName = CardMerchantCheck(cardNum);
                        trans.cardVendor = cardName;

                        //Tells the user what kind of card it is
                        Console.WriteLine("You are using a {0}", cardName);

                        //Validating the vendor name is an accepted vendor
                        if (cardName == "Visa" || cardName == "Discover" || cardName == "American Express" || cardName == "MasterCard")
                        {
                            //Running the luhn check on the card
                            bool validCard = LuhnCheck(cardNum);

                            //Card passed Luhn Check
                            if (validCard)
                            {
                                //Running money request function
                                string[] cardPayment = MoneyRequest(cardNum, amount);

                                //If card was NOT declined
                                if (cardPayment[1] != "declined")
                                {
                                    //Subtracts the payment from the total
                                    amount -= decimal.Parse(cardPayment[1]);
                                    trans.cardAmount += decimal.Parse(cardPayment[1]);

                                    //If total is still greater than 0
                                    if (amount > 0)
                                    {
                                        //Message to User
                                        Console.WriteLine("Your card vendor only approved a partial amount towards your total cost.");
                                        Console.WriteLine("{0:C} Remaining to pay", amount);
                                        Console.WriteLine("---------------------------------------");

                                        //Validating asking the user to use another card
                                        do
                                        {
                                            Console.Write("Would you like to try another card? (y/n) ");
                                            response = Console.ReadLine();

                                            if (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n")
                                            {
                                                Console.WriteLine("Enter valid response: (y/n) ");
                                            }
                                        } while (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n");

                                        //Stops the UsingCard function if user answer is not yes
                                        if (response.ToLower().Substring(0, 1) != "y") break;
                                    }
                                }
                                //Card was declined
                                else
                                {
                                    Console.WriteLine("Card DECLINED -- Insufficient funds.");
                                    Console.WriteLine("---------------------------------------");
                                    //Validating asking the user to use another card
                                    do
                                    {
                                        Console.Write("Would you like to try another card? (y/n) ");
                                        response = Console.ReadLine();

                                        if (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n")
                                        {
                                            Console.WriteLine("Enter valid response: (y/n) ");
                                        }
                                    } while (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n");

                                    //Stops the UsingCard function if user answer is not yes
                                    if (response.ToLower().Substring(0, 1) != "y") break;
                                }
                            }
                            //Card did NOT pass Luhn Check
                            else
                            {
                                Console.WriteLine("ERROR: Please enter a valid card number.");
                                Console.WriteLine();
                                //Validating asking the user to use another card
                                do
                                {
                                    Console.Write("Would you like to try another card? (y/n) ");
                                    response = Console.ReadLine();

                                    if (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n")
                                    {
                                        Console.WriteLine("Enter valid response: (y/n) ");
                                    }

                                } while (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n");

                                //Stops the UsingCard function if user answer is not yes
                                if (response.ToLower().Substring(0, 1) != "y") break;
                            }
                        }

                        //The card did not pass the card name check
                        else
                        {
                            Console.WriteLine("ERROR: Please enter a valid card number.");
                            Console.WriteLine("---------------------------------------");
                            //Validating asking the user to use another card
                            do
                            {
                                Console.Write("Would you like to try another card? (y/n) ");
                                response = Console.ReadLine();

                                if (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n")
                                {
                                    Console.WriteLine("Enter valid response: (y/n) ");
                                }
                            } while (response.ToLower().Substring(0, 1) != "y" && response.ToLower().Substring(0, 1) != "n");

                            //Stops the UsingCard function if user answer is not yes
                            if (response.ToLower().Substring(0, 1) != "y") break;
                        }
                        //Loop ends naturally when total is less than 0
                    } while (amount > 0);
                }

                //If the full amount was paid
                if (amount == 0)
                {
                    Console.WriteLine("Your payment was APPROVED.");
                    Console.WriteLine("---------------------------------------");

                    //Running Cash Back Function
                    CashBack(bank);
                }

                //Returns total even if there is nothing left to pay
                return amount;

            }//End of Using Card
            #endregion

            #region TransactionReset
            static void KioskTransactionReset(Bank kiosk) //Start of Transaction Reset
            {
                Console.WriteLine();

                //Display bank funds after transaction
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Amount of each type of currency currently in the kiosk");
                Console.WriteLine();
                for (int index = 0; index < kiosk.CurrencyAmount.Length; index++)
                {
                    Console.WriteLine(kiosk.CurrencyName[index] + " " + kiosk.CurrencyAmount[index]);
                }
                Console.ForegroundColor = ConsoleColor.White;

                //Reset kiosk for the next customer
                for (int i = 0; i < kiosk.UserPayment.Length; i++)
                {
                    //Reset values stored for bills/coins the user entered to zero
                    kiosk.UserPayment[i] = 0;
                }
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();


                DateTime dtmTime;
                dtmTime = DateTime.Now;

                trans.transactionDate = dtmTime.ToString("MMM-dd-yyyy,HH:mm");
                trans.cashAmount = 0;
                trans.cardVendor = "No Vendor Given";
                trans.cardAmount = 0;
                trans.changeGiven = 0;

            }//End of Transaction Reset
        #endregion

            #region Transaction Log
        public static void Logging()
        {
            string vendor = trans.cardVendor.Replace(' ', '_');
            string output = trans.transactionNum.ToString() + "," + trans.transactionDate + ",$" + trans.cashAmount.ToString()
            + "," + vendor + ",$" + trans.cardAmount + ",$" + trans.changeGiven.ToString();
            var dateOnly = DateTime.Now.ToString("MM-dd-yyyy");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"\C:\\Users\\nicol\\Documents\\GitHub\\MCA-Main-Projects\\Kiosk Project\\TransactionLoggingPackage2\\bin\\Debug\\net8.0\\TransactionLoggingPackage2.exe\"; // The name of the executable to run
            startInfo.Arguments = output; // Arguments to pass to the executable
                                          //startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            Process.Start(startInfo);// Starts the process

        }

#endregion


    } //End of Class 

} //End of Program