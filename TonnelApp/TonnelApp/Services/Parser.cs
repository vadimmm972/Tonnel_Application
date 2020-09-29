using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TonnelApp.Services
{
    class Parser
    {
        private Services.Simulator simulator = new Services.Simulator();
        private bool IsReady;
        private int count;
        public Parser()
        {
            IsReady = false;
            count = 0;
        }


        public void Run()
        {
            bool flag = true;
            try
            {
                while (flag)
                {
                    Console.WriteLine("\t\t\t\tWelcome to application");
                    Console.WriteLine("Options:\n1 - generate random tunel\n2 - generate tunnel from file\n3 - start\n4 - statistic\n5 - clear console\n0 - exit");
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            ToonelGegrator(1);
                            break;
                        case 2:
                            ToonelGegrator(2);
                            break;
                        case 3:
                            Start();
                            break;
                        case 4:
                            GetStatistic();
                            break;
                        case 5:
                            Console.Clear();
                            break;
                        case 0:
                            Console.WriteLine("have a nice day, bye:)");
                            flag = false;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid data, try again: ");
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
            }
           
        }

        private void GetStatistic()
        {
            Console.Clear();
            simulator.statistic.SegmentsCount = count;
            simulator.PrintStatistics();
            Stop();
        }

        private void ToonelGegrator(int type)
        {
            Console.Clear();
            Console.WriteLine("Please enter length of the tunnel:  (min - 3 , max - 100 000 000):");
            var length = Console.ReadLine();

            var isNumeric = int.TryParse(length, out int n);

            if(isNumeric == true)
            {
                if (type == 1)
                {
                    simulator.GenerateRandomTunnel(n);
                }
                else
                {
                    Console.WriteLine("Please enter file name:");
                    var fileName = Console.ReadLine();
                    simulator.GenerateTunnelFromFile(n, System.IO.Directory.GetCurrentDirectory() + $"\\Generation\\{fileName}");
                }
                Console.WriteLine("The tunnel has been generated successfully !");
                IsReady = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The tunnel has been generated successfully !");
                Console.ResetColor();
            }
            Stop();
        }

        private void Start()
        {
            if (IsReady)
            {
                Console.Clear();
                Console.WriteLine("in progress...");
                RobotController robotController = new RobotController();
                count = robotController.FindSolution(simulator);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("The robot has finished checking the tunnel !!! ");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The tunnel hasn't been generated !");
                Console.ResetColor();
            }
            Stop();
        }

        public void Stop()
        {
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
