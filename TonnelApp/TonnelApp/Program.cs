using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonnelApp.Services;

namespace TonnelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Services.Simulator simulatore = new Services.Simulator();
            // simulatore.GenerateTunnelFromFile(2, Directory.GetCurrentDirectory()+"\\Generation\\Initialization.txt");
            simulatore.GenerateRandomTunnel(42423423);
            RobotController robotController = new RobotController();

            int a = robotController.FindSolution(simulatore);
            simulatore.statistic.SegmentsCount = a;
            simulatore.PrintStatistics();
            Console.ReadKey();
        }
    }
}
