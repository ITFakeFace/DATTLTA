namespace TreeManagementApplication.Model.Interface
{
    interface ITree<T> where T : IComparable<T>
    {
        bool IsEmpty();
        void PrintConsole();
        //void UpdateNode(INode<T> node);
        //void RemoveNode(T Value);
        void PrintLNR(INode<T>? Node);
        void PrintLRN(INode<T>? Node);
        void PrintNLR(INode<T>? Node);
        void PrintNRL(INode<T>? Node);
        void PrintRLN(INode<T>? Node);
        void PrintRNL(INode<T>? Node);
        INode<T>? GetRoot();
        void SetRoot(INode<T> Node);
    }
}
