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
        private event SimulatorHandler Notify;
        private List<SegmentModel> Segments;
        private Random rand;
        private int CurrentPosition;
        private int MaxLenght;
        private int MinLength;

        public StatisticModel statistic;
        public Simulator()
        {
            InitializationOfData();
        }
        private void CheckLengthOfTonnel(ref int tunnelLength)
        {
            if (tunnelLength < MinLength)
                tunnelLength = MinLength;
            if (tunnelLength > MaxLenght)
                tunnelLength = MaxLenght;
        }
        public void InitializationOfData()
        {
            Segments = new List<SegmentModel>();
            rand = new Random();
            CurrentPosition = 0;
            statistic = new StatisticModel();
            statistic.TimeToMove = 2;
            statistic.TimeToSwitch = 1;
            Notify += DisplayMessage;
            MaxLenght = 1000000;
            MinLength = 3;
        }
        public void GenerateRandomTunnel(int tunnelLength)
        {
            try
            {
                CheckLengthOfTonnel(ref tunnelLength);
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
                CheckLengthOfTonnel(ref tunnelLength);

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
                    CurrentPosition++;
                    statistic.StepsCounter++;

                if (CurrentPosition >= Segments.Count)
                    CurrentPosition = 0;
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
                CurrentPosition--;
                statistic.StepsCounter++;
                if (CurrentPosition < 0)
                    CurrentPosition = Segments.Count - 1;
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
            Console.WriteLine($"Segments count: {statistic.SegmentsCount}");
            Console.WriteLine($"Switching time: {TimeSpan.FromSeconds(statisticService.CalculateSwitchingTime()).ToString(@"hh\:mm\:ss\:ff")}");
            Console.WriteLine($"Moving time: {TimeSpan.FromSeconds(statisticService.CalculateMovingTime()).ToString(@"hh\:mm\:ss\:ff")}");
            Console.WriteLine($"Average time by section: {TimeSpan.FromSeconds(statisticService.ClculateAverageTimeBySection()).ToString(@"hh\:mm\:ss\:ff")}");
            Console.WriteLine($"Elapsed time: {TimeSpan.FromSeconds(statisticService.CalculateElapsedTime()).ToString(@"hh\:mm\:ss\:ff")}\n");
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
