using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TonnelApp.Interfaces
{
    interface IRobotController
    {
        // Method launches the algorithm for finding a solution (you need to find the number of segments in the tunnel)
        // Return value - the number of segments found
        int FindSolution(ISimulator simulator);
    }
}
