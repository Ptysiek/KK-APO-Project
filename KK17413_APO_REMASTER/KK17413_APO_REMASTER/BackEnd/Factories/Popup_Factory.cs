using System;
using System.Collections.Generic;

using KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders;


namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    public class Popup_Factory
    {
        private readonly Dictionary<string, IPopupBuilder> popupList = new Dictionary<string, IPopupBuilder>()
        {
            { "Histogram_Popup", new HistogramPopup_Bilder() },
            { "DoubleParam_Popup", new DoubleParamPopup_Bilder() },
            { "SingleParam_Popup", new SingleParamPopup_Bilder() },

            { "Blur_Popup", new BlurPopup_Bilder() },
            { "GaussianBlur_Popup", new GaussianBlurPopup_Bilder() },
            { "MedianBlur_Popup", new MedianBlurPopup_Bilder() },

            { "EdgeDetection_Popup", new EdgeDetectionPopup_Bilder() },
            { "EdgeDetection_Sobel_Popup", new EdgeDetectionSobelPopup_Bilder() },
            { "EdgeDetection_Laplace_Popup", new EdgeDetectionLaplacePopup_Bilder() },
            { "PrewittMasks_Popup", new PrewittMasksPopup_Bilder() },

            { "CustomMatrix_Popup", new CustomMatrixPopup_Bilder() },
            { "SuperCustomMatrix_Popup", new SuperCustomMatrixPopup_Bilder() },

            { "ChooseSecondImage_Popup", new ChooseSecondImagePopup_Bilder() },
            { "ChooseSecondImagePopup_AND", new ChooseSecondImagePopup_AND_Bilder() }
        };


        // ##########################################################################################################


        // ##########################################################################################################
        public IPopupBuilder GetValue(string key)
        {
            if (!popupList.ContainsKey(key)) return null;
            return popupList[key];
        }

        public List<string> Keys()
        {
            List<string> result = new List<string>();
            foreach (string key in popupList.Keys)
            {
                result.Add(key);
            }
            result.Sort();
            return result;
        }
        // ##########################################################################################################
    }
}
