using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace TreeManagementApplication.Model.TreeStorage
{
    internal class textFileManipulator
    {
        string fileName { get; set; }
        string folderName { get; set; }
        private static int autoInscreNumb = 0;
        public string currentDirectory { get; set; }
        public string folderPath { get; set; }
        public textFileManipulator(string fileName = "savedTree", string folderName = "SavedFile")
        {
            this.fileName = fileName;
            this.folderName = folderName;
            currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
        public void fileWriter(List<string> treeVal)
        {
            string folderPath = Path.Combine(currentDirectory, folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var item in treeVal)
                    {
                        streamWriter.Write(item + ',');
                    }
                }
            }
            Console.WriteLine("File was create at  " + fullPath);
        }

        public String? fileReader()
        {
            String result;
            string fullPath = Path.Combine(folderPath, fileName);

            try
            {
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        result = streamReader.ReadToEnd();
                    }
                    return result;

                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                {
                    Console.WriteLine("File does not exist");
                }
            }
            return null;
        }

        public String? fileReader(string folderPath, string fileName)
        {
            String result;
            string fullPath = Path.Combine(folderPath, fileName);

            try
            {
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        result = streamReader.ReadToEnd();
                    }
                    return result;

                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                {
                    Console.WriteLine("File does not exist");
                }
            }
            return null;
        }
    }
}
