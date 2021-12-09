namespace ProphetsWay.AoC.Core.y2021.Day_01
{
    public class Logic : BaseLogic
    {
        private string Part1Logic(StreamReader reader)
        {
            int? last = null;
            var increases = 0;

            //process each number into a number list
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrEmpty(line))
                    break;

                var value = int.Parse(line);

                if (last != null)
                {
                    if (value > last)
                        increases++;
                }

                last = value;
            }

            return increases.ToString();
        }

        public override string Sample1()
        {
            var reader = GetInputTextReader(true);
            return Part1Logic(reader);
        }

        public override string Part1()
        {
            var reader = GetInputTextReader();
            return Part1Logic(reader);
        }

        public override string Sample2()
        {
            var reader = GetInputTextReader(true);
            return Part2Logic(reader);
        }

        public override string Part2()
        {
            var reader = GetInputTextReader();
            return Part2Logic(reader);
        }

        public string Part2Logic(StreamReader reader)
        {
            int? windowAlpha = null;
            int? windowBravo = null;
            int? windowCharlie = null;
            Window last = null;
            Window curr = null;
            var increases = 0;

            var windows = new List<Window>();

            //process each number into a number list
            
            var line = reader.ReadLine();
            for (; ; )
            {
                if(windowCharlie != null && windowAlpha != null && windowBravo != null)
                {
                    curr = new Window(windowAlpha.Value, windowBravo.Value, windowCharlie.Value);
                    windows.Add(curr);

                    if (last != null)
                    {
                        Console.Write($"Window {windows.Count,4}: {curr}");
                        if(curr.Value > last.Value)
                        {
                            increases++;
                            Console.Write("  (increased)");
                        }

                        Console.WriteLine();
                    }

                    last = curr;    
                }

                if (string.IsNullOrEmpty(line))
                    break;

                var value = int.Parse(line);

                windowCharlie = windowBravo;
                windowBravo = windowAlpha;
                windowAlpha = value;

                line = reader.ReadLine();
            }

            return increases.ToString();
        }

        public class Window
        {
            public Window(int alpha, int bravo, int charlie)
            {
                Alpha = alpha;
                Bravo = bravo;
                Charlie = charlie;
            }

            public int Alpha;
            public int Bravo;
            public int Charlie;

            public int Value => Alpha + Bravo + Charlie;

            public override string ToString()
            {
                return $"Alpha:  {Alpha,4} | Bravo:  {Bravo,4} | Charlie:  {Charlie,4} | Value:  {Value,4}";
            }
        }

    }
}
