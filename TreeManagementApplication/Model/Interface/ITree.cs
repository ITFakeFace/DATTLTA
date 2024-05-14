namespace TreeManagementApplication.Model.Interface
{
<<<<<<< HEAD
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
        void InsertNode(T Value);
        List<T>? GetValues();
        public INode<T>? FindParentNode(INode<T> node);
        public bool UpdateNode(INode<T> Node, T value);
    }
=======
	interface ITree<T> where T : IComparable<T>
	{
		bool IsEmpty();
		void PrintConsole();
		bool UpdateNode(INode<T> node, T value);
		INode<T> DeleteNode(T Value);
		void PrintLNR(INode<T>? Node);
		void PrintLRN(INode<T>? Node);
		void PrintNLR(INode<T>? Node);
		void PrintNRL(INode<T>? Node);
		void PrintRLN(INode<T>? Node);
		void PrintRNL(INode<T>? Node);
		INode<T>? FindNode(T Value);
		INode<T>? GetRoot();
		void SetRoot(INode<T> Node);

		void GenerateGridIndex();
		int GetLargestX(INode<T> Node);

		bool InsertNode(T Value);
		List<T>? GetValues();

		ITree<T> GenerateRandomTree(int Count, int Min, int Max);
	}
>>>>>>> Hùng1
}
