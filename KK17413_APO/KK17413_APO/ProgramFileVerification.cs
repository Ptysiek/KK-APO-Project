using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;


namespace KK17413_APO
{
    static class ProgramFileVerification
    {


        public static string[] BrowseFiles()
        {

            List<string> result = new List<string>();

            // FD - File Dialog for image browsing:
            OpenFileDialog FD = new OpenFileDialog()
            {
                // Set the default directory to Current Directory:
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "Browse Your Image",
                Multiselect = true,
                AddExtension = false,
            };

            if (FD.ShowDialog() == DialogResult.OK)
            {
                foreach (string value in FD.FileNames)
                {
                    if (Verify(value))                    
                        result.Add(value);                    
                }
            }

            return result.ToArray();
        }

        public static bool Verify(string filename)
        {
            var tmp = new FileInfo(filename);


            //tmp.Extension


            return true;
        }

    }
}
