using System.Transactions;

namespace Outguess___Addendum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Outguess();
            
        
        }//end main
        static void Outguess()
        {
            #region Finished OUTGUESS WITHOUT ADDENDUM
            Random r = new Random();
            bool run = true;
            int maxGuesses = 10;
            Random random = new Random();

            while (run)
            {
                //RANDOM NUMBER GUESS
                    int rnd = random.Next(1, 100);

                //BANNER & INSTRUCTIONS
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("__        __   _                            _        \r\n\\ \\      / /__| | ___ ___  _ __ ___   ___  | |_ ___  \r\n \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\ \r\n  \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |\r\n  _\\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|_ \\__\\___/ \r\n / _ \\ _   _| |_ __ _ _   _  ___  ___ ___| |         \r\n| | | | | | | __/ _` | | | |/ _ \\/ __/ __| |         \r\n| |_| | |_| | || (_| | |_| |  __/\\__ \\__ \\_|         \r\n \\___/ \\__,_|\\__\\__, |\\__,_|\\___||___/___(_)         \r\n                |___/                                ");
                    Console.ResetColor();
                    Console.WriteLine("Enter your bet. Select your wager and how many guesses (max 10) each round. \nYou cannot wager more than what you've brough to the table. \nThe less amount of guesses you use, the more your bet will increase. \nDON'T LOSE! ");

                //VARIABLES
                    int tries = 0;
                    bool solved = false;

                while (!solved && tries < maxGuesses)
                {
                    tries++;
                    int guess;
                    bool playerGuess;
                    //OTHER INPUT CONSOLE RESPONSE
                    do
                    {
                        playerGuess = int.TryParse(Console.ReadLine(), out guess);
                            if (!playerGuess)
                            {
                                Console.WriteLine("That's not a number. Try entering a number between 1-100.");
                                
                            } //END IF 0
                        } //END DO LOOP
                    while (!playerGuess);

                        if (guess > rnd) //PLAYER GUESSED A LOWER NUMBER
                        {
                            Console.WriteLine(string.Format("The number is lower than {0}. Try again!", guess));
                            Console.WriteLine($"You have {maxGuesses - tries} tries left. Please enter another number:");

                        } //END IF 1
                        else if (guess < rnd) //PLAYER GUESSED A HIGHER NUMBER
                        {
                            Console.WriteLine(string.Format("The number is higher than {0}. Try again!", guess));
                            Console.WriteLine($"You have {maxGuesses - tries} tries left. Please enter another number:");

                        } //END ELSE IF 1
                        else if (guess == rnd) //PLAYER  GUESSED THE CORRECT NUMBER 
                        {
                            solved = true;
                            Console.WriteLine(string.Format("Finally! Someone guessed right. The number was {0} and you used {1} tries", rnd, tries));
                        } //END ELSE IF 2
                    } //END WHILE 2
                    //END GAME CONSOLE OUTPUT
                    if (!solved)
                    {
                        Console.Clear();
                        Console.WriteLine(string.Format("You used all of your {0} tries. You suck! \n", maxGuesses));
                    } //END IF 2
                    //PROGRAM RE-RUN
                    Console.WriteLine("Do you wanna play again? Press Y + 'ENTER' to play again!\n Press 'ENTER' to end the game.");
                    string rerun = Console.ReadLine().ToUpper();

                    if (!rerun.Equals("Y"))
                    {
                        run = false;
                    } //END IF 3
                } //MAIN WHILE LOOP
                //PLAYER END GAME RESPONSE
                Console.Clear();
                Console.WriteLine("Shame you gave up, LOSER!");
                Console.ReadLine();



            static void OutguessAddendum()
            {
                //VARIABLES
                bool run = true;
                int bank = 0;
                int bet = 0;
                double percentage = 0.0;

                //HOW MUCH CASH THEY'RE BETTING (BANK)
                while (run)
                {
                    Console.WriteLine("How much are you bringing to the table?");
                }
                //INPUT VALIDATION LOOP


                //HOW MUCH MONEY FOR WAGER
                Console.WriteLine("How much do you want to bet?");
                //HOW MANY GUESSES
                Console.WriteLine("How many guesses do you want to use?");


                //WAGER X 10


                //FORFEIT WAGER


                //WIN PERCENTAGE
                Console.WriteLine(string.Format("Your win percentage is {0}", percentage));

                //LOST

            } //END OUTGUESS ADDENDUM FUNCTION

        } //END OUTGUESS MAIN FUNCTION 
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
