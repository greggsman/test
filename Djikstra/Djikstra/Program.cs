using System.Dynamic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

class UnDirectedWeightedStringGraph 
{
    public List<string> nodes;
    public List<List<int>> adjacencyMatrix;
    private int defaultValue;
    public UnDirectedWeightedStringGraph(List<string> nodes)
    {
        adjacencyMatrix = new List<List<int>>();
        this.nodes = nodes;
        this.defaultValue = 0;
        for(int i  = 0; i < nodes.Count; i++)
        {
            AddColumnToEdges();
        }
    }
    public UnDirectedWeightedStringGraph(List<string> nodes, List<List<int>> adjacencyMatrix)
    {
        this.nodes = nodes;
        this.adjacencyMatrix = adjacencyMatrix;
        this.defaultValue = 0;
    }
    private void AddColumnToEdges()
    {
        List<int> column = new List<int>();
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
    public int GetConnection(int i, int j) { return adjacencyMatrix[i][j]; }
    public virtual void SetConnection(string node1, string node2, int value) 
    {
        int i = GetNodeIndex(node1);
        int j = GetNodeIndex(node2);
        adjacencyMatrix[i][j] = value;
        adjacencyMatrix[j][i] = value;
    }
    public void GetConnections()
    {
        Console.Write(" ");
        foreach(string node in nodes) Console.Write(node);
        Console.WriteLine();
        for(int i = 0; i < adjacencyMatrix.Count; i++)
        {
            Console.Write(nodes[i]);
            for(int j = 0; j < nodes.Count; j++)
            {
                int connectionFound = GetConnection(i, j);
                Console.Write(connectionFound);
            }
            Console.WriteLine();
        }
    }
    bool[] visited;
    int[] distanceFromStart;
    string[] previousVertex;
    public void FindShortestPath(string start, string destination){
        int nodesCount = nodes.Count;
        visited = new bool[nodesCount];
        distanceFromStart = new int[nodesCount];
        previousVertex = new string[nodesCount];
        string emptyPreviousVertex = "";

        // populate arrays
        for(int i = 0; i < nodesCount; i++){
            visited[i] = false;
            if(nodes[i] == start){
                distanceFromStart[i] = 0;
            }
            else{
                distanceFromStart[i] = int.MaxValue;
            }
            previousVertex[i] = emptyPreviousVertex;
        }
        
        // Begin traversing
        int destinationIndex = GetNodeIndex(destination);
        string nodeToVisit = start;
        while(!visited[destinationIndex]){
            Visit(nodeToVisit);
            int shortestDistanceIndex = 0;
            int currentSmallestDistance = int.MaxValue;
            for(int i = 0; i < nodes.Count; i++){
                if(distanceFromStart[i] > 0){
                    if(distanceFromStart[i] < currentSmallestDistance){                       
                        currentSmallestDistance = distanceFromStart[i];
                        shortestDistanceIndex = i;
                    }
                }
            }
            nodeToVisit = nodes[shortestDistanceIndex];
        }
    }
    private void Visit(string node){
        Console.WriteLine("visiting " + node);
        int nodeIndex = GetNodeIndex(node);
        for(int i = 0; i < nodes.Count; i++){
            int connection = GetConnection(i, nodeIndex);
            if(!visited[i]){
                if(connection != defaultValue){ // check for connection
                    int newDistanceFromStart = distanceFromStart[nodeIndex] + connection;
                    if(newDistanceFromStart < distanceFromStart[i]){
                        distanceFromStart[i] = newDistanceFromStart;
                    }
                }
            }
        }
        visited[nodeIndex] = true;
        foreach(int distance in distanceFromStart){
            if(distance == int.MaxValue){
                Console.WriteLine("Inf");
            }
            else{
                Console.WriteLine(distance);
            }
        }
    }
    public int GetNodeIndex(string node){
        for(int i = 0; i < nodes.Count; i++){
            if(node == nodes[i]){
                return i;
            }
        }
        return -1;
    }
}
class Program{
    static void Main(string[] args){
        List<string> nodes = new List<string>() {"A", "B", "C", "D", "E", "F"};
        UnDirectedWeightedStringGraph graph = new UnDirectedWeightedStringGraph(nodes);
        graph.SetConnection("A", "B", 9);
        graph.SetConnection("A", "C", 5);
        graph.SetConnection("B", "D", 4);
        graph.SetConnection("C", "D", 8);
        graph.SetConnection("B", "E", 6);
        graph.SetConnection("D", "E", 7);
        graph.SetConnection("D", "F", 4);
        graph.SetConnection("E", "F", 3);
        graph.GetConnections();

        graph.FindShortestPath("A", "F");
    }
}