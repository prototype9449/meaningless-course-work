using System;

namespace SqlParcer
{
    public class SqlResult
    {
        public object Value { get; set; }
        public Type ValueType { get; set; }

        public SqlResult(object value, Type valueType)
        {
            Value = value;
            ValueType = valueType;
        }
    }
}