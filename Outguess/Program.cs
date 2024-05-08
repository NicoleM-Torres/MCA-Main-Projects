using System.ComponentModel.Design;

namespace Outguess
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Outguess();

        }//end main
            static void Outguess()
        {
            Random r = new Random();
            int val = r.Next(1, 100);
            int guess = 0;
            int chance = 7;
            int tries = 0;
            bool correct = false;
    
                while (!correct)
                {
                    Console.WriteLine("__        __   _                            _        \r\n\\ \\      / /__| | ___ ___  _ __ ___   ___  | |_ ___  \r\n \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\ \r\n  \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |\r\n  _\\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|_ \\__\\___/ \r\n / _ \\ _   _| |_ __ _ _   _  ___  ___ ___| |         \r\n| | | | | | | __/ _` | | | |/ _ \\/ __/ __| |         \r\n| |_| | |_| | || (_| | |_| |  __/\\__ \\__ \\_|         \r\n \\___/ \\__,_|\\__\\__, |\\__,_|\\___||___/___(_)         \r\n                |___/                                ");
                    Console.WriteLine("I've chosen a number between 1-100, guess it!");

                    tries--;
                        while (tries < chance)
                        {

                            Console.Write("Guess: ");
                            string input = Console.ReadLine();

                        if (!int.TryParse(input, out guess))
                        {
                            Console.WriteLine("That's not a number. Try entering a number between 1-100.");
                            continue;
                        }

                        if (guess < val)
                        {
                            Console.WriteLine($"Sorry! the number you entered is too low.");
                            Console.WriteLine($"You have {chance - guess} tries left. Please enter another number:");
                    }
                        else if (guess > val)
                        {
                            Console.WriteLine($"Sorry! the number you entered is too high.");
                            Console.WriteLine($"You have {chance - guess} tries left. Please enter another number:");

                    }
                        else
                        {
                            correct = true;
                            Console.WriteLine("You guessed right! The correct answer is " + val);
                           
                        }

                }  if (tries == chance){
                    Console.WriteLine($"You lose! The correct number was {val}");

                }
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
    }//end class
}//end namespace
