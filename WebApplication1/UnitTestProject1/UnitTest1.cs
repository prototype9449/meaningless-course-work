using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlParser;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var expression = "1 = 1";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var expression = "1 >= 1";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var expression = "2 > 1";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var expression = "0 < 1";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var expression = "-1 < 1";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var expression = "1 < f";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, object>()
            {
                {"f", 2},
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var expression = "1 + f = 3";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, object>()
            {
                {"f", 2},
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var expression = "1 - 4 = -3";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod9()
        {
            var expression = "-3 + 5 = 2";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod10()
        {
            var expression = "-1 - 4 as int = 0 as int - 5 as int";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod11()
        {
            var expression = "3 - (-5) = 8";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod12()
        {
            var expression = "(1 * (-3)) + (5 - 8) * 3 + 0/2 = -12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod13()
        {
            var expression = "0 / 2 = 0";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod14()
        {
            var expression = "2 * (5 / 2 ) = 4";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod15()
        {
            var expression = "( 5 - (-1)) / 2 = 3";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod16()
        {
            var expression = "( 5 * (-1)) / 2 = -2";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod17()
        {
            var expression = "( 5 + 2 ) * 3 + 2 * ( 1 + 3 ) = 29";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod18()
        {
            var expression = "12 >= 12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod19()
        {
            var expression = "15 >= 12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod20()
        {
            var expression = "11 <= 12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod21()
        {
            var expression = "11 <= 12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod22()
        {
            var expression = "11 + 10 > 12 and \"3,5\" as double > 0.5";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod23()
        {
            var expression = "f = z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, object>()
            {
                {"f", new byte[]{1,2}},
                {"z", new byte[]{1,2}}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod24()
        {
            var expression = "f = z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, object>()
            {
                {"f", new TimeSpan(1,1,1,1,1)},
                {"z", new TimeSpan(1,1,1,1,1)}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod25()
        {
            var expression = "f = z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, object>()
            {
                {"f", new DateTime(1,1,1)},
                {"z", new DateTime(1,1,1)}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod26()
        {
            var expression = "f > z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, object>()
            {
                {"f", new DateTime(2,1,1)},
                {"z", new DateTime(1,1,1)}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod27()
        {
            var expression = "f + k > z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, object>()
            {
                {"f", new DateTime(1,1,1)},
                {"k", new TimeSpan(1,1,1)},
                {"z", new DateTime(1,1,1)},
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod28()
        {
            var expression = "\"2\" as int = 2";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod29()
        {
            var expression = "\"12:12:12\" as timespan = f";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, object>()
            {
                {"f", new TimeSpan(12,12,12)}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod30()
        {
            var expression = "\"12:12:12\" as timespan > \"12:12:10\" as timespan";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod31()
        {
            var expression = "-(1) < 2";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod32()
        {
            var expression = "12.2 > 12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod33()
        {
            var expression = "12.2 + 10 = 22.2";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod34()
        {
            var expression = "12.2 / 10 = 1.22";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens);

            Assert.AreEqual(t, true);
        }
    }
}
