using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk_Project_1
{
    internal class CardCheckVal
    {
        public static bool ValidateCard(string creditCard)
        {
            //variables
            int[] array = new int[creditCard.Length];
            bool shouldDouble = true;
            char[] word = creditCard.ToCharArray();
            int sum = 0;
            int doubled = 0;
            string number = "";

            //put the credit card number into an array
            for (int index = 0; index < creditCard.Length; index++)
            {
                number = word[index].ToString();
                array[index] = int.Parse(number);
            }

            //this doubles every other number starting from the right
            for (int index = creditCard.Length - 2; index >= 0; index--)
            {
                if (shouldDouble)
                {
                    doubled = array[index] * 2;
                    if (doubled >= 10) sum += doubled - 9;
                    else sum += doubled;
                }
                else sum += array[index];
                shouldDouble = !shouldDouble;
            }
            //this returns the final number of credit card number
            sum += array[15];

            //if the sum is not evenly divisible by 10 then it returns false, or true if it is
            return (sum % 10 == 0);
        }
    }
}
