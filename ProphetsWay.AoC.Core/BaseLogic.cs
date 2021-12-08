using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProphetsWay.AoC.Core
{
    public abstract class BaseLogic
    {
        public StreamReader GetInputTextReader()
        {
            var t = GetType();
            var ns = t.Namespace;
            var parts = ns.Split(".");
            var year = parts[3];
            var day = parts[4];

            var path = $"{Directory.GetCurrentDirectory()}\\{year}\\{day}\\input.txt";
            var fi = new FileInfo(path);
            var reader = fi.OpenText();

            return reader;
        }

        public abstract string Part1();
        public abstract string Part2();
    }
}
