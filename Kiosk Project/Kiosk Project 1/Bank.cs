using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk_Project_1
{
    internal class Bank
    {
        #region CheckBank Cash
        public static bool CheckBankCash(decimal totalChange, bool v)
        {
            //create a replica bank to check if you have money in the bank 
            decimal moneyowed = totalChange;
            int[] array = new int[12];

            //fill the replica bank
            for (int index = 0; index < Kiosk.bank.Length; index++)
            {
                array[index] = Kiosk.bank[index];
            }

            //Start checking to see if the bank has enough to fufill the transaction
            for (int index = 0; moneyowed > 0; index++)
            {
                if (moneyowed >= Kiosk.moneyValues[index] && array[index] > 0)
                {
                    array[index]--;
                    moneyowed -= Kiosk.moneyValues[index];
                    index = 0;

                    //if the bank doesn't have enough money it will tell you and dispense their money back
                }
                else if (moneyowed > 0 && array[index] == 0)
                {
                    Console.WriteLine("This machine does not have enough\nmoney to despense change so you will be refunded");

                    //start dispensing the money back to the user
                    for (int count = 0; count < Kiosk.userMoney.Length; count++)
                    {
                        if (Kiosk.userMoney[count] > 0)
                        {
                            Console.WriteLine("{0:C} Dispensed change", Kiosk.moneyValues[count]);
                            Kiosk.userMoney[count]--;
                            Kiosk.bank[count]--;
                            count = 0;
                        }
                    }
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Please find another way to pay");

                    //return false since the bank cant fufill the transaction
                    return false;
                }
            }
            //return true if it could fufill the transaction
            return true;
        }

        #endregion

        #region checkBankCard
        public static bool CheckBankCard(decimal totalChange /*,bool Card*/)
        {
            //create a replica bank to check if you have money in the bank 
            decimal moneyowed = totalChange;
            int[] array = new int[12];

            //fill the replica bank
            for (int index = 0; index < Kiosk.bank.Length; index++)
            {
                array[index] = Kiosk.bank[index];
            }

            //Start checking to see if the bank has enough to fufill the transaction
            for (int index = 0; moneyowed > 0; index++)
            {
                if (moneyowed >= Kiosk.moneyValues[index] && array[index] > 0)
                {
                    array[index]--;
                    moneyowed -= Kiosk.moneyValues[index];
                    index = 0;

                    //if the bank doesn't have enough money it will tell you and dispense their money back
                }
                else if (moneyowed > 0 && array[index] == 0)
                {


                    //start dispensing the money back to the user
                    for (int count = 0; count < Kiosk.userMoney.Length; count++)
                    {
                        if (Kiosk.userMoney[count] > 0)
                        {
                            Console.WriteLine("{0:C} Dispensed change", Kiosk.moneyValues[count]);
                            Kiosk.userMoney[count]--;
                            Kiosk.bank[count]--;
                            count = 0;
                        }
                    }
                    //return false since the bank cant fufill the transaction
                    return false;
                }
            }
            //return true if it could fufill the transaction
            return true;
        }
        #endregion

    }
}
