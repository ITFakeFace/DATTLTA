namespace TreeManagementApplication.Model.Interface
{
    interface ITree<T> where T : IComparable<T>
    {
        void Print();
        void InsertNode(T value);
        void RemoveNode(T value);
        void editNode(T nodeValue, T valueReplaced);

        void PrintLNR(INode<T>? node);
        void PrintLRN(INode<T>? node);
        void PrintNLR(INode<T>? node);
        void PrintNRL(INode<T>? node);
        void PrintRLN(INode<T>? node);
        void PrintRNL(INode<T>? node);



    }
}
