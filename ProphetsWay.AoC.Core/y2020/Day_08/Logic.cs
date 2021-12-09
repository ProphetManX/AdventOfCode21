namespace ProphetsWay.AoC.Core.y2020.Day_08
{
    public class Logic : BaseLogic
    {
        public Step[] LoadSteps(bool runSample)
        {
            var reader = GetInputTextReader(runSample);

            var steps = new List<Step>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var step = new Step(line);
                steps.Add(step);
            }

            return steps.ToArray();
        }

        public (int, bool) ProcessSteps(Step[] steps)
        {
            var accumulator = 0;
            var cleanExit = false;

            var uniqueStepIds = new List<int>();
            var currStepIndex = 0;

            while (!uniqueStepIds.Contains(currStepIndex))
            {
                if (steps.Length <= currStepIndex)
                {
                    cleanExit = true;
                    break;
                }

                uniqueStepIds.Add(currStepIndex);
                var currStep = steps[currStepIndex];

                switch (currStep.Action)
                {
                    case "nop":
                        currStepIndex++;
                        break;

                    case "acc":
                        currStepIndex++;
                        accumulator += currStep.Magnitude;
                        break;

                    case "jmp":
                        currStepIndex += currStep.Magnitude;
                        break;
                }
            }


            return (accumulator, cleanExit);
        }

        public override string Part1(bool runSample = false)
        {
            var steps = LoadSteps(runSample);

            var (acc, clean) = ProcessSteps(steps);

            return acc.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var steps = LoadSteps(runSample);

            var (acc, clean) = ProcessSteps(steps);

            var stepChangeIndex = 0;
            while (!clean)
            {
                var currStepChange = steps[stepChangeIndex];
                var origAction = currStepChange.Action;

                switch (origAction)
                {
                    case "nop":
                        currStepChange.Action = "jmp";
                        break;

                    case "jmp":
                        currStepChange.Action = "nop";
                        break;

                    default:
                        stepChangeIndex++;
                        continue;
                }

                //need to try and change jmp to nop and vice versa?

                (acc, clean) = ProcessSteps(steps);
                currStepChange.Action = origAction;
                stepChangeIndex++;
            }

            return acc.ToString();
        }


        public class Step
        {
            public Step(string raw)
            {
                var parts = raw.Split(" ");
                Action= parts[0];
                Magnitude = int.Parse(parts[1]);
            }

            public override string ToString()
            {
                return $"{Action} {Magnitude}";
            }

            public string Action { get; set; }
            public int Magnitude { get; }
        }

    }
}
