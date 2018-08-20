﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA {

    public List<int> genes = new List<int>();
    public int maxLenght;
    public int maxValues;

    public DNA(int l, int v)
    {
        maxLenght = l;
        maxValues = v;
    }

    public void SetRandom()
    {
        genes.Clear();
        for(int i = 0; i < maxLenght; i++)
        {
            genes[i] = Random.Range(0, maxValues);
        }
    }

    public void SetInt(int pos, int val)
    {
        genes[pos] = val;
    }

    public void Combine(DNA parent1, DNA parent2)
    {
        for(int i = 0; i < maxLenght; i++)
        {
            if (i % 2 == 0)
            {
                genes[i] = parent1.genes[i];
            }
            else
            {
                genes[i] = parent2.genes[i];
            }
        }
    }

    public void Mutate()
    {
        genes[Random.Range(0, maxLenght)] = Random.Range(0, maxValues);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }
}
