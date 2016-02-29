using System;
using System.Configuration;

namespace ODEPlotterConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var timePeriod = double.Parse(ConfigurationManager.AppSettings["timePeriod"]);
            var timeStep = double.Parse(ConfigurationManager.AppSettings["timeStep"]);
            var phi0 = double.Parse(ConfigurationManager.AppSettings["phi0"]);
            var psi0 = double.Parse(ConfigurationManager.AppSettings["psi0"]);
            var theta0 = double.Parse(ConfigurationManager.AppSettings["theta0"]);
            Console.WriteLine("Вычисления ведутся с параметрами:" + Environment.NewLine +
                "Шаг интегрирования = {1}" + Environment.NewLine +
                "Начальные условия для самолетных углов:" + Environment.NewLine +
                "Угол Phi   = {2}" + Environment.NewLine +
                "Угол Psi   = {3}" + Environment.NewLine +
                "Угол Theta = {4}" + Environment.NewLine +
                "Угол  = {0}" + Environment.NewLine,
                timePeriod, timeStep, phi0, psi0, theta0);
            var initConditions = Solver.InitialConditions(phi0, theta0, psi0);

        }
    }
}
