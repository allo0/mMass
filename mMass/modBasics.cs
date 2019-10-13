using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions; // re
using System;// math(implemented in System)

//stoper
//obj_compound


namespace mMass
{
    class modBasics:obj_compound
    {
        const double ELECTRON_MASS = 0.00054857990924;
        const string FORMULA_PATTERN = @"^(
                                        ([\(])* # start parenthesis
                                        (
                                            ([A-Z][a-z]{0,2}) # atom symbol
                                            (\{[\d]+\})? # isotope
                                            (([\-][\d]+)|[\d]*) # atom count
                                        )+
                                        ([\)][\d]*)* # end parenthesis
                                        )*$";
        const string ELEMENT_PATTERN = @"([A-Z][a-z]{0,2}) # atom symbol
                                          (?:\{([\d]+)\})? # isotope
                                         ([\-]?[\d]*) # atom count";

      

        public void move()
        {
            obj_compound myBase = new obj_compound();
            modBasics myDerived = new modBasics();
            object o = myDerived;
            obj_compound b = myDerived;

            //if (String.Compare(agentFormula,"e")==0 && agentFormula.IsInstance)
            Console.WriteLine("mybase: Type is {0}", myBase.GetType());
            Console.WriteLine("myDerived: Type is {0}", myDerived.GetType());
            Console.WriteLine("object o = myDerived: Type is {0}", o.GetType());
            Console.WriteLine("MyBaseClass b = myDerived: Type is {0}", b.GetType());
        }

        public float delta(float measuredMass,float countedMass,string units)
        {
            /*
                 """Calculate error between measured Mass and counted Mass in specified units.
                      measuredMass (float) - measured mass
                      countedMass (float) - counted mass
                      units (Da, ppm or %) - error units
                  """
            */

            if (String.Compare(units, "ppm") == 1)
                return (measuredMass - countedMass) / countedMass * 1000000;
            else if (String.Compare(units, "Da") == 1)
                return (measuredMass - countedMass);
            else if (String.Compare(units, "%") == 1)
                return (measuredMass - countedMass) / countedMass * 100;
            else
            {
                throw new ArgumentException(String.Format("Unknown units for delta! -->", units));                        
            }

        }
    

        public float mz(float mass, int charge, int currentCharge, string agentFormula, int agentCharge, bool massType) {
            /*
                 """Calculate m/z value for given mass and charge.
                    mass (tuple of (Mo, Av) or float) - current mass
                    charge (int) - final charge of ion
                    currentCharge (int) - current mass charge
                    agentFormula (str or mspy.compound) - charging agent formula
                    agentCharge (int) - charging agent unit charge
                    massType (0 or 1)(false or true) - used mass type if mass value is float, 0(false) = monoisotopic, 1(true) = average
                """
             */
            currentCharge = 0;
            agentFormula = "H";
            agentCharge = 1;
            massType = false;



            return 0;
        }


    }
}
