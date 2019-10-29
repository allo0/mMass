using System;
using System.Data;
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

        public int atomicNumber;
        public int valence;
        public double[] isotop;
        public double[] mass = new double[2];
        public string name;
        public string symbol;
        public object[] mass_abud = new object[2];//for the isotope dictionary

                                                  // public Dictionary<double, object> isotopes = new Dictionary<double, object>();
        public DataTable isotopes = new DataTable();

        public element()
        {
            int atomicNumber = this.atomicNumber;
            int valence = this.valence;

            string name = this.name;
            string symbol = this.symbol;
            // Dictionary<double, object> isotopes = new Dictionary<double, object>();

            // Dictionary<double, object>.ValueCollection valueColl = isotopes.Values;

            double[] isotop = this.isotop;

            // init masses
            double massMo = 0;
            double massAv = 0;
            double maxAbundance = 0;
            double[] mass = new double[2];
            mass = this.mass;
            mass_abud[0] = mass;
            mass_abud[1] = maxAbundance;
            DataTable isotopes = new DataTable();
            isotopes.Columns.Add("Mass Number", typeof(double[]));
            isotopes.Columns.Add("Mass_Abud", typeof(object));
            isotopes = this.isotopes;
            isotopes.Rows.Add(mass, mass_abud);

            foreach (double cnt in isotopes.Columns)
            {
                massAv += isotop[0] * isotop[1];
                if (maxAbundance < isotop[1])
                {
                    massMo = isotop[0];
                    maxAbundance = isotop[1];
                }
            }
            if (massMo == 0 || massAv == 0)
            {
            }
            mass[0] = massMo;
            mass[1] = massAv;
            //mass = Tuple.Create<double, double>(massMo, massAv);//self.mass = (massMo, massAv)
        }
    }
}