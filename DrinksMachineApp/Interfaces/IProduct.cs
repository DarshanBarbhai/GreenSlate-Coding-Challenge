using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksMachineApp.Interfaces
{
    /// <summary>
    /// Interface meant to define what products are, can be implemented as required. Interface was made due to the different
    /// kind of product types that may need to be implemented in the future as specified by the documentation
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cost of the product.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// The number of the product in stock.
        /// </summary>
        public int Stock { get; set; }
    }
}
