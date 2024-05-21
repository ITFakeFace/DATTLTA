using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TreeManagementApplication.Model.TreeStorage
{
    internal class textFileManipulator
    {
        string fileName { get; set; }
        public textFileManipulator(string fileName = "\\TreeStorage")
        {
            this.fileName = fileName;
        }
        public void fileWriter(List<string> treeVal)
        {
            if (!File.Exists(fileName))
            {
                FileStream fileStream = new FileStream($"{fileName}", FileMode.Create);
            }


        }
    }
}
