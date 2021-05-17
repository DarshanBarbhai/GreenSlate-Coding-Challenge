using NUnit.Framework;
using DrinksMachineAppModel.Interfaces;
using DrinksMachineAppModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DrinksMachineAppTests
{
    public class Tests
    {
        IVendingMachine vendingMachine;
        List<ICoin> vendingMachineRegister;
        List<IProduct> vendingMachineInventory;
        List<ICoin> payment;
        List<IProduct> order;

        [SetUp]
        public void Setup()
        {
            // Set up coin register for vending machine
            vendingMachineRegister = new List<ICoin>();
            vendingMachineRegister.Add(new Coin(25, 0, "Quarter"));

            // Setup inventory for vending machine
            vendingMachineInventory = new List<IProduct>();
            vendingMachineInventory.Add(new Drink("Coke", 25, 1));

            // Create vending machine
            vendingMachine = new DrinkVendingMachine(vendingMachineInventory, vendingMachineRegister);

            // Reset the payment and order to new empy lists evey test
            payment = new List<ICoin>();
            order = new List<IProduct>();
        }

        [Test]
        public void SuccessfulOrderNoChange()
        {
            // Setup payment for test
            payment.Add(new Coin(25, 1, "Quarter"));

            // Setup order for test
            order.Add(new Drink("Coke", 25, 1));

            // Check that no change is returned and the inventory and register of vending machine are updated
            Assert.IsTrue(vendingMachine.Buy(payment, order).Count == 0);
            Assert.AreEqual(0, vendingMachine.Inventory[0].Stock);
            Assert.AreEqual(2, vendingMachine.Register[0].Amount);
        }

        [Test]
        public void SuccessfulOrderWithChange() {
            // Update vending machine with new drink and coin for the change
            vendingMachine.Register.Add(new Coin(10, 1, "Dime"));
            vendingMachineInventory.Add(new Drink("Sprite", 15, 1));

            // Setup payment for test
            payment.Add(new Coin(25, 1, "Quarter"));

            // Setup order for test
            order.Add(new Drink("Sprite", 15, 1));

            // Check that change is returned and the inventory and register of vending machine are updated
            Assert.AreEqual(new Coin(10, 1, "Dime"), vendingMachine.Buy(payment, order)[0]);
            Assert.AreEqual(0, vendingMachine.Inventory.First(s => s.Name == "Sprite").Stock);
            Assert.AreEqual(1, vendingMachine.Register.First(s => s.Denomination == 25).Amount);
            Assert.AreEqual(0, vendingMachine.Register.First(s => s.Denomination == 10).Amount);
        }

        [Test]
        public void FailedOrderNotEnoughStock() {
            // Setup payment for test
            payment.Add(new Coin(25, 2, "Quarter"));

            // Setup order for test
            order.Add(new Drink("Coke", 25, 2));

            // Check that the order fails and no changes are made to the vending machine register and inventory
            Assert.Throws<Exception>(() => vendingMachine.Buy(payment, order));
            Assert.AreEqual(1, vendingMachine.Inventory.First(s => s.Name == "Coke").Stock);
            Assert.AreEqual(0, vendingMachine.Register.First(s => s.Denomination == 25).Amount);
        }

        [Test]
        public void FailedOrderNotEnoughChange() {
            // Update vending machine with new drink and coin for the change
            vendingMachine.Register.Add(new Coin(10, 1, "Dime"));
            vendingMachineInventory.Add(new Drink("Root Beer", 35, 2));

            // Setup payment for test
            payment.Add(new Coin(25, 2, "Quarter"));

            // Setup order for test
            order.Add(new Drink("Root Beer", 35, 1));

            // Check that the order fails and no changes are made to the vending machine register and inventory
            Assert.Throws<Exception>(() => vendingMachine.Buy(payment, order));
            Assert.AreEqual(2, vendingMachine.Inventory.First(s => s.Name == "Root Beer").Stock);
            Assert.AreEqual(0, vendingMachine.Register.First(s => s.Denomination == 25).Amount);
            Assert.AreEqual(1, vendingMachine.Register.First(s => s.Denomination == 10).Amount);
        }

        [Test]
        public void FailedOrderNotEnoughPayment() {
            // Update vending machine with new drink and coin for the change
            vendingMachineInventory.Add(new Drink("Root Beer", 25, 2));

            // Setup payment for test
            payment.Add(new Coin(25, 1, "Quarter"));

            // Setup order for test
            order.Add(new Drink("Root Beer", 25, 2));

            // Check that the order fails and no changes are made to the vending machine register and inventory
            Assert.Throws<Exception>(() => vendingMachine.Buy(payment, order));
            Assert.AreEqual(2, vendingMachine.Inventory.First(s => s.Name == "Root Beer").Stock);
            Assert.AreEqual(0, vendingMachine.Register.First(s => s.Denomination == 25).Amount);
        }
    }
}