using NUnit.Framework;
using ProphetsWay.AoC.Core.Day_03;

using System;

namespace ProphetsWay.AoC.Core.Test
{
    public class Day_03_Tests
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

            Assert.AreEqual(result, 852500);
            Console.WriteLine($"Day 03 Part 1 Result: {result}");
        }

        [Test]
        public void TestPart2()
        {
            var logic = new Logic();

            var result = logic.Part2();

            Assert.AreNotEqual(result, 319104);  //wrong answer
            Assert.AreEqual(result, 1007985);

            Console.WriteLine($"Day 03 Part 2 Result: {result}");
        }


    }
}