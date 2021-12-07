using NUnit.Framework;
using ProphetsWay.AoC.Core.Day_04;

using System;

namespace ProphetsWay.AoC.Core.Test
{
    public class Day_04_Tests
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

            Assert.AreEqual(result, 27027);
            Console.WriteLine($"Day 04 Part 1 Result: {result}");
        }

        [Test]
        public void TestPart2()
        {
            var logic = new Logic();

            var result = logic.Part2();

            Assert.AreEqual(result, 36975);

            Console.WriteLine($"Day 04 Part 2 Result: {result}");
        }


    }
}