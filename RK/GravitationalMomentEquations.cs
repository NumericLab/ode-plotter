namespace RK
{
    public class GravitationalMomentEquations : RungeKuttaBase
    {
        public GravitationalMomentEquations(int aN) : base(aN) { }

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
}
