using Microsoft.Win32;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.FileHandle
{
    internal class FileHandler<T> where T : IComparable<T>
    {
        public string? saveFile(ITree<T> tree)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Text files (*.txt)|*.txt||Binary files (*.bin)|*.bin";
            fileDialog.ShowDialog();
            string fileName = fileDialog.FileName;
            string ext = Path.GetExtension(fileName);
            string result = string.Empty;
            try
            {
                if (fileName != "")
                {
                    if (Path.GetExtension(fileName) == ".txt")
                    {
                        using FileStream fs = (FileStream)fileDialog.OpenFile();
                        {
                            using (StreamWriter writer = new StreamWriter(fs))
                            {
                                result = tree.Serialize();
                                writer.Write(result);
                            }
                            return result;
                        }
                    }
                    else if (Path.GetExtension(fileName) == ".bin" || Path.GetExtension(fileName) == ".dat")
                    {
                        byte[] bytes = SerializeBinary(tree);

                        using (FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Open))
                        {
                            fileStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    return null!;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return null!;
            }
            return null;
        }
        public (Queue<object>?, INode<T>?) loadFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt|Binary files (*.bin)|*.bin";
            ofd.ShowDialog();
            if (Path.GetExtension(ofd.FileName) == ".txt")
            {
                Queue<object> result = loadTxtFile(ofd.FileName);
                return (result, null);
            }
            else
            {
                INode<T> node = loadBinFile(ofd.FileName);
                return (null, node);
            }
        }


        public INode<T> loadBinFile(string fileName)
        {

            byte[] bytes;
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    bytes = new byte[fileStream.Length]; // Initialize the array with the file size
                    fileStream.Read(bytes, 0, bytes.Length); // Read the entire file into the array
                }
                return DeSerializeBinary(bytes);
            }
            catch (Exception ex)
            {
                if (ex is SerializationException)
                {
                    Console.WriteLine("Error when load file");
                }
                return null!;
            }


        }

        public Queue<object> loadTxtFile(string fileName)
        {
            Queue<object> queueLine = new Queue<object>();
            using (StreamReader reader = new StreamReader(fileName))
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


#pragma warning disable SYSLIB0011

        public byte[] SerializeBinary(ITree<T> tree)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] byteArray;
            if (tree != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    formatter.Serialize(ms, tree.GetRoot()!);
                    byteArray = ms.ToArray();
                }
                Console.WriteLine(byteArray);
                tree.SetRoot(null!);
                return byteArray;
            }
            return null!;
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
