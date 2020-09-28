using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TonnelApp.Models
{
    class StatisticModel
    {
        public double ElapsedTime { get; set; }
        public double AverageTimeBySection { get; set; }
        public int SegmentsCount { get; set; }
        public int StepsCounter { get; set; }
        public int SwitchedTime { get; set; }
        public double TimeToSwitch { get; set; }
        public double TimeToMove { get; set; }
        
       
        public StatisticModel()
        {
            ElapsedTime = 0;
            AverageTimeBySection = 0;
            SegmentsCount = 0;
            StepsCounter = 0;
            SwitchedTime = 0;
            TimeToSwitch = 0;
            TimeToMove = 0;
        }
    }
}
