using NUnit.Framework;
using ProphetsWay.AoC.Core.Day_05;

using System;

namespace ProphetsWay.AoC.Core.Test
{
    public class Day_05_Tests
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

            Assert.AreEqual(result, 6189);
            Console.WriteLine($"Day 05 Part 1 Result: {result}");
        }

        [Test]
        public void TestPart2()
        {
            var logic = new Logic();

            var result = logic.Part2();

            Assert.AreEqual(result, 19164);

            Console.WriteLine($"Day 05 Part 2 Result: {result}");
        }


    }
}