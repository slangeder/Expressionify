using System.Diagnostics;
using System.Linq;
using Clave.Expressionify;
using Clave.Expressionify.Tests.First;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void TestExpressionify()
        {
            var data = new[]{
                "1",
                "2",
                "3"
            };

            data.AsQueryable()
                .Expressionify()
                .Select(x => x.ToInt())
                .ToList();

            Assert.Pass();
        }

        [Test]
        public void TestMethodParameterUsedTwice()
        {
            var data = new[]{
                1,
                2,
                3
            };

            data.AsQueryable()
                .Expressionify()
                .Select(x => x.Squared())
                .ToList();

            Assert.Pass();
        }

        [Test]
        public void TestMethodParameterUsedTwiceWithOverload()
        {
            var data = new[]{
                1.0,
                2.0,
                3.0
            };

            data.AsQueryable()
                .Expressionify()
                .Select(x => x.Squared())
                .ToList();

            Assert.Pass();
        }

        [Test]
        public void TestMethodWithMultipleArguments()
        {
            var data = new[]{
                new {a = "1", b = "5"},
                new {a = "3", b = "5"},
                new {a = "2", b = "5"},
            };

            data.AsQueryable()
                .Expressionify()
                .Select(x => x.a.Pluss(x.b))
                .ToList();

            Assert.Pass();
        }

        [Test]
        public void TestMethodCalledMultipleTimes()
        {
            var data = new[]{
                new {a = "1", b = "5"},
                new {a = "2", b = "5"},
                new {a = "3", b = "5"}
            };

            data.AsQueryable()
                .Expressionify()
                .Select(x => x.a.ToInt() + x.b.ToInt())
                .ToList();

            Assert.Pass();
        }

        [Test]
        public void TestExpressionifiedTwice()
        {
            var data = new[]{
                "1",
                "2",
                "3"
            };

            var sw = Stopwatch.StartNew();
            data.AsQueryable()
                .Expressionify()
                .Select(x => x.ToDouble())
                .ToList();
            var firstTime = sw.Elapsed;

            sw.Restart();
            data.AsQueryable()
                .Expressionify()
                .Select(x => x.ToDouble())
                .ToList();
            var secondTime = sw.Elapsed;

            Assert.Less(secondTime, firstTime);
        }

        [Test]
        public void TestNaming()
        {
            var name = ExpressionifyVisitor.GetExpressionifyClassName(typeof(ExtensionMethods).Name);

            Assert.AreEqual("ExtensionMethods_Expressionify", name);
        }
    }
}
