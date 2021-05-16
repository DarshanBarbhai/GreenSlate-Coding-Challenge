using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinksMachineAppModel.Interfaces;

namespace DrinksMachineAppModel
{
    /// <summary>
    /// Implementation of the IProduct interface, represents the drinks we will be ordering in our DrinkVendingMachine.
    /// </summary>
    public class Drink : IProduct
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Stock { get; set; }

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
            this.Stock = stock;
        }
    }
}
