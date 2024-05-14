using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration.Assemblies;
using System.Data;
using System.Data.Common;
using System.Formats.Asn1;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
class graph<T, weight> // weight is either int or boolean, depending on whether it should be weighted or not
{
    // This graph uses an adjacency matrix
    // This is a directed graph so it will only store one data point per connection
    // Adjacencies can be identified much more quickly than when using an adjacency list
    // Better for dense graphs
    public List<T> nodes;
    public List<List<weight>> adjacencyMatrix;
    private weight defaultValue;
    public graph(List<T> nodes, weight defaultValue)
    {
        adjacencyMatrix = new List<List<weight>>();
        this.nodes = nodes;
        this.defaultValue = defaultValue;
        for(int i  = 0; i < nodes.Count; i++)
        {
            AddColumnToEdges();
        }
    }
    public graph(List<T> nodes, List<List<weight>> adjacencyMatrix)
    {
        this.nodes = nodes;
        this.adjacencyMatrix = adjacencyMatrix;
    }
    private void AddColumnToEdges()
    {
        List<weight> column = new List<weight>();
        for (int j = 0; j < nodes.Count; j++)
        {
            column.Add(defaultValue);
        }
        adjacencyMatrix.Add(column);
    }
    public void AddNode(T node)
    {
        nodes.Add(node);
        AddColumnToEdges();
    }
    public weight GetConnection(int i, int j) { return adjacencyMatrix[i][j]; }
    public virtual void SetConnection(int i, int j, weight value) 
    { 
        adjacencyMatrix[i][j] = value;
    }
    public void GetConnections()
    {
        for(int i = 0; i < adjacencyMatrix.Count; i++)
        {
            for(int j = 0; j < nodes.Count; j++)
            {
                Console.Write(GetConnection(i,j) + " ");
            }
            Console.WriteLine();
        }
    }
}
class UndirectedGraph<T, weight> : graph<T, weight>
{
    public UndirectedGraph(List<T> nodes, weight defaultValue) : base(nodes, defaultValue) { }
    public UndirectedGraph(List<T> nodes, List<List<weight>> edges) : base(nodes, edges) { }
    public override void SetConnection(int i, int j, weight value)
    {
        base.SetConnection(i, j, value);
        base.SetConnection(j, i, value);
    }
}
class AdjacencyListGraph<T> {
    // Only stores data at adjacencies
    // Has to check whether a connections exists rather than just look it up
    // Better for sparse graphs
    // This is also an directed graph and its unweighted
    protected Dictionary<T, List<int>> adjacencyList = new Dictionary<T, List<int>>();
    public AdjacencyListGraph() {
        adjacencyList = new Dictionary<T, List<int>>();
    }
    public void AddNode(T additem){
        adjacencyList.Add(additem, new List<int>());
    }
    public virtual void SetConnections(T listPoint, int[] indicies){
        adjacencyList[listPoint].AddRange(indicies);
    }
    public void PrintAdjacencyList(){
        foreach(KeyValuePair<T, List<int>> kvp in adjacencyList){
            Console.Write(kvp.Key + ":");
            List<int> adjacencies = kvp.Value;
            foreach(int index in adjacencies){
                Console.Write(" {0},", adjacencyList.ElementAt(index).Key);
            }
            Console.WriteLine();
        }
    }
}
class UndirectedAdjacencyList<T> : AdjacencyListGraph<T>{
    public override void SetConnections(T listPoint, int[] indicies) {
        base.SetConnections(listPoint, indicies);
        int listPointIndex = adjacencyList.Keys.ToList().IndexOf(listPoint);
        foreach(int index in indicies){
            adjacencyList.ElementAt(index).Value.Add(listPointIndex);
        }
    }
}
class Binarytree
{
    public List<string> nodes;
    public List<int> leftIndexes;
    public List<int> rightIndexes;

    protected int defaultIndex;
    public Binarytree(int defaultValue, string root)
    {
        nodes = new List<string>();
        leftIndexes = new List<int>();
        rightIndexes = new List<int>();

        this.defaultIndex = defaultValue;
        nodes.Add(root);
        leftIndexes.Add(defaultIndex);
        rightIndexes.Add(defaultIndex);
    }
    public virtual void AddNode(string additem)
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
/*
class BinarySearchTree{
    private string[] nodes;
    private int[] leftIndexes;
    private int[] rightIndexes;
    private int defaultIndex;
    private string defaultNode;
    public BinarySearchTree(int size, int defaultIndex, string defaultNode){
        nodes = new string[size];
        leftIndexes = new int[size];
        rightIndexes = new int[size];
        for(int i = 0; i < size; i++){
            nodes[i] = defaultNode;
            leftIndexes[i] = defaultIndex;
            rightIndexes[i] = defaultIndex;
        }
        this.defaultIndex = defaultIndex;
        this.defaultNode = defaultNode;
    }
    public void AddNode(string additem){
        if()
    }
}
*/
class BinarySearchTree : Binarytree{
    private string defaultNode;
    public BinarySearchTree(int defaultValue, string root) : base(defaultValue, root){
    }
    public override void AddNode(string additem)
    {
        int newNodePosition = nodes.Count;
        nodes.Add(additem);
        leftIndexes.Add(defaultIndex);
        rightIndexes.Add(defaultIndex);
        bool placeFound = false;
        int currentNodeIndex = 0;
        try{
            while(!placeFound){
                int comparison = string.Compare(additem, nodes[currentNodeIndex]);
                if(comparison < 0){
                    if(leftIndexes[currentNodeIndex] == defaultIndex){
                        placeFound = true;
                        leftIndexes[currentNodeIndex] = newNodePosition;
                    }
                    else{
                        currentNodeIndex = leftIndexes[currentNodeIndex];
                    }
                }
                else{
                    if(rightIndexes[currentNodeIndex] == defaultIndex){
                        placeFound = true;
                        rightIndexes[currentNodeIndex] = newNodePosition;
                    }
                    else{
                        currentNodeIndex = rightIndexes[currentNodeIndex];
                    }
                }
            }
        }
        catch{
            Console.WriteLine("Error occured, currentNodeIndex: {0}, additem: {1}", currentNodeIndex, additem);
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        BinarySearchTree bst = new BinarySearchTree(-1, "M");
        bst.AddNode("A");
        bst.AddNode("Z");
        bst.AddNode("B");
        bst.AddNode("F");
        bst.AddNode("Q");

        bst.PrintAdjacencyList();
    }
}