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
            // Make sure that we have enough stock of drinks being ordered
            if (!CheckDrinksStock(desiredDrinks))
                throw new Exception("Drink is sold out, your purchase could not be processed.");

            // Get value of payment given and drinks being bought
            int paymentVal = GetCoinsValue(payment);
            int drinksVal = GetDrinksValue(desiredDrinks);

            // Check if there is enough coins given
            if (paymentVal >= drinksVal)
            {
                int changeRequired = paymentVal - drinksVal;
                List<ICoin> changeToReturn = new List<ICoin>();

                // No change required 
                if (changeRequired == 0) {
                    // Make changes to register and inventory and finish buy process
                    RemoveDrinksFromInventory(desiredDrinks);
                    AddCoinsToRegister(payment);

                    // No change returned
                    return null;
                }
                else
                {
                    return GetChange(paymentVal - drinksVal, desiredDrinks, payment);
                }
            }
            else {
                throw new Exception("Not enough payment, your purchase could not be processed.");
            }
        }

        /// <summary>
        /// Check that all the given ordered drinks are in stock.
        /// </summary>
        /// <param name="orderedDrinks">The drinks that are being ordered.</param>
        /// <returns>If there are enough drinks in stock for the given order.</returns>
        private bool CheckDrinksStock(List<IProduct> orderedDrinks) {
            // Go through all ordered drinks and make sure there is sufficient stock in machine 
            foreach (Drink drink in orderedDrinks) {
                int stockInMachine = Inventory.First(d => d.Name == drink.Name).Stock;

                // Check if machine does not have sufficient stock of drink
                if (stockInMachine < drink.Stock)
                    return false;
            }

            // There is a sufficient stock of all ordered drinks
            return true;
        }

        /// <summary>
        /// Gets the monetary value of all the coins in the given list.
        /// </summary>
        /// <param name="coins">List of coins.</param>
        /// <returns>The monetary value of the given coins.</returns>
        private int GetCoinsValue(List<ICoin> coins) {
            int result = 0;

            // Add value of coins to result
            foreach (Coin coin in coins) {
                result += (coin.Amount * coin.Denomination);
            }

            return result;
        }

        /// <summary>
        /// Gets the monetary value of all the drinks in the given list.
        /// </summary>
        /// <param name="drinks">List of drinks.</param>
        /// <returns>The monetary value of the given drinks.</returns>
        private int GetDrinksValue(List<IProduct> drinks)
        {
            int result = 0;

            // Add value of drinks to result
            foreach (Drink drink in drinks)
            {
                result += (drink.Cost * drink.Stock);
            }

            return result;
        }

        /// <summary>
        /// Remove the ordered drinks from the drink vending machine's inventory.
        /// </summary>
        /// <param name="orderedDrinks">The ordered drinks.</param>
        private void RemoveDrinksFromInventory(List<IProduct> orderedDrinks) {
            // subtract the ordered drinks from the drink vending machines stock
            foreach (Drink drink in orderedDrinks) {
                Inventory.First(s => s.Name == drink.Name).Stock -= drink.Stock;
            }
        }

        /// <summary>
        /// Add coins to the drink vending machine's register.
        /// </summary>
        /// <param name="coinsPaid">The coins to be added to the register.</param>
        private void AddCoinsToRegister(List<ICoin> coinsPaid) {
            // add the coins from the drink vending machines register
            foreach (Coin coin in coinsPaid)
            {
                Register.First(s => s.Denomination == coin.Denomination).Amount += coin.Amount;
            }
        }

        /// <summary>
        /// Remove coins from the drink vending machine's register.
        /// </summary>
        /// <param name="coins">The coins to remove from the register.</param>
        private void RemoveCoinsFromRegister(List<ICoin> coins)
        {
            // add the coins from the drink vending machines register
            foreach (Coin coin in coins)
            {
                Register.First(s => s.Denomination == coin.Denomination).Amount -= coin.Amount;
            }
        }

        /// <summary>
        /// Get the change required for an order of drinks.
        /// </summary>
        /// <param name="changeRequired">The value of the change required.</param>
        /// <param name="orderedDrinks">The drinks that were ordered.</param>
        /// <param name="payment">The coins that were given as payment for the drinks.</param>
        /// <returns>A list of coins containing the required change (if there is enough change to return, otherwise order fails and returns null).</returns>
        private List<ICoin> GetChange(int changeRequired, List<IProduct> orderedDrinks, List<ICoin> payment) {
            List<ICoin> change = new List<ICoin>();

            // Add coins to vending machine register
            AddCoinsToRegister(payment);

            // We'll be using a greedy approach to use the least amount of coins to make up the change
            // We'll start with the coins with the highest denomination
            Register.OrderByDescending(c => c.Denomination);

            foreach (Coin coin in Register) {
                // Get the most amount of the current coin that can be a part of the change
                int numCurrentCoin = changeRequired / coin.Denomination;

                // if you can make this coin a part of the change, add it to the change
                if (numCurrentCoin > 0) {
                    changeRequired -= coin.Denomination * numCurrentCoin;
                    change.Add(new Coin(coin.Denomination, numCurrentCoin, coin.Name));
                }
                
                // If enough change has been gathered, remove it from the register and return
                if (changeRequired == 0) {
                    RemoveCoinsFromRegister(change);
                    RemoveDrinksFromInventory(orderedDrinks);
                    return change;
                }      
            }

            // If all coin denominations have been cycled through and the change required did not reach 0, we don't have
            // enough change in the vending machine
            RemoveCoinsFromRegister(payment);
            throw new Exception("Not sufficient change in the inventory.");
        }
    }
}