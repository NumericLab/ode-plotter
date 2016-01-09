using System;
using System.IO;

namespace RK
{
    public abstract class RungeKutta
    {
        public int N;
        double _t; // текущее время 
        public double[] Y; // искомое решение Y[0] - само решение, Y[i] - i-тая производная решения

        double[] _yy, _y1, _y2, _y3, _y4; // внутренние переменные 

        public RungeKutta(int aN) // aN - размерность системы 
        {
            N = aN; // сохранить размерность системы

            if (N < 1)
            {
                N = -1; // если размерность меньше единицы, то установить флаг ошибки
                return; // и выйти из конструктора
            }

            Y = new double[N]; // создать вектор решения
            _yy = new double[N]; // и внутренних решений
            _y1 = new double[N];
            _y2 = new double[N];
            _y3 = new double[N];
            _y4 = new double[N];
        }

        public void SetInit(double t0, double[] y0) // установить начальные условия.
        {                                           // t0 - начальное время, Y0 - начальное условие
            _t = t0;
            int i;
            for (i = 0; i < N; i++)
            {
                Y[i] = y0[i];
            }
        }

        public double GetCurrent() // вернуть текущее время
        {
            return _t;
        }

        public abstract void F(double t, double[] y, ref double[] fy); // правые части системы.

        public void NextStep(double dt) // следующий шаг метода Рунге-Кутта, dt - шаг по времени (может быть переменным)
        {
            if (dt < 0)
            {
                return;
            }

            int i;

            F(_t, Y, ref _y1); // расчитать Y1

            for (i = 0; i < N; i++)
            {
                _yy[i] = Y[i] + _y1[i] * (dt / 2.0);
            }
            F(_t + dt / 2.0, _yy, ref _y2); // расчитать Y2

            for (i = 0; i < N; i++)
            {
                _yy[i] = Y[i] + _y2[i] * (dt / 2.0);
            }
            F(_t + dt / 2.0, _yy, ref _y3); // расчитать Y3

            for (i = 0; i < N; i++)
            {
                _yy[i] = Y[i] + _y3[i] * dt;
            }
            F(_t + dt, _yy, ref _y4); // расчитать Y4

            for (i = 0; i < N; i++)
            {
                Y[i] = Y[i] + dt / 6.0 * (_y1[i] + 2.0 * _y2[i] + 2.0 * _y3[i] + _y4[i]); // расчитать решение на новом шаге
            }

            _t = _t + dt; // увеличить шаг

        }
    }

    public class MyRk : RungeKutta
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
                write.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", Convert.ToString(rk4.GetCurrent()), Convert.ToString(rk4.Y[0]), Convert.ToString(rk4.Y[1]), Convert.ToString(rk4.Y[2]), Convert.ToString(rk4.Y[3]), Convert.ToString(rk4.Y[4]), Convert.ToString(rk4.Y[5]), Convert.ToString(rk4.Y[6]));

                //Console.WriteLine("{0}\t{1}\t{2}", RK4.GetCurrent(), RK4.Y[0], RK4.Y[1]); // вывести t, y, y'

                rk4.NextStep(0.01); // расчитать на следующем шаге, шаг интегрирования dt=0.01
            }


            write.Close();
            //Thread.Sleep(5000);
        }
    }
}