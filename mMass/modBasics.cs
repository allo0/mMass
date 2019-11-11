using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace mMass
{
    internal class modBasics : element
    {
        public const double ELECTRON_MASS = 0.00054857990924d;
        public const string FORMULA_PATTERN = @"^(([\(])*(([A-Za-z0-2])(\{[\d]+\})?(([\-][\d]+)|[\d]*))+([\)][\d]*)*)*$";
        public const string ELEMENT_PATTERN = @"([A-Za-z0-2])(\{[\d]+\})?(([\-][\d]+)|[\d]*)";

        public double massMo;
        public double massAv;

        public void move()//Testing Funciton
        {//string name, string symbol, int atomicNumber, Dictionary<double, mass_abud> isotopes, int valence
         //  element ele = new element();
         //Console.WriteLine(elements.Values);
            foreach (KeyValuePair<string, element> pair in elements)
            {
                foreach (var pairs in isotopess)
                {
                    Console.WriteLine("FOREACH KEYVALUEPAIR: {0}, {1}", pair.Key, pair.Value.atomicNumbers);
                }
            }
            var list = new List<mass_abud>(elements["Ag"].isotopess.Values);
            foreach (var val in list)
            {
                Console.WriteLine("KEY FROM LIST: " + val.mass);
            }
            Console.WriteLine(elements["Ag"].symbols);
        }

        //-----------------------------
        //BASIC FUNCTIONS
        //-----------------------------

        public float delta(float measuredMass, float countedMass, string units = "ppm")
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

        public double mz(Tuple<double, double> mass, int charge, int currentCharge = 0, int agentCharge = 1, int massType = 0, string agentFormula = "H")
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
            mass = new Tuple<double, double>(massMo, massAv);
            double masssum;
            double[] table_for_massTuple = new double[2];// to xrhsimopoiw sto else:  mass = mass * abs(currentCharge) - agentMass[massType] * agentCount

            //currentCharge = 0;
            //agentFormula = "H";
            // agentCharge = 1;
            // massType = 0;

            obj_compound searcher = new obj_compound(agentFormula); //for the isinstace

            //check agent formula
            if (!(agentFormula == "e") && !agentFormula.Equals(searcher.expression))   //isinstance comp
            {
                agentFormula = searcher.expression;
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
                }
            }

            //recalculate zero charge
            int agentCount = currentCharge / agentCharge;
            if (currentCharge != 0)
            {
                if (mass != null)//not so sure
                {
                    massMo = mass.Item1 * Math.Abs(currentCharge) - agentMass[0] * agentCount;
                    massAv = mass.Item2 * Math.Abs(currentCharge) - agentMass[1] * agentCount;
                    mass = new Tuple<double, double>(searcher.mass().Item1, searcher.mass().Item2);
                }
                else
                    table_for_massTuple[0] = mass.Item1 * Math.Abs(currentCharge) - agentMass[massType] * agentCount;
                table_for_massTuple[1] = mass.Item2 * Math.Abs(currentCharge) - agentMass[massType] * agentCount;
                mass = new Tuple<double, double>(table_for_massTuple[0], table_for_massTuple[1]);
            }

            if (charge == 0)
                return masssum = mass.Item1 + mass.Item2; //return  kanw ena masss pou einai mass[]+mass[]

            // calculate final charge
            agentCount = charge / agentCharge;
            if (mass != null)//not so sure
            {
                massMo = (mass.Item1 + agentMass[0] * agentCount) / Math.Abs(charge);
                massAv = (mass.Item2 + agentMass[1] * agentCount) / Math.Abs(charge);

                return masssum = mass.Item1 + mass.Item2;// auto emeine
            }
            else
                return (mass.Item1 + mass.Item2 + agentMass[massType] * agentCount) / Math.Abs(charge);
        }

        public double md(double mass, string mdType = "standard", string kendrickFormula = "CH2", string rounding = "floor")
        {
            //Calculate mass defect for given monoisotopic mass.
            //mass(double) - monoisotopic mass
            //mdType(fraction | standard | relative | kendrick) - mass defect type
            //kendrickFormula(str) - kendrick group formula
            //rounding(floor | ceil | round) - nominal mass rounding function

            obj_compound searcher = new obj_compound(kendrickFormula); //for the isinstace
            double kendrickF = 0;//svinw to 0 meta

            //return fractional part
            if (mdType == "fraction")
            {
                return mass - Math.Floor(mass);
            }

            //return standard mass defect
            else if (mdType == "standard")
            {
                return mass - nominalmass(mass, rounding);
            }

            //return relative mass defect
            else if (mdType == "relative")
            {
                return (10 ^ 6) * (mass - nominalmass(mass, rounding)) / mass;
            }

            //return Kendrick mass defect
            else if (mdType == "kendrick")
            {
                if (!kendrickFormula.Equals(searcher.expression))
                {
                    kendrickFormula = searcher.expression;
                }
                // kendrickF = kendrickFormula.nominalmass() / kendrickFormula.mass(0);      // na ftiaksw thn Nominalmass
                return nominalmass(mass * kendrickF, rounding) - (mass * kendrickF);
            }
            else
                throw new ArgumentException(String.Format("Unknown  mass defect type! --> ", mdType));
        }

        public double nominalmass(double mass, string rounding = "floor")
        {
            //Calculate for given monoisotopic mass and rounding function.
            //mass(float) - monoisotopic mass
            //rounding(floor | ceil | round) - nominal mass rounding function

            if (rounding == "floor")
            {
                return Math.Floor(mass);
            }
            else if (rounding == "ceil")
            {
                return Math.Ceiling(mass);
            }
            else if (rounding == "round")
            {
                return Math.Round(mass);
            }
            else
            {
                throw new ArgumentException(String.Format("Unknown nominal mass rounding! --> ", rounding));
            }
        }

        //-----------------------------
        //FORMULA FUNCTIONS
        //-----------------------------

        public double rdbe(string compound)
        {
            //Get RDBE(Range or Double Bonds Equivalents) of a given compound.
            //compound(str or mspy.compound) - compound

            //check compound
            obj_compound searcher = new obj_compound(compound); //for the isinstace
            if (!(compound.Equals(searcher.expression)))
            {
                compound = searcher.expression;
            }

            //get composition
            string comp = searcher.composition();

            // get atoms from composition
            List<string> atoms = new List<string>();
            foreach (var item in comp)
            {
                Match match = Regex.Match(item.ToString(), comp);

                if (match != null && match.Groups[0].Value == null)//8a einai group[0] h group[1]
                {
                    atoms.Add(match.Groups[0].Value);
                }
            }

            //get rdbe
            double rdbeValue = 0.0;
            foreach (var a in atoms)
            {
                int valence = element.elements[a].valences;
                if (valence != 0)
                {
                    rdbeValue += (valence - 2) * searcher.count(a, true);
                }
            }
            rdbeValue /= 2.0;
            rdbeValue += 1.0;

            return rdbeValue;
        }

        public bool frules(string compound, List<string> rules, Tuple<double, double> HC, Tuple<int, int, int, int> NOPSC, Tuple<int, int> RDBE)
        {
            //Check formula rules for a given compound.
            //compound(str or mspy.compound) - compound
            //rules(list of str) - rules to be checked
            //HC(tuple) - H / C limits
            //NOPSC(tuple) - NOPS / C max values
            //RDBE(tuple) - RDBE limits

            rules.Add("HC");
            rules.Add("NOPSC");
            rules.Add("NOPS");
            rules.Add("RDBE");
            rules.Add("RDBEInt");
            HC = Tuple.Create<double, double>(0.1, 3.0);
            NOPSC = Tuple.Create<int, int, int, int>(4, 3, 2, 3);
            RDBE = Tuple.Create<int, int>(-1, 40);

            //check compound
            obj_compound searcher = new obj_compound(compound); //for the isinstace
            if (!(compound.Equals(searcher.expression)))
            {
                compound = searcher.expression;
            }

            //get element counts
            double countC = searcher.count("C", true);
            double countH = searcher.count("H", true);
            double countN = searcher.count("N", true);
            double countO = searcher.count("P", true);
            double countP = searcher.count("P", true);
            double countS = searcher.count("S", true);

            double ratioHC = 0;
            double ratioNC = 0;
            double ratioOC = 0;
            double ratioPC = 0;
            double ratioSC = 0;

            //get carbon ratios
            if (countC != 0)
            {
                ratioHC = countH / countC;
                ratioNC = countN / countC;
                ratioOC = countO / countC;
                ratioPC = countP / countC;
                ratioSC = countS / countC;
            }

            //get RDBE
            double rdbeValue = rdbe(compound);

            //check HC rule
            if (rules.Contains("HC") && countC != 0)
                if (ratioHC < HC.Item1 || ratioHC > HC.Item2)
                    return false;

            // check NOPSC rule
            if (rules.Contains("NOPSC") && countC != 0)
                if (ratioNC > NOPSC.Item1 || ratioOC > NOPSC.Item2 || ratioPC > NOPSC.Item3 || ratioSC > NOPSC.Item4)
                    return false;

            //check NOPS all > 1 rule
            if (rules.Contains("NOPSC") && (countN > 1 && countO > 1 && countP > 1 && countS > 1))
                if (countN >= 10 || countO >= 20 || countP >= 4 || countS >= 3)
                    return false;

            //check NOP all > 3 rule
            if (rules.Contains("NOPS") && (countN > 3 && countO > 3 && countP > 3))
                if (countN >= 11 || countO >= 22 || countP >= 6)
                    return false;

            //check NOS all > 1 rule
            if (rules.Contains("NOPS") && (countN > 1 && countO > 1 && countS > 1))
                if (countN >= 19 || countO >= 14 || countS >= 8)
                    return false;

            //check NPS all > 1 rule
            if (rules.Contains("NOPS") && (countN > 1 && countP > 1 && countS > 1))
                if (countN >= 3 || countP >= 3 || countS >= 3)
                    return false;

            //check OPS all > 1 rule
            if (rules.Contains("NOPS") && (countO > 1 && countP > 1 && countS > 1))
                if (countO >= 14 || countP >= 3 || countS >= 3)
                    return false;

            //check RDBE range
            if (rules.Contains("RDBE"))
                if (rdbeValue < RDBE.Item1 || rdbeValue > RDBE.Item2)
                    return false;

            //check integer RDBE
            if (rules.Contains("RDBE"))
                if (rdbeValue % 1 != 0)
                    return false;

            //all guci
            return true;
        }
    }
}