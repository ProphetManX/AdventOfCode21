namespace ProphetsWay.AoC.Core.y2020.Day_09
{
    public class Logic : BaseLogic
    {
       public List<long> ReadNumbers(bool runSample)
        {
            var reader = GetInputTextReader(runSample);
            var numbers = new List<long>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                numbers.Add(long.Parse(line));
            }

            return numbers;
        }

        public bool CanSumNumberFromWindow(long number, long[] window)
        {
            foreach(var num in window)
            {
                var remainder = number - num;
                if (window.Contains(remainder))
                    return true;
            }

            return false;
        }

        public long FindFirstBadNumber(int preambleLength, List<long> numbers)
        {
            var window = new long[preambleLength];

            for (var i = 0; i < window.Length; i++)
                window[i] = numbers[i];

            for(var i = window.Length; i < numbers.Count; i++)
            {
                var number = numbers[i];

                if (!CanSumNumberFromWindow(number, window))
                    return number;

                window[i % preambleLength] = number;
            }

            return -1;
        }

        public override string Part1(bool runSample = false)
        {
            var numbers = ReadNumbers(runSample);
            var preamble = runSample
                ? 5 : 25;

            var bad = FindFirstBadNumber(preamble, numbers);

            return bad.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var numbers = ReadNumbers(runSample);
            var preamble = runSample
                ? 5 : 25;

            var bad = FindFirstBadNumber(preamble, numbers);

            var range = new List<long>();
            for(var i = 0; i<numbers.Count; i++)
            {
                var remainder = bad;
                range = new List<long>();
                
                for (var j = 0; remainder > 0; j++)
                {
                    var curr = numbers[i + j];
                    range.Add(curr);
                    remainder = remainder - curr;
                }

                if (remainder == 0)
                    break;
            }

            range.Sort();
            var min = range.Min();
            var max = range.Max();

            return (min + max).ToString();
        }


        

    }
}
