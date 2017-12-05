using DBModels;
using System;
using System.Text;

namespace DBFiller
{
    class Filler
    {
        public DBStructure DbStricture { get; set; }

        private readonly char[] chars = "abcdefghijklmnopqrstuvwxyz1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private readonly Random _random = new Random();

        public Filler(DBStructure dbStructure)
        {
            DbStricture = dbStructure;
        }

        private int GetRandomInt(int start, int end)
        {
            return start + _random.Next(end - start);
        }

        private char GetRandomChar()
        {
            return chars[GetRandomInt(0, chars.Length - 1)];
        }

        private string GetRandomString(int length)
        {
            var stringBuilder = new StringBuilder();
            for(int i = 0; i < length; i++)
            {
                stringBuilder.Append(GetRandomChar());
            }

            return stringBuilder.ToString();
        }

        public void FillDB()
        {
            var c = new OnlineShopContext();
            c.Categories.Add(new Category() { CategoryName = "fsd", Description = "fsd" });
            c.SaveChanges();
        }
    }
}
