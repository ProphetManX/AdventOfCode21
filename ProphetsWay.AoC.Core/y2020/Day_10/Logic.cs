using System.Text;

namespace ProphetsWay.AoC.Core.y2020.Day_10
{
    public class Logic : BaseLogic
    {
        public class Node
        {
            public override string ToString()
            {
                var childrenStr = "";

                if (Children.Count == 0)
                    childrenStr = "END";
                else
                {
                    var sb = new StringBuilder();
                    foreach (var child in Children)
                        sb.Append($"{child.Value}, ");

                    sb.Remove(sb.Length - 2, 2);
                    childrenStr = sb.ToString();
                }

                return $"{Value} -> {childrenStr}";
            }
            public Node(int node)
            {
                Value = node;
                Children = new List<Node>();
                Parents = new List<Node>();
            }
            public bool End
            {
                get
                {
                    return Children.Count == 0;
                }
            }

            public bool FinishedLoading { get; set; }
            public bool FinishedReducing { get; set; }
            public int Value { get; }
            public List<Node> Children { get; set; }

            public List<Node> Parents { get; set; }

            public void AddChild(Node child)
            {
                if (!Children.Any(c => c.Value == child.Value))
                    Children.Add(child);

                if (!child.Parents.Any(p => p.Value == Value))
                    child.Parents.Add(this);
            }
        }

        public void ReduceNodes(Node start)
        {
            ReduceNode(start);

            var x = "Complete";
        }

        public bool ShouldDropChildForGrandChild(Node node)
        {
            return node.Children.Count == 1 && node.Children.Single().Parents.Count == 1;
        }

        public void DropChildForGrandChildren(Node node, Node child)
        {
            var grandChildren = child.Children;
            node.Children = grandChildren;

            foreach (var gc in grandChildren)
            {
                gc.Parents.Remove(child);
                gc.Parents.Add(node);
            }

            child.FinishedReducing = true;

            if(ShouldDropChildForGrandChild(node))
                DropChildForGrandChildren(node, node.Children.Single());
        }

        public void ReduceNode(Node node)
        {
            if (node.FinishedReducing)
                return;

            if (ShouldDropChildForGrandChild(node))
                DropChildForGrandChildren(node, node.Children.Single());

            foreach (var child in node.Children)
                ReduceNode(child);

            node.FinishedReducing = true;
        }

        public Node LoadNodes(List<int> adapters)
        {
            var start = new Node(0);
            var lookups = new Dictionary<int, Node>();
            LoadNode(start, adapters, lookups);

            return start;
        }

        public class Stats
        {
            public long Count { get; set; }
        }

        public void LoadNode(Node node, List<int> adapters, Dictionary<int, Node> lookup)
        {
            if (node.FinishedLoading)
                return;

            var children = adapters.Where(a => 0 < (a - node.Value) && (a - node.Value) <= 3).ToList();
            
            foreach(var child in children)
            {
                if(!lookup.ContainsKey(child))
                    lookup.Add(child, new Node(child));

                var cNode = lookup[child];
                node.AddChild(cNode);
                LoadNode(cNode, adapters, lookup);
            }

            node.FinishedLoading = true;
        }

        public void CountUniqueChains(Node node, Stats stats)
        {
            if (node.End)
            {
                stats.Count++;
                return;
            }

            //Parallel.ForEach(node.Children, (child) => { CountUniqueChains(child, stats); });
            foreach (var child in node.Children)
                CountUniqueChains(child, stats);
        }
       
        public List<int> LoadAdapters(bool runSample)
        {
            var adapters = new List<int>();
            var reader = GetInputTextReader(runSample);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                adapters.Add(int.Parse(line));
            }

            adapters.Sort();
            adapters.Add(adapters.Max() + 3);

            return adapters;
        }

        public (int, int) ProcessAdapterChain(List<int> adapters)
        {
            var oneStep = 0;
            var threeStep = 0;
            var toggleables = 0;

            var currJoltage = 0;
            while(adapters.Count > 0)
            {
                var min = adapters.Min();
                adapters.Remove(min);

                switch (min - currJoltage)
                {
                    case 1:
                        oneStep++;
                        break;

                    case 3:
                        threeStep++;
                        break;

                    default:
                        throw new Exception("we shouldn't have anything besides 1 or 3 steps");
                }

                currJoltage = min;
            }

            return (oneStep, threeStep);
        }


        public void ProcessUniqueChains(List<int> adapters, int joltage, int max, ref long uniqueChains/*, string currPath = ""*/)
        {
            if(joltage == max)
            {
                uniqueChains++;
                return;
            }
            //if (joltage == 0)
            //    currPath = $"(0)";
            //else
            //{
            //    if (joltage == adapters.Max())
            //    {
            //        currPath = $"{currPath}, ({joltage})";
            //        uniqueChains++;
            //        return;
            //    }
            //    else
            //        currPath = $"{currPath}, {joltage}";
            //}

            var valids = adapters.Where(a => 0 < (a - joltage) && (a - joltage) <= 3).ToList();
            foreach (var valid in valids)
                ProcessUniqueChains(adapters, valid, max, ref uniqueChains/*, currPath*/);
        }

        public override string Part1(bool runSample = false)
        {
            var adapters = LoadAdapters(runSample);
            var (one, three) = ProcessAdapterChain(adapters);

            return (one * three).ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var adapters = LoadAdapters(runSample);

            var start = LoadNodes(adapters);

            ReduceNodes(start);


            var stats = new Stats();
            CountUniqueChains(start, stats);

            return stats.Count.ToString();

            long unique = 0;
            ProcessUniqueChains(adapters, 0, adapters.Max(), ref unique);

            return unique.ToString();
        }


        

    }
}
