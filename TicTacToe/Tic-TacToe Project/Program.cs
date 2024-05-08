using System.Dynamic;
using System.Numerics;

namespace Tic_TacToe_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TicTacToeGame();
            //VARIABLES DECLARED 
            char player1Symbol = 'X';
            char player2Symbol = 'O';
            int turn = 0;
            char[] gameBoard = new char[9] { '-', '-', '-', '-', '-', '-', '-', '-', '-' }; //BOARD ARRAY
            string winner = "";
            string player1Name = "";
            string player2Name = "";
            //BANNER, INPUT NAME
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("__        _______ _     ____ ___  __  __ _____   _____ ___  \r\n\\ \\      / / ____| |   / ___/ _ \\|  \\/  | ____| |_   _/ _ \\ \r\n \\ \\ /\\ / /|  _| | |  | |  | | | | |\\/| |  _|     | || | | |\r\n  \\ V  V / | |___| |__| |__| |_| | |  | | |___    | || |_| |\r\n __\\_/\\_/_ |_____|_____\\____\\___/|_|  |_|_____|_  |_|_\\___/ \r\n|_   _|_ _/ ___|  |_   _|/ \\  / ___|  |_   _/ _ \\| ____|    \r\n  | |  | | |   _____| | / _ \\| |   _____| || | | |  _|      \r\n  | |  | | |__|_____| |/ ___ \\ |__|_____| || |_| | |___     \r\n  |_| |___\\____|    |_/_/   \\_\\____|    |_| \\___/|_____|    ");
            Console.WriteLine("p1(X) -- ENTER YOUR NAME:");
            player1Name = Console.ReadLine();
            Console.WriteLine("p2(O) -- ENTER YOUR NAME:");
            player2Name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("__        _______ _     ____ ___  __  __ _____   _____ ___  \r\n\\ \\      / / ____| |   / ___/ _ \\|  \\/  | ____| |_   _/ _ \\ \r\n \\ \\ /\\ / /|  _| | |  | |  | | | | |\\/| |  _|     | || | | |\r\n  \\ V  V / | |___| |__| |__| |_| | |  | | |___    | || |_| |\r\n __\\_/\\_/_ |_____|_____\\____\\___/|_|  |_|_____|_  |_|_\\___/ \r\n|_   _|_ _/ ___|  |_   _|/ \\  / ___|  |_   _/ _ \\| ____|    \r\n  | |  | | |   _____| | / _ \\| |   _____| || | | |  _|      \r\n  | |  | | |__|_____| |/ ___ \\ |__|_____| || |_| | |___     \r\n  |_| |___\\____|    |_/_/   \\_\\____|    |_| \\___/|_____|    ");
            Console.WriteLine($"Welcome to Tic-Tac-Toe {player1Name} & {player2Name}! Enter a number (0-8) to select the spot you choose.\n");
            /*checks for a winner or a draw, and alternates 
             * turns between two players until the game is won
             * drawn, or interrupted.*/
            while (true)
            {
                PlaceMarker(gameBoard);

                int checkWinner = gameWon(gameBoard);

                switch (checkWinner)
                {
                    case 1:
                        Console.WriteLine($"{player1Name} wins!");
                        return;

                    case 2:
                        Console.WriteLine($"{player2Name} wins!");
                        return;
                    case 3:
                        Console.WriteLine("Draw!");
                        return;
                    default:
                        break;
                } //END SWITCH
                /*This code checks if the value of the variable turn is even 
                 * (i.e., divisible by 2). If turn is even, it calls the playTurn
                 * function with the player1Symbol and gameBoard as arguments, 
                 * and assigns the result back to the gameBoard variable.*/
                if (turn % 2 == 0)
                {
                    gameBoard = playTurn(player1Symbol, gameBoard);
                    /*calls playTurn function and assigs the return
                     * value of the function to gameBoard.*/
                }

                /*runs when the condition in the if statement above
                 * is not met. It calls the function playTurn with player2Symbol
                 * and gameBoard as arguments*/
                else
                {
                    gameBoard = playTurn(player2Symbol, gameBoard);
                    /*calls playTurn function and assigs the return
                     * value of the function to gameBoard.*/
                }
                turn++; //INCREASES VAR TURN
            }//end while loop

            }//end main


            /* Displays array of characters representing the gameBoard as a 3x3 'grid'*/
            static void PlaceMarker(char[] gameBoard)
            {
                /*uses values stored in gameBoard array to represent 
                 * each "box" in the tic-tac-toe board*/
                string outputBoard = $"| {gameBoard[0]} | {gameBoard[1]} | {gameBoard[2]} |\n| " +
                    $"{gameBoard[3]} | {gameBoard[4]} | {gameBoard[5]} |\n| {gameBoard[6]} | {gameBoard[7]}" +
                    $" | {gameBoard[8]} | \n";
                Console.WriteLine(outputBoard);
            }

            /*defines playTurn and takes a player symbol and the current game 
             * board as input parameters. It prompts the player to enter a 
             * position on the game board until a valid turn is made(0-8).*/
            static char[] playTurn(char playerSymbol, char[] gameBoard)
            {
                //Find if the spot selected by the player is legal
                while (true)
                {
                    int place = int.Parse(Console.ReadLine());
                    //Console.Clear();
                    /*verifies user input is valid, if it is loop runs again and displays result to the board*/
                    if (verifyPlayerTurn(place, gameBoard))
                    {
                        gameBoard[place] = playerSymbol;
                        //break;//
                    }//END IF STATEMENT

                }//END WHILE LOOP
            return gameBoard;

            } //END TURN FUNCTION


            /* Checks if the input place is within the bounds of the gameBoard
            * array and if the position on the board is empty (marked with '-'). */
            static bool verifyPlayerTurn(int place, char[] gameBoard)
            {
                /* If the place is outside the board bounds or not empty, it outputs a
                * message to the console and returns false. Otherwise, it returns 
                * true indicating that the move is within 0-8.*/
                bool wrongTurn = false;
                //prevents user input from using #'s > 8 and < than 0
                if (place >= gameBoard.Length || place < 0)
                {
                    Console.WriteLine("CAN'T YOU COUNT? I SAID 0-8!");
                    return wrongTurn;
                } //END IF STATEMENT
                  //Empty places in the board are marked as '_'
                if (gameBoard[place] == '-') { wrongTurn = true; }
                else
                {
                    Console.WriteLine("TRY AGAIN!");
                } //END ELSE STATEMENT
                return wrongTurn;
            }//END LEGALTURN FUNCTION

            static int gameWon(char[] gameBoard)
            {
                bool isBoardFilled = true;
                foreach (char elem in gameBoard)
                {
                    if (elem == '-') { isBoardFilled = false; break; }
                } //FOR LOOP
                if (isBoardFilled) return 3;

                //Check for P1 Win:
                if (rowCheck(gameBoard, 264) || colCheck(gameBoard, 264) || diagonalCheck(gameBoard, 264)) { return 1; }//p1win!
                //Check for P2 Win:
                else if (rowCheck(gameBoard, 237) || colCheck(gameBoard, 237) || diagonalCheck(gameBoard, 237)) { return 2; } //p2win!

                //X+X+X = 264
                //O+O+O = 237
                return 4;
            } //GAME WON FUNCTION

            /*THIS FUNCTION CHECKS IF THE BOARD HAS 3 CONSECUTIVE X-O'S, IF THERE
             ARE 3 CONSECUTIVE OF EITHER IN A DIAGONAL LINE, IT WILL RETURN THE DETECT WIN BOOL TRUE*/
            static bool diagonalCheck(char[] gameBoard, int target)
            {
                bool detectedWin = false;
                int sum1Diag = gameBoard[0] + gameBoard[4] + gameBoard[8];
                int sum2Diag = gameBoard[2] + gameBoard[4] + gameBoard[6];

                if (sum1Diag == target || sum2Diag == target) { detectedWin = true; }
                return detectedWin;
            } //DIAGONAL CHECK

            /*THIS FUNCTION CHECKS IF THE BOARD HAS 3 CONSECUTIVE X-O'S, IF THERE
             ARE 3 CONSECUTIVE OF EITHER IN A COLUMN, IT WILL RETURN THE DETECT WIN BOOL TRUE*/
            static bool colCheck(char[] gameBoard, int target)
            {
                bool detectedWin = false;
                int sum1Row = gameBoard[0] + gameBoard[3] + gameBoard[6];
                int sum2Row = gameBoard[1] + gameBoard[4] + gameBoard[7];
                int sum3Row = gameBoard[2] + gameBoard[5] + gameBoard[8];


                if (sum1Row == target || sum2Row == target || sum3Row == target) { detectedWin = true; }
                return detectedWin;
            }//COLUMN CHECK

            /*THIS FUNCTION CHECKS IF THE BOARD HAS 3 CONSECUTIVE X-O'S, IF THERE
             ARE 3 CONSECUTIVE OF EITHER IN A ROW, IT WILL RETURN THE DETECT WIN BOOL TRUE*/
            static bool rowCheck(char[] gameBoard, int target)
            {
                bool detectedWin = false;
                int sum1Row = gameBoard[0] + gameBoard[1] + gameBoard[2];
                int sum2Row = gameBoard[3] + gameBoard[4] + gameBoard[5];
                int sum3Row = gameBoard[6] + gameBoard[7] + gameBoard[8];


                if (sum1Row == target || sum2Row == target || sum3Row == target) { detectedWin = true; }
                return detectedWin;
            }//END ROW CHECK FUNCTION


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
    }//END CLASS
}//END NAMESPACE
