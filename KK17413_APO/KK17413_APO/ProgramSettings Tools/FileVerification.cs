using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;


namespace KK17413_APO.ProgramSettings_Tools
{
    public class FileVerification
    {        
        private OpenFileDialog FD;  // FD - File Dialog for image browsing

        //TEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEST  
        public FileVerification()
        {
            FD = new OpenFileDialog()
            {
                // Set the default directory to Current Directory:
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "Browse Your Image",
                Multiselect = true,
                AddExtension = false
            };            
        }


        public string[] BrowseFiles()
        {
            List<string> result = new List<string>();                      
            
            if (FD.ShowDialog() == DialogResult.OK)
            {
                foreach (string value in FD.FileNames)
                {
                    if (Verify(value))                    
                        result.Add(value);                    
                }
            }
            FD.Dispose();
            return result.ToArray();
        }

        public bool Verify(string filename)
        {
            var tmp = new FileInfo(filename);

            //tmp.Extension

            return true;
        }

    }
}
