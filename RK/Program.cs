using System;
using System.Globalization;
using System.IO;

namespace RK
{
    

    public class MyRk : RungeKuttaBase
    {
        public MyRk(int aN) : base(aN) { }

        public override void F(double t, double[] y, ref double[] fy)
        {
            var epsilon = 0.7;
            var delta = 1.5;
            double p;
            p = (y[0] * y[0] + y[1] * y[1] + y[2] * y[2] + y[3] * y[3] - 1);
            fy[0] = -0.5 * y[1] * y[4] - 0.5 * y[2] * y[5] - 0.5 * y[3] * y[6] + 0.5 * y[2] - 0.5 * y[0] * p;
            fy[1] = 0.5 * y[0] * y[4] + 0.5 * y[2] * y[6] - 0.5 * y[3] * y[5] - 0.5 * y[3] - 0.5 * y[1] * p;
            fy[2] = 0.5 * y[0] * y[5] + 0.5 * y[3] * y[4] - 0.5 * y[1] * y[6] - 0.5 * y[0] - 0.5 * y[2] * p;
            fy[3] = 0.5 * y[0] * y[6] + 0.5 * y[1] * y[5] - 0.5 * y[2] * y[4] + 0.5 * y[1] - 0.5 * y[3] * p;
            fy[4] = (epsilon - delta) * (-y[5] * y[6] + 3 * (2 * y[2] * y[3] + 2 * y[0] * y[1]) * (y[0] * y[0] - y[1] * y[1] - y[2] * y[2] + y[3] * y[3]));
            fy[5] = (1 - epsilon) / delta * (-y[6] * y[4] + 3 * (2 * y[1] * y[3] - 2 * y[0] * y[2]) * (y[0] * y[0] - y[1] * y[1] - y[2] * y[2] + y[3] * y[3]));
            fy[6] = (delta - 1) / epsilon * (-y[4] * y[5] + 3 * (2 * y[1] * y[3] - 2 * y[0] * y[2]) * (2 * y[2] * y[3] + 2 * y[0] * y[1]));
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var rk4 = new MyRk(7);

            double[] y0 = { 1.1, 0.2, 0.1, 0.3, 0.1, 1.2, 0.4 }; // зададим начальные условия y(0)=0, y'(0)=1

            rk4.SetInit(0, y0);
            StreamWriter write = new StreamWriter("Test.txt");

            while (rk4.GetCurrent() < 10) // решаем до 10
            {
                write.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", 
                    Convert.ToString(rk4.GetCurrent(), CultureInfo.InvariantCulture), 
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