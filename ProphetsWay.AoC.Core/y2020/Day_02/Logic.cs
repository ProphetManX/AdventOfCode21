namespace ProphetsWay.AoC.Core.y2020.Day_02
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
            var reader = GetInputTextReader(isSample);

            var valid = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var parts = line.Split(" ");
                var charConstraints = parts[0];
                var charTarget = parts[1];
                var password = parts[2];

                var limits = charConstraints.Split("-");
                var min = int.Parse(limits[0]);
                var max = int.Parse(limits[1]);

                var reqLetter = charTarget[0];
                var letterCount = password.Count(x=> x == reqLetter);

                if (min <= letterCount && letterCount <= max)
                    valid++;
            }

            return valid.ToString();
        }

        private string Part2Logic(bool isSample)
        {
            var reader = GetInputTextReader(isSample);

            var valid = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var parts = line.Split(" ");
                var charConstraints = parts[0];
                var charTarget = parts[1];
                var password = parts[2];

                var limits = charConstraints.Split("-");
                var min = int.Parse(limits[0]);
                var max = int.Parse(limits[1]);

                var reqLetter = charTarget[0];

                if(password.Length >= max)
                {
                    var alpha = password[min - 1];
                    var bravo = password[max - 1];

                    if(alpha == reqLetter || bravo == reqLetter)
                    {
                        if (alpha == bravo)
                            continue;

                        valid++;
                    } 
                }
                else
                {
                    var x = "what";
                }
            }

            return valid.ToString();
        }

       

    }
}
