namespace ProphetsWay.AoC.Core
{
    public abstract class BaseLogic
    {
        public StreamReader GetInputTextReader(bool isSample)
        {
            var t = GetType();
            var ns = t.Namespace;
            var parts = ns.Split(".");
            var year = parts[3];
            var day = parts[4];

            var file = isSample
                ? "sample.txt"
                : "input.txt";

            var path = $"{Directory.GetCurrentDirectory()}\\{year}\\{day}\\{file}";
            var fi = new FileInfo(path);
            var reader = fi.OpenText();

            return reader;
        }

        public abstract string Part1(bool runSample = false);
        public abstract string Part2(bool runSample = false);
    }
}
