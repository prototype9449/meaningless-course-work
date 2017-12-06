using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SqlParcer
{
    public static class Operations
    {
        public static Dictionary<string, Type> types = new Dictionary<string, Type>
        {
            {"byte", typeof(byte)},
            {"short", typeof(short)},
            {"int", typeof(int)},
            {"long", typeof(long)},
            {"double", typeof(double)},
            {"float", typeof(float)},

            {"byte[]", typeof(byte[])},
            {"bool", typeof(bool)},
            {"string", typeof(string)},

            {"datetime", typeof(DateTime)},
            {"timespan", typeof(TimeSpan)},
            {"datetimeoffset", typeof(DateTimeOffset)},
            {"guid", typeof(Guid) }
        };

        public static List<Type> TypeConvert = new List<Type>()
        {
            typeof(byte), typeof(short), typeof(int), typeof(long), typeof(double)
        };

        public static Tuple<object, object> ConvertToOneType(object first, object second)
        {
            if (first.GetType() == second.GetType())
                return new Tuple<object, object>(first, second);

            if (!TypeConvert.Contains(first.GetType()) || !TypeConvert.Contains(second.GetType()))
            {
                throw new Exception("Impossible to make implicit convertion");
            }

            object firstResult;
            object secondResult;

            if (TypeConvert.IndexOf(first.GetType()) > TypeConvert.IndexOf(second.GetType()))
            {
                secondResult = Convert.ChangeType(second, first.GetType());
                firstResult = first;
            }
            else
            {
                secondResult = second;
                firstResult = Convert.ChangeType(first, second.GetType());
            }

            return new Tuple<object, object>(firstResult, secondResult);
        }

        public static object As(object first, object second)
        {
            if (second.GetType() != typeof(string))
                throw new Exception("wrong type name");

            if (first is string)
            {
                if (!types.ContainsKey((string)second))
                    throw new Exception("wrong type");
                var typeToConvert = types[(string)second];
                if (typeToConvert == null)
                    throw new Exception("there was not fount a such type");
                var result = TypeDescriptor.GetConverter(typeToConvert).ConvertFromString((string)first);

                return result;
            }

            var type = types[(string)second];
            var newValue = Convert.ChangeType(first, type);

            return newValue;
        }

        public static object Add(object first, object second)
        {
            var tuple = (first is DateTime && second is TimeSpan)
                ? new Tuple<object, object>(first, second)
                : ConvertToOneType(first, second);
            first = tuple.Item1;
            second = tuple.Item2;

            if (first is string)
                return (string)first + (string)second;
            if (first is short)
                return (short)first + (short)second;
            if (first is int)
                return (int)first + (int)second;
            if (first is long)
                return Convert.ToInt64(first) + Convert.ToInt64(second);
            if (first is double)
                return (double)first + (double)second;
            if (first is float)
                return (float)first + (float)second;
            if (first is DateTime && second is TimeSpan)
                return (DateTime)first + (TimeSpan)second;
            if (first is TimeSpan && second is TimeSpan)
                return (TimeSpan)first + (TimeSpan)second;

            throw new Exception("It is impossible to add these values");
        }

        public static object Substract(object first, object second)
        {
            var tuple = (first is DateTime && second is TimeSpan)
                ? new Tuple<object, object>(first, second)
                : ConvertToOneType(first, second);
            first = tuple.Item1;
            second = tuple.Item2;

            if (first is short)
                return (short)first - (short)second;
            if (first is int)
                return (int)first - (int)second;
            if (first is long)
                return Convert.ToInt64(first) - Convert.ToInt64(second);
            if (first is double)
                return (double)first - (double)second;
            if (first is float)
                return (float)first - (float)second;
            if (first is TimeSpan)
                return (TimeSpan)first - (TimeSpan)second;
            if (first is DateTime && second is TimeSpan)
                return (DateTime)first - (TimeSpan)second;

            throw new Exception("It is impossible to SUBSTRACT these values");
        }

        public static object Multiply(object first, object second)
        {
            var tuple = ConvertToOneType(first, second);
            first = tuple.Item1;
            second = tuple.Item2;

            if (first is short)
                return (short)first * (short)second;
            if (first is int)
                return (int)first * (int)second;
            if (first is long)
                return Convert.ToInt64(first) * Convert.ToInt64(second);
            if (first is double)
                return (double)first * (double)second;
            if (first is float)
                return (float)first * (float)second;

            throw new Exception("It is impossible to MULTIPLY these values");
        }

        public static object Divide(object first, object second)
        {
            var tuple = ConvertToOneType(first, second);
            first = tuple.Item1;
            second = tuple.Item2;

            if (first is short)
                return (short)first / (short)second;
            if (first is int)
                return (int)first / (int)second;
            if (first is long)
                return Convert.ToInt64(first) / Convert.ToInt64(second);
            if (first is double)
                return (double)first / (double)second;
            if (first is float)
                return (float)first / (float)second;

            throw new Exception("It is impossible to DIVIDE these values");
        }

        public static bool More(object first, object second)
        {
            var tuple = ConvertToOneType(first, second);
            first = tuple.Item1;
            second = tuple.Item2;

            if (first is short)
                return (short)first > (short)second;
            if (first is int)
                return (int)first > (int)second;
            if (first is long)
                return Convert.ToInt64(first) > Convert.ToInt64(second);
            if (first is double)
                return (double)first > (double)second;
            if (first is float)
                return (float)first > (float)second;
            if (first is DateTime)
                return (DateTime)first > (DateTime)second;
            if (first is TimeSpan)
                return (TimeSpan)first > (TimeSpan)second;

            throw new Exception("It is impossible to make MORE these values");
        }

        public static bool Less(object first, object second)
        {
            var tuple = ConvertToOneType(first, second);
            first = tuple.Item1;
            second = tuple.Item2;

            if (first is short)
                return (short)first < (short)second;
            if (first is int)
                return (int)first < (int)second;
            if (first is long)
                return Convert.ToInt64(first) < Convert.ToInt64(second);
            if (first is double)
                return (double)first < (double)second;
            if (first is float)
                return (float)first < (float)second;
            if (first is DateTime)
                return (DateTime)first < (DateTime)second;
            if (first is TimeSpan)
                return (TimeSpan)first < (TimeSpan)second;

            throw new Exception("!!1");
        }

        public static bool And(object first, object second)
        {
            if (first is bool && second is bool)
                return (bool)first && (bool)second;

            throw new Exception("mismatched types");
        }

        public static bool Or(object first, object second)
        {
            if (first is bool && second is bool)
                return (bool)first || (bool)second;

            throw new Exception("mismatched types");
        }

        public static bool Equal(object first, object second)
        {
            var tuple = ConvertToOneType(first, second);
            first = tuple.Item1;
            second = tuple.Item2;

            if (first is byte[])
                return ((byte[])first).SequenceEqual((byte[])second);

            var result = Equals(first, second);

            return result;
        }
    }
}