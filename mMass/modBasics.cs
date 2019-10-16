using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions; // re
using System;// math(implemented in System)
using System.Linq;
using System.Collections;


//stoper
//obj_compound


namespace mMass
{
    class modBasics:obj_compound
    {
        const float ELECTRON_MASS = 0.00054857990924f;
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

           

           
            /* var base1 = new obj_compound();
             var agentFormulas = base1.GetType();
             Console.WriteLine("Is agetnFormulas an instance of BaseClass? {0}",
                             agentFormulas.IsInstanceOfType(base1));
             if ( !(agentFormulae=="e") && agentFormulas.IsInstanceOfType(base1))
             {
                 Console.WriteLine("This is a detest" );
             }*/

             obj_compound searcher = new obj_compound();
             string agentFormulae = "H";
            if (!(agentFormulae == "e") && !agentFormulae.Equals(searcher.hi()))
                Console.WriteLine("holla {0}",
                        searcher.hi().GetType() );

           
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

            if (units == "ppm")
                return (measuredMass - countedMass) / countedMass * 1000000;
            else if (units == "Da")
                return (measuredMass - countedMass);
            else if (units == "%")
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
            obj_compound searcher = new obj_compound(); //for the isinstace


            if( !(agentFormula == "e") && !agentFormula.Equals(searcher.hi())   ) //isinstance comp
            {
                //
            }
            if (agentFormula == "e")
            {
                float[] agentMass = new float[1];
                agentMass[0] = ELECTRON_MASS;
                agentMass[1] = ELECTRON_MASS;
            }
            else
            {

            }

            int agentCount = currentCharge / agentCharge;
            if (currentCharge != 0)
            {

            }
            


            return 0;
        }


    }
}
