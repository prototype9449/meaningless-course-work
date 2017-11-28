using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlParcer
{

    public abstract class Node
    {
        public abstract bool IsOperator();
        public string value { get; set; }
    }

    public class Operator : Node
    {

        public Operator(string oper)
        {
            this.value = oper;
        }

        public int GetPriority()
        {
            return ReversePolishNotation.GetPriority(this.value);
        }

        public override bool IsOperator()
        {
            return true;
        }
    }

    public class Value : Node
    {
        public Value(string value)
        {
            this.value = value;
        }

        public override bool IsOperator()
        {
            return false;
        }
    }

    public class ReversePolishNotation
    {
        public static List<string> GetTokens(string expression)
        {
            return expression.Split('"')
                .Select((element, index) => index % 2 == 0
                    ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    : new[] { element })
                .SelectMany(element => element).ToList();
        }


        public static int GetPriority(string token)
        {
            var result = -1;

            switch (token)
            {
                case "(":
                case ")":
                    result = 1;
                    break;
                case "and":
                case "or":
                    result = 2;
                    break;
                case "=":
                case ">":
                case "<":
                case "like":
                    result = 3;
                    break;
                case "-":
                case "+":
                    result = 4;
                    break;
                case "*":
                case "/":
                    result = 5;
                    break;
                case "as":
                    result = 6;
                    break;
            }

            return result;
        }

        public static List<Node> GetNodes(List<string> tokens)
        {
            return tokens.Select(x => GetPriority(x) == -1 ? (Node)new Value(x) : (Node)new Operator(x)).ToList();
        }

        public static List<Node> ConvertToPrefixVersion(List<string> tokens)
        {
            var nodes = GetNodes(tokens);

            var stack = new Stack<Node>();
            var result = new List<Node>();

            foreach (var node in nodes)
            {
                if (!node.IsOperator())
                {
                    result.Add(node);
                }
                else
                {
                    if (node.value == ")")
                    {
                        while (stack.Count != 0 && stack.Peek().value != "(")
                        {
                            result.Add(stack.Pop());
                        }
                        if (stack.Count != 0)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            throw new Exception("there was not )");
                        }
                    }
                    else
                    {
                        while (stack.Count != 0 && GetPriority(node.value) <= GetPriority(stack.Peek().value))
                        {
                            result.Add(stack.Pop());
                        }
                        stack.Push(node);
                    }

                }
            }

            while (stack.Count != 0)
            {
                result.Add(stack.Pop());
            }
            return result;
        }

        public static SqlResult GetValue(SqlResult first, SqlResult second, string oper)
        {
            var tuple = oper == "as" ? new Tuple<SqlResult, SqlResult>(first, second) : Operations.ConvertToOneType(first, second);
            first = tuple.Item1;
            second = tuple.Item2;

            switch (oper)
            {
                case "+":
                    return Operations.Add(first, second);
                case "as":
                    return Operations.As(first, second);
                case "-":
                    return Operations.Substract(first, second);
                case "*":
                    return Operations.Multiply(first, second);
                case "/":
                    return Operations.Divide(first, second);
                case ">":
                    return new SqlResult(Operations.More(first, second), typeof(bool));
                case "<":
                    return new SqlResult(Operations.Less(first, second), typeof(bool));
                case ">=":
                    return new SqlResult(Operations.Equal(first, second) || Operations.More(first, second), typeof(bool));
                case "<=":
                    return new SqlResult(Operations.Equal(first, second) || Operations.Less(first, second), typeof(bool));
                case "=":
                    return new SqlResult(Operations.Equal(first, second), typeof(bool));
                case "and":
                    return new SqlResult(Operations.And(first, second), typeof(bool));
                case "or":
                    return new SqlResult(Operations.Or(first, second), typeof(bool));
            }
            return null;
        }

        public static bool Evaluate(List<string> tokens, Dictionary<string, SqlResult> dict)
        {
            var nodes = ConvertToPrefixVersion(tokens);
            var stack = new Stack<SqlResult>();

            foreach (var node in nodes)
            {
                if (!node.IsOperator())
                {
                    if (dict.ContainsKey(node.value))
                    {
                        var v = dict[node.value];
                        stack.Push(new SqlResult(v.Value, v.ValueType));
                    }
                    else
                    {
                        if (node.value.StartsWith("\"") && node.value.EndsWith("\"") && node.value.Length > 1)
                        {
                            stack.Push(new SqlResult(node.value.Substring(1, node.value.Length - 2), typeof(string)));
                        }
                        else
                        {
                            stack.Push(new SqlResult(node.value, typeof(string)));
                        }
                    }
                }
                else
                {
                    var second = stack.Pop();
                    var first = stack.Pop();

                    var result = GetValue(first, second, node.value);

                    stack.Push(result);
                }
            }

            var res = stack.Pop();
            if (res.ValueType != typeof(bool))
            {
                throw new Exception("expression should return boolean");
            }

            return (bool)res.Value;
        }
    }
}
