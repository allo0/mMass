using System;// math(implemented in System)
using System.Text.RegularExpressions; // re

//stoper
//obj_compound

namespace mMass
{
    internal class modBasics
    {
        public const double ELECTRON_MASS = 0.00054857990924d;
        public const string FORMULA_PATTERN = @"^(([\(])*(([A-Za-z0-2])(\{[\d]+\})?(([\-][\d]+)|[\d]*))+([\)][\d]*)*)*$";
        public const string ELEMENT_PATTERN = @"([A-Za-z0-2])(?:\{([\d]+)\})?([\-]?[\d]*)";

        public void move()
        {
            double[] agentMass = new double[2];
            string agentFormula = "H";
            int agentCharge = 1;

            obj_compound searcher = new obj_compound(); //for the isinstace
            agentMass[0] = searcher.mass().Item1;
            agentMass[1] = searcher.mass().Item2;

            string input2 = "8";
            string input3 = "C26H48N8O8";
            Match match2 = Regex.Match(input3, FORMULA_PATTERN);

            Console.WriteLine(match2.Success);
            string key = match2.Groups[0].Value;
            Console.WriteLine(key);
            Console.WriteLine(TextTool.CountStringOccurrences(input3, input2));

            /*
            atom = new string[2] { "Hi", "HO" };
            Console.WriteLine(atom.Length);
            */
            for (int i = 0; i < TextTool.CountStringOccurrences(input3, input2); i++)
            {
                Console.WriteLine("...");
            }
        }

        public float delta(float measuredMass, float countedMass, string units)
        {
            /*
             *      Calculate error between measured Mass and counted Mass in specified units.
             *       measuredMass (float) - measured mass
             *       countedMass (float) - counted mass
             *       units (Da, ppm or %) - error units
             *
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

        public float mz(/*double mass,*/int charge, int currentCharge, string agentFormula, int agentCharge, int massType)
        {
            /*
             *
             *  Calculate m/z value for given mass and charge.
             *  mass (tuple of (Mo, Av) or float) - current mass
             *  charge (int) - final charge of ion
             *  currentCharge (int) - current mass charge
             *  agentFormula (str or mspy.compound) - charging agent formula
             *  agentCharge (int) - charging agent unit charge
             *  massType (0 or 1)(false or true) - used mass type if mass value is float, 0(false) = monoisotopic, 1(true) = average
             *
             */

            double[] agentMass = new double[2];
            double[] mass = new double[2];

            currentCharge = 0;
            agentFormula = "H";
            agentCharge = 1;
            massType = 0;

            obj_compound searcher = new obj_compound(); //for the isinstace

            //check agent formula
            if (!(agentFormula == "e") && !agentFormula.Equals(searcher.func_meto_compound()))   //isinstance comp
            {
                agentFormula = searcher.func_meto_compound();
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
                agentMass[1] = searcher.mass().Item2;
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

            //recalculate zero charge
            int agentCount = currentCharge / agentCharge;
            if (currentCharge != 0)
            {
                //  if () {
                //  } // na to κανω αφου κανω τους Constructors στην obj_compound
                //  else
                mass[0] = searcher.mass().Item1;
                mass[1] = searcher.mass().Item2;
                for (int i = 0; i < 2; i++)
                    mass[i] = mass[i] * Math.Abs(currentCharge) - agentMass[massType] * agentCount;

                //OR
                // else
                // mass = searcher.mass().Item1 + searcher.mass().Item2;
            }
            if (charge == 0)
                return 1; //return mass prepei na kanw na to dw pws;

            // calculate final charge
            agentCount = charge / agentCharge;
            /*

             */
            return 0;
        }
    }
}