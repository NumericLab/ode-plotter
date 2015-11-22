using System;
using System.Threading;

namespace RK
{
    public abstract class TRungeKutta
    {
        public int N;
        double t; // текущее время 
        public double[] Y; // искомое решение Y[0] - само решение, Y[i] - i-тая производная решения

        double[] YY, Y1, Y2, Y3, Y4; // внутренние переменные 

        public TRungeKutta(int aN) // aN - размерность системы 
        {
            N = aN; // сохранить размерность системы

            if (N < 1)
            {
                N = -1; // если размерность меньше единицы, то установить флаг ошибки
                return; // и выйти из конструктора
            }

            Y = new double[N]; // создать вектор решения
            YY = new double[N]; // и внутренних решений
            Y1 = new double[N];
            Y2 = new double[N];
            Y3 = new double[N];
            Y4 = new double[N];
        }

        public void SetInit(double t0, double[] Y0) // установить начальные условия.
        {                                           // t0 - начальное время, Y0 - начальное условие
            t = t0;
            int i;
            for (i = 0; i < N; i++)
            {
                Y[i] = Y0[i];
            }
        }

        public double GetCurrent() // вернуть текущее время
        {
            return t;
        }

        public abstract void F(double t, double[] Y, ref double[] FY); // правые части системы.

        public void NextStep(double dt) // следующий шаг метода Рунге-Кутта, dt - шаг по времени (может быть переменным)
        {
            if (dt < 0)
            {
                return;
            }

            int i;

            F(t, Y, ref Y1); // расчитать Y1

            for (i = 0; i < N; i++)
            {
                YY[i] = Y[i] + Y1[i] * (dt / 2.0);
            }
            F(t + dt / 2.0, YY, ref Y2); // расчитать Y2

            for (i = 0; i < N; i++)
            {
                YY[i] = Y[i] + Y2[i] * (dt / 2.0);
            }
            F(t + dt / 2.0, YY, ref Y3); // расчитать Y3

            for (i = 0; i < N; i++)
            {
                YY[i] = Y[i] + Y3[i] * dt;
            }
            F(t + dt, YY, ref Y4); // расчитать Y4

            for (i = 0; i < N; i++)
            {
                Y[i] = Y[i] + dt / 6.0 * (Y1[i] + 2.0 * Y2[i] + 2.0 * Y3[i] + Y4[i]); // расчитать решение на новом шаге
            }

            t = t + dt; // увеличить шаг

        }
    }

    public class TMyRK : TRungeKutta
    {
        public TMyRK(int aN) : base(aN) { }

        public override void F(double t, double[] Y, ref double[] FY)
        {
            FY[0] = Y[1]; // пример математический маятник 
            FY[1] = -Y[0]; // y''(t)+y(t)=0
            // y1''(t) + y1(t) = 0

            //y1'(t) = y2(t)
            //y2'(t) = -y1(t)
        }
    }

    public class TMyRK2 : TRungeKutta
    {
        public TMyRK2(int aN) : base(aN) { }

        public override void F(double t, double[] Y, ref double[] FY)
        {
            FY[0] = Y[1]; 
            FY[1] = -Y[0];
            FY[2] = Y[0];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var RK4 = new TMyRK(2);

            double[] Y0 = { 0, 1 }; // зададим начальные условия y(0)=0, y'(0)=1

            RK4.SetInit(0, Y0);

            while (RK4.GetCurrent() < 10) // решаем до 10
            {
                Console.WriteLine("{0}\t{1}\t{2}", RK4.GetCurrent(), RK4.Y[0], RK4.Y[1]); // вывести t, y, y'

                RK4.NextStep(0.01); // расчитать на следующем шаге, шаг интегрирования dt=0.01
            }

            var RK4_new = new TMyRK2(3); 
            double[] Y0_new = { 0, 1, 2 };
            RK4_new.SetInit(0, Y0_new); 
            while (RK4_new.GetCurrent() < 10) // решаем до 10
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", RK4_new.GetCurrent(), RK4_new.Y[0], RK4_new.Y[1], RK4_new.Y[2]); // вывести t, y, y'
                
                RK4_new.NextStep(0.01); // расчитать на следующем шаге, шаг интегрирования dt=0.01
            }

            Thread.Sleep(5000);
        }
    }
}