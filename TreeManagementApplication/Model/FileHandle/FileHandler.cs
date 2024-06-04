﻿using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.FileHandle
{
    internal class FileHandler<T> where T : IComparable<T>
    {
        string folderPathBin { get; }
        string filePathBin { get; }
        string filePathTxt = String.Empty;
        public FileHandler()
        {
            this.folderPathBin = $@"{Directory.GetCurrentDirectory()}\TreeSnapshot";
            this.filePathBin = $@"{folderPathBin}\BinaryFormatFile.dat"; ;
        }

        public void saveFile(string content)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Text files (*.txt)|*.txt";
            fileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            fileDialog.ShowDialog();
            try
            {
                if (fileDialog.FileName != "")
                {
                    using FileStream fs = (FileStream)fileDialog.OpenFile();
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            writer.Write(content);
                        }
                        this.filePathTxt = fileDialog.FileName;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void saveFile(ITree<T> tree)
        {
            byte[] bytes = SerializeBinary(tree);
            string directoryPath = $@"{Directory.GetCurrentDirectory()}\TreeSnapshot";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (FileStream fileStream = new FileStream(this.filePathBin, FileMode.OpenOrCreate))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
        public INode<T> loadBinFile()
        {
            byte[] bytes;
            using (FileStream fileStream = new FileStream(this.filePathBin, FileMode.Open))
            {
                bytes = new byte[fileStream.Length]; // Initialize the array with the file size
                fileStream.Read(bytes, 0, bytes.Length); // Read the entire file into the array
            }

            return DeSerializeBinary(bytes);
        }

        public Queue<object> loadTxtFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            Queue<object> queueLine = new Queue<object>();
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                using (StreamReader reader = new StreamReader(ofd.FileName))
                {
                    string line = reader.ReadToEnd();
                    string[] lineSplit = line.TrimEnd(',').Split(',');
                    foreach (string item in lineSplit)
                    {
                        queueLine.Enqueue(item);
                    }
                }
                return queueLine;
            }
            return null!;
        }


#pragma warning disable SYSLIB0011

        public byte[] SerializeBinary(ITree<T> tree)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] byteArray;

            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, tree.GetRoot()!);
                byteArray = ms.ToArray();
            }
            Console.WriteLine(byteArray);
            tree.SetRoot(null!);
            return byteArray;
        }

        public INode<T> DeSerializeBinary(byte[] byteArray)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return (INode<T>)formatter.Deserialize(ms);
            }
        }

    }
}
