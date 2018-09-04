using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public float maxValue;
    public int length;

    private List<float> genes = new List<float>();

    public DNA(float v, int l)
    {
        maxValue = v;
        length = l;
        SetRandom();
    }

    private void SetRandom()
    {
        genes.Clear();
        for( int i = 0; i < length; i++)
        {
            genes.Add(Random.Range(-maxValue, maxValue));
        }
    }

    public void Combine(DNA d1, DNA d2)
    {
        for (int i = 0; i < length; i++)
        {
            if(i < length / 2)
            {
                genes.Add(d1.GetGene(i));
            }
            else
            {
                genes.Add(d2.GetGene(i));
            }
        }
    }

    public void Mutate()
    {
        genes[Random.Range(0, length)] = Random.Range(-maxValue, maxValue);
    }

    public float GetGene(int pos)
    {
        return genes[pos];
    }
}
