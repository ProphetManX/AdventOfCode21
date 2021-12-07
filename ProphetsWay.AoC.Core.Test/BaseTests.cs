using NUnit.Framework;
using System;
using System.Linq;

namespace ProphetsWay.AoC.Core.Test
{
    public abstract class BaseTests<T> where T: BaseLogic, new()
    {
        private T _logic;
        private string _day;

        public abstract long Part1Result { get; }
        public abstract long Part2Result { get; }

        [SetUp]
        public void Setup()
        {
            _logic = new T();
            var t = _logic.GetType();
            var ns = t.Namespace;
            _day = ns.Split(".").Last();
        }

        [Test]
        public void TestPart1()
        {
            var result = _logic.Part1();
            Assert.AreEqual(result, Part1Result);
            Console.WriteLine($"{_day} Part 1 Result: {result}");
        }

        [Test]
        public void TestPart2()
        {
            var result = _logic.Part2();
            Assert.AreEqual(result, Part2Result);
            Console.WriteLine($"{_day} Part 2 Result: {result}");
        }
    }
}