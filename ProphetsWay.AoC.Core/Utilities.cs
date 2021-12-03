namespace ProphetsWay.AoC.Core
{
    public static class Utilities
    {
        public static string ReadFile(this FileInfo fileInfo)
        {
            var reader = fileInfo.OpenText();
            var text = reader.ReadToEnd();
            return text;
        }



    }
}
