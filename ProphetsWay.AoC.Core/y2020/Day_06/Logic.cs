using System.Reflection.Metadata.Ecma335;

namespace ProphetsWay.AoC.Core.y2020.Day_06
{
    public class Logic : BaseLogic
    {
        public List<Group> LoadGroups(bool runSample)
        {
            var reader = GetInputTextReader(runSample);

            var groups = new List<Group>();
            var currGroup = new Group();
            groups.Add(currGroup);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                if (string.IsNullOrEmpty(line))
                {
                    currGroup = new Group();
                    groups.Add(currGroup);
                    continue;
                }

                var aSet = new AnswerSet(line);
                currGroup.AddPerson(aSet);
            }

            return groups;
        }

        public override string Part1(bool runSample = false)
        {
            var groups = LoadGroups(runSample);

            var total = groups.Sum(x => x.UniqueAnswers.Count);
            return total.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var groups = LoadGroups(runSample);

            var total = groups.Sum(x => x.UnanimousAnswers.Count);
            return total.ToString();
        }
        
        public class Group
        {
            public Group()
            {
                People = new List<AnswerSet>();
                UniqueAnswers = new List<char>();
                UnanimousAnswers = new List<char>();
            }

            public List<char> UniqueAnswers { get; }

            public List<char> UnanimousAnswers { get; }

            public void AddPerson(AnswerSet answerSet)
            {

                foreach (var question in answerSet.PositiveAnswers)
                {
                    if (!UniqueAnswers.Contains(question))
                        UniqueAnswers.Add(question);

             
                    if(People.Count == 0)
                        UnanimousAnswers.Add(question);
                }

                var badQuestions = new List<char>();
                foreach(var uQuestion in UnanimousAnswers)
                    if (!answerSet.PositiveAnswers.Contains(uQuestion))
                        badQuestions.Add(uQuestion);

                foreach (var bad in badQuestions)
                    UnanimousAnswers.Remove(bad);

                People.Add(answerSet);
            }

            public List<AnswerSet> People { get; }
        }

        public class AnswerSet
        {
            public AnswerSet(string personalAnswers)
            {
                PositiveAnswers = personalAnswers;
            }
            
            public string PositiveAnswers { get; }
        }

    }
}
