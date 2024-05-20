using System;
using System.Collections.Generic;
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
    private int GetNodeIndex(string node){
        for(int i = 0; i < nodes.Count; i++){
            if(node == nodes[i]){
                return i;
            }
        }
        return -1;
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
    private bool[] visited;
    private int[] distanceFromStart;
    private string[] previousVertex;
    public void Dijkstra(string start, string destination){
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
                    if(distanceFromStart[i] < currentSmallestDistance && !visited[i]){                       
                        currentSmallestDistance = distanceFromStart[i];
                        shortestDistanceIndex = i;
                    }
                }
            }
            nodeToVisit = nodes[shortestDistanceIndex];
        }
        Console.WriteLine("The shortest path from {0} to {1} is {2} with distance {3}", start, destination, DetermineShortestPath(start, destination), distanceFromStart[destinationIndex]);
    }
    private void Visit(string node){
        int nodeIndex = GetNodeIndex(node);
        visited[nodeIndex] = true;
        for(int i = 0; i < nodes.Count; i++){
            int connection = GetConnection(i, nodeIndex);
            if(!visited[i]){
                if(connection != defaultValue){ // check for connection
                    int newDistanceFromStart = distanceFromStart[nodeIndex] + connection;
                    if(newDistanceFromStart < distanceFromStart[i]){
                        distanceFromStart[i] = newDistanceFromStart;
                        previousVertex[i] = node;
                    }
                }
            }
        }
    }
    private string DetermineShortestPath(string start, string destination){
        if(start == destination){
            return destination;
        }
        int destinationIndex = GetNodeIndex(destination);
        return DetermineShortestPath(start, previousVertex[destinationIndex]) + destination;
    }
}
class Program{
    static void Main(string[] args){
        List<string> nodes = new List<string>() {"A", "B", "C", "D", "E", "F", "G"};
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

        graph.Dijkstra("A", "F");
    }
}