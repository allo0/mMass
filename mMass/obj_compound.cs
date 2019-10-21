using System;
using System.Collections.Generic;
using System.Text;

namespace mMass
{
    class obj_compound
    {
        public obj_compound(string expression, List<string> atrr) { 
            //this.
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
        public Tuple<double, double> mass()
        {
            int massType = 0;
            double massMO = 1;
            double massAV = 2;
            var mass = Tuple.Create<double, double>(massMO,massAV);

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
    }
}
