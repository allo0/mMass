using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace mMass
{
    internal class obj_compound     //Compound object definition.
    {
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private object[] atom = new object[2];

        public Dictionary<string, element> elements = new Dictionary<string, element>();
        public string name;
        public double vale;
        public string expression;
        public string _composition;
        public string _formula;
        public double _mass; //paizei na einai  double[]
        public double _nominalmass;
        private string compound;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public obj_compound()
        {
        }

        public obj_compound(string expression)  /*, Dictionary<string, double> attr)*/
        {
            string name = this.name;
            double vale = this.vale;

            // check formula
            this._checkFormula(expression);
            this.expression = expression;

            //buffers
            this._composition = null;
            this._formula = null;
            this._mass = 0;
            this._nominalmass = 0;

            //get additional attributes
            //attr = new Dictionary<string, double>();
            //for (int i = 0; i < attr.Count; i++)        //for name, value in attr.items():
            //{                                           //  self.attributes[name] = value
            //    for (int j = 0; j < i; j++)
            //    {
            //        attr.Add(name, vale);
            //    }
            //}
        }

        public string _iadd_(string other)//Append formula.
        {
            //check and append value
            if (other.Equals(compound))
            {
                this.expression = this.expression + other;
            }
            else
            {
                _checkFormula(other);
                this.expression = this.expression + other;
            }

            //clear buffers
            reset();

            return expression;
        }

        public void reset() //Clear formula buffers.
        {
            this._composition = null;
            this._formula = null;
            this._mass = 0;
            this._nominalmass = 0;
        }

        //---------------------------
        // Getters
        //---------------------------

        public int count(string item, bool groupIsotopes = false)//Count atom in formula.
        {
            element ele = new element();
            string atom = null;

            int atomsCount = 0;

            // get composition prob dictionary instead of string prob dictionary instead of string
            string comp = this.composition();

            //get atoms to count
            var atoms = new List<string>();
            if (groupIsotopes && item != null)// not so sure       if groupIsotopes and item in blocks.elements:
            {
                foreach (var massNo in ele.isotopess)//blocks.elements[item].isotopes:  den yparxei ele.item
                {
                    atom = item + massNo;//atom = '%s{%d}' % (item,massNo)
                    atoms.Insert(0, atom);
                }
            }

            //count atom
            for (int i = 0; i < atoms.Count; i++)
            {
                if (comp.Contains(atom))
                {
                    atomsCount += comp[i];
                }
            }

            return atomsCount;
        }

        public string formula() //Get Formula
        {
            if (this._formula != null)
            {
                return this._formula;
            }

            this._formula = "";

            //get composition
            string comp = this.composition();//na to kanw

            return comp;
        }

        public string composition() //na thn ftiaksw einai terma adeia
        {   //Get elemental composition
            int count = 0;
            string symbol = "";
            string isotop = "";

            //check composition buffer
            if (this._composition != null)
            {
                return this._composition;
            }

            // unfold brackets
            string unfoldedFormula = this._unfoldBrackets(this.expression);

            //group elements
            Dictionary<string, string> composition = new Dictionary<string, string>();
            for (int i = 0; i < TextTool.CountStringOccurrences(modBasics.ELEMENT_PATTERN, unfoldedFormula); i++)
            {
                //make atom
                if (isotop != null)
                {
                }
            }

            string x = "a";
            return x;
        }

        public Tuple<double, double> mass(int massType = 0)//Get Mass
        {
            int count = 0;
            double[] atomMass = new double[2];
            double massMo = 1.0;  // αλλαζω τις τιμές,τις έχω μονο γia debug
            double massAv = 2;  // αλλαζω τις τιμές,τις έχω μονο γia debug
            double isotope = 3.2;

            //var _mass = Tuple.Create<double, double>(massMo, massAv);
            double[] _mass = new double[2];
            double masss = _mass[0] + _mass[1];

            string match;
            string comp = composition();    // get composition prob dictionary instead of string prob dictionary instead of string
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
            //massNumber = match;
            massNumber = 123.34;
            tmp = match;
            if (massNumber != null)
            {
                isotope = ele.symbols[(int)massNumber];    // blocks.elements[symbol].isotopes[int(massNumber)] // line 200 in python
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

        public double rdbe(string compound)
        {
            return rdbe(compound);
        }

        public bool isvalid(int charge = 0, string agentFormula = "H", int agentCharge = 1)//Check ion composition
        {
            //check agent formula
            if (!(agentFormula == "e") && !agentFormula.Equals(expression))   //isinstance comp
            {
                agentFormula = expression;
            }
            //make ion compound
            if (charge != 0 && !(agentFormula == "e"))
            {
                string ionFormula = this.expression;
            }
        }

        public bool frules(List<string> rules, Tuple<double, double> HC, Tuple<int, int, int, int> NOPSC, Tuple<int, int> RDBE)//Check formula rules.
        {
            modBasics mb = new modBasics();
            rules = new List<string>() { "HC", "NOPSC", "NOPS", "RDBE" };
            HC = new Tuple<double, double>(0.1, 3.0);
            NOPSC = new Tuple<int, int, int, int>(4, 3, 2, 3);
            RDBE = new Tuple<int, int>(-1, 40);

            return mb.frules(compound, rules, HC, NOPSC, RDBE);
        }

        //---------------------------
        //Modifier(s)
        //---------------------------

        public void negate()//Make all atom counts negative
        {
            // get composition prob dictionary instead of string
            string comp = composition();    // get composition prob dictionary instead of string prob dictionary instead of string

            //negate composition
            string formula = "";
            //   foreach (var el in comp.Keys())
            {
                //       formula += el + comp[el] * -1;         // '%s%d' % (el, -1 * comp[el])
            }
            this.expression = formula;

            //clear buffers
            this.reset();
        }

        //---------------------------
        //Helpers
        //---------------------------

        public void _checkFormula(string formula)//Check given formula.
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
                if (!ele.symbols.Contains(atom[0].ToString())) //δεν ξέρω ακομα αν ειναι το symbol ή απο το isotopes το πρώτο ορισμα
                {
                    throw new ArgumentException(String.Format("Unknown element in formula! -->{0} in {1}-", atom[0], formula));
                }
                else if (!ele.symbols.Contains(atom[0].ToString()))
                {
                    atom[1] = ele.isotopess;
                    // ele.isotopes[]
                }
            }
        }

        public string _unfoldBrackets(string str) //Unfold formula and count each atom.
        {
            string unfoldedFormula = "";
            int[,] brackets = new int[1, 2];//instead of a list its a 1x2 array
            string enclosedFormula = "";

            int i = 0;
            while (i < str.Length)
            {
                //handle brackets
                if (str[i].Equals("("))
                {
                    brackets[0, 0] += 1;
                }
                else if (str[i].Equals(")"))
                {
                    brackets[0, 1] += 1;
                }

                //part outside brackets
                if (brackets[0, 0] == 0 || brackets[0, 1] == 0)
                {
                    unfoldedFormula += str[i];
                }
                // part within brackets
                else
                {
                    enclosedFormula += str[i];
                    //unfold part within brackets
                    if (brackets[0, 0] == brackets[0, 1])
                    {
                        string trimmed = str.Substring(1, str.Length - 1);
                        enclosedFormula = this._unfoldBrackets(trimmed);

                        //multiply part within brackets
                        string count = "";
                        bool b1 = TextTool.IsNumeric(str[i + 1]); //true
                        while (str.Length > (i + 1) && b1)
                        {
                            count += str[i + 1];
                            i += 1;
                        }
                        if (count != null)
                        {
                            int count_num = int.Parse(count);  //int(count)
                            enclosedFormula = String.Concat(Enumerable.Repeat(enclosedFormula, count_num));
                        }
                        // add and clear
                        unfoldedFormula += enclosedFormula;
                        enclosedFormula = "";
                        brackets[0, 0] = 0;
                        brackets[0, 1] = 0;
                    }
                }
                i += 1;
            }
            return unfoldedFormula;
        }
    }
}