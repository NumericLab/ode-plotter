using System;
using System.Collections.Generic;
using System.Linq;

namespace ODEPlotter.Model
{
    public class OdeIntegrationType
    {
        public static List<string> Types = Enum.GetNames(typeof (TypesEnum)).ToList();

        private enum TypesEnum
        {
            RungeKutta = 1,
            Second = 2
        }
    }
}
