using System.Text;

namespace ProphetsWay.AoC.Core.y2021.Day_12
{
    public class Logic : BaseLogic
    {
        public class Cave
        {
            public Cave(string name)
            {
                Name = name;
                Connections = new List<Cave>();

                var upper = name.ToUpper();

                IsBig = name.Equals(upper, StringComparison.Ordinal);
            }

            public override string ToString()
            {
                var conns = new StringBuilder();
                foreach (var conn in Connections)
                {
                    conns.Append(conn.Name);
                    conns.Append(", ");
                }

                conns.Remove(conns.Length - 2, 2);

                return $"{Name} -> {conns}";
            }

            public bool IsBig { get; }

            public string Name { get;  }

            public List<Cave> Connections{ get; }
        }
        
        public Dictionary<string, Cave> LoadCaveMapping(bool runSample)
        {
            var reader = GetInputTextReader(runSample);

            var lookup = new Dictionary<string, Cave>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var junction = line.Split("-");

                var pointA = junction[0];
                var pointB = junction[1];

                if (!lookup.ContainsKey(pointA))
                    lookup.Add(pointA, new Cave(pointA));

                if (!lookup.ContainsKey(pointB))
                    lookup.Add(pointB, new Cave(pointB));

                var caveA = lookup[pointA];
                var caveB = lookup[pointB];

                caveA.Connections.Add(caveB);
                caveB.Connections.Add(caveA);
            }

            return lookup;
        }

        public class PathStats
        {
            public PathStats()
            {
                CurrentProgress = new Stack<Cave>();
                KnownPathways = new List<string>();
            }

            public List<string> KnownPathways { get; }

            public Stack<Cave> CurrentProgress { get; }

            public Cave? CurrentDoubleStep { get; set; }
        }

        
        public void FindExitPathways(Cave curr, PathStats stats, bool singleSmall = false)
        {
            stats.CurrentProgress.Push(curr);
            foreach (var cave in curr.Connections)
            {
                if (cave.Name == "start")
                    continue;

                if (!cave.IsBig && stats.CurrentProgress.Contains(cave))  
                {
                    if (stats.CurrentDoubleStep == null && singleSmall)
                    {
                        stats.CurrentDoubleStep = cave;
                        FindExitPathways(cave, stats, singleSmall);
                        stats.CurrentDoubleStep = null;
                        continue;
                    }
                    else
                        //skip cave if it's small and we've already been in it within our current progress
                        continue;
                }

                if(cave.Name == "end")
                {
                    var sb = new StringBuilder();
                    stats.CurrentProgress.Push(cave);
                    foreach(var step in stats.CurrentProgress.Reverse())
                    {
                        sb.Append(step.Name);
                        sb.Append(",");
                    }

                    sb.Remove(sb.Length - 1, 1);
                    stats.KnownPathways.Add(sb.ToString());
                    stats.CurrentProgress.Pop();
                }
                else
                {
                    FindExitPathways(cave, stats, singleSmall);
                }
            }

            stats.CurrentProgress.Pop();
        }


        public override string Part1(bool runSample = false)
        {
            var caves = LoadCaveMapping(runSample);

            var start = caves["start"];

            var stats = new PathStats();
            FindExitPathways(start, stats);

            return stats.KnownPathways.Count.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var caves = LoadCaveMapping(runSample);

            var start = caves["start"];

            var stats = new PathStats();
            FindExitPathways(start, stats, true);

            return stats.KnownPathways.Count.ToString();
        }
       
    }
}

