using System.Reflection.Metadata;

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
    public virtual void SetConnection(int i, int j, int value) 
    { 
        adjacencyMatrix[i][j] = value;
        adjacencyMatrix[j][i] = value;
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
    /*
    public string FindShortestPath(string start, string end){
        int startIndex = -1;
        int endIndex = -1;
        for(int i = 0; i < nodes.Count; i++){
            if(nodes[i] == start){
                startIndex = i;
            }
            if(nodes[i] == end){
                startIndex = i;
            }
        }
        else{
            return start;
        }
        int[] connections = new int[nodes.Count];
        return Djikstras(startIndex, endIndex, connections);
    }
    private string Djikstras(int startIndex, int endIndex, int[] connections, List<string> path){
        if(startIndex == endIndex){
            // base case
        }
        for(int i = 0; i < connections.Length; i++){
            int connectionValue = GetConnection(startIndex, i);
            connections[i] += connectionValue;
        }
        Djikstras(startIndex, endIndex, connections);
    }
    */
}