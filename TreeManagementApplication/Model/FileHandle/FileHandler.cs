using Microsoft.Win32;
using System.IO;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Documents;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.FileHandle
{
    internal class FileHandler<T> where T : IComparable<T>
    {
        string fileName { get; set; } = String.Empty;

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
                        this.fileName = fileDialog.FileName;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void saveFile(AVLTree<T> tree)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter writer = new BinaryWriter(ms);
            byte buffer = Convert.ToByte(tree.GetRoot());
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Binary files (*.bin)|*.bin";
            fileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            fileDialog.ShowDialog();
            try
            {
                if (fileDialog.FileName != "")
                {
                    using FileStream fs = (FileStream)fileDialog.OpenFile();
                    {
                        writer.Write(buffer);
                        this.fileName = fileDialog.FileName;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void BinaryEncode(string content)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter writer = new BinaryWriter(ms);
            foreach (var item in content)
            {
                writer.Write(item);
            }

        }
    }
}
