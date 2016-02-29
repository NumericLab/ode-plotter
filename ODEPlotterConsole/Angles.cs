using System;

namespace ODEPlotterConsole
{
    public static class Angles
    {
        public static double GetPsi(this double[] y)
        {
            var alpha1 = y[0] * y[0] + y[1] * y[1] - y[2] * y[2] - y[3] * y[3];
            var beta1 = 2 * (y[1] * y[2] + y[0] * y[3]);
            return Math.Abs(Math.Atan(beta1 / alpha1));
        }

        public static double GetPhi(this double[] y)
        {
            var gamma2 = 2 * (y[2] * y[3] + y[0] * y[1]);
            var gamma3 = y[0] * y[0] - y[1] * y[1] - y[2] * y[2] + y[3] * y[3];
            return Math.Abs(Math.Atan(gamma2 / gamma3));
        }

        public static double GetTheta(this double[] y)
        {
            var gamma1 = 2 * (y[1] * y[3] - y[0] * y[2]);
            return Math.Abs(-Math.Asin(gamma1));
        }

    }
}
