using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kiosk_Project_1
{
    internal class RecieptLog
    {

        #region Receipt
        public static void transactionLogging()
        {
            string vendor = Kiosk.cardVendor.Replace(' ', '`');
            string arguments = Kiosk.transactionNum.ToString() + "," + Kiosk.date + ",$" + Kiosk.cashAmount.ToString() + "," + vendor + ",$" + Kiosk.cardAmount.ToString() + ",$" + Kiosk.changeDue.ToString();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "logging.exe";
            startInfo.Arguments = arguments;
            Process.Start(startInfo);
            

        }
        #endregion
    

    }
}
