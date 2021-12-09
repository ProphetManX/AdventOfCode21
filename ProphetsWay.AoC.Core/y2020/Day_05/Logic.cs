using System.Collections.Immutable;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ProphetsWay.AoC.Core.y2020.Day_05
{
    public class Logic : BaseLogic
    {
        public List<BoardingPass> LoadPasses(bool runSample)
        {
            var reader = GetInputTextReader(runSample);
            var passes = new List<BoardingPass>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var pass = new BoardingPass(line);
                passes.Add(pass);
            }

            return passes;
        }

        public override string Part1(bool runSample = false)
        {
            var passes = LoadPasses(runSample);

            return passes.Max(x=> x.ID).ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var passes = LoadPasses(runSample);

            var ids = new List<int>();
            foreach(var pass in passes)
                ids.Add(pass.ID);

            ids.Sort();

            for(var i = ids.First(); ; i++)
            {
                if (!ids.Contains(i))
                    return i.ToString();
            }

            return null;
        }

        public class BoardingPass
        {
            public int Row { get; }

            public int Col { get; }

            public int ID => Row * 8 + Col;

            public BoardingPass(string input)
            {
                var row = input.Substring(0, 7);
                var col = input.Substring(7);

                var rowBin = row.Replace("F", "0").Replace("B", "1");
                var colBin = col.Replace("L", "0").Replace("R", "1");

                Row = Convert.ToInt32(rowBin, 2);
                Col = Convert.ToInt32(colBin, 2);

                //FBFBBFF   RLR
                //0101100   101


            }



        }
    }
}
