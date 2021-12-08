using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProphetsWay.AoC.Core.y2021.Day_03
{
    public class Logic : BaseLogic
    {
        public override string Part1()
        {
            //read in input text file
            var reader = GetInputTextReader();

            var totalReadings = 0;
            var positionPositives = new Dictionary<int, int>();

            while (true)
            {
                var line = reader.ReadLine();
                if (line == null)
                    break;
                
                totalReadings++;

                var pos = 0;
                foreach(var letter in line.ToCharArray())
                {
                    if (!positionPositives.ContainsKey(pos))
                        positionPositives.Add(pos, 0);

                    var val = int.Parse(letter.ToString());

                    positionPositives[pos] += val;
                    pos++;
                }
            }

            var half = totalReadings / 2;
            var sbGamma = new StringBuilder();
            var sbEpsilon = new StringBuilder();
            foreach (var key in positionPositives.Keys)
            {
                if (positionPositives[key] > half)
                {
                    sbGamma.Append("1");
                    sbEpsilon.Append("0");
                }
                else
                {
                    sbGamma.Append("0");
                    sbEpsilon.Append("1");
                }
            }

            var gamma = Convert.ToInt32(sbGamma.ToString(), 2);
            var epsilon = Convert.ToInt32(sbEpsilon.ToString(), 2);

            return (gamma * epsilon).ToString();
        }

        public override string Part2()
        {
            //read in input text file
            var reader = GetInputTextReader();

            var readings = new List<string>();

            while (true)
            {
                var line = reader.ReadLine();
                if (line == null)
                    break;

                readings.Add(line);
            }



            var strLength = readings.First().Length;

            var oxySubset = readings.ToArray().ToList();
            for (var i = 0; i < strLength; i++)
            {
                var oneStatus = MoreOnes(oxySubset, i);
                if (oneStatus.HasValue)
                {
                    var oxyChar = oneStatus.Value
                        ? '1'
                        : '0';

                    oxySubset = oxySubset.Where(x => x[i] == oxyChar).ToList();

                    if (oxySubset.Count == 1)
                        break;
                }
                else
                {
                    var x = "what?";
                }
            }


            var co2Subset = readings.ToArray().ToList();
            for (var i = 0; i < strLength; i++)
            {
                var oneStatus = MoreOnes(co2Subset, i);

                if (oneStatus.HasValue)
                {
                    var oxyChar = oneStatus.Value
                                        ? '0'
                                        : '1';

                    co2Subset = co2Subset.Where(x => x[i] == oxyChar).ToList();

                    if (co2Subset.Count == 1)
                        break;
                }
                else
                {
                    var x = "what?";
                }


            }

            var oxy = Convert.ToInt32(oxySubset.Single(), 2);
            var co2 = Convert.ToInt32(co2Subset.Single(), 2);

            return (oxy * co2).ToString();
        }


        private bool? MoreOnes(List<string> readings, int position)
        {
            var count = 0;
            foreach(var reading in readings)
                count += int.Parse(reading[position].ToString());

            if(count == 0 || count == readings.Count)
                return null;

            var half = (int)Math.Ceiling((float)readings.Count / 2);
            return count >= half;
        }

    }
}
