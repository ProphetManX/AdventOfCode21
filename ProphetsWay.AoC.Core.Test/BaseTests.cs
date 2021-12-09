using NUnit.Framework;
using System;
using System.Linq;

namespace ProphetsWay.AoC.Core.Test
{
    public abstract class BaseTests<T> where T: BaseLogic, new()
    {
        private T _logic;
        private string _day;

        public abstract Results Part1 { get; }
        public abstract Results Part2 { get; }

        public class Results
        {
            public Results(string sample, string test)
            {
                Sample = sample;
                Test = test;
            }

            public string Sample { get; }
            public string Test { get; }
        }

        [SetUp]
        public void Setup()
        {
            _logic = new T();
            var t = _logic.GetType();
            var ns = t.Namespace;
            _day = ns.Split(".").Last();
        }

        [Test]
        public void TestSample1()
        {
            var result = _logic.Part1(true);
            Assert.AreEqual(Part1.Sample, result);
            Console.WriteLine($"{_day} Sample 1 Result: {result}");
        }

        [Test]
        public void TestPart1()
        {
            var result = _logic.Part1();
            Assert.AreEqual(Part1.Test, result);
            Console.WriteLine($"{_day} Part 1 Result: {result}");
        }

        [Test]
        public void TestSample2()
        {
            var result = _logic.Part2(true);

            if (string.IsNullOrEmpty(Part2.Sample))
            {
                Console.WriteLine($"{_day} Sample 2 | SKIPPED no data");
                return;
            }

            Assert.AreEqual(Part2.Sample, result);
            Console.WriteLine($"{_day} Sample 2 Result: {result}");
        }

        [Test]
        public void TestPart2()
        {
            var result = _logic.Part2();
            Assert.AreEqual(Part2.Test, result);
            Console.WriteLine($"{_day} Part 2 Result: {result}");
        }
    }
}