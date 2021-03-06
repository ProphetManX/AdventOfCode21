namespace ProphetsWay.AoC.Core.y2021.Day_15
{
    public class Logic : BaseLogic
    {
        public class Position
        {
            public override string ToString()
            {
                return $"{X},{Y} -> {Value} | RiskToHere:{CheapestRiskToHere}";
            }
            public Position(char c, int x, int y) : this(x, y)
            {
                Value = int.Parse(c.ToString());
            }
            public Position(int value, int x, int y) : this(x, y)
            {
                if(value > 9)
                    value = value - 9;
                
                Value = value;
            }
            internal Position(int x, int y)
            {
                X = x;
                Y = y;
                Connections = new List<Position>();
                CheapestRiskToHere = (x == 0 && y == 0)
                    ? 0 : int.MaxValue;
            }

            public int X { get; }
            public int Y { get; }

            public long CheapestRiskToHere { get; set; }

            public List<Position> Connections { get; }

            public int Value { get; }

            public void CalculateHeuristic(int endX, int endY)
            {
                var dx = Math.Abs(endX - X);
                var dy = Math.Abs(endY - Y);
                Heuristic = dx + dy;
            }
            public int Heuristic { get; private set; }
        }

        public Position[,] LoadDataSet(bool runSample)
        {
            var reader = GetInputTextReader(runSample);

            var positions = new List<Position>();
            var grid = new Dictionary<int, Dictionary<int, Position>>();

            var colPos = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                grid.Add(colPos, new Dictionary<int, Position>());

                var rowPos = 0;
                foreach (var c in line)
                {
                    var pos = new Position(c, rowPos, colPos);
                    positions.Add(pos);
                    grid[colPos].Add(rowPos, pos);

                    rowPos++;
                }

                colPos++;
            }

            var maxY = grid.Keys.Count;
            var maxX = grid[0].Keys.Count;
            var pGrid = new Position[maxX, maxY];

            for (var y = 0; y < maxY; y++)
                for (var x = 0; x < maxX; x++)
                    pGrid[x, y] = grid[y][x];

            return pGrid;
        }

        public void ConstructConnections(Position[,] grid)
        {
            var maxX = grid.GetUpperBound(0);
            var maxY = grid.GetUpperBound(1);

            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    var curr = grid[x, y];
                    curr.CalculateHeuristic(maxX, maxY);

                    if(x > 0)
                    {
                        var left = grid[x - 1, y];
                        CheckAndMakeConnections(curr, left);
                    }

                    if(y > 0)
                    {
                        var up = grid[x, y - 1];
                        CheckAndMakeConnections(curr, up);
                    }

                }
            }
        }

        public void CheckAndMakeConnections(Position a, Position b)
        {
            if (!a.Connections.Contains(b)) {
                a.Connections.Add(b);
                b.Connections.Add(a);
            }
        }

        public long UseHeuristics(Position[,] grid)
        {
            var maxX = grid.GetUpperBound(0);
            var maxY = grid.GetUpperBound(1);

            var start = grid[0, 0];

            HPath(start);

            for (var y = 0; y <= maxY; y++)
                for (var x = 0; x <= maxX; x++)
                {

                }

            return -1;
        }

        public void HPath(Position curr)
        {
            Position bestOption = null;
            foreach(var adj in curr.Connections)
            {
                adj.CheapestRiskToHere = curr.CheapestRiskToHere + adj.Value;

                if (bestOption == null || adj.Heuristic + adj.Value < bestOption?.Value + bestOption?.Heuristic)
                    bestOption = adj;
            }

            HPath(bestOption);
        }

        public long CrawlConnections(Position[,] grid)
        {
            var maxX = grid.GetUpperBound(0);
            var maxY = grid.GetUpperBound(1);


            for(var y = 0; y <= maxY;y++)
                for(var x = 0; x <= maxX;x++)
                {
                    var curr = grid[x, y];
                    foreach(var conn in curr.Connections)
                    {
                        var connRisk = conn.CheapestRiskToHere;
                        var newRisk = curr.CheapestRiskToHere + conn.Value;
                        if (newRisk < connRisk)
                            conn.CheapestRiskToHere = newRisk;

                        //conn.Connections.Remove(curr);
                    }
                }


            return grid[maxX, maxY].CheapestRiskToHere;
        }

        public void CrawlFromTopLeft(Position[,] grid, int maxX, int maxY)
        {
            for (var y = 0; y <= maxY; y++)
                for (var x = 0; x <= maxX; x++)
                {
                    //need to figure out the risk to get to here
                    var curr = grid[x, y];

                    if (y > 0)
                    {
                        var up = grid[x, y - 1];
                        var upRisk = up.CheapestRiskToHere + curr.Value;
                        if (upRisk < curr.CheapestRiskToHere)
                            curr.CheapestRiskToHere = upRisk;
                    }

                    //no up value, only look left
                    if (x > 0)
                    {
                        var left = grid[x - 1, y];
                        var leftRisk = left.CheapestRiskToHere + curr.Value;
                        if (leftRisk < curr.CheapestRiskToHere)
                            curr.CheapestRiskToHere = leftRisk;
                    }
                }
        }

        public void CrawlFromBottomRight(Position[,] grid, int maxX, int maxY)
        {
            for (var y = maxY; y >= 0; y--)
                for (var x = maxX; x >= 0; x--)
                {
                    var curr = grid[x, y];

                    if (x < maxX)
                    {
                        var right = grid[x + 1, y];
                        var rightRisk = right.CheapestRiskToHere + curr.Value;
                        if (rightRisk < curr.CheapestRiskToHere)
                            curr.CheapestRiskToHere = rightRisk;
                    }

                    if (y < maxY)
                    {
                        var down = grid[x, y + 1];
                        var downRisk = down.CheapestRiskToHere + curr.Value;
                        if (downRisk < curr.CheapestRiskToHere)
                            curr.CheapestRiskToHere = downRisk;
                    }
                }
        }

        public long GoLineByLine(Position[,] grid)
        {
            var maxX = grid.GetUpperBound(0);
            var maxY = grid.GetUpperBound(1);

            CrawlFromTopLeft(grid, maxX, maxY);
            CrawlFromBottomRight(grid, maxX, maxY);
            CrawlFromTopLeft(grid, maxX, maxY);

            return grid[maxX, maxY].CheapestRiskToHere;
        }

        public Position[,] BlowoutGrid(Position[,] grid)
        {
            var origMaxX = grid.GetUpperBound(0) + 1;
            var origMaxY = grid.GetUpperBound(1) + 1;

            var maxX = origMaxX * 5;
            var maxY = origMaxY * 5;

            var newGrid = new Position[maxX, maxY];


            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    var additionX = x / origMaxX;
                    var offsetX = x % origMaxX;

                    var additionY = y / origMaxY;
                    var offsetY = y % origMaxY;

                    var source = grid[offsetX, offsetY];
                    var pos = new Position(source.Value + additionX + additionY, x, y);

                    newGrid[x, y] = pos;
                }

            }

            ConstructConnections(newGrid);

            return newGrid;
        }

        public void RenderBlowoutGrid(Position[,] grid)
        {
            var maxX = grid.GetUpperBound(0) + 1;
            var maxY = grid.GetUpperBound(1) + 1;

            var divisorX = maxX / 5;
            var divisorY = maxY / 5;

            for (var y = 0; y < maxY; y++)
            {
                if (y > 0 && y % divisorY == 0)
                {
                    //then render a line across x width
                    for (var x = 0; x < maxX + 4; x++)
                        Console.Write("-");

                    Console.WriteLine();
                }

                for (var x = 0; x < maxX; x++)
                {
                    if(x > 0 && x % divisorX == 0)
                        Console.Write("|");

                    Console.Write(grid[x, y].Value);
                }

                Console.WriteLine();
            }
        }

        public override string Part1(bool runSample = false)
        {
            var grid = LoadDataSet(runSample);

            ConstructConnections(grid);
            var leastRisk3 = CrawlConnections(grid);
            return leastRisk3.ToString();

            var leastRisk2 = GoLineByLine(grid);
            return leastRisk2.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var grid = LoadDataSet(runSample);
            var newGrid = BlowoutGrid(grid);

            var leastRisk4 = UseHeuristics(newGrid);
            return leastRisk4.ToString();

            var leastRisk3 = CrawlConnections(newGrid);
            return leastRisk3.ToString();   //return 2837  wrong answer... too high

            var leastRisk2 = GoLineByLine(newGrid);
            return leastRisk2.ToString();  //returns 2835   right answer to another puzzle  but not right, too high


            RenderBlowoutGrid(newGrid);
        }

    }
}
