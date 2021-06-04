using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateRandomNumber : MonoBehaviour
{
    private readonly System.Random randomNum = new System.Random();
    [SerializeField] int minVal;
    [SerializeField] int maxVal;
    public int theChoosenNum;

    // Start is called before the first frame update
    void Start()
    {
        theChoosenNum = RandomNumber(minVal, maxVal);
    }

    // Generates a random number within a range.      
    public int RandomNumber(int min, int max)
    {
        return randomNum.Next(min, max); //Note that this will yield random numbers in the range min..max-1 inclusive 
    }

}
