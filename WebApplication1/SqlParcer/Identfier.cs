namespace SqlParcer
{
    public class Identfier
    {
        public string Column { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public Identfier(string column, string value, string type)
        {
            Column = column;
            Value = value;
            Type = type;
        }
    }
}