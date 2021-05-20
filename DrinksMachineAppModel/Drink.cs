using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DrinksMachineAppModel.Interfaces;

namespace DrinksMachineAppModel
{
    /// <summary>
    /// Implementation of the IProduct interface, represents the drinks we will be ordering in our DrinkVendingMachine.
    /// </summary>
    public class Drink : IProduct, INotifyPropertyChanged
    {
        private int stock;

        public string Name { get; set; }
        public int Cost { get; set; }
        public int Stock {
            get {
                return stock;
            } 
            set {
                if (value >= 0) {
                    this.stock = value;
                    NotifyPropertyChanged();
                }
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A drink product that can be ordered from a IVendingMachine.
        /// </summary>
        /// <param name="name">Name of the drink.</param>
        /// <param name="cost">Cost of the drink in cents</param>
        /// <param name="stock">Number of the drink in stock.</param>
        public Drink(string name, int cost, int stock)
        {
            this.Name = name;
            this.Cost = cost;
            this.stock = stock;
        }

        

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return this.Name + ": " + this.Stock + " cans";
        }
    }
}
