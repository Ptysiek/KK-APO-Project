
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    public class Language
    {
        protected Language() { }

        protected Dictionary<string, string> wordFields_Dict;


        public string GetValue(string key)
        {
            if (!wordFields_Dict.ContainsKey(key)) return "[NULL-KEY]";
            if (wordFields_Dict[key] == null) return "[NULL-VALUE]";

            return wordFields_Dict[key];
        }

        public List<string> Keys()
        {
            if (wordFields_Dict == null) { return null; }

            List<string> result = new List<string>();
            foreach (string key in wordFields_Dict.Keys)
            {
                result.Add(key);
            }
            result.Sort();
            return result;
        }
    }

    //##################################################################################################
    class PL_Language : Language
    {
        public PL_Language()
        {
            wordFields_Dict = new Dictionary<string, string>()
            {
                // ---------------------------------------------------------------------------------------------
                // MAIN_FORM -----------------------------------------------------------------------------------
                { "file_tsmi", "Plik" },
                { "open_tsmi", null },
                { "project_tsmi", "Projekt" },
                { "settings_tsmi", "Ustawienia" },
                { "language_tsmi", "Język" },
                { "colorTheme_tsmi", "Motyw Kolorystyczny" },
                { "histogram_tsmi", "Histogram" },
                { "fileInfo_tsmi", null },

                { "operations_tsmi", "Operacje" },
                { "histogram_Stretching_tsmi", "Rozciąganie Histogramu" },
                { "histogram_Equalization_tsmi", "Wyrównanie Histogramu" },
                { "negation_tsmi", "Negacja" },

                { "histogram_iwn", "Histogram" },
                { "fileInfo_iwn", "Informacje o pliku" }

                //  *tsmi - Tool Strip Menu Item
                //  **iwn - Image Workspace Nodes
                // ---------------------------------------------------------------------------------------------
            };
        }
    }

    //##################################################################################################
    class ANG_Language : Language
    {
        public ANG_Language()
        {
            wordFields_Dict = new Dictionary<string, string>()
            {
                // ---------------------------------------------------------------------------------------------
                // WINDOW_FORM -----------------------------------------------------------------------------------
                { "file_tsmi", "File" },
                { "open_tsmi", "Open" },
                { "project_tsmi", "Project" },
                { "settings_tsmi", "Settings" },
                { "language_tsmi", "Language" },
                { "colorTheme_tsmi", "Color Theme" },
                { "histogram_tsmi", "Histogram" },
                { "fileInfo_tsmi", "File Info" },

                { "Ok_Button", "Ok" },
                { "Cancel_Button", "Cancel" },
                { "Apply_Button", "Apply" },
                
                // ----------------------------------------------------------------
                { "operations_tsmi", "Operations" },
                { "CreateNewData", "Create New Data" },

                // ----------------------------------------------------------------              
                { "ConversionOperations_tsmi", "Conversions" },
                    { "ToBlackGrayWhite_tsmi",                  "To BlackGrayWhite" },

                // ----------------------------------------------------------------              
                { "HistogramOperations_tsmi", "Histogram Operations" },
                    { "RecalculateHistogramData_tsmi",          "Recalculate Histogram" },
                    { "Histogram_Stretching_tsmi",              "Histogram Stretching"  },
                    { "Histogram_Equalization_tsmi",            "Histogram Equalization" },
                    { "Histogram_SelectiveEqualization_tsmi",   "Histogram Selective Equalization" },

                // ----------------------------------------------------------------              
                { "LogicalOperations_tsmi", "Logical Operations" },
                    { "Negation_tsmi",                          "Negation" },

                // ----------------------------------------------------------------              
                { "ThresholdingOperations_tsmi", "Thresholding Operations" },
                    { "BinaryThresholding_tsmi",                "Binary Thresholding" },
                    { "Thresholding_tsmi",                      "Double Param Thresholding" },
                    { "AdaptiveThresholding_tsmi",              "Adaptive Thresholding" },
                    { "Posterize_tsmi",                         "Posterize" },

                // ----------------------------------------------------------------              
                { "SmoothingOperations_tsmi", "Smoothing Operations" },
                    { "Blur_tsmi",                              "Blur" },
                    { "GaussianBlur_tsmi",                      "GaussianBlur" },
                    { "MedianBlur_tsmi",                        "MedianBlur" },

                // ----------------------------------------------------------------              
                { "LinearSharpeningOperations_tsmi", "Sharpening Operations" },
                    { "laplaceMasksSharpening_tsmi",            "Laplace Masks" },

                // ----------------------------------------------------------------              
                { "EdgeDetectionOperations_tsmi", "EdgeDetection Operations" },
                    { "CannyDetection_tsmi",                    "Canny" },
                    { "SobelDetection_tsmi",                    "Sobel" },
                    { "LaplaceDetection_tsmi",                  "Laplace" },

                // ----------------------------------------------------------------              
                { "histogram_iwn", "Histogram" },
                { "fileInfo_iwn", "File info" },
                { "history_iwn", "Modifications history" }
                //  *tsmi - Tool Strip Menu Item
                //  **iwn - Image Workspace Nodes
                // ---------------------------------------------------------------------------------------------
            };
        }
    }
}
