using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProphetsWay.AoC.Core.Day_02
{
    public class Logic
    {
        public int Part1()
        {
            //read in input text file
            var path = $"{Directory.GetCurrentDirectory()}\\Day_02\\input.txt";
            var fi = new FileInfo(path);
            var reader = fi.OpenText();

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

            return horizontal * depth;
        }

        public int Part2()
        {
            //read in input text file
            var path = $"{Directory.GetCurrentDirectory()}\\Day_02\\input.txt";
            var fi = new FileInfo(path);
            var reader = fi.OpenText();

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

            return horizontal * depth;
        }

    }
}
