using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProphetsWay.AoC.Core.y2021.Day_02
{
    public class Logic : BaseLogic
    {
        public override string Part1()
        {
            //read in input text file
            var reader = GetInputTextReader();

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

        public override string Part2()
        {
            //read in input text file
            var reader = GetInputTextReader();

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
