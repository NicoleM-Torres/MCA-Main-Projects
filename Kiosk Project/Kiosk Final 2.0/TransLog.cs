using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSKiosk
{
    internal class TransLog
    {
        #region Receipt

        public static void Logging()
        {
            string vendor = Program.trans.cardVendor.Replace(' ', '_');
            string output = Program.trans.transactionNum.ToString() + "," + Program.trans.transactionDate + ",$" + Program.trans.cashAmount.ToString()
            + "," + vendor + ",$" + Program.trans.cardAmount + ",$" + Program.trans.changeGiven.ToString();
            var dateOnly = DateTime.Now.ToString("MM-dd-yyyy");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"\C:\\Users\\nicol\\Documents\\GitHub\\MCA-Main-Projects\\Kiosk Project\\TransactionLoggingPackage2\\bin\\Debug\\net8.0\\TransactionLoggingPackage2.exe\"; // The name of the executable to run
            startInfo.Arguments = output; // Arguments to pass to the executable
                                          //startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            Process.Start(startInfo);// Starts the process

        }

        #endregion
    } //END LOGGING METHOD
} //END TRANS LOG CLASS
