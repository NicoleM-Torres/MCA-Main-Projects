using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk
{
    internal class Food
    {
        //FIELDS
        private string _name;
        private decimal _price;
        private int _count;

        public static List<Food> orderedFood = new List<Food>();

        //CONSTRUCTOR

            public Food (string name, decimal price)
        {
            this._name = name;
            this._price = price;
            this._count = 0;
            orderedFood.Add(this);
        } //END CONSTRUCTOR

        //PROPERTIES
        
        //NAME PROPERTY
        public string Name
        {
            get { return _name; }
        } //END NAME PROPERTY

        //PRICE PROPERTY
        public decimal Price {
            get { return _price; } 
        } //END PRICE PROPERY

        //COUNTING PROPERTY
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        } //END COUNT PROPERTY

        //TOTAL PRICE PROPERTY

        public decimal TotalPrice { 
            get { return _price * _count; } 
        }// END TOTAL PRICE PROPERTY

        //OVERRIDE METHOD

        //OVERRIDE PROPERTY
        public override string ToString()
        {
            return String.Format ("{0,-35}| {1,-6}| {2,8:C}", Name, _count.ToString(), TotalPrice);
        } //END OVERRIDE


    } //END CLASS
} //END NAMESPACE
