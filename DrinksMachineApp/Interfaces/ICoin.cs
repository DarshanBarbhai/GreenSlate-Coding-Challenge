using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksMachineApp.Interfaces
{
    /// <summary>
    /// Interface meant to define what coins are, can be implemented as required. Interface was made due to the different
    /// kind of coin types that may need to be implemented in the future as specified by the documentation
    /// </summary>
    public interface ICoin
    {
        /// <summary>
        /// The denomination/value of a coin
        /// </summary>
        public int Denomination { get; set; }

        /// <summary>
        /// The amount of coins of this denomination
        /// </summary>
        public int Amount { get; set; }
    }
}
