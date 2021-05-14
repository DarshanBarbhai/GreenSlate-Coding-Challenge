using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinksMachineApp.Interfaces;

namespace DrinksMachineApp.Models
{
    /// <summary>
    /// Implementation of the ICoin interface
    /// </summary>
    public class Coin : ICoin
    {
        public int Denomination { get; set; }
        public int Amount { get; set; }

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
        public Coin(int denomination, int amount, string name = "") {
            this.Denomination = denomination    ;
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
    }
}