namespace ProphetsWay.AoC.Core.y2021.Day_11
{
    public class Logic : BaseLogic
    {

        public class Octopus
        {
            public override string ToString()
            {
                return $"{EnergyLevel}";
            }
            public Octopus(char currEnergy)
            {
                EnergyLevel = int.Parse(currEnergy.ToString());
                HasFlashed = false;
                AdjacentOctopie = new List<Octopus>();
            }

            public int FlashCounter { get; private set; }

            private int _energyLevel;
            public int EnergyLevel
            {
                get
                {
                    return _energyLevel;
                }
                set
                {
                    _energyLevel = value;
                    if(_energyLevel > 9 && !HasFlashed)
                    {
                        //then flash
                        HasFlashed = true;
                        FlashCounter++;
                        AdjacentOctopie.ForEach(o => o.EnergyLevel++);
                    }
                }
            }

            public void EndStep()
            {
                if(EnergyLevel > 9 && HasFlashed)
                {
                    EnergyLevel = 0;
                    HasFlashed = false;
                }
            }

            public bool HasFlashed { get; set; }

            public List<Octopus> AdjacentOctopie { get; }

            public void AddAdjacency(Octopus other)
            {
                this.AdjacentOctopie.Add(other);
                other.AdjacentOctopie.Add(this);
            }
        }

        

        public void CheckAbove(Octopus curr, Octopus[,] grid, int row, int col)
        {
            if (row - 1 < 0)
                return;

            var above = grid[row - 1, col];
            curr.AddAdjacency(above);
        }

        public void CheckForward(Octopus curr, Octopus[,] grid, int row, int col)
        {
            if (row - 1 < 0)
                return;

            if (col + 1 > 9)
                return;

            var forward = grid[row - 1, col + 1];
            curr.AddAdjacency(forward);
        }

        public void CheckBack(Octopus curr, Octopus[,] grid, int row, int col)
        {
            if (row - 1 < 0)
                return;

            if (col - 1 < 0)
                return;

            var back = grid[row - 1, col - 1];
            curr.AddAdjacency(back);
        }

        public void CheckBehind(Octopus curr, Octopus[,] grid, int row, int col)
        {
            if (col - 1 < 0)
                return;

            var back = grid[row, col - 1];
            curr.AddAdjacency(back);
        }

        public List<Octopus> LoadStartingSet(bool runSample)
        {
            var reader = GetInputTextReader(runSample);

            var grid = new Octopus[10, 10];
            var octopie = new List<Octopus>();

            var row = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var col = 0;
                foreach(var o in line)
                {
                    var octo = new Octopus(o);
                    octopie.Add(octo);
                    grid[row, col] = octo;

                    CheckAbove(octo, grid, row, col);
                    CheckForward(octo, grid, row, col);
                    CheckBack(octo, grid, row, col);
                    CheckBehind(octo, grid, row, col);

                    col++;
                }

                row++;
            }

            return octopie;
        }

        public override string Part1(bool runSample = false)
        {
            var octopie = LoadStartingSet(runSample);

            for (var step = 0; step < 100; step++)
            {
                octopie.ForEach(o => o.EnergyLevel++);
                octopie.ForEach(o => o.EndStep());
            }

            var sum = octopie.Sum(o => o.FlashCounter);

            return sum.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var octopie = LoadStartingSet(runSample);

            var step = 0;
            for(; ;)
            {
                octopie.ForEach((o) => o.EnergyLevel++);
                step++;

                if (octopie.All(o => o.HasFlashed))
                    break;

                octopie.ForEach(o => o.EndStep());
            }

            return step.ToString();
        }

       
    }
}

