using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonnelApp.Interfaces;

namespace TonnelApp.Services
{
    class RobotController : IRobotController
    {
        public int FindSolution(ISimulator simulator)
        {
            int stepForward = 4;
            int previousSteps = 0;
            int segmentCounter = 0;
            int counterNextSteps = 0;
            bool IsEndPosition = false;
            bool WasSwitched = false;
            

            try
            {
                while (!IsEndPosition)
                {
                    for (int i = 0; i < stepForward; i++)
                    {

                        if (simulator.IsLightOn() && i == 0 && previousSteps == 0 || !simulator.IsLightOn() && i != 0)
                        {
                            simulator.SwitchLight();
                            WasSwitched = true;
                            segmentCounter += counterNextSteps;
                            counterNextSteps = 0;
                        }
                        if (!WasSwitched)
                        {
                            if (previousSteps < i || previousSteps == 0)
                            {
                                segmentCounter++;
                            }
                        }
                        else
                        {
                            counterNextSteps++;
                        }

                        if (i != stepForward)
                        {
                            simulator.MoveForward();
                        }
                    }

                    for (int j = stepForward - 1; j >= 0; j--)
                    {
                        simulator.MoveBackward();
                        if (j == 0 && simulator.IsLightOn())
                        {
                            IsEndPosition = true;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("End of the tunnel !!! ");
                            Console.ResetColor();
                        }

                    }
                    if (!IsEndPosition)
                    {
                        previousSteps = stepForward - 1;
                        stepForward *= 2;
                        segmentCounter += counterNextSteps;
                        counterNextSteps = 0;
                        WasSwitched = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return segmentCounter;
        }
    }
}
