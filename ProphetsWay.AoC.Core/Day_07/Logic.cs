using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProphetsWay.AoC.Core.Day_07
{
    public class Logic : BaseLogic
    {
        public long CalculateCrabFuelDelta(bool basicFuel)
        {
            var reader = GetInputTextReader();

            var line = reader.ReadToEnd();
            var initPositions = line.Split(",");

            var crabs = new List<CrabSub>();

            int? lowest = null, highest = null;
            foreach (var pos in initPositions)
            {
                var posInt = int.Parse(pos);
                var crab = new CrabSub(posInt);
                crabs.Add(crab);

                if (!lowest.HasValue || lowest > posInt)
                    lowest = posInt;

                if (!highest.HasValue || highest < posInt)
                    highest = posInt;
            }

            long lowestFuelUse = long.MaxValue;
            for (var i = lowest.Value; i <= highest.Value; i++)
            {
                long currFuelUse = 0;
                foreach (var crab in crabs)
                {
                    var delta = Math.Abs(crab.InitialPosition - i);

                    if (!basicFuel)
                        delta = CalculatePart2FuelDelta(delta);
                 
                    currFuelUse += delta;
                }

                if (lowestFuelUse > currFuelUse)
                    lowestFuelUse = currFuelUse;
            }

            return lowestFuelUse;
        }

        public int CalculatePart2FuelDelta(int delta)
        {
            var actualCost = 0;
            for(var i = 1; i <= delta; i++)
            {
                actualCost += i;
            }

            return actualCost;
        }
    
        public long Part1()
        {
            return CalculateCrabFuelDelta(true);
        }

        public long Part2()
        {
            return CalculateCrabFuelDelta(false);
        }

        public class CrabSub
        {
            public CrabSub(int initPos)
            {
                InitialPosition = initPos;
            }

            public int InitialPosition { get; }
        }
      
    }
}
