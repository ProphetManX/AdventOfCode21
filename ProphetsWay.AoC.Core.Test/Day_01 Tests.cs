using NUnit.Framework;
using ProphetsWay.AoC.Core.Day_01;

using System;

namespace ProphetsWay.AoC.Core.Test
{
    public class Day_01_Tests
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

            Assert.AreEqual(result, 1298);
            Console.WriteLine($"Day 01 Part 1 Result: {result}");
        }

        [Test]
        public void TestPart2()
        {
            var logic = new Logic();

            var result = logic.Part2();

            Assert.AreEqual(result, 1248);
            Console.WriteLine($"Day 01 Part 2 Result: {result}");
        }


    }
}