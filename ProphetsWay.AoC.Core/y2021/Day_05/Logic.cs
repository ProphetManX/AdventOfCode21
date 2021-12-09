namespace ProphetsWay.AoC.Core.y2021.Day_05
{
    public class Logic : BaseLogic
    {
        private List<Segment> ReadSegments(bool runSample)
        {
            var reader = GetInputTextReader(runSample);

            var segments = new List<Segment>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    break;

                segments.Add(new Segment(line));
            }

            return segments;
        }

        private Map PlotSegments(List<Segment> segments, bool includeDiag = false)
        {
             var map = new Map();
            foreach(var segment in segments)
            {
                if(segment.IsVertical || segment.IsHorizontal || (includeDiag && segment.IsDiagonal))
                {
                    map.PlotSegment(segment);
                }
            }

            return map;
        }

        public override string Part1(bool runSample = false)
        {
            var segments = ReadSegments(runSample);
            var map = PlotSegments(segments);

            return map.Positions.Where(p => p.Coverage > 1).Count().ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var segments = ReadSegments(runSample);
            var map = PlotSegments(segments, true);

            return map.Positions.Where(p => p.Coverage > 1).Count().ToString();
        }

        public class Map
        {
            public Map()
            {
                Graph = new Position[1000, 1000];
                Positions = new List<Position>();
            }

            public List<Position> Positions { get; private set; }
            public Position[,] Graph { get; private set; }

            public void PlotSegment(Segment segment)
            {
                if (segment.IsVertical)
                {
                    var (low, high) = SortPoints(segment.Start.Y, segment.End.Y);
                    for (var y = low; y <= high; y++)
                    {
                        var pos = Graph[segment.Start.X, y];
                        if(pos == null)
                        {
                            pos = new Position(segment.Start.X, y);
                            Graph[segment.Start.X, y] = pos;
                            Positions.Add(pos);
                        }

                        pos.Coverage++;
                    }
                }

                if (segment.IsHorizontal)
                {
                    var (low, high) = SortPoints(segment.Start.X, segment.End.X);
                    for (var x = low; x <= high; x++)
                    {
                        var pos = Graph[x, segment.Start.Y];
                        if(pos == null)
                        {
                            pos = new Position(x, segment.Start.Y);
                            Graph[x, segment.Start.Y] = pos;
                            Positions.Add(pos);
                        }
                        
                        pos.Coverage++;
                    }
                }

                if (segment.IsDiagonal)
                {
                    var delta = Math.Abs(segment.Start.X - segment.End.X);

                    var xDirection = segment.Start.X - segment.End.X > 0
                        ? -1 : 1;

                    var yDirection = segment.Start.Y - segment.End.Y > 0
                        ? -1 : 1;

                    for(var d = 0; d <= delta; d++)
                    {
                        var xD = d * xDirection;
                        var yD = d * yDirection;

                        var pos = Graph[segment.Start.X + xD, segment.Start.Y + yD];
                        if(pos == null)
                        {
                            pos = new Position(segment.Start.X + xD, segment.Start.Y + yD);
                            Graph[segment.Start.X + xD, segment.Start.Y + yD] = pos;
                            Positions.Add(pos);
                        }

                        pos.Coverage++;
                    }
                }
            }

            public (int, int) SortPoints(int start, int end)
            {
                if (start < end)
                    return (start, end);
                else
                    return (end, start);
            }
        }

        public class Position : Point
        {
            public Position(int x, int y) : base(x, y) { }

            public int Coverage { get; set; }

            public override string ToString()
            {
                if (Coverage == 0)
                    return ".";

                return $"{Coverage}";
            }
        }

        public class Segment
        {
            public override string ToString()
            {
                var straight = "";
                if (IsHorizontal)
                    straight = "| Horizontal";

                if (IsVertical)
                    straight = "| Vertical";

                if (IsDiagonal)
                    straight = "| Diagonal";

                return $"{Start} -> {End} {straight}";
            }

            public Segment(string line)
            {
                var segment = line.Split(" -> ");
                Start = new Point(segment.First());
                End = new Point(segment.Last());
            }

            public bool IsVertical => Start.X == End.X;
            public bool IsHorizontal => Start.Y == End.Y;

            public bool IsDiagonal => Math.Abs(Start.X - End.X) == Math.Abs(Start.Y - End.Y);

            public Point Start { get; set; }
            public Point End { get; set; }  


        }

        public class Point
        {
            public override string ToString()
            {
                return $"{X},{Y}";
            }

            public Point(string position)
            {
                var coords = position.Split(",");
                X = int.Parse(coords.First());
                Y = int.Parse(coords.Last());
            }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; private set; }

            public int Y { get; private set; }
        }
       
    }
}
