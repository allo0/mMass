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
        const double ELECTRON_MASS = 0.00054857990924d;
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

           double[] agentMass = new double[2];
           string agentFormula = "H";
           int agentCharge = 1;

            obj_compound searcher = new obj_compound(); //for the isinstace
            agentMass[0] = searcher.mass().Item1;
            agentMass[1] = searcher.mass().Item2;
            /* 
             if (agentMass[1] == 0)
                 agentMass[1] = ELECTRON_MASS;
             if (agentMass[0] == 0)
                 agentMass[0] = ELECTRON_MASS;
             Console.WriteLine("...{0}...", agentMass[0]);
             Console.WriteLine("...{0}...", agentMass[1]);
             for (int i = 0; i < 2; i++)
             {
                     agentMass[i] = agentMass[i] - agentCharge * ELECTRON_MASS;
                   //  Console.WriteLine("...{0}...", agentMass[i]);
             }*/
           

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
    
        
        public float mz(/*double mass,*/int charge, int currentCharge, string agentFormula, int agentCharge, int massType) {
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
           
            double[] agentMass = new double[2];
            double[] mass = new double[2]; 
            currentCharge = 0;
            agentFormula = "H";
            agentCharge = 1;
            massType = 0;
            obj_compound searcher = new obj_compound(); //for the isinstace

            //check agent formula
            if ( !(agentFormula == "e") && !agentFormula.Equals(searcher.func_meto_compound())   )   //isinstance comp
            {
                agentFormula = searcher.func_meto_compound();   //
            }

            // get agent mass
            if (agentFormula == "e")
            {
                agentMass[0] = ELECTRON_MASS;
                agentMass[1] = ELECTRON_MASS;
            }
            else
            {
              
               agentMass[0] = searcher.mass().Item1;
               agentMass[1]= searcher.mass().Item2;
                if (agentMass[1] == 0)
                    agentMass[1] = ELECTRON_MASS;
                if (agentMass[0] == 0)
                    agentMass[0] = ELECTRON_MASS;

                for (int i = 0; i < 2; i++)
                {
                    agentMass[i] = agentMass[i] - agentCharge * ELECTRON_MASS;
                    //  Console.WriteLine("...{0}...", agentMass[i]);
                }

            }

            int agentCount = currentCharge / agentCharge;
            if (currentCharge != 0)
            {
              //  if () {
              //  } // na to κανω αφου κανω τους Constructors στην obj_compound
              //  else
                    mass[0] = searcher.mass().Item1;
                mass[1] = searcher.mass().Item2;
                for(int i=0;i<2;i++)
                 mass[i] = mass[i] * Math.Abs(currentCharge) - agentMass[massType] * agentCount;
                 
                //OR
               // else
               // mass = searcher.mass().Item1 + searcher.mass().Item2;

            }
            if(charge==0)
              //  return


            return 0;
        }
    

    }
}
