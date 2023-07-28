using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class RollingAverage : MonoBehaviour
{
    private Queue<float> window; // Sliding window
    public int windowSize; // Size of the window
    private float sum; // Sum of values in the window
    public List<float> values;
    public List<float> output;
    public List<float> reducedPoints;
    private void Start()
    {
        window = new Queue<float>(windowSize);
        sum = 0;
        GenerateRandomPoints(300);
    }
    private void GenerateRandomPoints(int pointCount)
    {
        for (int i = 0; i < pointCount; i++)
        {
            float randomValue = Random.Range(0, 500);
            values.Add(randomValue);
        }
    }
    public float CalculateRollingAverage(float newValue)
    {
        if (window.Count == windowSize)
        {
            float oldValue = window.Dequeue();
            sum -= oldValue;
        }

        window.Enqueue(newValue);
        sum += newValue;

        return sum / window.Count;
    }
    [Button]
    public void Calculate()
    {
        foreach (float value in values)
        {
            float rollingAverage = CalculateRollingAverage(value);
            output.Add(rollingAverage);
        }
        reducedPoints = ReducePoints(output, 100);
    }

    public List<float> ReducePoints(List<float> originalPoints, int desiredCount)
    {
        int totalPoints = originalPoints.Count;
        // Calculate the number of points to select from each stratum
        float pointProportion = (float)desiredCount / totalPoints;

        // Iterate over each point and select from each stratum
        foreach (float point in originalPoints)
        {
            if (Random.value <= pointProportion)
            {
                reducedPoints.Add(point);
            }
        }
        return reducedPoints;
    }
}