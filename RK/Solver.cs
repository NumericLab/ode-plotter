using System;
using System.Globalization;
using System.IO;

namespace RK
{
    public static class Solver
    {
        static void Solve(double tInit, double[] yInit, 
            double epsilon, double delta, 
            double timePeriod, double timeStep)
        {
            var rk4 = new GravitationalMomentEquations(0.7, 1.5);

            double[] y0 = { 1.1, 0.2, 0.1, 0.3, 0.1, 1.2, 0.4 }; // зададим начальные условия y(0)=0, y'(0)=1

            rk4.SetInit(0, y0);
            var write = new StreamWriter("Test.txt");

            while (rk4.GetTime() < 10) // решаем до 10
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

                //Console.WriteLine("{0}\t{1}\t{2}", RK4.GetCurrent(), RK4.Y[0], RK4.Y[1]); // вывести t, y, y'

                rk4.NextStep(0.01); // расчитать на следующем шаге, шаг интегрирования dt=0.01
            }


            write.Close();
            //Thread.Sleep(5000);
        }
    }
}