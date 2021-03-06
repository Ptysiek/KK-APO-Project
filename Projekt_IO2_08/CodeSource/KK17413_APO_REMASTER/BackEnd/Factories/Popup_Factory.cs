﻿using System;
using System.Collections.Generic;

using KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders;


namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    public class Popup_Factory
    {
        private readonly Dictionary<string, IPopupBuilder> popupList = new Dictionary<string, IPopupBuilder>()
        {
            { "DoubleParamRadiometricCustom_Popup", new DoubleParamRadiometricCustomPopup_Builder() }
        };

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
