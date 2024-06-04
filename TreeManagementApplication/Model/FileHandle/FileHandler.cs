using Microsoft.Win32;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Documents;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.FileHandle
{
    internal class FileHandler<T> where T : IComparable<T>
    {
        string filePathBin { get; set; } = String.Empty;
        string filePathTxt { get; set; } = String.Empty;

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
            string filePathBin = $@"{directoryPath}\BinaryFormatFile.dat";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (FileStream fileStream = new FileStream(filePathBin, FileMode.OpenOrCreate))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
        public INode<T> loadFile()
        {
            byte[] bytes;
            using (FileStream fileStream = new FileStream(filePathBin, FileMode.Open))
            {
                for (int i = 0; i < fileStream.Length; i++)
                {
                    bytes[i] = (byte)fileStream.ReadByte();
                }
            }

            return DeSerializeBinary(bytes);


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
