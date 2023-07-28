using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class MovingAverageExample : MonoBehaviour
{
    public int k;
    public List<float> input;
    public List<float> output;

    private int[] values;
    private int index = 0;
    private int sum = 0;

    private void Start()
    {
        input = new List<float>() {39,42,50,60,71,79,85,81,76 };
        if (k <= 0)
        {
            Debug.LogError("k must be greater than 0; usually 3");
            return;
        }
        CalculateMovingAverage();
    }
    [Button]
    public void CalculateMovingAverage()
    {
        values = new int[k];

        foreach (int value in input)
        {
            float sma = UpdateScore(value);
            output.Add(sma);
        }
    }

    private float UpdateScore(int nextInput)
    {
        // calculate the new sum
        sum = sum - values[index] + nextInput;

        // overwrite the old value with the new one
        values[index] = nextInput;

        // increment the index (wrapping back to 0)
        index = (index + 1) % k;

        // calculate the average
        return ((float)sum) / k;
    }
}
