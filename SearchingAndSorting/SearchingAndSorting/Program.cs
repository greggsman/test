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
    static int[] mergeSort(int[] a)
    {
        //base case
        if( a.Length <= 1)
        { return a; }

        //recursive case
        //split it into two halves
        int midpoint = a.Length / 2;
        int[]left= new int[midpoint];
        int[]right= new int[a.Length - midpoint];

        for (int i = 0; i < midpoint; i++) // we take the vaules up to them midpoint into the left  half
        {
            left[i] = a[i];
        }
        for(int i=midpoint; i < a.Length; i++) // we want values from the midpoint into the right half
        {
            right[i - midpoint] = a[i];
        
        }
        // merge sort each half
        left = mergeSort(left);
        right = mergeSort(right);

        //recombine

        int leftv = 0;
        int rightv = 0;
        int ansPointer = 0;

        while(leftv<left.Length || rightv<right.Length) // are there values left to enter?
        {
            if(leftv < left.Length && rightv < right.Length) //are there values on both sides?
            {
                if (left[leftv] < right[rightv]) // if the left value is smaller take it
                { 
                    a[ansPointer] = left[leftv];
                    leftv=leftv+1;          
                }
                else
                {
                    a[ansPointer] = right[rightv]; //take the right value
                    rightv=rightv+1;
                }
            }
            else if (leftv < left.Length) // only vlaues on the left
            {
                a[ansPointer] = left[leftv];
                leftv = leftv + 1;
            }
            else if(rightv<right.Length) // only values on the right
            {
                a[ansPointer] = right[rightv];
                rightv = rightv + 1;
            }
            ansPointer = ansPointer + 1;
        }
        return a;
    }
}