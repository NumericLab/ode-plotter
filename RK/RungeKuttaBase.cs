using System;

namespace RK
{
    public class RungeKuttaBase
    {
        public int N;
        double _t; // current time 
        public double[] Y; //solutions

        private double[] _y1, _y2, _y3, _y4, _yy;

        private void BaseSetup(int aN)
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

        public RungeKuttaBase(int aN) // aN - размерность системы 
        {
            BaseSetup(aN);
        }

        public RungeKuttaBase(int aN, double t0, double[] y0)
        {
            BaseSetup(aN);
            SetInit(t0, y0);
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

        public double GetTime()
        {
            return _t;
        }

        public virtual void Equations(double t, double[] y, ref double[] yDot)
        {
            throw new NotImplementedException();
        }

        public void NextStep(double dt) // следующий шаг метода Рунге-Кутта, dt - шаг по времени (может быть переменным)
        {
            if (dt < 0)
            {
                return;
            }

            int i;

            Equations(_t, Y, ref _y1); // расчитать Y1

            for (i = 0; i < N; i++)
            {
                _yy[i] = Y[i] + _y1[i] * (dt / 2.0);
            }
            Equations(_t + dt / 2.0, _yy, ref _y2); // расчитать Y2

            for (i = 0; i < N; i++)
            {
                _yy[i] = Y[i] + _y2[i] * (dt / 2.0);
            }
            Equations(_t + dt / 2.0, _yy, ref _y3); // расчитать Y3

            for (i = 0; i < N; i++)
            {
                _yy[i] = Y[i] + _y3[i] * dt;
            }
            Equations(_t + dt, _yy, ref _y4); // расчитать Y4

            for (i = 0; i < N; i++)
            {
                Y[i] = Y[i] + dt / 6.0 * (_y1[i] + 2.0 * _y2[i] + 2.0 * _y3[i] + _y4[i]); // расчитать решение на новом шаге
            }

            _t = _t + dt; // увеличить шаг

        }
    }
}