using System.Diagnostics;
using System.IO;

namespace TransactionLoggingPackage2
{
    internal class Program
    {
        static void Main(string[] args)


        {
            
            //string vendor = trans.cardVendor.Replace(' ', '_');
            //string output = trans.transactionNum.ToString() + "," + trans.transactionDate + ",$" + trans.cashAmount.ToString()
            //+ "," + vendor + ",$" + trans.cardAmount + ",$" + trans.changeGiven.ToString();
            //var dateOnly = DateTime.Now.ToString("MM-dd-yyyy");

            string transactionNumber = args[0];
            string transDate = args[1];
            string transCashAmount = args[2];
            string vendor = args[3];
            string cardAmount = args[4];
            string changeGiven = args[5];

            //create log file folder
            string folder = @"C:\Users\nicol\Documents\GitHub\MCA-Main-Projects\Kiosk Project\TransactionLoggingPackage2";
            string receiptLogFile = transDate;
            string filepath = @"C:\Users\nicol\Documents\GitHub\MCA-Main-Projects\Kiosk Project\TransactionLoggingPackage2\bin\Debug\net8.0\TransactionLoggingPackage2.exe";


            if (!File.Exists(filepath))
            {
                // Create a file to write to.

                using (StreamWriter sw = File.CreateText(filepath))

                {
                    //Write text to file
                    for (int i = 0; i < args.Length; i++) { sw.WriteLine(args[i]); }
                    //Close the file
                    sw.Close();
                }
            }

            using (StreamWriter sw = File.AppendText(filepath))

            {
                for (int i = 0; i < args.Length; i++) { sw.WriteLine(args[i]); }
                //Close the file
                sw.Close();

            }
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
