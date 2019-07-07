using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class TrainingSet
{
    public double[] input;
    public double output;
}

public class Perceptron : MonoBehaviour
{
    public TrainingSet[] ts;
    double[] weights = { 0, 0 };
    double bias = 0;
    double totalError = 0;

    public SimpleGrapher sg;

    private double DotProductBias(double[] weights, double[] inputs)
    {
        if (weights == null || inputs == null)
            return -1;

        if (weights.Length != inputs.Length)
            return -1;

        double d = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            d += weights[i] * inputs[i];
        }

        d += bias;

        return d;
    }

    private double CalcOutput(int i)
    {
        double dp = DotProductBias(weights, ts[i].input);

        if (dp > 0) return 1;
        return 0;
    }

    private void InitializeWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1f, 1f);
        }

        bias = Random.Range(-1f, 1f);
    }

    double CalcOutput(double i1, double i2)
    {
        double[] inp = new double[] { i1, i2 };
        double dp = DotProductBias(weights, inp);
        if (dp > 0) return 1;
        return 0;
    }

    private void Train(int epochs)
    {
        InitializeWeights();
        for (int i = 0; i < epochs; i++)
        {
            totalError = 0;
            for (int t = 0; t < ts.Length; t++)
            {
                UpdateWeight(t);
                Debug.Log("W1: " + weights[0] + "W2: " + weights[1] + "B: " + bias);
            }
            Debug.Log("TOTAL ERROR:" + totalError);
        }
    }

    private void Start()
    {
        DrawAllPoints();
        Train(200);
        sg.DrawRay((float)(-(bias / weights[1]) / (bias / weights[0])), (float)(-bias / weights[1]), Color.red);
        
        if(CalcOutput(0.3, 0.9) == 0)
        {
            sg.DrawPoint(0.3f, 0.9f, Color.red);
        }
        else
        {
            sg.DrawPoint(0.3f, 0.9f, Color.yellow);
        }
    }

    private void DrawAllPoints()
    {
        for (int t = 0; t < ts.Length; t++)
        {
            if(ts[t].output == 0)
            {
                sg.DrawPoint((float)ts[t].input[0], (float)ts[t].input[1], Color.magenta);
            }
            else
            {
                sg.DrawPoint((float)ts[t].input[0], (float)ts[t].input[1], Color.green);
            }
        }
    }

    private void UpdateWeight(int j)
    {
        double error = ts[j].output - CalcOutput(j);
        totalError += Mathf.Abs((float)error);

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] + error * ts[j].input[i];
        }
        bias += error;
    }
}
