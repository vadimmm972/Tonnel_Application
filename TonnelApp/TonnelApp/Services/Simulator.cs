using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonnelApp.Interfaces;
using TonnelApp.Models;

namespace TonnelApp.Services
{
    class Simulator : ITunnelGenerator , ISimulator, ISimulatorStatistics
    {
        public delegate void SimulatorHandler(string message);
        public event SimulatorHandler Notify;
        public List<SegmentModel> Segments;
        private Random rand;
        private int CurrentPosition;
        private int PreviousSteps; // counter previous steps
        public int StepForward; //  number of  steps forward
        private int StepBack;  // number of  steps back
        public bool IsWorkingProcess;
        public bool IsEndPosition;
        private StatisticModel statistic;
        public Simulator()
        {
            InitializationOfData();
        }
        public void InitializationOfData()
        {
            Segments = new List<SegmentModel>();
            rand = new Random();
            CurrentPosition = 0;
            StepForward = 0;
            StepBack = 0;
            statistic = new StatisticModel();
            statistic.TimeToMove = 2;
            statistic.TimeToSwitch = 1;
            Notify += DisplayMessage;
        }
        public void GenerateRandomTunnel(int tunnelLength)
        {
            try
            {
                for (int i = 0; i < tunnelLength; i++)
                {
                    Segments.Add(new SegmentModel() { IsLightOn = rand.Next(0, 2) });
                }
            }
            catch (Exception ex)
            {
                Notify?.Invoke($"Simulator (GenerateRandomTunnel)\n Error: {ex.Message}\n");
            }
        }
        public void GenerateTunnelFromFile(int tunnelLength, string inputFileName)
        {
            try
            {
                int sizeFile = 0;
                using (StreamReader sr = new StreamReader(inputFileName, Encoding.Default))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!String.IsNullOrEmpty(line))
                        {
                            Segments.Add(new SegmentModel() { IsLightOn = Convert.ToInt32(line) });
                            sizeFile++;
                        }
                    }
                }
                if (tunnelLength > sizeFile)
                {
                    int number;
                    using (StreamWriter sw = new StreamWriter(inputFileName, true, Encoding.Default))
                    {
                        for (int j = 0; j < tunnelLength - sizeFile; j++)
                        {
                            number = rand.Next(0, 2);
                            sw.WriteLine(number);
                            Segments.Add(new SegmentModel() { IsLightOn = number });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Notify?.Invoke($"Simulator (GenerateTunnelFromFile)\n Error: {ex.Message}\n");
            }

        }
        public bool IsLightOn()
        {
            return Segments[CurrentPosition].IsLightOn == 1 ? true : false;
        }
        public void MoveForward()
        {
            try
            {
                for (int i = 0; i <= StepForward; i++)
                {
                    if (CurrentPosition >= Segments.Count)
                        CurrentPosition = 0;

                    if (IsLightOn() && i == 0 || !IsLightOn() && i < StepForward || i == StepForward && !IsLightOn())
                    {
                        SwitchLight();
                    }
                    if(i != StepForward)
                    {
                        CurrentPosition++;
                        statistic.StepsCounter++;
                    }
                    
                }
            }
            catch(Exception ex)
            {
                Notify?.Invoke($"Simulator (MoveForward)\n Error: {ex.Message}\n");
            }
        }

        public void MoveBackward()
        {
            try
            {
                for (int i = StepForward; i >= 0; i--)
                {
                    if (CurrentPosition < 0)
                        CurrentPosition = Segments.Count - 1;

                    if(i == 0 && IsLightOn())
                    {
                        Console.WriteLine("The end of  the tonnel !!! ");
                    }
                    if(i != 0)
                    {
                        CurrentPosition--;
                        statistic.StepsCounter++;
                    }
                   
                }
            }
            catch(Exception ex)
            {
                Notify?.Invoke($"Simulator (MoveBackward)\n Error: {ex.Message}\n");
            }
        }

        public void SwitchLight()
        {
            if (Segments[CurrentPosition].IsLightOn == 1)
                Segments[CurrentPosition].IsLightOn = 0;
            else
                Segments[CurrentPosition].IsLightOn = 1;

            statistic.SwitchedTime++;
        }

        public void PrintStatistics()
        {
            StatisticService statisticService = new StatisticService(statistic);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\tStatistic");
            Console.WriteLine($"Switching time: {statisticService.CalculateSwitchingTime()}");
            Console.WriteLine($"Moving time: {statisticService.CalculateMovingTime()}");
            Console.WriteLine($"Average time by section: {statisticService.ClculateAverageTimeBySection()}");
            Console.WriteLine($"Elapsed time: {statisticService.CalculateElapsedTime()}\n");
            Console.ResetColor();
        }
        private static void DisplayMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
