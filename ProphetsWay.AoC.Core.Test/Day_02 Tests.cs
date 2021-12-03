using NUnit.Framework;
using ProphetsWay.AoC.Core.Day_02;

using System;

namespace ProphetsWay.AoC.Core.Test
{
    public class Day_02_Tests
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

            Assert.AreEqual(result, 1698735);
            Console.WriteLine($"Day 02 Part 1 Result: {result}");
        }

        [Test]
        public void TestPart2()
        {
            var logic = new Logic();

            var result = logic.Part2();

            Assert.AreEqual(result, 1594785890);
            Console.WriteLine($"Day 02 Part 2 Result: {result}");
        }


    }
}