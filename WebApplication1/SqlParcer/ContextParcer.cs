using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace SqlParser
{

    public static class ContextParser
    {
        public static readonly string ConnectionString = "Data Source=CO-YAR-WS208;Initial Catalog=OnlineShop;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

        public static readonly Random random = new Random();

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

        private static List<Point> GetPredicates(List<Token> tokens)
        {
            return tokens.Where(x => x._type == TokenType.Identifier)
                .Select(x => Point.Parse(x._lexeme))
                .Where(x => x.Type == VariableType.Context || x.Type == VariableType.Row).ToList();
        }

        private static List<string> GetColumns(List<Point> predicates, VariableType variableType)
        {
            return predicates
                .Where(x => x.Type == variableType)
                .Select(x => x.Value)
                .Distinct()
                .ToList();
        }

        private static Dictionary<string, object> GetRowValues(string sqlEntityName, List<Identfier> identifiers, List<string> columns)
        {
            var whereStatement = string.Join(" and ", identifiers.Select(x => x.Column + " = @" + x.Column));
            var columnString = string.Join(", ", columns);
            var sqlstringRequest = "select " + columnString + " from " + sqlEntityName + " where " + whereStatement;

            var result = new Dictionary<string, object>();

            using (var connection = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand(sqlstringRequest, connection) { CommandType = CommandType.Text };
                identifiers.ForEach(x =>
                {
                    cmd.Parameters.AddWithValue("@" + x.Column, TypeDescriptor.GetConverter(Operations.types[x.Type]).ConvertFromString(x.Value));
                });

                cmd.Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            result.Add(reader.GetName(i), reader.GetValue(i));
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
        public static bool ExecuteStaticPredicate
            (string expressions,
            int id1, bool boolType1, int intType1, string stringType1, DateTime dateTimeType1, DateTimeOffset datetimeOffsetType1, TimeSpan timeType1, Guid guidType1,
            int id2, bool boolType2, int intType2, string stringType2, DateTime dateTimeType2, DateTimeOffset datetimeOffsetType2, TimeSpan timeType2, Guid guidType2)
        {
            var tokens = ReversePolishNotation.GetTokens(expressions);

            var dict = new Dictionary<string, object>()
            {
                {"R.BoolType",boolType1 },
                {"R.IntType",id1 },
                {"R.StringType",stringType1 },
                {"R.DateTimeType",dateTimeType1 },
                {"R.DateTimeOffsetType",datetimeOffsetType1 },
                {"R.TimeType",timeType1 },
                {"R.GuidType",guidType1 },

                {"C.BoolType",boolType2 },
                {"C.IntType",id2 },
                {"C.StringType",stringType2 },
                {"C.DateTimeType",dateTimeType2 },
                {"C.DateTimeOffsetType",datetimeOffsetType2 },
                {"C.TimeType",timeType2 },
                {"C.GuidType",guidType2 },
            };
            return true;
            return ReversePolishNotation.Evaluate(tokens, dict);
        }

        [SqlFunction(DataAccess = DataAccessKind.Read)]
        public static bool ExecutePredicate(string currentTableName, string contextTableName, string expressions,
            string rowIdentfierKeys, string contextIdentifierKeys)
        {
            var tokens = ReversePolishNotation.GetTokens(expressions);
            var predicates = GetPredicates(tokens);

            var rowIdentifiers = GetIdentifiers(rowIdentfierKeys);
            var rowColumns = GetColumns(predicates, VariableType.Row);

            var contextIdentifers = GetIdentifiers(contextIdentifierKeys);
            var contextColumns = GetColumns(predicates, VariableType.Context);

            var resultValues = new Dictionary<string, object>();
            if(rowColumns.Count > 0)
            {
                resultValues = GetRowValues(currentTableName, rowIdentifiers, rowColumns).ToDictionary(key => "R." + key.Key, value => value.Value);
            }

            if(contextColumns.Count > 0)
            {
                var contextValues = GetRowValues(contextTableName, contextIdentifers, contextColumns).ToDictionary(key => "C." + key.Key, value => value.Value);

                foreach (var sqlResult in contextValues)
                {
                    resultValues.Add(sqlResult.Key, sqlResult.Value);
                }
            }
            

            return ReversePolishNotation.Evaluate(tokens, resultValues);
        }
    }

}
