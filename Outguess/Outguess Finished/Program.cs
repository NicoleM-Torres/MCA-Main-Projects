using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using System.Transactions;

namespace Outguess_Finished
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Outguess_Finished();
        }//end main

        static void Outguess_Finished()
        {
            //VARIABLES
            int guesses; //user input
            int bank; //user input 
            bool win;
            int guessLimit = 10;
            int winningAmt; //winning cash
            int rndNumber; //saved rnd generated # value
            int playerNum = 0; //user input
            int winsTracker = 0; // tracks wins
            double winPercent; //
            int wager; //user input value
            bool gameOver = false;
            int guessTries = 0; //tracks amount of tries
            int gameNum = 1; //tracks num of rounds

            //BANNER & INSTRUCTIONS
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("__        __   _                            _        \r\n\\ \\      / /__| | ___ ___  _ __ ___   ___  | |_ ___  \r\n \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\ \r\n  \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |\r\n  _\\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|_ \\__\\___/ \r\n / _ \\ _   _| |_ __ _ _   _  ___  ___ ___| |         \r\n| | | | | | | __/ _` | | | |/ _ \\/ __/ __| |         \r\n| |_| | |_| | || (_| | |_| |  __/\\__ \\__ \\_|         \r\n \\___/ \\__,_|\\__\\__, |\\__,_|\\___||___/___(_)         \r\n                |___/                                ");
            Console.ResetColor();
            Console.WriteLine("****STEPS**** \n 1. Enter the amount on money you want to use for your round. \n 2. Select how many guesses (max 10) you want to use for the round.\n 3. Enter your wager. \n 4. Start guessing (#'s between 1 & 100). \n ****WARNING**** \nYou cannot wager more than what you've brough to the table. \nThe less amount of guesses you use, the more your bet will increase. \nDON'T LOSE!");
            
            do //DO-While userinput=true && the remaining balance in the bank is greater than 0
            {
                //DO-WHILE LOOP -- PREVENTS USER FROM ENTERING MORE THAN 1000 OR LESS THAN 1 TO THEIR BANK
                do
                {

                    Console.Write("Enter the amount you're bringing to the table: $");
                    bank = int.Parse( Console.ReadLine()); //stores user input to bank variable
                   

                    if (bank < 1 || bank > 999 )
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Bet entered must be a valid amount.");
                        Console.ResetColor();
                        Console.WriteLine("-------------");
                    }

                    

                } while (bank < 1 || bank > 999);

                //RND NUMBER GENERATOR .next generates random num between 1 & 100 and assigns it to the variable rndNumber
                Random rnd_maker = new Random();
                rndNumber = rnd_maker.Next(1, 100); 

                // DOWHILE ENTER WAGER AMOUNT
                do
                {
                    Console.Write("Enter your wager amount: $");
                    wager = int.Parse(Console.ReadLine()); //stores user input to wager variable

                    //IF WAGER IS GREATER THAN BANK OR LESSER= TO 1 RUN LOOP
                    if (wager > bank || wager < 1)
                    {  
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Your wager is either higher or lower than your current balance. Try again.");
                        Console.WriteLine("-------------");
                        Console.ResetColor();
                        Console.Write("Enter wager amount: $");
                    }

                } while (wager > bank || wager < 1);

                //NUMBER OF GUESSES -- PLAYER INPUT 
                // DOWHILE runs while guesses are greater than guess limit OR guesses are less than 1
                do
                { 
                    Console.Write("How many guesses do you want to use? (10 max): ");
                    guesses = int.Parse(Console.ReadLine());
                    
                    //GUESSES WAGERED VALIDATION LOOP
                    if (guesses > guessLimit || guesses < 1) //ERROR CODE if user enters an int greater than 10 or less than 1
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Please enter a valid number of guesses (1-10).");
                        Console.WriteLine("-------------");
                        Console.ResetColor();
                    }

                } while (guesses > guessLimit || guesses < 1);

                //TURNS LOOP
                while (guesses > 0 && playerNum != rndNumber) //while guesses are remaining and the player# does NOT equal random #
                {
                    do //DOWHILE player# is less than 1 ot greater than 100
                    {

                        Console.WriteLine();
                        Console.WriteLine("What's your guess?");
                        playerNum = int.Parse(Console.ReadLine()); //saves user input into the variable
                        gameNum++;
                        guessTries ++; //increments the value of  variable

                        if (playerNum == 0 || playerNum >100) //IF player# is less than 1 ot greater than 100
                        {                            
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Please enter a number greater than 0 or less than 100.Total guesses remaining: {0}", guesses - 1);
                            Console.ResetColor();                           
                            Console.WriteLine("-------------");
                            Console.ResetColor();
                            gameNum++;
                            guesses -= 1; //decreses value of 'guesses' variable
                            //guessTries++;
                        }
                    //PLAYER GUESS VALIDATION
                    } while (playerNum < 1 || playerNum > 100); //loop will run as long as player inputs a # between 1 & 100

                    if (playerNum == rndNumber) //if player# equals random# -- CONGRATS Message
                    {
                        win = true; //sets bool to true for wagerMultiplier
                        winsTracker ++; //increments variable value                       
                        gameNum += 1; //increments gameNum variable
                        winningAmt = wager * Multiplier(guessTries, win);
                        bank += winningAmt; // adds the value of winningAmt variable to bank variable                                                                 
                        //FORMULA FOR WINNINGS (alculates the winning amount by multiplying the wager by the number of guesses made
                        //returns the multiplier based on the number of guesses
                        //winningAmt = wager * Multiplier();                                             
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Finally! Someone guessed right!");
                        Console.ResetColor();                        
                        Console.WriteLine("You won {0:C} this round.", winningAmt);                        
                        Console.WriteLine("You have {0:C} remaining. Want to play again? Press Y for YES or N for NO.", bank);//{0:C} displays variable value in currency 
                        gameOver = Console.ReadKey(true).KeyChar.ToString().ToLower() == "n"; //bool gameOver is set to true if userinput is 'n'
                        //gameOver = Console.ReadKey(false).KeyChar.ToString().ToLower() == "y";
                        Console.WriteLine("-------------");
                        

                    } else if (playerNum < rndNumber) // ELSE IF -- Player# guessed was low
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("The number guessed was too LOW.Try again. \nTotal guesses remaining: {0}", guesses - 1);
                        Console.ResetColor();
                        gameNum += 1;
                        guesses -= 1; //decreses variable value
                        //guessTries ++; //adds 1 to variable
                    } else if (playerNum > rndNumber) //ELSE IF -- Player# guessed was too high
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("The number guessed was too HIGH. Try again. \nTotal guesses remaining: {0}", guesses - 1);
                        Console.ResetColor();
                        gameNum++;
                        guesses -= 1; //decreses variable value
                        //guessTries ++; //adds 1 to variable
                    }
                    //LOSSES---------------------------------------------------------------------------------------
                    //If player# does not equal rnd# & there are no guesses left over & bank balance is greater than 0
                    //playerNum != rndNumber && 
                    if (guesses == 0 || bank == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("You're POOR now! What a waste of money... The correct number was {0}. ", rndNumber);
                        Console.WriteLine("Your wager is forfeit and the round is over.");
                        //bank--;
                        Console.ResetColor() ;
                        Console.WriteLine("Want to play again? Press Y for YES or N for NO.");
                        gameOver = Console.ReadKey(true).KeyChar.ToString().ToLower() == "n";//bool gameOver is set to true if userinput is 'n'
                        //gameOver = Console.ReadKey(false).KeyChar.ToString().ToLower() == "y";
                        Console.WriteLine("-------------");                    
                        
                    } //END IF             

                } //END WHILE                    

                //PLAYER ENDS GAME -- if GameOver bool = true
                if (gameOver == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("You give up? LOSER!");
                    Console.ResetColor();
                    //WINNINGS PERCENTAGE
                    winPercent = (double)(winsTracker / gameNum * 100);
                    Console.WriteLine("Your win percentage: {0}%", winPercent);
                    Console.WriteLine("Your balance is {0:C}", bank); //displays variable in currency
                    Console.WriteLine("See you next pay day!");
                }
                //PLAYER CONTINUES GAME -- if gameOver bool = false
                if (gameOver == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Wooohoooo, let's play again!!");
                    Console.ResetColor();
                }
                        
                static int Multiplier(int guessTries, bool win)
                                        { //checks the value of tries and the value of win, and returns a specific integer value based on those conditions
                                            if (guessTries == 10 && win == true) return 1; //if used 10 guesses and won the retun value is 1
                                            if (guessTries == 9 && win == true) return 2;
                                            if (guessTries == 8 && win == true) return 3;
                                            if (guessTries == 7 && win == true) return 4;
                                            if (guessTries == 6 && win == true) return 5;
                                            if (guessTries == 5 && win == true) return 6;
                                            if (guessTries == 4 && win == true) return 7;
                                            if (guessTries == 3 && win == true) return 8;
                                            if (guessTries == 2 && win == true) return 9;
                                            if (guessTries == 1 && win == true) return 10;
                                            else return 0;
                                        }
            } while (gameOver == false  && bank > 0);  
            
     
        }//END Outguess function

        //WAGER INCREASE FORMULA
        //returns the multiplier based on the number of guesses.
        /*static int Multiplier(int number)
        {

            int multiplier = 0;

            multiplier = 11 - number;

            return multiplier;
        }*/               

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
