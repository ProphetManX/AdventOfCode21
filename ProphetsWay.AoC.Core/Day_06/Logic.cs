using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProphetsWay.AoC.Core.Day_06
{
    public class Logic : BaseLogic
    {
        public long ProcessFish(int days)
        {
            var reader = GetInputTextReader();
            
            //a listing of current quantity of fish at each stage of their cooldowns
            var school = new Dictionary<int, long>();
            for (var i = 0; i < 9; i++)
                school.Add(i, 0);


            var init = reader.ReadToEnd();
            var initStr = init.Split(",");

            foreach (var str in initStr)
            {
                var initInt = int.Parse(str);
                school[initInt]++;
            }


            for (var i = 0; i < days; i++)
            {
                var tomorrowSchool = new Dictionary<int, long>();
                tomorrowSchool.Add(8, school[0]);
                tomorrowSchool.Add(7, school[8]);
                tomorrowSchool.Add(6, school[7] + school[0]);
                tomorrowSchool.Add(5, school[6]);
                tomorrowSchool.Add(4, school[5]);
                tomorrowSchool.Add(3, school[4]);
                tomorrowSchool.Add(2, school[3]);
                tomorrowSchool.Add(1, school[2]);
                tomorrowSchool.Add(0, school[1]);

                school = tomorrowSchool;
            }

            long totalFish = 0;
            foreach (var val in school.Values)
                totalFish += val;

            return totalFish;
        }

        public override string Part1()
        {
            return ProcessFish(80).ToString();
        }

        public override string Part2()
        {
            return ProcessFish(256).ToString();
        }

        public class Fish
        {
            public Fish(int currentCooldown)
            {
                Cooldown = currentCooldown;
            }

            public int Cooldown { get; private set; }

            public Fish ProcessDay()
            {
                if(Cooldown == 0)
                {
                    Cooldown = 6;
                    return new Fish(8);
                }

                Cooldown--;
                return null;
            }
        }
    
    }
}
