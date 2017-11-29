using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlParcer;

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
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var expression = "1 as int >= 1";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var expression = "2 > 1 as int";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult(int.MaxValue, typeof(int))},
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var expression = "0 < 1 as int";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var expression = "-1 as int < 1";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var expression = "1 < f";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult(2, typeof(int))},
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var expression = "1 + f = 12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult("2", typeof(string))},
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var expression = "1 - 4 as int = -3";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod9()
        {
            var expression = "-3 + 5 as int = 2";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod10()
        {
            var expression = "-1 - 4 as int = -5";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod11()
        {
            var expression = "3 as int - -5 as int = 8";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod12()
        {
            var expression = "( 1 * -3 as int ) + ( 5 - 8 as int ) * 3 + 0 / 2 as int = -12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod13()
        {
            var expression = "0 / 2 as int = 0";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod14()
        {
            var expression = "2 * ( 5 / 2 as int ) as int = 4";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod15()
        {
            var expression = "( 5 - -1 as int ) / 2 as int = 3";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod16()
        {
            var expression = "( 5 * -1 as int ) / 2 as int = -2";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod17()
        {
            var expression = "( 5 + 2 as int ) * 3 + 2 * ( 1 + 3 as int ) = 29";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod18()
        {
            var expression = "12 as int >= 12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod19()
        {
            var expression = "15 >= 12 as int";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod20()
        {
            var expression = "11 <= 12 as int";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod21()
        {
            var expression = "11 as int <= 12";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod22()
        {
            var expression = "11 + 10 as int > 12 and 1 > 0,5 as float";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod23()
        {
            var expression = "f = z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult(new byte[]{1,2}, typeof(byte[]))},
                {"z", new SqlResult(new byte[]{1,2}, typeof(byte[]))}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod24()
        {
            var expression = "f = z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult(new TimeSpan(1,1,1,1,1), typeof(TimeSpan))},
                {"z", new SqlResult(new TimeSpan(1,1,1,1,1), typeof(TimeSpan))}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod25()
        {
            var expression = "f = z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult(new DateTime(1,1,1), typeof(DateTime))},
                {"z", new SqlResult(new DateTime(1,1,1), typeof(DateTime))}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod26()
        {
            var expression = "f > z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult(new DateTime(2,1,1), typeof(DateTime))},
                {"z", new SqlResult(new DateTime(1,1,1), typeof(DateTime))}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod27()
        {
            var expression = "f + k > z";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult(new DateTime(1,1,1), typeof(DateTime))},
                {"k", new SqlResult(new TimeSpan(1,1,1), typeof(TimeSpan))},
                {"z", new SqlResult(new DateTime(1,1,1), typeof(DateTime))},
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod28()
        {
            var expression = "\"2\" as int = 2";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod29()
        {
            var expression = "\"12:12:12\" as timespan = f";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>()
            {
                {"f", new SqlResult(new TimeSpan(12,12,12), typeof(TimeSpan))}
            });

            Assert.AreEqual(t, true);
        }

        [TestMethod]
        public void TestMethod30()
        {
            var expression = "\"12:12:12\" as timespan > \"12:12:10\"";
            var tokens = ReversePolishNotation.GetTokens(expression);
            var t = ReversePolishNotation.Evaluate(tokens, new Dictionary<string, SqlResult>());

            Assert.AreEqual(t, true);
        }
    }
}
