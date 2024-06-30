using TreeManagementApplication.Model.BinaryTree;

namespace TreeManagementApplication.Model.Interface
{
    public interface ITree<T> where T : IComparable<T>
    {
        bool IsEmpty();
        void PrintConsole();
        INode<T> DeleteNode(T Value);
        String PrintLNR();
        String PrintLRN();
        String PrintNLR();
        String PrintNRL();
        String PrintRLN();
        String PrintRNL();
        String PrintLNR(INode<T>? Node, ref String result);
        String PrintLRN(INode<T>? Node, ref String result);
        String PrintNLR(INode<T>? Node, ref String result);
        String PrintNRL(INode<T>? Node, ref String result);
        String PrintRLN(INode<T>? Node, ref String result);
        String PrintRNL(INode<T>? Node, ref String result);
        INode<T>? FindNode(T Value);
        INode<T>? FindNode(int XIndex, int Level);
        INode<T>? GetRoot();
        void SetRoot(INode<T> Node);
        void GenerateGridIndex();
        int GetLargestX(INode<T> Node);
        int GetLargestY(INode<T> Node);
        bool InsertNode(T Value);
        List<T>? GetValues();
        public bool UpdateNode(INode<T> Node, T value);

        public INode<T>? FindParentNode(INode<T> node);
        public void DeleteNode(int XIndex, int Level);
        ITree<T> GenerateRandomTree(int Count, int Min, int Max);
        string Serialize();
        int Deserialize(Queue<Object> readFromFile);
        string nodeTypetoString();
    }
}
