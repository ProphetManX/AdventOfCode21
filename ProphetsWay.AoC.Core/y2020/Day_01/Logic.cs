using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProphetsWay.AoC.Core.y2020.Day_01
{
    public class Logic : BaseLogic
    {
        public Dictionary<int, int> GetNumberInput()
        {
            var reader = GetInputTextReader();

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

        public override string Part1()
        {
            var items = GetNumberInput();

            var result = FindSumProduct(items, 2020);

            return result?.ToString();
        }

        public override string Part2()
        {
            var items = GetNumberInput();

            var min = items.Keys.Min();
            var max = items.Keys.Max();

            var targetValue = 2020;
            foreach(var key in items.Keys)
            {
                var remainder = targetValue - key;

                var subItems = items.Where(x => x.Key < remainder).Select(y=> y.Value).ToDictionary(x=> x);
                var partialResult = FindSumProduct(subItems, remainder);

                if (partialResult.HasValue)
                {
                    var result = partialResult.Value * (long)key;
                    return result.ToString();
                }
            }
            
            return null;
        }

       

    }
}
