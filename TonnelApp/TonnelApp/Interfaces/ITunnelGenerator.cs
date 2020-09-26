using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TonnelApp.Interfaces
{
    interface ITunnelGenerator
    {
        // Method generates the initial state of tunnel segments with  length tunnelLength randomly
        void GenerateRandomTunnel(int tunnelLength);

        // Method generates the initial state of tunnel segments with length tunnelLength based on the data from the file
        void GenerateTunnelFromFile(int tunnelLength, string inputFileName);

    }
}
