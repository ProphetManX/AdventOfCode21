namespace ProphetsWay.AoC.Core.y2021.Day_02
{
    public class Logic : BaseLogic
    {
        public override string Part1()
        {
            return Part1Logic(false);
        }

        public override string Part2()
        {
            return Part2Logic(false);
        }

        public override string Sample1()
        {
            return Part1Logic(true);
        }

        public override string Sample2()
        {
            return Part2Logic(true);
        }

        private string Part1Logic(bool isSample)
        {
            //read in input text file
            var reader = GetInputTextReader(isSample);

            int horizontal = 0;
            int depth = 0;

            while(true)
            {
                var line = reader.ReadLine();
                if (line == null)
                    break;

                var parts = line.Split(' ');
                var movement = int.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward":
                        horizontal += movement;
                        break;

                    case "down":
                        depth += movement;
                        break;

                    case "up":
                        depth -= movement;
                        break;

                    default:
                        break;
                }
            }

            return (horizontal * depth).ToString();
        }

        private string Part2Logic(bool isSample)
        {
            //read in input text file
            var reader = GetInputTextReader(isSample);

            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            while (true)
            {
                var line = reader.ReadLine();
                if (line == null)
                    break;

                var parts = line.Split(' ');
                var movement = int.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward":
                        horizontal += movement;
                        depth += (movement * aim);
                        break;

                    case "down":
                        aim += movement;
                        break;

                    case "up":
                        aim -= movement;
                        break;

                    default:
                        break;
                }
            }

            return (horizontal * depth).ToString();
        }

    }
}
