using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TonnelApp.Models;

namespace TonnelApp.Services
{
    class StatisticService
    {
        StatisticModel _statistic;
        private double _switchingTime;
        private double _movingTime;
        public StatisticService(StatisticModel staticsticModel)
        {
            _statistic = staticsticModel;
            _switchingTime = staticsticModel.TimeToSwitch;
            _movingTime = staticsticModel.TimeToMove;
        }

        public double CalculateSwitchingTime()
        {
            if (_statistic.SwitchedTime > 0)
                return _statistic.SwitchedTime * _switchingTime;

            return 0;
        }

        public double CalculateMovingTime()
        {
            if(_statistic.StepsCounter > 0)
                return _statistic.StepsCounter * _movingTime;

            return 0;
        }

        public double CalculateElapsedTime()
        {
            return CalculateMovingTime() + CalculateSwitchingTime();
        }

        public double ClculateAverageTimeBySection()
        {
            if(_statistic.SegmentsCount > 0)
                return (CalculateMovingTime() + CalculateSwitchingTime()) / _statistic.SegmentsCount;

            return 0;
        }
    }
}
