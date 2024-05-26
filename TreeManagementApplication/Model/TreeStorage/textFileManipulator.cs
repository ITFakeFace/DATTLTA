using Microsoft.VisualBasic;
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
        private FileMode modeOpen = FileMode.Open;
        private FileMode modeOpenOrCreate = FileMode.OpenOrCreate;
        private FileAccess accessRead = FileAccess.Read;
        private FileAccess accessWrite = FileAccess.Write;
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

            string filePath = Path.Combine(folderPath, fileName);

            using (FileStream fileStream = new FileStream(filePath, modeOpenOrCreate, accessWrite))
            {
                Console.WriteLine("File was create at  " + filePath);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var item in treeVal)
                    {
                        streamWriter.Write(item + ',');
                    }
                }
            }
        }

        public Queue<object>? fileReader(string? fullPath = null)
        {
            Queue<object> listVal = new Queue<object>();
            try
            {
                using (FileStream fileStream = (fullPath is null)
                        ? new FileStream(this.fullPath, modeOpen, accessRead)
                        : new FileStream(fullPath, modeOpen, accessRead))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        string[] result = streamReader.ReadLine()!.TrimEnd(',').Split(',');
                        foreach (var item in result)
                        {
                            listVal.Enqueue(item);
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
