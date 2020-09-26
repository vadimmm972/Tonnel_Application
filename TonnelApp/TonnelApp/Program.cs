using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TonnelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Services.Simulator simulatore = new Services.Simulator();
            simulatore.GenerateTunnelFromFile(6, Directory.GetCurrentDirectory()+"\\Generation\\Initialization.txt");
        }
    }
}
