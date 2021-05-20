using DrinksMachineAppModel;
using DrinksMachineAppModel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrinksMachineApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private int orderTotal;
        private int paymentTotal;
        private bool orderButtonEnabled;

        public List<ICoin> PaymentCoins { get; set; }

        public List<IProduct> OrderedDrinks { get; set; }

        public DrinkVendingMachine VendingMachine { get; set; }

        public int OrderTotal {
            get
            {
                return orderTotal;
            }
            set
            {
                if (value >= 0)
                {
                    orderTotal = value;
                    NotifyPropertyChanged();
                }
            } 
        }

        public int PaymentTotal
        {
            get
            {
                return paymentTotal;
            }
            set
            {
                if (value >= 0)
                {
                    paymentTotal = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool OrderButtonEnabled
        {
            get
            {
                return orderButtonEnabled;
            }
            set
            {
                orderButtonEnabled = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// View Model that provides the data context for the Main Window.
        /// </summary>
        public MainWindowViewModel() {
            // Setup initial values for payment coins, ordered drinks and order total
            this.PaymentCoins = GenerateCoins();
            this.OrderedDrinks = GenerateDrinks();
            this.orderTotal = 0;
            this.paymentTotal = 0;

            // Setup register for vending machine
            List<ICoin> machineRegister = GenerateCoins();
            machineRegister[0].Amount = 100;
            machineRegister[1].Amount = 10;
            machineRegister[2].Amount = 5;
            machineRegister[3].Amount = 25;

            // Setup inventory for vending machine
            List<IProduct> machineInventory = GenerateDrinks();
            machineInventory[0].Stock = 5;
            machineInventory[1].Stock = 15;
            machineInventory[2].Stock = 3;

            // Setup Drink Vending Machine
            VendingMachine = new DrinkVendingMachine(machineInventory, machineRegister);
            this.orderButtonEnabled = VendingMachine.Inventory.Where(x => x.Stock > 0).Any();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OrderDrinks() {
            try {
                List<ICoin> change = VendingMachine.Buy(PaymentCoins, OrderedDrinks);

                string message = "Your purchase was successful!\nHere is your receipt:\n";

                // Add receipt to the order success message
                foreach (Drink drink in OrderedDrinks)
                    message += drink.ToString() + "\n";

                if(change != null)
                {
                    message += "Here is your change: \n";
                    foreach (Coin coin in change)
                        message += coin.ToString() + "\n";
                }

                MessageBox.Show(message, "Order Processed!", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset view for next order
                ResetView();
            }
            catch(Exception error) {
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateOrderTotal() {
            int total = 0;

            foreach (IProduct drink in OrderedDrinks) {
                total += (drink.Cost * drink.Stock);
            }

            OrderTotal = total;
        }

        public void UpdatePaymentTotal()
        {
            int total = 0;

            // Calculate the total value of the coins being entered for payment
            foreach (ICoin coin in PaymentCoins)
                total += (coin.Denomination * coin.Amount);

            PaymentTotal = total;
        }

        private List<ICoin> GenerateCoins() {
            List<ICoin> coins = new List<ICoin>();

            // setup all coins with desired denomination and names; initial amount for all coins will be 0
            coins.Add(new Coin(1, 0, "Penny"));
            coins.Add(new Coin(5, 0, "Nickle"));
            coins.Add(new Coin(10, 0, "Dime"));
            coins.Add(new Coin(25, 0, "Quarter"));

            return coins;
        }

        private List<IProduct> GenerateDrinks() {
            List<IProduct> drinks = new List<IProduct>();

            // setup all the drinks with require names and prices; initial stock will be 0
            drinks.Add(new Drink("Coke", 25, 0));
            drinks.Add(new Drink("Pepsi", 36, 0));
            drinks.Add(new Drink("Soda", 45, 0));

            return drinks;
        }

        private void ResetView() {
            // Reset payment coins
            foreach (Coin coin in PaymentCoins)
                coin.Amount = 0;

            // Reset ordered drinks
            foreach (Drink drink in OrderedDrinks)
                drink.Stock = 0;

            // Reset totals
            this.OrderTotal = 0;
            this.PaymentTotal = 0;

            // Check if any stock is available, if not disable order button
            if (!VendingMachine.Inventory.Where(x => x.Stock > 0).Any())
                OrderButtonEnabled = false;
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
