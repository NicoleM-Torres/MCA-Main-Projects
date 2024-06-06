using System.Diagnostics;

namespace receiptLogFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Logging

            //Declare Variable
            //Putting variables into a single string

            string fileName
          
            if (!File.Exists(path))
                {
                    // Create a file to write to.

                    using (StreamWriter sw = File.CreateText(path))

                    {
                        //Write text to file
                        for (int i = 0; i < output.Length; i++) { sw.WriteLine(output[i]); }
                        //Close the file
                        sw.Close();
                    }
                }

                using (StreamWriter sw = File.AppendText(path))

                {
                    for (int i = 0; i < output.Length; i++) { sw.WriteLine(output[i]); }
                    //Close the file
                    sw.Close();

                }
                //throw new NotImplementedException();
                /*
                //Replacing spaces with | for formatting

            Console.WriteLine(vendor);
                Console.WriteLine(output);
                */
            }

            #endregion
        }//end main

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
