using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinksMachineAppModel.Interfaces;

namespace DrinksMachineAppModel
{
    /// <summary>
    /// Implementation of the IVendingMachine interface, represents the drink vending machine we will be buying drinks from.
    /// </summary>
    public class DrinkVendingMachine : IVendingMachine
    {
        public List<IProduct> Inventory { get; set; }
        public List<ICoin> Register { get; set; }

        /// <summary>
        /// Drink Vending Machine that we order drinks from.
        /// </summary>
        /// <param name="inventory">Inventory of drinks in the vending machine.</param>
        /// <param name="register">Register of coins in the vending machine.</param>
        public DrinkVendingMachine(List<IProduct> inventory, List<ICoin> register)
        {
            this.Inventory = inventory;
            this.Register = register;
        }

        /// <summary>
        /// Buy desired products from the vending machine with a given payment.
        /// </summary>
        /// <param name="payment">The coins to pay for the desired products.</param>
        /// <param name="desiredDrinks">The drinks to be bought.</param>
        /// <returns>Change as a list of Coins if there is any, empty list returned otherwise.</returns>
        public List<ICoin> Buy(List<ICoin> payment, List<IProduct> desiredDrinks)
        {
            throw new NotImplementedException();
        }
    }
}
