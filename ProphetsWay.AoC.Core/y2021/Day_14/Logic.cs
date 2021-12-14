namespace ProphetsWay.AoC.Core.y2021.Day_14
{
    public class Logic : BaseLogic
    {
        public (string, Dictionary<string, string>) LoadDataSet(bool runSample)
        {
            var reader = GetInputTextReader(runSample);

            var rules = new Dictionary<string, string>();
            var template = string.Empty;

            var parsingRules = false;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                if (string.IsNullOrEmpty(line))
                {
                    parsingRules = true;
                    continue;
                }

                if (parsingRules)
                {
                    var parts = line.Split(" -> ");
                    rules.Add(parts[0], parts[1]);
                }
                else
                    template = line;
            }

            return (template, rules);
        }

        public string ProcessTemplateTimes(string strTemplate, Dictionary<string, string> rules, int times)
        {
            var template = LoadTemplate(strTemplate);

            for (var i = 0; i < times; i++)
                template = ProcessTemplate(template, rules);

            var metrics = new Dictionary<char, long>();
            foreach (var keyPair in template)
            {
                var a = keyPair.Key[0];
                if (!metrics.ContainsKey(a))
                    metrics.Add(a, 0);

                metrics[a] += keyPair.Value;
            }

            metrics[strTemplate[strTemplate.Length - 1]]++;

            var min = metrics.Values.Min();
            var max = metrics.Values.Max();

            var diff = max - min;

            return diff.ToString();
        }

        public Dictionary<string, long> ProcessTemplate(Dictionary<string, long> currTemplate, Dictionary<string, string> rules)
        {
            var newTemplate = new Dictionary<string, long>();

            foreach(var keyPair in currTemplate)
            {
                var insert = rules[keyPair.Key];
                var newA = $"{keyPair.Key[0]}{insert}";
                var newB = $"{insert}{keyPair.Key[1]}";

                if (!newTemplate.ContainsKey(newA))
                    newTemplate.Add(newA, 0);

                if (!newTemplate.ContainsKey(newB))
                    newTemplate.Add(newB, 0);

                newTemplate[newA] += keyPair.Value;
                newTemplate[newB] += keyPair.Value;
            }

            return newTemplate;
        }

        public Dictionary<string, long> LoadTemplate(string template)
        {
            var newTemplate = new Dictionary<string, long>();

            for(var i = 1; i< template.Length; i++)
            {
                var pair = $"{template[i-1]}{template[i]}";
             
                if (!newTemplate.ContainsKey(pair))
                    newTemplate.Add(pair, 0);

                newTemplate[pair]++;
            }

            return newTemplate;
        }


        public override string Part1(bool runSample = false)
        {
            var (template, rules) = LoadDataSet(runSample);
            return ProcessTemplateTimes(template, rules, 10);
        }

        public override string Part2(bool runSample = false)
        {
            var (template, rules) = LoadDataSet(runSample);
            return ProcessTemplateTimes(template, rules, 40);
        }
       
    }
}

