using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.SqlServer.Server;

namespace SqlParcer
{
    public static class ContextParcer
    {
        public static string ConnectionString = "context connection=true";

        public class Tuple
        {
            public Tuple(string variable, string value)
            {
                Variable = variable;
                Value = value;
            }
            public string Variable { get; set; }
            public string Value { get; set; }
        }

        public enum VariableType
        {
            Context,
            Row,
            Constant
        }

        public class Point
        {
            public VariableType Type { get; }
            public string Value { get; }

            public Point(VariableType type, string value)
            {
                Type = type;
                Value = value;
            }

            public static Point Parse(string row)
            {
                if (row.StartsWith("C."))
                {
                    return new Point(VariableType.Context, row.Substring(2));
                }
                else if (row.StartsWith("R."))
                {
                    return new Point(VariableType.Row, row.Substring(2));
                }
                else
                {
                    return new Point(VariableType.Constant, row);
                }
            }
        }

        public class Predicate
        {
            public Point Left { get; }
            public Point Right { get; }

            public Predicate(Point left, Point right)
            {
                Left = left;
                Right = right;
            }
        }

        public class Identfier
        {
            public string Column { get; }
            public string Value { get; }
            public string Type { get; }

            public Identfier(string column, string value, string type)
            {
                Column = column;
                Value = value;
                Type = type;
            }
        }

        public class SqlResult
        {
            public object Value { get; }
            public Type ValueType { get; }

            public SqlResult(object value, Type valueType)
            {
                Value = value;
                ValueType = valueType;
            }
        }

        private static List<Identfier> GetIdentifiers(string identifierRow)
        {
            var keyValues = identifierRow.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            if (keyValues.Length % 3 != 0)
            {
                throw new Exception("should be 3 values in a row");
            }
            var result = new List<Identfier>();

            for (var i = 0; i < keyValues.Length; i += 3)
            {
                result.Add(new Identfier(keyValues[i], keyValues[i + 1], keyValues[i + 2]));
            }
            return result;
        }

        private static List<Predicate> GetPredicates(string expressions)
        {
            var predicates = expressions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var predicateTuples = new List<Predicate>();
            foreach (var pred in predicates)
            {
                var values = pred.Split(new[] { '='}, StringSplitOptions.RemoveEmptyEntries);
                predicateTuples.Add(new Predicate(Point.Parse(values[0]), Point.Parse(values[1])));
            }

            return predicateTuples;
        }

        private static List<string> GetColumns(List<Predicate> predicates, VariableType variableType)
        {
            return predicates
                .Where(x => x.Left.Type == variableType)
                .Select(x => x.Left.Value)
                .Union(
                    predicates
                        .Where(x => x.Right.Type == variableType)
                        .Select(x => x.Right.Value)
                ).ToList();
        }

        private static Dictionary<string, SqlResult> GetRowValues(string sqlEntityName, List<Identfier> identifiers, List<string> columns)
        {
            var whereStatement = string.Join(" and ", identifiers.Select(x => $"{x.Column} = @{x.Column}"));
            var columnString = string.Join(", ", columns);
            var sqlstringRequest = $"select {columnString} from {sqlEntityName} where {whereStatement}";

            var result = new Dictionary<string, SqlResult>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlstringRequest, connection) { CommandType = CommandType.Text };
                identifiers.ForEach(x =>
                {
                    object value;
                    if (x.Type == "string")
                    {
                        value = x.Value;
                    }
                    else
                    {
                        value = int.Parse(x.Value);
                    }

                    var parameter = cmd.CreateParameter();
                    parameter.Value = value;
                    parameter.ParameterName = $"@{x.Column}";
                    cmd.Parameters.Add(parameter);
                });

                cmd.Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            result.Add(reader.GetName(0), new SqlResult(reader.GetValue(i), reader.GetFieldType(i)));
                        }
                    }
                }
                finally
                {
                    cmd.Connection.Close();
                    reader.Close();
                }

            }

            return result;

        }

        [SqlFunction(DataAccess = DataAccessKind.Read)]
        public static bool ExecutePredicate(string currentTableName, string contextTableName, string expressions, 
            string rowIdentfierKeys, string contextIdentifierKeys)
        {
            var predicates = GetPredicates(expressions);

            var rowIdentifiers = GetIdentifiers(rowIdentfierKeys);
            var rowColumns = GetColumns(predicates, VariableType.Row);

            var contextIdentifers = GetIdentifiers(contextIdentifierKeys);
            var contextColumns = GetColumns(predicates, VariableType.Context);

            var rowValues = GetRowValues($"{currentTableName}", rowIdentifiers, rowColumns);
            var contextValues = GetRowValues($"{contextTableName}", contextIdentifers, contextColumns);

            var result = predicates.Aggregate<Predicate, bool, bool>(false, (acc, predicate) =>
            {
                var leftValue = predicate.Left.Type == VariableType.Row
                    ? rowValues[predicate.Left.Value]
                    : predicate.Left.Type == VariableType.Context
                        ? contextValues[predicate.Left.Value].Value
                        : predicate.Left.Value;

                var rightValue = predicate.Right.Type == VariableType.Row
                    ? rowValues[predicate.Right.Value]
                    : predicate.Right.Type == VariableType.Context
                        ? contextValues[predicate.Right.Value].Value
                        : predicate.Right.Value;

                return (int)leftValue == (int)rightValue && acc;
            }, b => b);

            return result;
        }
    }

}
