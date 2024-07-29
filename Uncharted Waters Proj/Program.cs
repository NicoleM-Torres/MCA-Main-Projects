using System.Data.Common;
using System.Dynamic;
using System.Resources;

namespace Uncharted_Waters_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //SATELLITE GRAPH ARRAY
            int[,,] graph = GetSatelliteData();

            //--------------------------------------------------- LEVEL DENSITY -----------------------------------------------------

            /*AW SURFACE DENSITY -- calculates the surface density of a given graph at a specified
             *surface level and then prints the calculated surface density to the console.*/
            double aWaterDensity = 0;
            int aWaterLevel = 0;
            aWaterDensity = Density(graph, aWaterLevel);
            Console.WriteLine("Surface sub density\t\t- {0}", aWaterDensity);


            /*UW SURFACE DENTISITY -- calculates the underwater density of a given graph at a 
             * specified uw level and then prints the calculated surface density to the console.*/
            int underwaterLevel = 1;
            double underwaterDensity = Density(graph, underwaterLevel);
            Console.WriteLine("Underwater sub density\t\t- {0}", underwaterDensity);


            /*DW SURFACE DENTISTY -- calculates the surface density of a given graph at a specified
             *surface level and then prints the calculated surface density to the console.*/
            int deepwaterLevel = 2;
            double deepwaterDensity = Density(graph, deepwaterLevel);
            Console.WriteLine("Deepwater sub density\t\t- {0}", deepwaterDensity);


            //--------------------------------------------------- SUB RATIOS -----------------------------------------------------

            /*calculates the ratio of the number of submarines to the number of enemy 
             *submarines in a graph and then prints out the result in the console. 
             *The ratio is calculated by the Ratio() function, which takes the graph 
             *as input and returns a double value representing the ratio.*/

            //RATIO US SUBS VS ENEMY
            double ratio = Ratio(graph);
            Console.WriteLine("U.S. Sub / Enemy Sub ratio\t- {0}", ratio);


            //--------------------------------------------------- SUB ATTACK -----------------------------------------------------

            /*calls the 'Attack' function with different parameters (0, 1, and 2) 
             * representing different attack levels (surface, underwater, deepwater)
             * on a graph. It then prints out whether to go for a surface attack, 
             * underwater attack, or deepwater attack based on the returned boolean values.*/

            //US SUB ATTACK -- SURFACE LEVEL

            bool attackSurface = Attack(graph, 0);
            Console.WriteLine("Go for surface attack?\t\t- {0}", attackSurface);

            //US SUB ATTACK -- UW LEVEL
            bool attackUnderwater = Attack(graph, 1);
            Console.WriteLine("Go for underwater attack?\t- {0}", attackUnderwater);

            //US SUB ATTACK -- DW LEVEL
            bool attackDeepwater = Attack(graph, 2);
            Console.WriteLine("Go for deepwater attack?\t- {0}", attackDeepwater);
            Console.WriteLine("");

            //--------------------------------------------------- GRAPH DISPLAY ---------------------------------------------------

            /*sets the console text color to different shades of cyan and blue,
             * then displays three different graphs (water levels) using the 
             * DisplyGraph method with different parameters (0, 1, and 2). 
             * Finally, it resets the console text color back to the default.*/

            //ABOVE WATER GRAPH DISPLAY
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("SURFACE -- AW");
            DisplyGraph(graph, 0);
            Console.WriteLine("");

            //UNDER WATER GRAPH DISPLAY
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("UNDERWATER -- UW");
            DisplyGraph(graph, 1);
            Console.WriteLine("");

            //DEEP WATER GRAPH DISPLAY
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("DEEPWATER -- DW");
            DisplyGraph(graph, 2);

            //CONSOLE COLOR RESET
            Console.ResetColor();
        
        }//end main

        //--------------------------------------------------- SATELLITE FUNCTION -----------------------------------------------------
        /*generates random numbers to populate a 3-dimensional array 
         *representing satellite data. It fills the array with random integers 
         *between 1 and 3 based on a condition where a random number between 0 
         *and 100 is generated and if it is less than 25, the element in the array
         *is set to a random number between 1 and 3.*/
        //initilize graph

        static int[,,] GetSatelliteData()
        {
            Random rand = new Random();
            int[,,] data = new int[10, 10, 3];
            for (int z = 0; z < data.GetLength(2); z++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    for (int x = 0; x < data.GetLength(0); x++)
                    {
                        if (rand.Next(0, 101) < 25)
                        {
                            data[x, y, z] = rand.Next(1, 3);
                        }
                    }
                }
            }
            return data;
        }//end GetSatelliteData()

        //--------------------------------------------------- DENSITY FUNCTION -----------------------------------------------------

        /* calculates the density of values 1 or 2 in a 3-dimensional array at a specific level.
        *  It counts the number of occurrences of values 1 or 2 at the specified level and divides
        * it by the total number of elements in the x and y dimensions of the array.*/
        static double Density(int[,,] array, int level)
        {
            double num = 0;
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {

                    if (array[x, y, level] == 1 || array[x, y, level] == 2) num++;

                }
            }
            return num / (array.GetLength(0) * array.GetLength(1));
        }//end Density()

        //--------------------------------------------------- RATIO FUNCTION -----------------------------------------------------

        /* defines a static method named Ratio that takes a 3-dimensional integer array as input.
         * It then iterates over each element in the array and counts the occurrences of the values
         * 1 and 2. The variable USNavy counts the number of occurrences of the value 1, and the variable
         * losers counts the number of occurrences of the value 2.*/
        static double Ratio(int[,,] array)
        {
            double USNavy = 0;
            double losers = 0;

            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    for (int z = 0; z < array.GetLength(2); z++)
                    {
                        if (array[x, y, z] == 1) USNavy++;
                        if (array[x, y, z] == 2) losers++;
                    }
                }
            }
            return USNavy / losers;
        }//end density()

        //--------------------------------------------------- BOOL FUNCTION -----------------------------------------------------

        /*defines a static method named Attack that takes a 3-dimensional integer array
         * and an integer level as input parameters. It then iterates over the elements of 
         * the array at the specified level and counts the occurrences of the values 1 and 2. 
         * If the count of value 1 is greater than the count of value 2, the method returns true;
         * otherwise, it returns false.*/

        static bool Attack(int[,,] array, int level)
        {
            int USNavy = 0;
            int enemy = 0;

            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    if (array[x, y, level] == 1) USNavy++;
                    if (array[x, y, level] == 2) enemy++;
                }
            }

            if (USNavy > enemy) return true;
            else return false;
        }//end Attack()

        //--------------------------------------------------- DISPLAY FUNCTION ----------------------------------------------------

        /*defines a method called DisplayGraph that takes a 3D integer 
         * array array and an integer level as parameters. It then iterates
         * over the first two dimensions of the array using nested loops and
         * prints out the values at the specified level for each pair of 
         * x and y indices.*/
        static void DisplyGraph(int[,,] array, int level)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    Console.Write("{0} ", array[x, y, level]);
                }
                Console.WriteLine("");
            }
        }//end DisplayGraph

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
