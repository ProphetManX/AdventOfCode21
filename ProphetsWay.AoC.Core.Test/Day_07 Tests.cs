using NUnit.Framework;
using ProphetsWay.AoC.Core.Day_07;

using System;

namespace ProphetsWay.AoC.Core.Test
{
    public class Day_07_Tests
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

            Assert.AreEqual(result, 355150);
            Console.WriteLine($"Day 07 Part 1 Result: {result}");
        }

        [Test]
        public void TestPart2()
        {
            var logic = new Logic();

            var result = logic.Part2();

            Assert.AreEqual(result, 98368490);

            Console.WriteLine($"Day 07 Part 2 Result: {result}");
        }


    }
}