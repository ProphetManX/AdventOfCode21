using System.Data.Common;

namespace ProphetsWay.AoC.Core.y2020.Day_07
{
    public class Logic : BaseLogic
    {

        public Dictionary<string, Bag> ParseBagRules(bool runSample)
        {
            var reader = GetInputTextReader(runSample);

            var uniqueBags = new Dictionary<string, Bag>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var basic = line.Split("bags contain");
                var bagName = basic[0].Trim();

                if (!uniqueBags.ContainsKey(bagName))
                    uniqueBags.Add(bagName, new Bag(bagName));

                var bag = uniqueBags[bagName];

                var rawRules = basic[1].Replace(".", "").Split(",");

                foreach (var rawRule in rawRules)
                {
                    var trimRule = rawRule.Trim();

                    if (trimRule == "no other bags")
                        continue;

                    var firstSpace = trimRule.IndexOf(" ");
                    var qtyStr = trimRule.Substring(0, firstSpace);
                    var rule = trimRule.Substring(firstSpace).Replace("bags", "").Replace("bag", "").Trim();

                    var bg = new BagRule();
                    bg.Quantity = int.Parse(qtyStr);

                    if (!uniqueBags.ContainsKey(rule))
                        uniqueBags.Add(rule, new Bag(rule));

                    var ruleBag = uniqueBags[rule];
                    bg.Bag = ruleBag;

                    bag.Rules.Add(bg);

                    if (!ruleBag.ParentBags.Contains(bag))
                        ruleBag.ParentBags.Add(bag);
                }
            }

            return uniqueBags;
        }

        public void CrawlParents(Bag bag, List<string> uniqueParents)
        {
            foreach(var parent in bag.ParentBags)
            {
                if (uniqueParents.Contains(parent.Name))
                    continue;

                uniqueParents.Add(parent.Name);

                CrawlParents(parent, uniqueParents);
            }
        }

        public void CrawlChildren(Bag bag, List<string> children)
        {
            foreach(var child in bag.Rules)
            {
                for (var i = 0; i < child.Quantity; i++)
                {
                    children.Add(child.Bag.Name);
                    CrawlChildren(child.Bag, children);
                }
            }
        }

        public override string Part1(bool runSample = false)
        {
            var bags = ParseBagRules(runSample);
            var targetBag = "shiny gold";

            var uniqueParents = new List<string>();
            CrawlParents(bags[targetBag], uniqueParents);

            return uniqueParents.Count.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var bags = ParseBagRules(runSample);
            var targetBag = "shiny gold";

            var children = new List<string>();
            var parent = bags[targetBag];

            CrawlChildren(parent, children);

            return children.Count.ToString();
        }

        
        
        public class Bag
        {
            public override string ToString()
            {
                return $"{Name} : Total Rules: {Rules.Count}";
            }

            public Bag(string name)
            {
                Name = name;
                Rules = new List<BagRule>();
                ParentBags = new List<Bag>();
            }

            public string Name { get; }

            public List<BagRule> Rules { get; }

            public List<Bag> ParentBags { get; }
        }

        public class BagRule
        {
            public override string ToString()
            {
                return $"{Quantity} {Bag}";
            }

            public int Quantity { get; set; }
            public Bag Bag { get; set; }
        }

    }
}
