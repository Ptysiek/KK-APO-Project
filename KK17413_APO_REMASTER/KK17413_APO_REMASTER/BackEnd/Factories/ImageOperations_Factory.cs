using System;
using System.Collections.Generic;

using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;


namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    public class ImageOperations_Factory
    {
        private readonly Dictionary<string, IOperationsFamily> operationsList = new Dictionary<string, IOperationsFamily>()
        {
            { "ConversionOperations_tsmi", new ConversionOperations() },
            { "HistogramOperations_tsmi", new HistogramOperations() },
            { "LogicalOperations_tsmi", new LogicalOperations() },
            { "ThresholdingOperations_tsmi", new ThresholdingOperations() },
            { "SmoothingOperations_tsmi", new SmoothingOperations() },
            { "EdgeDetectionOperations_tsmi", new EdgeDetectionOperations() }
        };

        // ##########################################################################################################
        public IOperationsFamily GetFamily(string key)
        {   
            if (!operationsList.ContainsKey(key)) return null;
            if (operationsList[key] == null) return null;

            return operationsList[key];
        }

        public IOperation GetOperation(string key)
        {
            IOperation tmp = null;

            foreach (var pair in operationsList)
            {
                tmp = pair.Value.GetValue(key);

                if (tmp != null)                
                    break;                
            }
            return tmp;
        }

        public List<string> FamilyKeys()
        {
            List<string> result = new List<string>();
            foreach (string key in operationsList.Keys)
            {
                result.Add(key);
            }
            result.Sort();
            return result;
        }
        // ##########################################################################################################
    }
}
