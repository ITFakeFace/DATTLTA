using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.TreeStorage
{
    internal class textFileManipulator
    {
        string fileName { get; set; }
        private static int autoInscreNumb = 0;
        public string currentDirectory { get; set; }
        public string folderPath { get; set; }
        public string fullPath { get; set; }
        public textFileManipulator(string fileName = "savedTree.txt", string? folderPath = null)
        {
            this.fileName = fileName;
            this.currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.folderPath = (folderPath is null) ? this.currentDirectory : folderPath;
            this.fullPath = Path.Combine(this.folderPath, this.fileName);

        }
        public void fileWriter(List<string> treeVal)
        {

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("Folder was create at  " + folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                Console.WriteLine("File was create at  " + fullPath);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var item in treeVal)
                    {
                        streamWriter.Write(item + ',');
                    }
                }
            }
        }

        public List<object>? fileReader(string? fullPath = null)
        {
            string[] result;
            List<object> listVal = new List<object>();
            try
            {
                using (FileStream fileStream = (fullPath is null)
                    ? new FileStream(this.fullPath, FileMode.Open, FileAccess.Read)
                    : new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        result = streamReader.ReadLine()!.TrimEnd(',').Split(',');
                        foreach (var item in result)
                        {
                            listVal.Add(item);
                        }
                        return listVal;
                    }
                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                {
                    Console.WriteLine($@"File {this.folderPath}\{this.fileName} does not exist ");
                    return null;

                }
                else if (e is System.Security.SecurityException)
                {
                    Console.WriteLine("Access Denied");
                    return null;
                }
            }


            return null;
        }
    }
}
