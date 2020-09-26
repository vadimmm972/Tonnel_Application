using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TonnelApp.Interfaces
{
    interface ISimulatorStatistics
    {
        // Method displays statistics on the robot's movement to the console: total seconds elapsed, average time spent on one section of the tunnel (in seconds), the number of sections of the tunnel
        void PrintStatistics();

    }
}
