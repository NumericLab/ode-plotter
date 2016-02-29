namespace ODEPlotterConsole
{
    public class GravitationalMomentEquations : RungeKuttaBase
    {
        private readonly double _epsilon;
        private readonly double _delta;

        public GravitationalMomentEquations(double epsilon, double delta) : base(7)
        {
            _epsilon = epsilon;
            _delta = delta;
        }

        public override void Equations(double t, double[] y, ref double[] yDot)
        {
            var p = y[0] * y[0] + y[1] * y[1] + y[2] * y[2] + y[3] * y[3] - 1;
            yDot[0] = -0.5 * y[1] * y[4] - 0.5 * y[2] * y[5] - 0.5 * y[3] * y[6] + 0.5 * y[2] - 0.5 * y[0] * p;
            yDot[1] = 0.5 * y[0] * y[4] + 0.5 * y[2] * y[6] - 0.5 * y[3] * y[5] - 0.5 * y[3] - 0.5 * y[1] * p;
            yDot[2] = 0.5 * y[0] * y[5] + 0.5 * y[3] * y[4] - 0.5 * y[1] * y[6] - 0.5 * y[0] - 0.5 * y[2] * p;
            yDot[3] = 0.5 * y[0] * y[6] + 0.5 * y[1] * y[5] - 0.5 * y[2] * y[4] + 0.5 * y[1] - 0.5 * y[3] * p;
            yDot[4] = (_epsilon - _delta) * (-y[5] * y[6] + 3 * (2 * y[2] * y[3] + 2 * y[0] * y[1]) 
                * (y[0] * y[0] - y[1] * y[1] - y[2] * y[2] + y[3] * y[3]));
            yDot[5] = (1 - _epsilon) / _delta * (-y[6] * y[4] + 3 * (2 * y[1] * y[3] - 2 * y[0] * y[2]) 
                * (y[0] * y[0] - y[1] * y[1] - y[2] * y[2] + y[3] * y[3]));
            yDot[6] = (_delta - 1) / _epsilon * (-y[4] * y[5] + 3 * (2 * y[1] * y[3] - 2 * y[0] * y[2]) 
                * (2 * y[2] * y[3] + 2 * y[0] * y[1]));
        }
    }
}
