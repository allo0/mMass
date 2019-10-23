using System.Collections.Generic;

namespace mMass
{
    internal class element
    {
        /*Element object definition.
        name: (str) name
        symbol: (str) symbol
        atomicNumber: (int) atomic number
        isotopes: (dict) dict of isotopes { mass number:(mass, abundance),...}
        valence: (int)
       */

        public int atomicNumber;
        public int valence;

        public string name;
        public string symbol;
        private Dictionary<int, string> isotopes = new Dictionary<int, string>();

        public element(string name, string symbol, int atomicNumber, int valence)
        {
            this.atomicNumber = atomicNumber;
            this.valence = valence;
            this.name = name;
            this.symbol = symbol;

            Dictionary<int, string> isotopes = new Dictionary<int, string>();
            // this.isotopes[]=isotopes[];
        }
    }
}