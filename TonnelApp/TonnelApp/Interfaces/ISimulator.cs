using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TonnelApp.Interfaces
{
    interface ISimulator
    {
        // Method returns true if the light in the current segment is on
        bool IsLightOn();

        // Method moves the robot forward
        void MoveForward();

        // Method moves the robot to the segment back
        void MoveBackward();

        // Method changes state of the light bulb (from on to off and vice versa)
        void SwitchLight();

    }
}
