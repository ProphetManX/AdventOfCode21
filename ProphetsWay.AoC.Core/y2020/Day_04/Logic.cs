using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ProphetsWay.AoC.Core.y2020.Day_04
{
    public class Logic : BaseLogic
    {
        public List<Passport> LoadPassports(bool runSample)
        {
            var reader = GetInputTextReader(runSample);
            var passports = new List<Passport>();
            
            var sb = new StringBuilder();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                if (string.IsNullOrEmpty(line))
                {
                    var passport = new Passport(sb.ToString());
                    passports.Add(passport);

                    sb = new StringBuilder();
                    continue;
                }

                sb.Append(line);
                sb.Append(" ");
            }

            var last = new Passport(sb.ToString());
            passports.Add(last);

            return passports;
        }

        public override string Part1(bool runSample = false)
        {
            var passports = LoadPassports(runSample);

            return passports.Where(x => x.IsBasicValid).Count().ToString();
        }

        public override string Part2(bool runSample = false)
        {
            var passports = LoadPassports(runSample);

            return passports.Where(x => x.IsSmartValid).Count().ToString();
        }

        public class Passport
        {
            public Passport(string text)
            {
                AllFields = new List<Field>();

                Func<string, int, int, bool> checkRange = (val, min, max) =>
                {
                    if (string.IsNullOrEmpty(val))
                        return false;

                    if (!int.TryParse(val, out int valInt))
                        return false;

                    if (min <= valInt && valInt <= max)
                        return true;

                    return false;
                };

                Func<string, int, int, bool> checkYear = (val, min, max) =>
                {
                    if (string.IsNullOrEmpty(val) || val.Length != 4)
                        return false;

                    return checkRange(val, min, max);
                };

                //required Fields
                AllFields.Add(new Field("byr", (val) => checkYear(val, 1920, 2002)));
                AllFields.Add(new Field("iyr", (val) => checkYear(val, 2010, 2020)));
                AllFields.Add(new Field("eyr", (val) => checkYear(val, 2020, 2030)));

                AllFields.Add(new Field("hgt", (val) =>
                {
                    if (string.IsNullOrEmpty(val))
                        return false;

                    if (val.Contains("cm"))
                    {
                        var number = val.Replace("cm", "");
                        return checkRange(number, 150, 193);
                    }

                    if (val.Contains("in"))
                    {
                        var number = val.Replace("in", "");
                        return checkRange(number, 59, 76);
                    }

                    return false;
                }));

                AllFields.Add(new Field("hcl", (val) => {
                    if (string.IsNullOrEmpty(val) || val.Length != 7)
                        return false;

                    var hex = val.Replace("#", "");
                    if (hex.Length != 6)
                        return false;

                    try
                    {
                        Convert.FromHexString(hex);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }));

                AllFields.Add(new Field("ecl", (val)=>
                {
                    switch (val)
                    {
                        case "amb":
                        case "blu":
                        case "brn":
                        case "gry":
                        case "grn":
                        case "hzl":
                        case "oth":
                            return true;

                        default:
                            return false;
                    }
                }));

                AllFields.Add(new Field("pid", (val) =>
                {
                    if (string.IsNullOrEmpty(val) || val.Length != 9)
                        return false;

                    if (!int.TryParse(val, out int valInt))
                        return false;

                    return true;
                }));

                AllFields.Add(new Field("cid"));

                var props = text.Trim().Split(" ");
                foreach(var prop in props)
                {
                    var details = prop.Split(":");
                    var name = details[0];
                    var value = details[1];

                    var field = AllFields.SingleOrDefault(x => x.Name == name);
                    
                    if(field == null)
                    {
                        field = new Field(name);
                        AllFields.Add(field);
                    }

                    field.Value = value;
                }
            }

            public bool IsSmartValid => !AllFields.Any(x=> x.IsRequired && !x.IsSmartValid);

            public bool IsBasicValid => !AllFields.Any(x => x.IsRequired && !x.IsBasicValid);

            public List<Field> AllFields { get; set; }
        }

        public class Field
        {
            public override string ToString()
            {
                return $"{Name}: {Value,15} | IsSmartValid: {IsSmartValid,5} |IsBasicValid: {IsSmartValid,5} | IsRequired: {IsRequired,5}";
            }

            public Field(string name, Func<string,bool> validateFN = null)
            {
                Name = name;
                IsRequired = validateFN != null;

                if (IsRequired)
                    Validate = validateFN;
                else
                    Validate = (val) => { return true; };
            }

            public string Name { get; set; }

            public string Value { get; set; }

            public bool IsRequired { get; set; }

            public Func<string,bool> Validate { get; set; }

            public bool IsSmartValid => !IsRequired || Validate(Value);

            public bool IsBasicValid => !IsRequired || !string.IsNullOrEmpty(Value);
        }
    }
}
