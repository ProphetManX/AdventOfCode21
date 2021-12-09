namespace ProphetsWay.AoC.Core.y2021.Day_07
{
    public class Logic : BaseLogic
    {
        public override string Part1(bool runSample = false)
        {
            return Part1Logic(runSample);
        }

        public override string Part2(bool runSample = false)
        {
            return Part2Logic(runSample);
        }

        public long CalculateCrabFuelDelta(bool isSample, bool basicFuel)
        {
            var reader = GetInputTextReader(isSample);

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

        private Dictionary<int, int> Part2DeltaCosts = new Dictionary<int, int> { { 1, 1 } };
        public int CalculatePart2FuelDelta(int delta)
        {
            if (delta == 0)
                return 0;

            if(Part2DeltaCosts.ContainsKey(delta))
                return Part2DeltaCosts[delta];

            var actualCost = Part2DeltaCosts.Values.Last();
            for(var i = Part2DeltaCosts.Keys.Last() + 1; i <= delta; i++)
            {
                actualCost += i;
                Part2DeltaCosts.Add(i, actualCost);
            }

            return actualCost;
        }

        private string Part1Logic(bool isSample)
        {
            return CalculateCrabFuelDelta(isSample, true).ToString();
        }

        private string Part2Logic(bool isSample)
        {
            return CalculateCrabFuelDelta(isSample, false).ToString();
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
