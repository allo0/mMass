using System.Collections.Generic;

//using System.IO;
//using System.Xml;

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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public static string name { get; set; }

        public static string symbol { get; set; }
        public static int atomicNumber { get; set; }
        public static int valence { get; set; }

        public double[] mass = new double[2];
        public static Dictionary<double, mass_abud> isotopes = new Dictionary<double, mass_abud>();

        public static mass_abud ma = new mass_abud();

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public element()
        {
            string name;
            string symbol;
            int atomicNumber;
            int valence;

            //Dictionary<double, mass_abud> isotopes = new Dictionary<double, mass_abud>();
            // isotopes = this.isotopes;

            // init masses
            double massMo = 0;
            double massAv = 0;
            double maxAbundance = 0;
            double[] mass = new double[2];
            double massMoAv = massMo + massAv;
            //mass = this.mass;

            mass_abud ma = new mass_abud(); //put in mass and mass_abud the vaules mass and max_abudance
            ma.mass = massMoAv;
            ma.mas_abud = maxAbundance;

            foreach (KeyValuePair<double, mass_abud> isotop in isotopes)  //for isotop in self.isotopes.values()
            {
                massAv += isotop.Value.mass * isotop.Value.mas_abud;
                if (maxAbundance < isotop.Value.mas_abud)
                {
                    massMo = isotop.Value.mass;
                    maxAbundance = isotop.Value.mas_abud;
                }
            }
            ma.mass = massMoAv;
            ma.mas_abud = maxAbundance;

            if (massMo == 0 || massAv == 0)
            {
                //line 50-60 pyton
            }
            mass[0] = massMo;
            mass[1] = massAv;   //self.mass = (massMo, massAv)
        }

        public element(string name, string symbol, int atomicNumber, Dictionary<double, mass_abud> isotopes, int valence)
        {
        }

        internal Dictionary<string, element> Elements = new Dictionary<string, element>()
        {
             {"Ac",new element( name="Actinium", symbol="Ac", atomicNumber=89, isotopes.Add(key:227,value:(227.02774700000001, 1.0)), valence = 3) }
        };
    }

    /*
     * Used in the Dictionary ,Holds the vlues of the mass and the abundance
     */

    internal class mass_abud
    {
        //internal double mass;
        //internal double mas_abud;

        public double mass { get { return mass; } set { value = mass; } }
        public double mas_abud { get; set; }
    }
}