namespace TicTacToe_Original
{
        internal class Program
        {
            static void Main(string[] args)
            {
                char player1X = 'X';
                char player2O = 'O';
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
                Console.WriteLine($"Welcome to Tic-Tac-Toe {player1Name} & {player2Name}! Select a number (0-8) to mark the spot you choose.\n");


                //----------------------------------------------- CHECK WINNER,TURNS,DRAW -----------------------------------------------|
                /*checks for a winner or a draw, and alternates 
                * turns between two players until the game is won
                * drawn, or interrupted.*/

                while (true)
                {
                    placeMaker(gameBoard);

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
                            Console.WriteLine("TIE, such losers!");
                            return;
                        default:
                            break;
                    }//END SWITCH
                    if (turn % 2 == 0)
                    {
                        gameBoard = playerTurn(player1X, gameBoard);
                    } //END IF
                    else
                    {
                        gameBoard = playerTurn(player2O, gameBoard);
                    } //END ELSE
                    turn++;
                } //END WHILE

            } //END MAIN

        //----------------------------------------------- PLACE MARKER FUNCTION -----------------------------------------------|

        /*takes a char array gameBoard as input. Then it prints out the current 
         * state of a tic-tac-toe board using the values in the gameBoard array.
         * It displays the board layout with each cell labeled with a letter (a, b, c)
         * and its corresponding value from the gameBoard array.*/
        private static void placeMaker(char[] gameBoard)
            {
                /*uses values stored in gameBoard array to represent 
                * each "box" in the tic-tac-toe board*/
                Console.WriteLine("    a   b   c");
                string outputBoard = $"0 | {gameBoard[0]} | {gameBoard[1]} | {gameBoard[2]} |\n1 | {gameBoard[3]} | {gameBoard[4]} | {gameBoard[5]} |\n2 | {gameBoard[6]} | {gameBoard[7]} | {gameBoard[8]} | \n";
                Console.WriteLine(outputBoard);
            } //END PLACEMAKER FUNCTION

            //----------------------------------------------- PLAYER TURN FUNCTIONS -----------------------------------------------|

            /*defines playTurn and takes a player symbol and the current game 
             * board as input parameters. It prompts the player to enter a 
             * position on the game board until a valid turn is made(0-8).*/
            private static char[] playerTurn(char playerSymbol, char[] gameBoard)
            {
                //Find if the spot selected by the player is legal

                while (true)
                {
                    /*verifies user input is valid, if it is loop runs again and displays result to the board*/
                    int place = int.Parse(Console.ReadLine());
                    if (verifyPlayerTurn(place, gameBoard))
                    {
                        gameBoard[place] = playerSymbol;
                        break;
                    } //END IF STATEMENT

                } //END WHILE LOOP
                return gameBoard;
            } //END PLAYER TURN FUNCTION

            /* Checks if the input place is within the bounds of the gameBoard
             * array and if the position on the board is empty (marked with '-'). */
            private static bool verifyPlayerTurn(int place, char[] gameBoard)
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
                }//END IF STATEMENT
                 //Empty places in the board are marked as '-'
                if (gameBoard[place] == '-') { wrongTurn = true; }
                else
                {
                    Console.WriteLine("TRY AGAIN!");
                }//END ELSE STATEMENT
                return wrongTurn;
            } //END VERIFYTURN FUNCTION

            //-----------------------------------------------FUNCTIONS FOR WINNING CRITERIA -----------------------------------------------|
            private static int gameWon(char[] gameBoard)
            {
                bool isGameBoardFull = true;
                foreach (char elem in gameBoard)
                {
                    if (elem == '-') { isGameBoardFull = false; break; }
                } //END FOREACH LOOP
                if (isGameBoardFull) return 3;

                //Check win:
                if (rowCheck(gameBoard, 264) || colCheck(gameBoard, 264) || diagonalCheck(gameBoard, 264)) { return 1; }                                                                                                                   //Check for P2 Win:
                else if (rowCheck(gameBoard, 237) || colCheck(gameBoard, 237) || diagonalCheck(gameBoard, 237)) { return 2; }

                //X+X+X = 264
                //O+O+O = 237
                return 4;
            } //GAME WON FUNCTION

            /*THIS FUNCTION CHECKS IF THE BOARD HAS 3 CONSECUTIVE X-O'S, IF THERE
             ARE 3 CONSECUTIVE OF EITHER IN A DIAGONAL LINE, IT WILL RETURN THE DETECT WIN BOOL TRUE*/
            private static bool diagonalCheck(char[] gameBoard, int target)
            {
                bool winDiag = false;
                int sum1Diag = gameBoard[0] + gameBoard[4] + gameBoard[8];
                int sum2Diag = gameBoard[2] + gameBoard[4] + gameBoard[6];

                if (sum1Diag == target || sum2Diag == target) { winDiag = true; }
                return winDiag;
            } //DIAGONAL CHECK FUNCTION


            /*THIS FUNCTION CHECKS IF THE BOARD HAS 3 CONSECUTIVE X-O'S, IF THERE
             ARE 3 CONSECUTIVE OF EITHER IN A COLUMN, IT WILL RETURN THE DETECT WIN BOOL TRUE*/
            private static bool colCheck(char[] gameBoard, int target)
            {
                bool winCol = false;
                int sum1Row = gameBoard[0] + gameBoard[3] + gameBoard[6];
                int sum2Row = gameBoard[1] + gameBoard[4] + gameBoard[7];
                int sum3Row = gameBoard[2] + gameBoard[5] + gameBoard[8];


                if (sum1Row == target || sum2Row == target || sum3Row == target) { winCol = true; }
                return winCol;
            } //END COLUMN CHECK FUNCTION


            /*THIS FUNCTION CHECKS IF THE BOARD HAS 3 CONSECUTIVE X-O'S, IF THERE
             ARE 3 CONSECUTIVE OF EITHER IN A ROW, IT WILL RETURN THE DETECT WIN BOOL TRUE*/
            private static bool rowCheck(char[] gameBoard, int target)
            {
                bool winRow = false;
                int sum1Row = gameBoard[0] + gameBoard[1] + gameBoard[2];
                int sum2Row = gameBoard[3] + gameBoard[4] + gameBoard[5];
                int sum3Row = gameBoard[6] + gameBoard[7] + gameBoard[8];


                if (sum1Row == target || sum2Row == target || sum3Row == target) { winRow = true; }
                return winRow;
            } //END ROW CHECK FUNCTION

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