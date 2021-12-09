namespace ProphetsWay.AoC.Core.y2021.Day_08
{
    public class Logic : BaseLogic
    {
        public List<Entry> LoadData(bool runSample)
        {
            var entries = new List<Entry>();

            var reader = GetInputTextReader(runSample);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                entries.Add(new Entry(line));
            }

            return entries;
        }

        public override string Part1(bool runSample = false)
        {
            var entries = LoadData(runSample);

            var easyNumbers = 0;
            foreach (var entry in entries)
                foreach (var number in entry.Outputs)
                {
                    switch (number.Segments.Length)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 7:
                            easyNumbers++;
                            break;

                        default:
                            break;
                    }
                }

            return easyNumbers.ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var entries = LoadData(runSample);

            var runningSum = 0;
            foreach(var entry in entries)
            {
                entry.DecodeSegmentWires();
                runningSum += entry.OutputValue;
            }

            return runningSum.ToString();
        }

        public class Entry
        {
            public Entry(string rawData)
            {
                RawData = rawData;
                var parts = rawData.Split(" | ");
                InputString = parts[0];
                OutputString = parts[1];

                Inputs = ParseDisplays(InputString);
                Outputs = ParseDisplays(OutputString);

                OutputValues = "";
            }

            private void Decode_17A()
            {
                One = Inputs.Single(x => x.Number == 1);
                Seven = Inputs.Single(x => x.Number == 7);

                var sevenSegments = Seven.Segments.ToList();
                foreach (var segment in One.Segments)
                    sevenSegments.Remove(segment);

                A = sevenSegments.Single();
            }

            private void Decode_49G()
            {
                Four = Inputs.Single(x => x.Number == 4);

                var possibleNines = Inputs.Where(x => x.Segments.Length == 6);
                foreach (var possibleNine in possibleNines)
                {
                    var segments = possibleNine.Segments.ToList();
                    foreach (var segment in Four.Segments)
                        segments.Remove(segment);

                    if (segments.Count == 2)
                    {
                        Nine = possibleNine;
                        Nine.Number = 9;

                        segments.Remove(A);

                        G = segments.Single();
                        break;
                    }
                }
            }

            private void Decode_06()
            {
                var oneSegments = One.Segments.ToList();
                var possibleZeros = Inputs.Where(x => x.Segments.Length == 6 && x != Nine);
                foreach(var possibleZero in possibleZeros)
                {
                    var segments = possibleZero.Segments;
                    var containsBoth = true;
                    
                    foreach(var segment in oneSegments)
                        if (!segments.Contains(segment))
                            containsBoth = false;

                    if (containsBoth)
                    {
                        Zero = possibleZero;
                        Zero.Number = 0;
                    }
                    else
                    {
                        Six = possibleZero;
                        Six.Number = 6;
                    }
                }
            }

            private void Decode_DE()
            {
                //remove all of 0 from 9, you're left with D
                var nineSegments = Nine.Segments.ToList();
                foreach (var zeroSegment in Zero.Segments.ToList())
                    nineSegments.Remove(zeroSegment);

                D = nineSegments.Single();

                //remove all of 9 from 0, you're left with E
                var zeroSegments = Zero.Segments.ToList();
                foreach(var nineSegment in Nine.Segments.ToList())
                    zeroSegments.Remove(nineSegment);

                E = zeroSegments.Single();
            }

            private void Decode_B()
            {
                var fourSegments = Four.Segments.ToList();
                foreach(var oneSegment in One.Segments.ToList())    
                    fourSegments.Remove(oneSegment);

                fourSegments.Remove(D);

                B = fourSegments.Single();
            }

            private void Decode_CF()
            {
                var fourSegments = Four.Segments.ToList();
                foreach (var sixSegments in Six.Segments.ToList())
                    fourSegments.Remove(sixSegments);

                C = fourSegments.Single();

                fourSegments = Four.Segments.ToList();
                fourSegments.Remove(B);
                fourSegments.Remove(D);
                fourSegments.Remove(C);

                F = fourSegments.Single();
            }

            public void DecodeSegmentWires()
            {
                Decode_17A();
                Decode_49G();
                Decode_06();
                Decode_DE();
                Decode_B();
                Decode_CF();

                foreach (var output in Outputs)
                {
                    output.DecodeNumber(A, B, C, D, E, F, G);
                    OutputValues += output.Number;
                }

                OutputValue = int.Parse(OutputValues);
            }

            public char A { get; set; }
            public char B { get; set; }
            public char C { get; set; }
            public char D { get; set; }   
            public char E { get; set; }   
            public char F { get; set; }
            public char G { get; set; }

            public Display Zero { get; private set; }
            public Display One { get; private set; }
            public Display Two { get; private set; }
            public Display Three { get; private set; }
            public Display Four { get; private set; }
            public Display Five { get; private set; }
            public Display Six { get; private set; }
            public Display Seven { get; private set; }
            public Display Eight { get; private set; }
            public Display Nine { get; private set; }


            public string RawData { get; set; }

            public string InputString { get; set; }

            public string InputValues { get; private set; }

            public string OutputString { get; set; }

            public string OutputValues { get; private set; }
            public int OutputValue { get; private set; }

            public Display[] Inputs { get; set; }
            public Display[] Outputs { get; set; }

        }

        public static Display[] ParseDisplays(string sequence)
        {
            var displays = new List<Display>();
            foreach (var number in sequence.Split(" "))
                displays.Add(new Display(number));

            return displays.ToArray();
        }

        public class Display
        {
            public override string ToString()
            {
                return $"{Segments} | {Number}";
            }

            public void DecodeNumber(char A, char B, char C, char D, char E, char F, char G)
            {
                if (Number.HasValue)
                    return;

                switch (Segments.Length)
                {
                    case 5:
                        //either 2, 3, or 5
                        if (Segments.Contains(C))
                        {
                            if (Segments.Contains(F))
                                Number = 3;
                            else
                                Number = 2;
                        }
                        else
                            Number = 5;
                        break;

                    case 6:
                        // either 0, 6, or 9
                        if (Segments.Contains(D))
                        {
                            if (Segments.Contains(C))
                                Number = 9;
                            else
                                Number = 6;
                        }
                        else
                            Number = 0;
                        break;
                }
            }

            public Display(string segments)
            {
                Segments = segments;
                switch (Segments.Length)
                {
                    case 2:
                        Number = 1;
                        break;

                    case 3:
                        Number = 7;
                        break;

                    case 4:
                        Number = 4;
                        break;

                    case 7:
                        Number = 8;
                        break;

                    default:
                        Number = null;
                        break;
                }
            }

            public int? Number { get; set; }

            public string Segments { get; set; }
        }

    }
}
