using System.Data;
using System.Runtime.Versioning;

class Vector{
    private int[] components;
    public Vector(int dimensions){
        components = new int[dimensions];
    }
    public Vector(int[] components){
        this.components = components;
    }
    public int GetDimension(){
        return components.Length;
    }
    public double GetMagnitude(){
        int sumOfSquares = 0;
        foreach(int component in components){
            sumOfSquares += component * component;
        }
        return Math.Sqrt(sumOfSquares);
    }
    // for Add(), i implemented it so that you the vectors don't have to have the same dimensions
    // e.g. you can add 2d vectors to 3d vectors, and the 3d part is just added to the end
    public static Vector Add(Vector vectorOne, Vector vectorTwo){
        Vector longVector = vectorOne;
        if(vectorOne.GetDimension() < vectorTwo.GetDimension()){
            longVector = vectorTwo;
        }
        int longDimension = longVector.GetDimension();
        Vector sum = new Vector(longDimension);
        for(int i = 0; i < longDimension; i++){
            try{
                sum.components[i] = vectorOne.components[i] + vectorTwo.components[i];
            }
            catch(IndexOutOfRangeException){
                sum.components[i] = longVector.components[i];
            }
        }
        return sum;
    }
    public static double DotProduct(Vector vectorOne, Vector vectorTwo){
        if(vectorOne.GetDimension() != vectorTwo.GetDimension()){
            throw new Exception("Those vectors aren't the same dimension");
        }
        double dotproduct = 0;
        for(int i = 0; i < vectorOne.GetDimension(); i++){
            dotproduct += vectorOne.components[i] * vectorTwo.components[i];
        }
        return dotproduct;
    }
}