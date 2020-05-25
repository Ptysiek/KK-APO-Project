using System.Collections.Generic;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    public class OperationsFamily
    {
        protected OperationsFamily() { }

        protected Dictionary<string, IOperation> operations_Dict;


        public IOperation GetValue(string key)
        {
            if (!operations_Dict.ContainsKey(key)) return null;
            if (operations_Dict[key] == null) return null;

            return operations_Dict[key];
        }

        public List<string> OperationsKeys()
        {
            if (operations_Dict == null) { return null; }

            List<string> result = new List<string>();
            foreach (string key in operations_Dict.Keys)
            {
                result.Add(key);
            }
            result.Sort();
            return result;
        }
    }
}