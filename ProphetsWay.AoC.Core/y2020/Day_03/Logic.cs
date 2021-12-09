namespace ProphetsWay.AoC.Core.y2020.Day_03
{
    public class Logic : BaseLogic
    {
        public Dictionary<int, Dictionary<int, char>> LoadMap(bool runSample = false)
        {
            var reader = GetInputTextReader(runSample);

            var map = new Dictionary<int, Dictionary<int, char>>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var row = new Dictionary<int, char>();
                foreach (var plot in line)
                    row.Add(row.Count, plot);

                map.Add(map.Count, row);
            }

            return map;
        }

        public long CalculateTreesHit(int right, int down, Dictionary<int, Dictionary<int, char>> map)
        {
            long treesHit = 0;
            var columns = map[0].Count;
            var x = 0;
            for (var y = 0; y < map.Count; y+= down)
            {
                if (map[y][x % columns] == '#')
                    treesHit++;

                x += right;
            }

            return treesHit;
        }


        public override string Part1(bool runSample = false)
        {
            var map = LoadMap(runSample);
            var treesHit = CalculateTreesHit(3, 1, map);
            return treesHit.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var map = LoadMap(runSample);

            var a = CalculateTreesHit(1, 1, map);
            var b = CalculateTreesHit(3, 1, map);
            var c = CalculateTreesHit(5, 1, map);
            var d = CalculateTreesHit(7, 1, map);
            var e = CalculateTreesHit(1, 2, map);

            var product = a * b * c * d * e;

            return product.ToString();
        }
    }
}
