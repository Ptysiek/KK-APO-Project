using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;


namespace KK17413_APO_REMASTER.BackEnd
{
    public static class FileVerification
    {
        public static string[] BrowseFiles()
        {
            OpenFileDialog FD = new OpenFileDialog()
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "Browse Your Image",
                Multiselect = true,
                AddExtension = false
            };

            string[] result = null;
            if (FD.ShowDialog() == DialogResult.OK)
            {
                result = Verify(FD.FileNames);
            }
            FD.Dispose();
            FD = null;

            return result;
        }

        public static string[] Verify(string[] FileNames)
        {
            List<string> result = new List<string>();
            foreach (string value in FileNames)
            {
                if (Tests(value))
                    result.Add(value);
            }
            return result.ToArray();
        }

        private static bool Tests(string filename)
        {
            var tmp = new FileInfo(filename);

            //tmp.Extension

            return true;
        }
    }
}
