class HashTable<TValue>{
    // Hash table with integer keys
    private List<KeyValuePair<int, TValue>>[] keyValuePairs;
    // The hash table in this instance is a an array of lists of KVPs
    // The index of the array in the index that the KVP will be stored in
    // The reason for having an array of lists rather than just an array of kvps is this allows for CHAINING.
    // Chaining is a method of dealing with collisions in which each key with the same hash is stored in the same list
    // A collision is when two keys to be added to the table produce the same hash
    // Clustering is when the hashes are not evenly/randomly distributed
    // Load factor is when the ratio of indices available to total indecies
    // The alternative is rehashing in which alternative hashes are used until a unique key is created
    // Probing is when the algorithm searches for an empty slot
    public HashTable(int tableSize){
        keyValuePairs = new List<KeyValuePair<int, TValue>>[tableSize];
        for(int i = 0; i < tableSize; i++){
            keyValuePairs[i] = new List<KeyValuePair<int, TValue>>();
        }
    }
    private int TextbookHash_GetIndex(int key){
        List<int> listOfInts = new List<int>();
        int num = key;
        int total = 0;
        while(num > 0)
        {
            listOfInts.Add(num % 10);
            num = num / 10;
            total += num;
        }
        return total % 6;
    }
    public void AddKvp(KeyValuePair<int, TValue> input){
        int index = TextbookHash_GetIndex(input.Key);
        if(index < keyValuePairs.Length){
            keyValuePairs[index].Add(input);
            // Adds to the list - this is chaining
        }
        else{
            Console.WriteLine("You're gonna need a bigger hash table");
        }
    }
    public List<KeyValuePair<int, TValue>> FindKvp(int key){
        int index = TextbookHash_GetIndex(key);
        try{
            return keyValuePairs[index];
        }
        catch{
            Console.WriteLine("You're gonna need a bigger hash table");
            return null;
        }
    }

}