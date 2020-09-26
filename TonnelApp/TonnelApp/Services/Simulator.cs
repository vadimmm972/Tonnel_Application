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
    class Simulator : ITunnelGenerator
    {
        public delegate void SimulatorHandler(string message);
        public event SimulatorHandler Notify;
        private List<SegmentModel> Segments;
        private Random rand;
        public Simulator()
        {
            Segments = new List<SegmentModel>();
            rand = new Random();
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
    }
}
