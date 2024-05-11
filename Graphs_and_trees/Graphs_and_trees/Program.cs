using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Xml;

class myGraph<T, weight> 
{
    public List<T> nodes;
    public List<List<weight>> adjancencyMatrix = new List<List<weight>>();
    // alternative to an adjacency list
    private weight defaultValue;
    public myGraph(List<T> nodes, weight defaultValue)
    {
        this.nodes = nodes;
        this.defaultValue = defaultValue;
        for(int i  = 0; i < nodes.Count; i++)
        {
            AddColumnToEdges();
        }
    }
    public myGraph(List<T> nodes, List<List<weight>> edges)
    {
        this.adjancencyMatrix = edges;
    }
    private void AddColumnToEdges()
    {
        List<weight> column = new List<weight>();
        for (int j = 0; j < nodes.Count; j++)
        {
            column.Add(defaultValue);
        }
        adjancencyMatrix.Add(column);
    }
    public void AddNode(T node)
    {
        nodes.Add(node);
        AddColumnToEdges();
    }
    public weight GetConnection(int i, int j) { return adjancencyMatrix[i][j]; }
    public virtual void SetConnection(int i, int j, weight value) 
    { 
        adjancencyMatrix[i][j] = value;
    }
    public void GetConnections()
    {
        for(int i = 0; i < adjancencyMatrix.Count; i++)
        {
            for(int j = 0; j < nodes.Count; j++)
            {
                Console.Write(GetConnection(i,j) + " ");
            }
            Console.WriteLine();
        }
    }
}
class UndirectedGraph<T, weight> : myGraph<T, weight>
{
    public UndirectedGraph(List<T> nodes, weight defaultValue) : base(nodes, defaultValue) { }
    public UndirectedGraph(List<T> nodes, List<List<weight>> edges) : base(nodes, edges) { }
    public override void SetConnection(int i, int j, weight value)
    {
        base.SetConnection(i, j, value);
        base.SetConnection(j, i, value);
    }
}
class Binarytree<T>
{
    public List<T> nodes;
    public List<int> leftIndexes;
    public List<int> rightIndexes;

    int defaultIndex;
    public Binarytree(int defaultValue, T root)
    {
        // this is a bit shit so the alternative method is using a Dictionary<T, List<T>> where T is the type you're using and the list represents the children for each element in the tree
        nodes = new List<T>();
        leftIndexes = new List<int>();
        rightIndexes = new List<int>();

        this.defaultIndex = defaultValue;
        nodes.Add(root);
        leftIndexes.Add(defaultIndex);
        rightIndexes.Add(defaultIndex);
    }
    public void AddNode(T additem)
    {
        nodes.Add(additem);
        leftIndexes.Add(defaultIndex);
        rightIndexes.Add(defaultIndex);
        int addItemPosition = nodes.Count - 1;
        for (int i = 0; i < addItemPosition; i++)
        {
            if (leftIndexes[i] != defaultIndex)
            {
                if (rightIndexes[i] != defaultIndex) // if neither index is free
                {
                    continue;
                }
                else // if right index is free
                {
                    rightIndexes[i] = addItemPosition;
                    break;
                }
            }
            else // if left index is free
            {
                leftIndexes[i] = addItemPosition;
                break;
            }
        }
    }
    public void PreOrderTraversal(int rootIndex)
    {
        if (rootIndex == defaultIndex) { return; }
        Console.WriteLine(nodes[rootIndex]);
        PreOrderTraversal(leftIndexes[rootIndex]);
        PreOrderTraversal(rightIndexes[rootIndex]);
    }
    public void PrintAdjacencyList()
    {
        for(int i = 0; i < nodes.Count; i++)
        {
            Console.Write(nodes[i] + " " + leftIndexes[i] + " " + rightIndexes[i] + "\n");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Binarytree<string> family = new Binarytree<string>(-1, "Livia");
        family.nodes.AddRange(new List<string>() { "Tony", "Janice", "Meadow", "AJ" });
        family.leftIndexes = new List<int>() { 1, 3, -1, -1, -1};
        family.rightIndexes = new List<int>() { 2, 4, -1, -1, -1};
        family.AddNode("Paulie");
        family.PreOrderTraversal(0);
        Console.WriteLine("Hello world from the graphs file");
    }
}