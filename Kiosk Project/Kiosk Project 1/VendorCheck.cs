using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk_Project_1
{
    internal class VendorCheck
    {

        #region Check vendor
        public static string ValidateVendor(string creditCard)
        {
            //This identifies the card vendor 
            //visa
            if (creditCard[0] == '4') return "Visa";
            //mastercard
            else if (creditCard[0] == '5' && creditCard[1] == '1') return "Mastercard";
            else if (creditCard[0] == '5' && creditCard[1] == '2') return "Mastercard";
            else if (creditCard[0] == '5' && creditCard[1] == '3') return "Mastercard";
            else if (creditCard[0] == '5' && creditCard[1] == '4') return "Mastercard";
            else if (creditCard[0] == '5' && creditCard[1] == '5') return "Mastercard";
            //discover
            else if (creditCard[0] == '6' && creditCard[1] == '0' && creditCard[2] == '1' && creditCard[3] == '1') return "Discover";
            else if (creditCard[0] == '6' && creditCard[1] == '4' && creditCard[2] == '4') return "Discover";
            else if (creditCard[0] == '6' && creditCard[1] == '5') return "Discover";
            //American success
            else if (creditCard[0] == '3' && creditCard[1] == '4') return "American Express";
            else if (creditCard[0] == '3' && creditCard[1] == '7') return "American Express";
            //non valid card
            else return "Non valid card";
        }
        #endregion

    }
}
