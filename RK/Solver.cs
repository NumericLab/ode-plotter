using System;
using System.Globalization;
using System.IO;

namespace RK
{
    public static class Solver
    {
        static void SolveOnePoint(double tInit, double[] yInit, 
            double epsilon, double delta, 
            double timePeriod, double timeStep)
        {
            var rk4 = new GravitationalMomentEquations(epsilon, delta);
            rk4.SetInit(tInit, yInit);
            var write = new StreamWriter("Test.txt");

            while (rk4.GetTime() < timePeriod)
            {
                write.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", 
                    Convert.ToString(rk4.GetTime(), CultureInfo.InvariantCulture), 
                    Convert.ToString(rk4.Y[0], CultureInfo.InvariantCulture), 
                    Convert.ToString(rk4.Y[1], CultureInfo.InvariantCulture), 
                    Convert.ToString(rk4.Y[2], CultureInfo.InvariantCulture), 
                    Convert.ToString(rk4.Y[3], CultureInfo.InvariantCulture), 
                    Convert.ToString(rk4.Y[4], CultureInfo.InvariantCulture), 
                    Convert.ToString(rk4.Y[5], CultureInfo.InvariantCulture), 
                    Convert.ToString(rk4.Y[6], CultureInfo.InvariantCulture));

                rk4.NextStep(timeStep);
            }
            
            write.Close();
        }
    }
}