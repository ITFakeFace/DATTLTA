using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinarySearchTree
{
	internal class AVLTree<T> : ITree<T> where T : IComparable<T>
	{
		AVLNode<T> root;

		int height(AVLNode<T> N)
		{
			if (N == null)
				return 0;

			return N.height;
		}

		// A utility function to get 
		// maximum of two integers  
		int max(int a, int b)
		{
			return (a > b) ? a : b;
		}

		AVLNode<T> rightRotate(AVLNode<T> y)
		{
			AVLNode<T> x = y.left;
			AVLNode<T> T2 = x.right;

			// Perform rotation  
			x.right = y;
			y.left = T2;

			// Update heights  
			y.height = max(height(y.left),
						height(y.right)) + 1;
			x.height = max(height(x.left),
						height(x.right)) + 1;

			// Return new root  
			return x;
		}

		// A utility function to left 
		// rotate subtree rooted with x  
		// See the diagram given above.  
		AVLNode<T> leftRotate(AVLNode<T> x)
		{
			AVLNode<T> y = x.right;
			AVLNode<T> T2 = y.left;

			// Perform rotation  
			y.left = x;
			x.right = T2;

			// Update heights  
			x.height = max(height(x.left),
						height(x.right)) + 1;
			y.height = max(height(y.left),
						height(y.right)) + 1;

			// Return new root  
			return y;
		}

		// Get Balance factor of node N  
		int getBalance(AVLNode<T> N)
		{
			if (N == null)
				return 0;

			return height(N.left) - height(N.right);
		}
		public INode<T> DeleteNode(T Value)
		{
			throw new NotImplementedException();
		}

		public INode<T>? FindNode(T Value)
		{
			throw new NotImplementedException();
		}

		public INode<T>? FindNode(int XIndex, int Level)
		{
			throw new NotImplementedException();
		}

		public INode<T>? FindParentNode(INode<T> node)
		{
			throw new NotImplementedException();
		}

		public void GenerateGridIndex()
		{
			throw new NotImplementedException();
		}

		public int GetLargestX(INode<T> Node)
		{
			throw new NotImplementedException();
		}

		public INode<T>? GetRoot()
		{
			throw new NotImplementedException();
		}

		public List<T>? GetValues()
		{
			throw new NotImplementedException();
		}

		public bool InsertNode(T Value)
		{
			return Insert(root, Value) != root;
		}

		public AVLNode<T> Insert(AVLNode<T> node, T key)
		{
			/* 1. Perform the normal BST insertion */
			if (node == null)
				return (new AVLNode<T>(key));

			if (key.CompareTo(node.key) < 0)
				node.left = Insert(node.left, key);
			else if (key.CompareTo(node.key) > 0)
				node.right = Insert(node.right, key);
			else // Duplicate keys not allowed  
				return node;

			/* 2. Update height of this ancestor node */
			node.height = 1 + max(height(node.left),
								height(node.right));

			/* 3. Get the balance factor of this ancestor  
				node to check whether this node became  
				unbalanced */
			int balance = getBalance(node);

			// If this node becomes unbalanced, then there  
			// are 4 cases Left Left Case  
			if (balance > 1 && key.CompareTo(node.left.key) < 0)
				return rightRotate(node);

			// Right Right Case  
			if (balance < -1 && key.CompareTo(node.right.key) > 0)
				return leftRotate(node);

			// Left Right Case  
			if (balance > 1 && key.CompareTo(node.left.key) > 0)
			{
				node.left = leftRotate(node.left);
				return rightRotate(node);
			}

			// Right Left Case  
			if (balance < -1 && key.CompareTo(node.right.key) < 0)
			{
				node.right = rightRotate(node.right);
				return leftRotate(node);
			}

			/* return the (unchanged) node pointer */
			return node;
		}
		public bool IsEmpty()
		{
			throw new NotImplementedException();
		}

		public void PrintConsole()
		{
			throw new NotImplementedException();
		}

		public void PrintLNR(INode<T>? Node)
		{
			throw new NotImplementedException();
		}

		public void PrintLRN(INode<T>? Node)
		{
			throw new NotImplementedException();
		}

		public void PrintNLR(INode<T>? Node)
		{
			throw new NotImplementedException();
		}

		public void PrintNRL(INode<T>? Node)
		{
			throw new NotImplementedException();
		}

		public void PrintRLN(INode<T>? Node)
		{
			throw new NotImplementedException();
		}

		public void PrintRNL(INode<T>? Node)
		{
			throw new NotImplementedException();
		}

		public void SetRoot(INode<T> Node)
		{
			throw new NotImplementedException();
		}

		public bool UpdateNode(INode<T> Node, T value)
		{
			throw new NotImplementedException();
		}

		public ITree<T> GenerateRandomTree(int Count, int Min, int Max)
		{
			throw new NotImplementedException();
		}

		public int GetLargestY(INode<T> Node)
		{
			throw new NotImplementedException();
		}
	}
}
