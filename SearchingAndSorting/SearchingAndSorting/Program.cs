class Program{
    static void Main(string[] args){
        int[] unsortedArray = {4, 5, 2, 3, 6, 7, 8, 1, 0, 9};
        Console.WriteLine(BinarySearch(unsortedArray, 3).ToString());
        Console.WriteLine(BinarySearch(unsortedArray, 15).ToString());
    }
    static void LinearSearch(int[] arr, int item){
        for(int i = 0; i < arr.Length; i++){
            if(arr[i] == item){
                Console.WriteLine("Found {0} at position {1}", item.ToString(), i.ToString());
            }
        }
    }
    static int[] BubbleSort(int[] arr){
        for(int i = 0; i < arr.Length - 1; i++){
            for(int j = 0; j < arr.Length - 1; j++){
                if(arr[j] > arr[j + 1]){
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
        return arr;
    }
    static bool BinarySearch(int[] arr, int item){
        arr = BubbleSort(arr);
        while(arr.Length != 0) {
            int midpoint = (int) arr.Length / 2;
            if(arr[midpoint] == item){
                return true;
            }
            else{
                int[] halfOfArr = new int[midpoint];
                if(item < arr[midpoint]){
                    for(int i = 0; i < midpoint; i++){
                        halfOfArr[i] = arr[i];
                    }
                }
                else{ 
                    for(int i = 0; i < midpoint; i++){
                        halfOfArr[i] = arr[i + midpoint];
                    }
                }
                arr = halfOfArr;
            }
        }
        return false;
    }
}