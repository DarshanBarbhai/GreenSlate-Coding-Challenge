using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksMachineAppModel.Interfaces
{
    /// <summary>
    /// Interface meant to define what vending machines are, can be implemented as required. Interface was made due to the different
    /// kind of vending types that may need to be implemented in the future as specified by the documentation
    /// </summary>
    public interface IVendingMachine
    {
        /// <summary>
        /// The inventory of products that the Vending Machine holds.
        /// </summary>
        public List<IProduct> Inventory { get; set; }

        /// <summary>
        /// Contains the coins that are in the vending machine.
        /// </summary>
        public List<ICoin> Register { get; set; }

        /// <summary>
        /// Buy desired products from the vending machine with a given payment.
        /// </summary>
        /// <param name="payment">The coins to pay for the desired products.</param>
        /// <param name="desiredProducts">The products to be bought.</param>
        /// <returns>Change as a list of Coins if there is any, empty list returned otherwise.</returns>
        public List<ICoin> Buy(List<ICoin> payment, List<IProduct> desiredProducts);
    }
}
