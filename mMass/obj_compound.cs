using System;
using System.Text.RegularExpressions;

namespace mMass
{
    internal class obj_compound
    {
        protected string atom;  //θα αλλάξει πιο μετά ο τύπος του atom

        public obj_compound(/*string expression, List<string> atrr*/)
        {
        }

        public string func_meto_compound()
        {
            string agentFormulae = "Hollla";

            return agentFormulae;
        }

        public double he()
        {
            double[] agentMass = new double[2];
            agentMass[0] = 1.123145;
            agentMass[1] = 2.231416;

            return agentMass[1];
        }

        public Tuple<double, double> mass() //Get Mass
        {
            int massType = 0;   // αλλαζω τις τιμές,τις έχω μονο γia debug
            int count = 0;

            double massMO = 1;  // αλλαζω τις τιμές,τις έχω μονο γia debug
            double massAV = 2;  // αλλαζω τις τιμές,τις έχω μονο γia debug
            double isotope;

            var mass = Tuple.Create<double, double>(massMO, massAV);

            string match;
            string comp = composition();    // get composition
            string symbol;
            string massNumber; //με μια μικρή επιφύλαξη για τον τύπο
            string tmp; //με μια μικρή επιφύλαξη για τον τύπο

            atom = "C26H";//8a fygei apla gia testing;
            Match matchEL = Regex.Match(atom, modBasics.ELEMENT_PATTERN);
            // element ele = new element();

            //get mass
            if (mass == null)
            {
                massMO = 0;
                massAV = 0;
            }
            //get mass for each atom
            for (int i = 0; i < TextTool.CountStringOccurrences(modBasics.ELEMENT_PATTERN, comp); i++)
            {
                count = comp[i];
            }

            //check specified isotope and mass
            match = matchEL.Groups[0].Value;
            symbol = match;
            massNumber = match;
            tmp = match;
            if (massNumber != null)
            {
                //  isotope = ele.    //it's blocks.elements[symbol].isotopes[int(massNumber)] in python
            }

            //return mass
            if (massType == 0)
            {
                massAV = 0;
                mass = Tuple.Create<double, double>(massMO, massAV);
                return mass;
            }
            if (massType == 1)
            {
                massMO = 0;
                mass = Tuple.Create<double, double>(massMO, massAV);
                return mass;
            }
            else
                return mass;
        }

        public void _checkFormula(string formula)
        {
            Match match2 = Regex.Match(formula, modBasics.FORMULA_PATTERN);

            if (!match2.Success)
            {
                throw new ArgumentException(String.Format("Unknown formula -->", formula));
            }
            //atom.Length;
            for (int i = 0; i < TextTool.CountStringOccurrences(modBasics.ELEMENT_PATTERN, formula); i++)
            {
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