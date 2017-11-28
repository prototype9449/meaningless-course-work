using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace SqlParcer
{
    public static class Operations
    {
        public static Dictionary<string, Type> types = new Dictionary<string, Type>()
        {
            {"long", typeof(long)},
            {"int", typeof(int)},
            {"short", typeof(short)},
            {"byte[]", typeof(byte[])},
            {"bool", typeof(bool)},
            {"string", typeof(string)},
            {"float", typeof(float)},
            {"double", typeof(double)},
            {"datetime", typeof(DateTime)},
            {"time", typeof(TimeSpan)},
            {"byte", typeof(byte)},
        };


        public static Tuple<SqlResult, SqlResult> ConvertToOneType(SqlResult first, SqlResult second)
        {
            if (first.ValueType != typeof(string) && second.ValueType == typeof(string))
            {
                var converter = TypeDescriptor.GetConverter(first.ValueType);
                if (converter.CanConvertFrom(typeof(string)))
                {
                    second = new SqlResult(converter.ConvertFromString((string)second.Value), first.ValueType);
                    return new Tuple<SqlResult, SqlResult>(first, second);
                }
                else
                {
                    throw new Exception("impossible to make an implcit convertion");
                }
            }
            else if (first.ValueType == typeof(string) && second.ValueType != typeof(string))
            {
                var converter = TypeDescriptor.GetConverter(second.ValueType);
                if (converter.CanConvertFrom(typeof(string)))
                {
                    first = new SqlResult(converter.ConvertFromString((string)first.Value), second.ValueType);
                    return new Tuple<SqlResult, SqlResult>(first, second);
                }
                else
                {
                    throw new Exception("impossible to make an implcit convertion");
                }
            }
            else if (first.ValueType == second.ValueType)
            {
                return new Tuple<SqlResult, SqlResult>(first, second);
            }

            throw new Exception("impossible to make an implcit convertion");
        }

        public static SqlResult Add(SqlResult first, SqlResult second)
        {
            if (first.ValueType == typeof(DateTime) && second.ValueType != typeof(TimeSpan))
            {
                throw new Exception("mismatched types");
            }

            object value = null;
            Type resultType = null;

            if (first.ValueType == typeof(string))
            {
                value = (string)first.Value + (string)second.Value;
                resultType = typeof(string);
            }
            else if (first.ValueType == typeof(int))
            {
                value = (int)first.Value + (int)second.Value;
                resultType = typeof(int);
            }
            else if (first.ValueType == typeof(double))
            {
                value = (double)first.Value + (double)second.Value;
                resultType = typeof(double);
            }
            else if (first.ValueType == typeof(float))
            {
                value = (float)first.Value + (float)second.Value;
                resultType = typeof(float);
            }
            else if (first.ValueType == typeof(DateTime) && second.ValueType == typeof(TimeSpan))
            {
                value = (DateTime)first.Value + (TimeSpan)second.Value;
                resultType = typeof(DateTime);
            }
            else if (first.ValueType == typeof(TimeSpan) && second.ValueType == typeof(TimeSpan))
            {
                value = (TimeSpan)first.Value + (TimeSpan)second.Value;
                resultType = typeof(TimeSpan);
            }

            return new SqlResult(value, resultType);
        }
        public static SqlResult As(SqlResult first, SqlResult second)
        {
            if (first.ValueType == typeof(string) && second.ValueType == typeof(string))
            {
                if (!types.ContainsKey((string)second.Value))
                {
                    throw new Exception("wrong type");
                }
                var typeToConvert = types[(string)second.Value];
                if (typeToConvert == null)
                {
                    throw new Exception("there was not fount a such type");
                }
                var result = TypeDescriptor.GetConverter(typeToConvert).ConvertFromString((string)first.Value);

                return new SqlResult(result, typeToConvert);
            }

            var newValue = Convert.ChangeType(first.Value, second.ValueType);

            return new SqlResult(newValue, second.ValueType);
        }

        public static SqlResult Substract(SqlResult first, SqlResult second)
        {
            if (first.ValueType != second.ValueType)
            {
                throw new Exception("mismatched types");
            }

            object value = null;
            Type resultType = null;

            if (first.ValueType == typeof(int))
            {
                value = (int)first.Value - (int)second.Value;
                resultType = typeof(int);
            }
            else if (first.ValueType == typeof(double))
            {
                value = (double)first.Value - (double)second.Value;
                resultType = typeof(double);
            }
            else if (first.ValueType == typeof(float))
            {
                value = (float)first.Value - (float)second.Value;
                resultType = typeof(float);
            }
            else if (first.ValueType == typeof(TimeSpan))
            {
                value = (TimeSpan)first.Value - (TimeSpan)second.Value;
                resultType = typeof(TimeSpan);
            }
            else if (first.ValueType == typeof(DateTime) && second.ValueType == typeof(TimeSpan))
            {
                value = (DateTime)first.Value - (TimeSpan)second.Value;
                resultType = typeof(DateTime);
            }

            return new SqlResult(value, resultType);
        }

        public static SqlResult Multiply(SqlResult first, SqlResult second)
        {
            if (first.ValueType != second.ValueType)
            {
                throw new Exception("mismatched types");
            }

            object value = null;
            Type resultType = null;

            if (first.ValueType == typeof(int))
            {
                value = (int)first.Value * (int)second.Value;
                resultType = typeof(int);
            }
            else if (first.ValueType == typeof(double))
            {
                value = (double)first.Value * (double)second.Value;
                resultType = typeof(double);
            }
            else if (first.ValueType == typeof(float))
            {
                value = (float)first.Value * (float)second.Value;
                resultType = typeof(float);
            }

            return new SqlResult(value, resultType);
        }

        public static SqlResult Divide(SqlResult first, SqlResult second)
        {
            if (first.ValueType != second.ValueType)
            {
                throw new Exception("mismatched types");
            }

            object value = null;
            Type resultType = null;

            if (first.ValueType == typeof(int))
            {
                value = (int)first.Value / (int)second.Value;
                resultType = typeof(int);
            }
            else if (first.ValueType == typeof(double))
            {
                value = (double)first.Value / (double)second.Value;
                resultType = typeof(double);
            }
            else if (first.ValueType == typeof(float))
            {
                value = (float)first.Value / (float)second.Value;
                resultType = typeof(float);
            }

            return new SqlResult(value, resultType);
        }

        public static bool More(SqlResult first, SqlResult second)
        {
            if (first.ValueType != second.ValueType || first.ValueType == typeof(DateTime) && second.ValueType != typeof(TimeSpan))
            {
                throw new Exception("mismatched types");
            }


            if (first.ValueType == typeof(int))
            {
                return (int)first.Value > (int)second.Value;
            }
            if (first.ValueType == typeof(double))
            {
                return (double)first.Value > (double)second.Value;
            }
            if (first.ValueType == typeof(float))
            {
                return (float)first.Value > (float)second.Value;
            }
            if (first.ValueType == typeof(DateTime))
            {
                return (DateTime)first.Value > (DateTime)second.Value;
            }
            if (first.ValueType == typeof(TimeSpan))
            {
                return (TimeSpan)first.Value > (TimeSpan)second.Value;
            }
            throw new Exception("!!1");
        }

        public static bool Less(SqlResult first, SqlResult second)
        {
            if (first.ValueType != second.ValueType || first.ValueType == typeof(DateTime) && second.ValueType != typeof(TimeSpan))
            {
                throw new Exception("mismatched types");
            }

            if (first.ValueType == typeof(int))
            {
                return (int)first.Value < (int)second.Value;
            }
            if (first.ValueType == typeof(double))
            {
                return (double)first.Value < (double)second.Value;
            }
            if (first.ValueType == typeof(float))
            {
                return (float)first.Value < (float)second.Value;
            }
            if (first.ValueType == typeof(DateTime))
            {
                return (DateTime)first.Value < (DateTime)second.Value;
            }
            if (first.ValueType == typeof(TimeSpan))
            {
                return (TimeSpan)first.Value < (TimeSpan)second.Value;
            }

            throw new Exception("!!1");
        }

        public static bool And(SqlResult first, SqlResult second)
        {
            if (first.ValueType == second.ValueType && first.ValueType == typeof(bool))
            {
                return (bool)first.Value && (bool)second.Value;
            }

            throw new Exception("mismatched types");
        }

        public static bool Or(SqlResult first, SqlResult second)
        {
            if (first.ValueType == second.ValueType && first.ValueType == typeof(bool))
            {
                return (bool)first.Value || (bool)second.Value;
            }

            throw new Exception("mismatched types");
        }

        public static bool Equal(SqlResult first, SqlResult second)
        {
            if (first.ValueType != second.ValueType)
            {
                throw new Exception("mismatched types");
            }

            var result = Equals(first.Value, second.Value);

            return result;
        }
    }


}