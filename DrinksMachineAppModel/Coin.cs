using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DrinksMachineAppModel.Interfaces;

namespace DrinksMachineAppModel
{
    /// <summary>
    /// Implementation of the ICoin interface
    /// </summary>
    public class Coin : ICoin, INotifyPropertyChanged
    {
        private int amount;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Denomination { get; set; }
        public int Amount {
            get
            {
                return amount;
            }
            set {
                if (value >= 0)
                {
                    amount = value;
                    NotifyPropertyChanged();
                }
            } 
        }

        /// <summary>
        /// The name for this type of coin
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor of the Coin class; this class is very barebones and can be used as a parent class for any subclasses
        /// that may need to be implemented in the future.
        /// </summary>
        /// <param name="denomination">denomonation/value of the coin</param>
        /// <param name="amount">amount of coins</param>
        /// <param name="name">name of coin</param>
        public Coin(int denomination, int amount, string name = "")
        {
            this.Denomination = denomination;
            this.Amount = amount;
            this.Name = name;
        }

        /// <summary>
        /// Get summary for coin object in string format.
        /// </summary>
        /// <returns>String summary of Coin object.</returns>
        public override string ToString()
        {
            return this.Name + ", " + "denomiation: " + this.Denomination + ", amount: " + this.Amount;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
