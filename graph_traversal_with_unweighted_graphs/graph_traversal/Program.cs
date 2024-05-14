using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
public class Stack<T>{
    protected List<T> items;
    protected int endPointer;
    public Stack(){
        items = new List<T>();
        endPointer = 0;
    }
    public virtual void Push(T itemToAdd){
        try{
            items[endPointer] = itemToAdd;
        }
        catch(ArgumentOutOfRangeException){
            items.Add(itemToAdd);
        }
        endPointer++;
    }
    public virtual T Peek(){
        return items[endPointer];
    }
    public virtual T Pop(){
        T itemToPush = items[endPointer - 1];
        items.RemoveAt(endPointer - 1);
        endPointer--;
        return itemToPush;
    }
    public virtual int GetLength(){
        return items.Count;
    }
}
public class my_Queue<T> : Stack<T>{
    protected int startPointer;
    public my_Queue() : base(){
        startPointer = 0;
    }
    public override T Peek()
    {
        return items[startPointer];
    }
    public override T Pop()
    {
        T itemToPop = items[startPointer];
        items.RemoveAt(startPointer);
        endPointer--;
        return itemToPop;
    }
    public bool IsEmpty(){
        if(startPointer == endPointer){
            return true;
        }
        return false;
    }
    public override int GetLength()
    {
        return endPointer - startPointer;
    }
}

class UnweightedStringGraph{
    public List<string> nodes;
    public List<List<bool>> adjacencyMatrix;
    private bool defaultValue;
    public UnweightedStringGraph(List<string> nodes)
    {
        adjacencyMatrix = new List<List<bool>>();
        this.nodes = nodes;
        this.defaultValue = false;
        for(int i  = 0; i < nodes.Count; i++)
        {
            AddColumnToEdges();
        }
    }
    public UnweightedStringGraph(List<string> nodes, List<List<bool>> adjacencyMatrix)
    {
        this.nodes = nodes;
        this.adjacencyMatrix = adjacencyMatrix;
    }
    private void AddColumnToEdges()
    {
        List<bool> column = new List<bool>();
        for (int j = 0; j < nodes.Count; j++)
        {
            column.Add(defaultValue);
        }
        adjacencyMatrix.Add(column);
    }
    public void AddNode(string node)
    {
        nodes.Add(node);
        AddColumnToEdges();
    }
    public bool GetConnection(int i, int j) { return adjacencyMatrix[i][j]; }
    public void SetConnection(string valueOne, string valueTwo, bool value) 
    {
        if(valueOne != valueTwo){
            int valueOneIndex = -1;
            int valueTwoIndex = -1;
            for(int i = 0; i < nodes.Count; i++){
                if(nodes[i] == valueOne){
                    valueOneIndex = i;
                }
                if(nodes[i] == valueTwo){
                    valueTwoIndex = i;
                }
            }
            adjacencyMatrix[valueOneIndex][valueTwoIndex] = value;
            adjacencyMatrix[valueTwoIndex][valueOneIndex] = value;
            // i made it unweighted because i cba
        }
    }
    public void PrintAdjacencyMatrix()
    {
        Console.Write(" ");
        for(int i = 0; i < nodes.Count; i++){
            Console.Write(nodes[i]);
        }
        Console.WriteLine();
        for(int i = 0; i < adjacencyMatrix.Count; i++)
        {
            Console.Write(nodes[i]);
            for(int j = 0; j < nodes.Count; j++)
            {
                bool connectionFound = GetConnection(i, j);
                if(connectionFound){
                    Console.Write("1");
                }
                else{
                    Console.Write("0");
                }
            }
            Console.WriteLine();
        }
    }
    bool[] visitedMatrix;
    public void DepthFirstTraversal(int rootIndex){
        visitedMatrix = new bool[nodes.Count];
        for(int i = 0; i < visitedMatrix.Length; i++){
            visitedMatrix[i] = false;
        }
        DFTRecursion(rootIndex);
    }
    private void DFTRecursion(int nodeIndex){
        Console.WriteLine(nodes[nodeIndex] + " ");
        visitedMatrix[nodeIndex] = true;
        // Look at each node, check for route
        for(int i = 0; i < nodes.Count; i++){
            // Check for a route (a connection)
            if(GetConnection(nodeIndex, i) == true){
                // Check if node has been visited
                if(visitedMatrix[i] == false){
                    DFTRecursion(i);
                }
            }
        }
    }
    public void BreadthFirstTraversal(){
        visitedMatrix = new bool[nodes.Count];
        for(int i = 0; i < visitedMatrix.Length; i++){
            visitedMatrix[i] = false;
        }
        my_Queue<int> indexQueue = new my_Queue<int>();
        int currentIndex = 0;
        indexQueue.Push(currentIndex);
        visitedMatrix[currentIndex] = true;
        while(!indexQueue.IsEmpty()){
            currentIndex = indexQueue.Pop();
            Console.Write(nodes[currentIndex]);
            for(int i = 0; i < nodes.Count; i++){
                if(GetConnection(currentIndex, i)){
                    if(visitedMatrix[i] == false){
                        visitedMatrix[i] = true;
                        indexQueue.Push(i);
                    }
                }
            }
        }
    }
}
class Program{
    static void Main(){
        UnweightedStringGraph usg = new UnweightedStringGraph(new List<string>() {"A", "B", "C", "D", "E"});
        usg.SetConnection("A", "B", true);
        usg.SetConnection("B", "C", true);
        usg.SetConnection("D", "C", true);
        usg.SetConnection("D", "E", true);
        usg.SetConnection("E", "B", true);
        usg.PrintAdjacencyMatrix();

        Console.WriteLine();
        usg.DepthFirstTraversal(0);

        Console.WriteLine();
        usg.BreadthFirstTraversal();
    }
}