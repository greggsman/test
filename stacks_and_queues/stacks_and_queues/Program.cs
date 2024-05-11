using System.Drawing;
using System.Net.Security;

class Stack<T>{
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
class Queue<T> : Stack<T>{
    protected int startPointer;
    public Queue() : base(){
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
class CircularQueue<T> : Queue<T>{
    public override void Push(T itemToAdd)
    {
        try{
            items[endPointer] = itemToAdd;
        }
        catch(ArgumentOutOfRangeException){
            endPointer = 0;
            items[endPointer] = itemToAdd;
        }
        endPointer++;
    }
}
class StringPriorityQueue{
    private Dictionary<string, int> nodes_with_weights;
    private int startPointer;
    private int endPointer;
    public StringPriorityQueue(){
        nodes_with_weights = new Dictionary<string, int>();
        startPointer = 0;
        endPointer = 0;
    }
    public bool IsEmpty(){
        if(startPointer == endPointer){
            return true;
        }
        return false;
    }
    private void Push(string item, int priority){
        KeyValuePair<string, int> kvpToAdd = new KeyValuePair<string, int>(item, priority);        
        if(IsEmpty()){
            
        }
    }
    private void Pop(){

    }
}
class PriorityQueue<T> : Queue<T>{
    // higher priority = higher value
    private List<int> associatedPriorities;
    public PriorityQueue() : base(){
        associatedPriorities = new List<int>();
    }
    public void Push(T itemToAdd, int priority)
    {
        if(startPointer == endPointer){
            items[startPointer] = itemToAdd;
            associatedPriorities[startPointer] = priority;
        }
        int tempPointer = endPointer;
        while(tempPointer != startPointer){
            if(priority > associatedPriorities[tempPointer]){ // if its a higher priority
                
            }
            else{

            }
        }
        endPointer++;
    }
}
class Program{
    static void Main(){
        Stack<int> stackOfInts = new Stack<int>();
        const int difference = 2;
        const int limit = 10;
        for(int i = 0; i < difference * limit; i += difference){
            stackOfInts.Push(i);
        }
        List<int> test = new List<int>();
        for(int i = 0 ; i < limit; i++){
            test.Add(stackOfInts.Pop());
        }
        foreach(int item in test){
            Console.Write(item + " ");
        }
        Console.WriteLine("Queues:");

        Queue<int> queueOfInts = new Queue<int>();
        for(int i = 0; i < difference * limit; i += difference){
            queueOfInts.Push(i);
            Console.Write(i.ToString() + " ");
        }
        Console.WriteLine();
        int queueLength = queueOfInts.GetLength();
        for(int i = 0; i < queueLength; i++){
            Console.Write(queueOfInts.Pop() + " ");
        }
        Console.ReadLine();
    }
}