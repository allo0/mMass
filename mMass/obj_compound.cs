using System;
using System.Text.RegularExpressions;

namespace mMass
{
    internal class obj_compound
    {
        private object[] atom = new object[2];

        public obj_compound(/*string expression, List<string> atrr*/)
        {
        }

        public string func_meto_compound()
        {
            string agentFormulae = "Hollla";

            return agentFormulae;
        }

        public Tuple<double, double> mass() //Get Mass
        {
            int massType = 0;   // αλλαζω τις τιμές,τις έχω μονο γia debug
            int count = 0;
            double[] atomMass = new double[2];
            double massMo = 1.0;  // αλλαζω τις τιμές,τις έχω μονο γia debug
            double massAv = 2;  // αλλαζω τις τιμές,τις έχω μονο γia debug
            double isotope = 3.2;

            //var _mass = Tuple.Create<double, double>(massMo, massAv);
            double[] _mass = new double[2];
            double masss = _mass[0] + _mass[1];

            string match;
            string comp = composition();    // get composition
            string symbol;
            double massNumber; //με μια μικρή επιφύλαξη για τον τύπο
            string tmp; //με μια μικρή επιφύλαξη για τον τύπο

            atom[0] = "C38H65N11O11S";//8a fygei apla gia testing;
            Match matchEL = Regex.Match(atom[0].ToString(), modBasics.ELEMENT_PATTERN);
            element ele = new element();

            //get mass
            if (_mass == null)
            {
                massMo = 0;
                massAv = 0;
            }
            //get mass for each atom
            for (int i = 0; i < TextTool.CountStringOccurrences(modBasics.ELEMENT_PATTERN, comp); i++)
            {
                count = comp[i];
            }

            //check specified isotope and mass
            match = matchEL.Groups[0].Value;
            symbol = match;
            // massNumber = match;
            massNumber = 123.34;
            tmp = match;
            if (massNumber != null)
            {
                //  isotope = ele.isotopes; // blocks.elements[symbol].isotopes[int(massNumber)]   line 200 in python
                atomMass[0] = isotope;
                atomMass[1] = isotope;
            }
            else
            {
                atomMass[0] = ele.mass[0];
                atomMass[1] = ele.mass[1];
            }

            // multiply atom mass
            massMo += atomMass[0] * count;
            massAv += atomMass[1] * count;

            //store mass in buffer
            _mass[0] = massMo;
            _mass[1] = massAv;

            //return mass
            if (massType == 0)
            {
                massAv = 0;
                _mass[0] = massMo;
                _mass[1] = massAv;
                return Tuple.Create<double, double>(massMo, massAv);
            }
            if (massType == 1)
            {
                massMo = 0;
                _mass[0] = massMo;
                _mass[1] = massAv;
                return Tuple.Create<double, double>(massMo, massAv);
            }
            else
            {
                return Tuple.Create<double, double>(massMo, massAv);
            }
        }

        public void _checkFormula(string formula)
        {
            Match match2 = Regex.Match(formula, modBasics.FORMULA_PATTERN);
            element ele = new element();

            //check formula
            if (!match2.Success)
            {
                throw new ArgumentException(String.Format("Wrong formula -->", formula));
            }

            for (int i = 0; i < TextTool.CountStringOccurrences(modBasics.ELEMENT_PATTERN, formula); i++)
            {
                //check elements and isotopes
                if (!ele.symbol.Contains(atom[0].ToString())) //δεν ξέρω ακομα αν ειναι το symbol ή απο το isotopes το πρώτο ορισμα
                {
                    throw new ArgumentException(String.Format("Unknown element in formula! -->{0} in {1}-", atom[0], formula));
                }
                else if (!ele.symbol.Contains(atom[0].ToString()))
                {
                    atom[1] = ele.isotopes;
                    //  ele.isotopes[]
                }
            }
        }

        public string composition() //na thn ftiaksw einai terma adeia
        {
            string x = "a";
            return x;
        }
    }

    /// <summary>
    /// Contains static text methods.
    /// Put this in a separate class in your project.
    /// </summary>
    public static class TextTool
    {
        /// <summary>
        /// Count occurrences of strings.
        /// </summary>
        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }
    }
}