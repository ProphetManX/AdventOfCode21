using NUnit.Framework;
using ProphetsWay.AoC.Core.Day_06;

using System;

namespace ProphetsWay.AoC.Core.Test
{
    public class Day_06_Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestPart1()
        {
            var logic = new Logic();

            var result = logic.Part1();

            Assert.AreEqual(result, 380243);
            Console.WriteLine($"Day 06 Part 1 Result: {result}");
        }

        [Test]
        public void TestPart2()
        {
            var logic = new Logic();

            var result = logic.Part2();

            Assert.AreEqual(result, 1708791884591);

            Console.WriteLine($"Day 06 Part 2 Result: {result}");
        }


    }
}