using System.Data;

namespace TicTacToe___Finished
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ticTacToeGame();

        } //END MAIN
        static void ticTacToeGame()
        { 

            //DECLARED VARIABLES
            char player1X = 'X';
            char player2O = 'O';
            bool playAgain = false;
            int turn = 0;
            bool gameOver = false;
            char[,] gameBoard = {
            //col  0    1    2
                { '*', '*', '*' }, //0 row
                { '*', '*', '*' }, //1 row
                { '*', '*', '*' } }; //2 row
            string winner = "";
            string player1Name = "";
            string player2Name = "";

            //BANNER, INPUT NAME
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("__        _______ _     ____ ___  __  __ _____   _____ ___  \r\n\\ \\      / / ____| |   / ___/ _ \\|  \\/  | ____| |_   _/ _ \\ \r\n \\ \\ /\\ / /|  _| | |  | |  | | | | |\\/| |  _|     | || | | |\r\n  \\ V  V / | |___| |__| |__| |_| | |  | | |___    | || |_| |\r\n __\\_/\\_/_ |_____|_____\\____\\___/|_|  |_|_____|_  |_|_\\___/ \r\n|_   _|_ _/ ___|  |_   _|/ \\  / ___|  |_   _/ _ \\| ____|    \r\n  | |  | | |   _____| | / _ \\| |   _____| || | | |  _|      \r\n  | |  | | |__|_____| |/ ___ \\ |__|_____| || |_| | |___     \r\n  |_| |___\\____|    |_/_/   \\_\\____|    |_| \\___/|_____|    ");
            Console.WriteLine("Player 'X' -- ENTER YOUR NAME:");
            player1Name = Console.ReadLine();
            Console.WriteLine("Player 'O' -- ENTER YOUR NAME:");
            player2Name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("__        _______ _     ____ ___  __  __ _____   _____ ___  \r\n\\ \\      / / ____| |   / ___/ _ \\|  \\/  | ____| |_   _/ _ \\ \r\n \\ \\ /\\ / /|  _| | |  | |  | | | | |\\/| |  _|     | || | | |\r\n  \\ V  V / | |___| |__| |__| |_| | |  | | |___    | || |_| |\r\n __\\_/\\_/_ |_____|_____\\____\\___/|_|  |_|_____|_  |_|_\\___/ \r\n|_   _|_ _/ ___|  |_   _|/ \\  / ___|  |_   _/ _ \\| ____|    \r\n  | |  | | |   _____| | / _ \\| |   _____| || | | |  _|      \r\n  | |  | | |__|_____| |/ ___ \\ |__|_____| || |_| | |___     \r\n  |_| |___\\____|    |_/_/   \\_\\____|    |_| \\___/|_____|    ");
            Console.WriteLine($"Welcome to Tic-Tac-Toe {player1Name} & {player2Name}! Select a column(0,1,2) and a row(0,1,2) to mark the spot you choose.\n");


            while (!gameOver)
            {
                //----------------------------------------------- CHECK WINNER,TURNS,DRAW -----------------------------------------------|
                /*checks for a winner or a draw, and alternates 
                * turns between two players until the game is won
                * drawn, or interrupted.*/

                /* alternates between two players taking turns
                 * updates gameBoard with each players move. It checks for wins/draws
                 * If a win/draw occurs, it prints a msg to the console*/
                while (true)
                {
                    int checkWinner = gameWon(gameBoard);


                    switch (checkWinner)
                    {
                        case 1:
                            Console.WriteLine($"{player1Name} wins!");
                            //Console.Write($"{Play again}? (y/n)");
                            //Console.ReadKey(playAgain);
                            return;

                        case 2:
                            Console.WriteLine($"{player2Name} wins!");
                            //Console.Write($"Play again? (y/n)");
                            //Console.ReadKey(playAgain);
                            return;
                        case 3:
                            Console.WriteLine("Draw!");
                            //Console.Write($"Play again? (y/n)");
                            //Console.ReadKey(playAgain);
                            return;
                        default:
                            break;
                    }//END SWITCH
                } //end playagain while loop

            }


            /*If the turn number is even (turn % 2 == 0), it calls the function 
             * playerTurn for player1X and updates the game board. Otherwise, 
             * it calls the function playerTurn for player2O and updates the game
             * board. After each turn, the turn counter is incremented by 1.*/
            if (turn % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{player1Name}'s turn:");
                gameBoard = placeMarker(player1X, gameBoard);
                Console.Clear();
            } //END IF
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player2Name}'s turn:");
                gameBoard = placeMarker(player2O, gameBoard);
                Console.Clear();
            } //END ELSE
            turn++;
        } //END WHILE

        //----------------------------------------------- PLACE MARKER FUNCTION -----------------------------------------------|

        /* Displays array of characters representing the gameBoard as a 3x3 'grid'*/
        static void consoleGameBoard(char[,] gameBoard)
        {
            /*uses values stored in gameBoard array to represent 
            * each "box" in the tic-tac-toe board*/
            Console.WriteLine("C>  0   1   2");
            string outputBoard = $"0 | {gameBoard[0, 0]} | {gameBoard[0, 1]} | {gameBoard[0, 2]} |" +
                        $"\n1 | {gameBoard[1, 0]} | {gameBoard[1, 1]} | {gameBoard[1, 2]} |" +
                        $"\n2 | {gameBoard[2, 0]} | {gameBoard[2, 1]} | {gameBoard[2, 2]} | \nR^";
            Console.WriteLine(outputBoard);
        } //END PLACEMAKER FUNCTION

        //----------------------------------0------------- PLAYER TURN FUNCTIONS -----------------------------------------------|

        /*defines playerTurn and takes a player symbol and the current game 
         * board as input parameters. It prompts the player to enter a 
         * position on the game board until a valid turn is made(0-8).*/
        static char[,] placeMarker(char playerSymbol, char[,] gameBoard)
        {
            //Find if the spot selected by the player is valid

            while (true)
            {

                Console.WriteLine("Select a row:");
                int row = int.Parse(Console.ReadLine());
                Console.WriteLine("Select a column:");
                int col = int.Parse(Console.ReadLine());
                /*verifies user input is valid, if it is loop runs again and displays result to the board*/
                if (validatePlayerTurn(row, col, gameBoard))
                {
                    gameBoard[row, col] = playerSymbol;
                    break;
                } //END IF STATEMENT

            } //END WHILE LOOP 
            return gameBoard;
        } //END PLAYER TURN FUNCTION

        /* Checks if the input place is within the bounds of the gameBoard
         * array and if the position on the board is empty (marked with '-'). */
        static bool validatePlayerTurn(int row, int col, char[,] gameBoard)
        {
            /* If the place is outside the board bounds or not empty, it outputs a
            * message to the console and returns false. Otherwise, it returns 
            * true indicating that the move is within 0-2.*/
            bool wrongTurn = false;
            //prevents user input from using #'s > 2 and < than 0
            if (row >= gameBoard.Length || row < 0 || col >= gameBoard.Length || col < 0)
            {
                Console.WriteLine("CAN'T YOU COUNT? I SAID 0-2!");
                return wrongTurn;
            }//END IF STATEMENT
            //Empty places in the board are marked as '-'
            if (gameBoard[row, col] == '*') { wrongTurn = true; }
            else
            {
                Console.WriteLine("TRY AGAIN!");
            }//END ELSE STATEMENT
            return wrongTurn;
        } //END VERIFYTURN FUNCTION

        //-----------------------------------------------FUNCTIONS FOR WINNING CRITERIA -----------------------------------------------|
        static int gameWon(char[,] gameBoard)
        {
            /*defines a method named gameWon that takes a 2D character
             * array gameBoard as input. It first checks if the gameboard
             * is full by iterating through each element in the array and
             * setting isGameBoardFull to false if it finds any empty cell
             * (represented by '-'). If the board is full, it returns 3
             * indicating a tie*/
            bool isGameBoardFull = true;
            foreach (char elem in gameBoard)
            {
                //if any elements = "-" it means the isGameBoardFull = false
                if (elem == '*')
                {
                    isGameBoardFull = false; break;
                } //end if
                /*if (isGameBoardFull)
                        {
                            Console.WriteLine("Game Over, do you want to play again?");
                        }*/
            } //END FOREACH LOOP

            if (isGameBoardFull) return 3;
            /*checks for player 1 (P1) win by calling rowCheck, colCheck,
             * and diagonalCheck functions with the value 111 (reps'X+X+X').
             * If any of these checks return true, it returns 1 indicating player 1 has won
              checks for a winning row, column, or diagonal for the player with the value 111.
            If any of these conditions are met, the code returns 1, indicating that the player
            with the value 111 has won the game.*/
            //Check for P1 Win:
            if (rowCheckWin(gameBoard, 264) || colCheckWin(gameBoard, 264) || diagonalCheckWin(gameBoard, 264)) { return 1; }
            /*check for player 2 (P2) win by calling the same check functions with the value
             * 222 (reps 'O+O+O'). If any of these checks return true, it returns 2 
             * indicating player 2 has won.  checks for a winning row, column, or diagonal for
             * the player with the value 237. If any of these conditions are met, the code returns 1,
             * indicating that the player with the value 237 has won the game.*/
            else if (rowCheckWin(gameBoard, 237) || colCheckWin(gameBoard, 237) || diagonalCheckWin(gameBoard, 237)) { return 2; }

            /*If neither player has won and the board is not full, it returns 4.*/
            return 4;
        } //GAME WON FUNCTION

        /*THIS FUNCTION CHECKS IF THE BOARD HAS 3 CONSECUTIVE X-O'S, IF THERE
         ARE 3 CONSECUTIVE OF EITHER IN A DIAGONAL LINE, IT WILL RETURN THE DETECT WIN BOOL TRUE*/
        static bool diagonalCheckWin(char[,] gameBoard, int threeMarks)
        {
            bool winDiag = false;
            int sumDiag1 = gameBoard[0, 0] + gameBoard[1, 1] + gameBoard[2, 2];
            int sumDiag2 = gameBoard[0, 2] + gameBoard[1, 1] + gameBoard[2, 0];

            if (sumDiag1 == threeMarks || sumDiag2 == threeMarks) { winDiag = true; }
            return winDiag;
        } //DIAGONAL CHECK FUNCTION


        /*THIS FUNCTION CHECKS IF THE BOARD HAS 3 CONSECUTIVE X-O'S, IF THERE
         ARE 3 CONSECUTIVE OF EITHER IN A COLUMN, IT WILL RETURN THE DETECT WIN BOOL TRUE*/
        static bool colCheckWin(char[,] gameBoard, int threeMarks)
        {
            bool winCol = false;
            int sumCol1 = gameBoard[0, 0] + gameBoard[1, 0] + gameBoard[2, 0];
            int sumCol2 = gameBoard[0, 1] + gameBoard[1, 1] + gameBoard[2, 1];
            int sumCol3 = gameBoard[0, 2] + gameBoard[1, 2] + gameBoard[2, 2];


            if (sumCol1 == threeMarks || sumCol2 == threeMarks || sumCol3 == threeMarks) { winCol = true; }
            return winCol;
        } //END COLUMN CHECK FUNCTION


        /*THIS FUNCTION CHECKS IF THE BOARD HAS 3 CONSECUTIVE X-O'S, IF THERE
         ARE 3 CONSECUTIVE OF EITHER IN A ROW, IT WILL RETURN THE DETECT WIN BOOL TRUE*/
        static bool rowCheckWin(char[,] gameBoard, int threeMarks)
        {
            bool winRow = false;
            int sumRo1 = gameBoard[0, 0] + gameBoard[0, 1] + gameBoard[0, 2];
            int sumRow2 = gameBoard[1, 0] + gameBoard[1, 1] + gameBoard[1, 2];
            int sumRow3 = gameBoard[2, 0] + gameBoard[2, 1] + gameBoard[2, 2];


            if (sumRo1 == threeMarks || sumRow2 == threeMarks || sumRow3 == threeMarks) { winRow = true; }
            return winRow;
        } //END ROW CHECK FUNCTION
    
      //WHILE GAME OVER LOOP
    //END TIC TAC TOE

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
}//end namespace
