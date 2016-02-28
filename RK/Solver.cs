using System;
using System.Globalization;
using System.IO;

namespace RK
{
    public static class Solver
    {
        public const double Pi = Math.PI;

        public static double[] InitialConditions(double phi0, double theta0, double psi0)
        {
            var sPh0 = Math.Sin(phi0 * Pi / 2);
            var sPs0 = Math.Sin(psi0 * Pi / 2);
            var sTh0 = Math.Sin(theta0 * Pi / 2);
            var cPh0 = Math.Cos(phi0 * Pi / 2);
            var cPs0 = Math.Cos(psi0 * Pi / 2);
            var cTh0 = Math.Cos(theta0 * Pi / 2);

            var lambda0 = cPh0*cPs0*cTh0 + sPh0*sPs0*sTh0;
            var lambda1 = sPh0*cPs0*cTh0 - cPh0*sPs0*sTh0;
            var lambda2 = cPh0*cPs0*sTh0 + sPh0*sPs0*cTh0;
            var lambda3 = cPh0*sPs0*cTh0 - sPh0*cPs0*sTh0;
            return new[] { lambda0, lambda1, lambda2, lambda3, 0, 1, 0 };
            
        }

        private static double Psi(this double[] y)
        {
            var alpha1 = y[0] * y[0] + y[1] * y[1] - y[2] * y[2] - y[3] * y[3];
            var beta1 = 2 * (y[1] * y[2] + y[0] * y[3]);
            return Math.Abs(Math.Atan(beta1 / alpha1));
        }

        private static double Phi(this double[] y)
        {
            var gamma2 = 2 * (y[2] * y[3] + y[0] * y[1]);
            var gamma3 = y[0] * y[0] - y[1] * y[1] - y[2] * y[2] + y[3] * y[3];
            return Math.Abs(Math.Atan(gamma2 / gamma3));
        }

        private static double Theta(this double[] y)
        {
            var gamma1 = 2 * (y[1] * y[3] - y[0] * y[2]);
            return Math.Abs(-Math.Asin(gamma1));
        }

        static void SolveOnePoint(double tInit, double[] yInit, 
            double epsilon, double delta, 
            double timePeriod, double timeStep)
        {
            var rk4 = new GravitationalMomentEquations(epsilon, delta);
            rk4.SetInit(tInit, yInit);
            
            while (rk4.GetTime() < timePeriod)
            {
                //rk4.Y
                rk4.NextStep(timeStep);
            }
        }
    }
}