
using System.Runtime.InteropServices;
using System.Text;

namespace ProphetsWay.AoC.Core.y2021.Day_13
{
    public class Logic : BaseLogic
    {
        public class Coords
        {
            public override string ToString()
            {
                return IsVoid ? "Void Space" : $"{X},{Y}";
            }

            public int X { get; set; }
            public int Y { get; set; }

            public string OriginalCoords { get; }

            public Coords(string line)
            {
                OriginalCoords = line;
                var coords = line.Split(",");
                X = int.Parse(coords[0]);
                Y = int.Parse(coords[1]);
                IsVoid = false;
            }

            public Coords(int x, int y)
            {
                X = x;
                Y = y;
                IsVoid = true;
            }

            public bool IsVoid { get; }
        }

        public class Instruction
        {
            public override string ToString()
            {
                return $"{Axis} = {Position}";
            }

            public Instruction(string line) {
                var parts = line.Split("=");

                Axis = parts[0][parts[0].Length - 1];
                Position = int.Parse(parts[1]);
            }

            public char Axis { get; }
            public int Position { get; }

            public List<Coords> FoldGrid(List<Coords> coords)
            {
                var newCoords = new List<Coords>();

                var foldingX = Axis == 'x';

                var origMaxX = coords.Max(c => c.X);
                var origMaxY = coords.Max(c => c.Y);

                var maxX = foldingX ? Position : origMaxX + 1;
                var maxY = foldingX ? origMaxY + 1 : Position;

                var grid = new Coords[maxX, maxY];

                foreach (var coord in coords)
                {
                    if ((foldingX && coord.X < Position) || (!foldingX && coord.Y < Position))
                    {
                        //if we don't already have a coord plotted, then plot/keep this one
                        if (grid[coord.X, coord.Y] == null)
                        {
                            newCoords.Add(coord);
                            grid[coord.X, coord.Y] = coord;
                        }
                     
                        continue;
                    }

                    //if down here, then we know coord is greater than position
                    // "past the fold"
                    var newX = coord.X;
                    var newY = coord.Y;

                    if (foldingX && newX == Position)
                        throw new Exception("this is bad");

                    if(!foldingX && newY == Position)
                        throw new Exception("this is bad");

                    if (foldingX)
                    {
                        var deltaX = coord.X - Position;
                        newX = Position - deltaX;
                    }
                    else
                    {
                        var deltaY  = coord.Y - Position;
                        newY = Position - deltaY;
                    }

                    if (grid[newX, newY] == null)
                    {
                        newCoords.Add(coord);
                        grid[newX, newY] = coord;
                        coord.X = newX;
                        coord.Y = newY;
                    }
                }

                return newCoords;
            }
        }

       public (List<Coords>, List<Instruction> instructions) LoadDataSet(bool runSample)
        {
            var reader = GetInputTextReader(runSample);
            var coords = new List<Coords>();
            var instructions = new List<Instruction>();

            var parsingInstructions = false;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                if (string.IsNullOrEmpty(line))
                {
                    parsingInstructions = true;
                    continue;
                }

                if (parsingInstructions)
                    instructions.Add(new Instruction(line));
                else
                    coords.Add(new Coords(line));        
            }

            return (coords, instructions);
        }

        public bool[,] PlotGrid(List<Coords> coords)
        {
            var maxX = coords.Max(c => c.X);
            var maxY = coords.Max(c => c.Y);

            var grid = new bool[maxX + 1, maxY + 1];

            foreach (var coord in coords)
                grid[coord.X, coord.Y] = true;

            return grid;
        }

        public override string Part1(bool runSample = false)
        {
            var (coords, instructions) = LoadDataSet(runSample);

            var instruction = instructions.First();

            coords = instruction.FoldGrid(coords);

            return coords.Count.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var (coords, instructions) = LoadDataSet(runSample);

            foreach(var instruct in instructions)
                coords = instruct.FoldGrid(coords);

            var grid = PlotGrid(coords);
            var sb = new StringBuilder();
            for (var y = 0; y <= grid.GetUpperBound(1); y++)
            {
                for (var x = 0; x <= grid.GetUpperBound(0); x++)
                {
                    if (grid[x, y])
                        sb.Append("#");
                    else
                        sb.Append(" ");
                }

                Console.WriteLine(sb);
                sb = new StringBuilder();
            }

            return null;
        }
       
    }
}

