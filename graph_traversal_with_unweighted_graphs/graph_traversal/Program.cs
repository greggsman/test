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
            // Check for route
            // Console.WriteLine("Checking node {0}, Connection: {1}", nodes[i], GetConnection(nodeIndex, i));
            if(GetConnection(nodeIndex, i) == true){
                // Check if node has been visited
                if(visitedMatrix[i] == false){
                    DFTRecursion(i);
                }
            }
        }
    }
    public void BreadthFirstTraversal(){
        
    }
}
class Program{
    static void Main(){
        UnweightedStringGraph usg = new UnweightedStringGraph(new List<string>() {"A", "B", "C", "D", "E"});
        usg.SetConnection("A", "B", true);
        usg.SetConnection("B", "C", true);
        usg.SetConnection("D", "C", true);
        usg.SetConnection("D", "E", true);
        usg.PrintAdjacencyMatrix();

        Console.WriteLine();
        usg.DepthFirstTraversal(0);
    }
}