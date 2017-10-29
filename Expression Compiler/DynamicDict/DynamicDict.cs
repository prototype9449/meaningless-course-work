namespace DynamicDict
{
    public class DynamicDict : System.Dynamic.DynamicObject
    {
        System.Collections.Generic.Dictionary<string, object> dictionary = new System.Collections.Generic.Dictionary<string, object>();

        public int Count
        {
            get { return dictionary.Count; }

        }

        public void SetValue(string key, object value)
        {
            this.dictionary.Add(key, value);
        }

        public void SetDictionary(System.Collections.Generic.Dictionary<string, object> dictionary)
        {
            this.dictionary = dictionary;
        }

        public override bool TryGetMember(
            System.Dynamic.GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();

            return dictionary.TryGetValue(name, out result);
        }

        public override bool TrySetMember(
            System.Dynamic.SetMemberBinder binder, object value)
        {
            dictionary[binder.Name.ToLower()] = value;

            return true;
        }
    }
}

