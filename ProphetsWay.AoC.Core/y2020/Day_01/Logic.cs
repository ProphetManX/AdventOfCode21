namespace ProphetsWay.AoC.Core.y2020.Day_01
{
    public class Logic : BaseLogic
    {
        public Dictionary<int, int> GetNumberInput(bool isSample)
        {
            var reader = GetInputTextReader(isSample);

            var items = new Dictionary<int, int>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var value = int.Parse(line);
                items.Add(value, value);
            }

            return items;
        }

        public long? FindSumProduct(Dictionary<int, int> items, int targetValue)
        {
            foreach (var key in items.Keys)
            {
                var possibleMatch = targetValue - key;
                if (items.ContainsKey(possibleMatch))
                {
                    return key * items[possibleMatch];
                }
            }

            return null;
        }

        public override string Sample1()
        {
            var items = GetNumberInput(true);
            var result = FindSumProduct(items, 2020);

            return result?.ToString();
        }

        public override string Part1()
        {
            var items = GetNumberInput(false);

            var result = FindSumProduct(items, 2020);

            return result?.ToString();
        }

        private string Part2Logic(bool isSample)
        {
            var items = GetNumberInput(isSample);

            var min = items.Keys.Min();
            var max = items.Keys.Max();

            var targetValue = 2020;
            foreach (var key in items.Keys)
            {
                var remainder = targetValue - key;

                var subItems = items.Where(x => x.Key < remainder).Select(y => y.Value).ToDictionary(x => x);
                var partialResult = FindSumProduct(subItems, remainder);

                if (partialResult.HasValue)
                {
                    var result = partialResult.Value * (long)key;
                    return result.ToString();
                }
            }

            return null;
        }

        public override string Part2()
        {
            return Part2Logic(false);
        }

        public override string Sample2()
        {
            return Part2Logic(true);
        }



    }
}
