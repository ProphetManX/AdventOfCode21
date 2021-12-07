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
            var day = ns.Split(".").Last();

            var path = $"{Directory.GetCurrentDirectory()}\\{day}\\input.txt";
            var fi = new FileInfo(path);
            var reader = fi.OpenText();

            return reader;
        }

    }
}
