namespace ProphetsWay.AoC.Core.y2021.Day_10
{
    public class Logic : BaseLogic
    {

        private Dictionary<char, long> _illegalScore = new Dictionary<char, long>
        {
            {')', 3 },
            {']', 57 },
            {'}', 1197 },
            {'>', 25137 }
        };

        private Dictionary<char, long> _autoCompleteScore = new Dictionary<char, long>
        {
            {')', 1 },
            {']', 2 },
            {'}', 3 },
            {'>', 4 }
        };

        private Dictionary<char, char> _pairs = new Dictionary<char, char>
        {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'},
            {'<', '>'}
        };


        public long CalculateSyntaxErrorPoints(string line, bool autoComplete = false)
        {
            var s = new Stack<char>();

            foreach(var c in line)
            {
                switch (c)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        s.Push(c);
                        break;

                    default:
                        if(s.TryPop(out char pop))
                        {
                            if (_pairs[pop] == c)
                                break;
                            else
                            {
                                return autoComplete
                                    ? 0
                                    : _illegalScore[c];
                            }
                        }
                        else
                        {
                            throw new Exception("this is weird, we have a close without an open");
                        }
                        break;
                }
            }

            if (autoComplete)
            {
                long totalScore = 0;

                foreach(var leftOpen in s)
                {
                    totalScore = totalScore * 5 + _autoCompleteScore[_pairs[leftOpen]];
                }

                return totalScore;
            }

            return 0;
        }
       


        public override string Part1(bool runSample = false)
        {
            var reader = GetInputTextReader(runSample);

            long totalScore = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                totalScore += CalculateSyntaxErrorPoints(line);
            }


            return totalScore.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var reader = GetInputTextReader(runSample);

            var scores = new List<long>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var score = CalculateSyntaxErrorPoints(line, true);
                if (score == 0)
                    continue;

                scores.Add(score);
            }

            scores.Sort();
            var midIndex = scores.Count / 2;
            return scores[midIndex].ToString();
        }

       
    }
}
