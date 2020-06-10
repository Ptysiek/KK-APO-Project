using System.Collections.Generic;


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
                // WINDOW_FORM -----------------------------------------------------------------------------------
                { "file_tsmi", "Plik" },
                { "open_tsmi", "Otwórz" },
                { "project_tsmi", "Projekt" },
                { "settings_tsmi", "Ustawienia" },
                { "language_tsmi", "Język" },
                { "colorTheme_tsmi", "Motyw Kolorystyczny" },
                { "histogram_tsmi", "Histogram" },
                { "fileInfo_tsmi", "Inf.o pliku" },

                { "Ok_Button", "Zatwierdź" },
                { "Cancel_Button", "Anuluj" },
                { "Apply_Button", "Zastosuj" },
                
                // ----------------------------------------------------------------
                { "operations_tsmi", "Operacje" },
                { "CreateNewData", "Nowy Obraz" },

                // ----------------------------------------------------------------              
                { "ConversionOperations_tsmi", "Konwersja" },
                    { "ToBlackGrayWhite_tsmi",                  "Do CzarnoBiałego" },

                // ----------------------------------------------------------------              
                { "HistogramOperations_tsmi", "Operacje na Histogramie" },
                    { "RecalculateHistogramData_tsmi",          "Obliczenie Histogram" },
                    { "Histogram_Stretching_tsmi",              "Rozciągnięcie Histogramu"  },
                    { "Histogram_Equalization_tsmi",            "Wyrównanie Histogramu" },
                    { "Histogram_SelectiveEqualization_tsmi",   "Wyrównanie Histogramu przez Equalizację" },

                // ----------------------------------------------------------------              
                { "LogicalOperations_tsmi", "Operacje Logiczne" },
                    { "Negation_tsmi",                          "Negacja" },
                    { "Sum_tsmi",                               "Dodawanie" },
                    { "Blending_tsmi",                          "Mieszanie" },
                    { "And_tsmi",                               "And" },
                    { "Xor_tsmi",                               "Xor" },
                    { "Or_tsmi",                                "Or" },
                    { "Not_tsmi",                               "Not" },
                    
                // ----------------------------------------------------------------              
                { "RadiometricsCorectionsOperations_tsmi", "Korekcje Zniekształceń Radiometrycznych" },
                    { "PointAutoCorrection_tsmi",                   "Automatyczna korekcja do punktu" },

                // ----------------------------------------------------------------              
                { "SimpleRadiometricsOperations_tsmi", "Zniekształcenia Radiometryczne" },
                    { "RadiometricDarkening_tsmi",                  "Przyciemnianie" },
                    { "RadiometricLightening_tsmi",                 "Rozjaśnianie" },
                    { "RadiometricExternLightening_tsmi",           "Zewnętrzne Rozjaśnianie" },
                    { "RadiometricExternDarkening_tsmi",            "Zewnętrzne Przyciemnianie" },

                    { "RadiometricSmoothDarkening_tsmi",            "Stopniowe Przyciemnianie" },
                    { "RadiometricInvertedSmoothDarkening_tsmi",    "Odwrócone Stopniowe Przyciemnianie" },

                    { "RadiometricSmoothLightening_tsmi",           "Stopniowe Rozjaśnianie" },
                    { "RadiometricInvertedSmoothLightening_tsmi",   "Odwrócone Stopniowe Rozjaśnianie" },

                    { "RadiometricExternSmoothLightening_tsmi",     "Zewnętrzne Stopniowe Rozjaśnianie" },
                    { "RadiometricExternSmoothDarkening_tsmi",      "Zewnętrzne Stopniowe Przyciemnianie" },

                // ----------------------------------------------------------------              
                { "ThresholdingOperations_tsmi", "Operacje Progowania" },
                    { "BinaryThresholding_tsmi",                "Progowanie Binarne" },
                    { "Thresholding_tsmi",                      "Progowanie Dwuparametrowe" },
                    { "AdaptiveThresholding_tsmi",              "Progowanie Adaptacyjne" },
                    { "Posterize_tsmi",                         "Posteryzacja" },

                // ----------------------------------------------------------------              
                { "SmoothingOperations_tsmi", "Operacje Wygładzania" },
                    { "Blur_tsmi",                              "Blur" },
                    { "GaussianBlur_tsmi",                      "GaussianBlur" },
                    { "MedianBlur_tsmi",                        "MedianBlur" },

                // ----------------------------------------------------------------              
                { "LinearSharpeningOperations_tsmi", "Operacje Wyostrzania" },
                    { "laplaceMasksSharpening_tsmi",            "Laplace Masks" },
                    { "CustomMasksSharpening_tsmi",             "Custom Masks" },

                // ----------------------------------------------------------------              
                { "MorphologicalOperations_tsmi", "Operacje Morfologiczne" },
                    { "Erode_tsmi",                             "Erozja (erode)" },
                    { "Dilate_tsmi",                            "Rozszerzenie (Dilate)" },
                    { "Open_tsmi",                              "Otwarcia (Open)" },
                    { "Close_tsmi",                             "Zamknięcia (Close)" },
                    { "Gradient_tsmi",                          "Gradient" },
                    { "Tophat_tsmi",                            "Tophat" },
                    { "Blackhat_tsmi",                          "Blackhat" },

                // ----------------------------------------------------------------              
                { "EdgeDetectionOperations_tsmi", "Operacje wykrycia krawędzi" },
                    { "CannyDetection_tsmi",                    "Canny" },
                    { "SobelDetection_tsmi",                    "Sobel" },
                    { "LaplaceDetection_tsmi",                  "Laplace" },
                    { "PrewittMasks_tsmi",                      "Prewitt Masks" },

                // ----------------------------------------------------------------              
                { "histogram_iwn", "Histogram" },
                { "fileInfo_iwn", "Inf.o pliku" },
                { "history_iwn", "Historia Modyfikacji" }
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
                    { "Sum_tsmi",                               "Sum" },
                    { "Blending_tsmi",                          "Blending" },
                    { "And_tsmi",                               "And" },
                    { "Xor_tsmi",                               "Xor" },
                    { "Or_tsmi",                                "Or" },
                    { "Not_tsmi",                               "Not" },

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
                { "SimpleRadiometricsOperations_tsmi", "Radial Distortions" },
                    { "RadiometricDarkening_tsmi",                  "Darkening" },
                    { "RadiometricLightening_tsmi",                 "Lightening" },
                    { "RadiometricExternLightening_tsmi",           "External Lightening" },
                    { "RadiometricExternDarkening_tsmi",            "External Darkening" },

                    { "RadiometricSmoothDarkening_tsmi",            "Smooth Darkening" },
                    { "RadiometricInvertedSmoothDarkening_tsmi",    "Inverted Smooth Darkening" },

                    { "RadiometricSmoothLightening_tsmi",           "Smooth Lightening" },
                    { "RadiometricInvertedSmoothLightening_tsmi",   "Inverted Smooth Lightening" },

                    { "RadiometricExternSmoothLightening_tsmi",     "External Smooth Lightening" },
                    { "RadiometricExternSmoothDarkening_tsmi",      "External Smooth Darkening" },

                // ----------------------------------------------------------------              
                { "LinearSharpeningOperations_tsmi", "Sharpening Operations" },
                    { "laplaceMasksSharpening_tsmi",            "Laplace Masks" },
                    { "CustomMasksSharpening_tsmi",             "Custom Masks" },

                // ----------------------------------------------------------------              
                { "MorphologicalOperations_tsmi", "Morphological Operations" },
                    { "Erode_tsmi",                             "Erode" },
                    { "Dilate_tsmi",                            "Dilate" },
                    { "Open_tsmi",                              "Open" },
                    { "Close_tsmi",                             "Close" },
                    { "Gradient_tsmi",                          "Gradient" },
                    { "Tophat_tsmi",                            "Tophat" },
                    { "Blackhat_tsmi",                          "Blackhat" },

                // ----------------------------------------------------------------              
                { "EdgeDetectionOperations_tsmi", "EdgeDetection Operations" },
                    { "CannyDetection_tsmi",                    "Canny" },
                    { "SobelDetection_tsmi",                    "Sobel" },
                    { "LaplaceDetection_tsmi",                  "Laplace" },
                    { "PrewittMasks_tsmi",                      "Prewitt Masks" },

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
