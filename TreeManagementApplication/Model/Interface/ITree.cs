namespace TreeManagementApplication.Model.Interface
{
    interface ITree<T> where T : IComparable<T>
    {
        bool IsEmpty();
        void PrintConsole();
        INode<T> DeleteNode(T Value);
        void PrintLNR(INode<T>? Node);
        void PrintLRN(INode<T>? Node);
        void PrintNLR(INode<T>? Node);
        void PrintNRL(INode<T>? Node);
        void PrintRLN(INode<T>? Node);
        void PrintRNL(INode<T>? Node);
        INode<T>? FindNode(T Value);
        INode<T>? FindNode(int XIndex, int Level);
        INode<T>? GetRoot();
        void SetRoot(INode<T> Node);
        void GenerateGridIndex();
        int GetLargestX(INode<T> Node);
        bool InsertNode(T Value);
        List<T>? GetValues();
        INode<T>? FindParentNode(INode<T> node);
        bool UpdateNode(INode<T> Node, T value);
        List<String> Serialize();
    }
}
