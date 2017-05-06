using System;
using System.Data.Sql;
using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;

public class ContextParser
{
    public class PredicateTumple
    {
        public PredicateTumple(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static bool ExecutePredicate(string predicate)
    {
        var predicates = predicate.Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries);
        var contextTumples = new List<PredicateTumple>();
        foreach (var pred in predicates)
        {
            var values = pred.Split(new[] {'=', ' '}, StringSplitOptions.RemoveEmptyEntries);
            contextTumples.Add(new PredicateTumple(values[0], values[1].Trim('"')));
        }
        var selectPart = string.Join(", ", contextTumples.Select(x => String.Format("SESSION_CONTEXT(N'{0}') as {0}", x.Name)));
        var result = false;
        using (SqlConnection connection = new SqlConnection("context connection=true"))
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader; 

            cmd.CommandText = "select " + selectPart;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    result = contextTumples.All(x => reader[x.Name].ToString() == x.Value);
                }
            }
            finally
            {
                // Always call Close when done reading.
                cmd.Connection.Close();
                reader.Close();
            }
            
        }

        return result;
    }
}
