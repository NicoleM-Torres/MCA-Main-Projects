using System.Diagnostics;

namespace TransactionLogging
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Declare Variable
            var dateOnly = DateTime.Now.ToString("MM-dd-yyyy");
            string path = @"C:\Users\nicol\OneDrive\Documents\GitHub\MCA-Main-Projects\Kiosk Project\TransactionLogging\Transaction Logs";
            string fileName = DateTime.Now.ToString("MMM-dd-yyyy") + " Transactions.log";
            string fullPath = Path.Combine(path, fileName);
            //startInfo.Arguments = $"{trans.transactionNum} {trans.transactionDate} {trans.cashAmount} {trans.cardVendor} {trans.cardAmount} { trans.changeGiven}"; // Arguments to pass to the executable

            int transNum = int.Parse(args[0]);
            string transDate = args[1];
            decimal transcashAmount = decimal.Parse(args[2]);
            string transcardVendor = args[3];
            decimal transcardAmount = decimal.Parse(args[4]);
            decimal transchangeGiven = decimal.Parse(args[5]);

            if (!File.Exists(fullPath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(fullPath))
                {
                    //Write text to file
                    //for (int i = 0; i < args.Length; i++) { sw.WriteLine(args[i]); }
                    sw.WriteLine("================================");
                    sw.WriteLine("");
                    sw.WriteLine($"Transaction Number:{transNum}");
                    sw.WriteLine($"Date:{transDate}");
                    sw.WriteLine("");
                    sw.WriteLine($"Total:\t\t\t${transcardAmount + transcashAmount - transchangeGiven}");
                    sw.WriteLine("--------------------------------");
                    sw.WriteLine($"Cash:${transcashAmount}");
                    sw.WriteLine($"Card Merchant:{transcardVendor}");
                    sw.WriteLine($"Card:${transcardAmount}");
                    sw.WriteLine($"Change:${transchangeGiven}");


                    //Close the file
                    sw.Close();
                }//end using
            } else { //end if

            using (StreamWriter sw = File.AppendText(fullPath))
            {
                sw.WriteLine("================================");
                sw.WriteLine("");
                sw.WriteLine($"Transaction Number:{transNum}");
                sw.WriteLine($"Date:{transDate}");
                sw.WriteLine("");
                sw.WriteLine($"Total:\t\t\t${transcardAmount + transcashAmount - transchangeGiven}");
                sw.WriteLine("--------------------------------");
                sw.WriteLine($"Cash:${transcashAmount}");
                sw.WriteLine($"Card Merchant:{transcardVendor}");
                sw.WriteLine($"Card:${transcardAmount}");
                sw.WriteLine($"Change:${transchangeGiven}");
                sw.WriteLine("");
                    //for (int i = 0; i < args.Length; i++) { sw.WriteLine(args[i]); }

                    //Close the file
                    sw.Close();
            }//end using
}
    }//end main 

    #region original
    /*
     * //Declare Variable
        var dateOnly = DateTime.Now.ToString("MM-dd-yyyy");
        string path = @"C:\Users\nicol\Documents\GitHub\MCA-Main-Projects\Kiosk Project\TransactionLogging\Transaction Logs\";
        string fileName = DateTime.Now.ToString("MMM-dd-yyyy") + " Transactions.log";
        string fullPath = path + fileName;

        if (!File.Exists(fullPath))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(fullPath))
            {
                //Write text to file
                //for (int i = 0; i < args.Length; i++) { sw.WriteLine(args[i]); }
                //Close the file
                sw.Close();
            }//end using
        }//end if

        using (StreamWriter sw = File.AppendText(fullPath))
        {

            for (int i = 0; i < args.Length; i++){ sw.WriteLine(args[i]); } 

            //Close the file
            sw.Close();
        }//end using
     */
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

    }//END CLASS    
}//end namespace
