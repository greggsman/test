static int[] bubbleSort(int[] a)
{
    int compariomsC = 0;
    for (int j = 0; j < a.Length; j++)
    {
        for (int i = 1; i < a.Length ; i++)
        //each time we reduce the number of comparisons by one to avoid comparing against values we know will already be sorted
        {
            if (a[i - 1] > a[i])
            {
                int temp = a[i - 1];
                a[i - 1] = a[i];
                a[i] = temp;
                
            }
            compariomsC++;
        }
        
    }
    Console.WriteLine(compariomsC);
    return a;
}

static int[] bestBubbleSort(int[] a)
{
    int counter = 0;
    bool swaps = true;
    while(swaps) //repeat until we haven't made any swaps (the list is sorted)
    {
        swaps = false;
        for (int i = 1; i < a.Length-counter; i++)  
        //each time we reduce the number of comparisons by one to avoid comparing against values we know will already be sorted
        {
            if (a[i - 1] > a[i])
            {
                int temp = a[i - 1];
                a[i - 1] = a[i];
                a[i] = temp;
                swaps = true;// if we swaped we make this true
            }
        }
        counter++;
    }
    return a;
}

static int[] mergeSort(int[] a)
{
    //base case
    if( a.Length<=1)
    { return a; }

    //recursive case
    //split it into two halves
    int midpoint = a.Length / 2;
    int[]left= new int[midpoint];
    int[]right= new int[a.Length-midpoint];

    for (int i = 0; i < midpoint; i++) // we take the vaules up to them midpoint into the left  half
    {
        left[i] = a[i];
    }
    for(int i=midpoint; i < a.Length; i++) // we want values from the midpoint into the right half
    {
        right[i - midpoint] = a[i];
    }
    // merge sort each half
    left=mergeSort(left);
    right=mergeSort(right);

    //recombine

    int leftv = 0;
    int rightv= 0;
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
