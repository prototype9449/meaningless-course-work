namespace SqlParcer
{
    public class Point
    {
        public VariableType Type { get; set; }
        public string Value { get; set; }

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
}