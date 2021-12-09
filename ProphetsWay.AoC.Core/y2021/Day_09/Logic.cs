namespace ProphetsWay.AoC.Core.y2021.Day_09
{
    public class Logic : BaseLogic
    {
        public Dictionary<int, Dictionary<int, Position>> LoadMap(bool runSample = false)
        {
            var reader = GetInputTextReader(runSample);

            var map = new Dictionary<int, Dictionary<int, Position>>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var row = new Dictionary<int, Position>();
                foreach (var plot in line)
                    row.Add(row.Count, new Position(plot));

                map.Add(map.Count, row);
            }

            return map;
        }

        public override string Part1(bool runSample = false)
        {
            var map = LoadMap(runSample);

            var maxY = map.Count;
            var maxX = map[0].Count;

            var riskSum = 0;

            for(var x = 0; x < maxX; x++ )
                for(var y = 0; y < maxY; y++)
                {
                    var curr = map[y][x];

                    if(x + 1 < maxX)
                    {
                        var right = map[y][x + 1];
                        if (right.Height <= curr.Height)
                            continue;
                    }

                    if(0 <= x - 1)
                    {
                        var left = map[y][x - 1];
                        if (left.Height <= curr.Height)
                            continue;
                    }

                    if(y+1 < maxY)
                    {
                        var up = map[y + 1][x];
                        if ( up.Height <= curr.Height)
                            continue;
                    }

                    if(0 <= y - 1)
                    {
                        var down = map[y - 1][x];
                        if (down.Height <= curr.Height)
                            continue;
                    }

                    var riskLevel = curr.Height + 1;

                    riskSum += riskLevel;
                }

            return riskSum.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var map = LoadMap(runSample);

            var maxY = map.Count;
            var maxX = map[0].Count;

            var basins = new List<int>();

            for (var x = 0; x < maxX; x++)
                for (var y = 0; y < maxY; y++)
                {
                    var curr = map[y][x];

                    if (x + 1 < maxX)
                    {
                        var right = map[y][x + 1];
                        if (right.Height <= curr.Height)
                            continue;
                    }

                    if (0 <= x - 1)
                    {
                        var left = map[y][x - 1];
                        if (left.Height <= curr.Height)
                            continue;
                    }

                    if (y + 1 < maxY)
                    {
                        var up = map[y + 1][x];
                        if (up.Height <= curr.Height)
                            continue;
                    }

                    if (0 <= y - 1)
                    {
                        var down = map[y - 1][x];
                        if (down.Height <= curr.Height)
                            continue;
                    }

                    //found a low point... now need to figure out the size of the basin
                    var basinSize = CheckBasinSize(map, x, y, maxX, maxY, 0);

                    basins.Add(basinSize);

                }


            basins.Sort();
            basins.Reverse();
            var top3 = basins.Take(3);

            var product = 1;

            foreach(var top in top3)
                product *= top;

            return product.ToString();
        }

        private int CheckBasinSize(Dictionary<int, Dictionary<int, Position>> map, int x, int y, int maxX, int maxY, int currBasinSize)
        {
            var curr = map[y][x];
            if (curr.Height == 9 || curr.InBasin)
                return 0;

            curr.InBasin = true;

            if (x + 1 < maxX)
            {
                currBasinSize += CheckBasinSize(map, x + 1, y, maxX, maxY, 0);
            }

            if (0 <= x - 1)
            {
                currBasinSize += CheckBasinSize(map, x - 1, y, maxX, maxY, 0);
            }

            if (y + 1 < maxY)
            {
                currBasinSize += CheckBasinSize(map, x, y + 1, maxX, maxY, 0);
            }

            if (0 <= y - 1)
            {
                currBasinSize += CheckBasinSize(map, x, y - 1, maxX, maxY, 0);
            }

            return currBasinSize + 1;
        }

        public class Position
        {
            public override string ToString()
            {
                return $"{Height} | InBasin:{InBasin}";
            }
            public Position(char heightChar)
            {
                Height = int.Parse(heightChar.ToString());
            }

            public int Height { get; set; }
            public bool InBasin { get; set; }
        }
    }
}
