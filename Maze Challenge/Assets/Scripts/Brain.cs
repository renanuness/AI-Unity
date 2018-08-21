using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    private DNA dna;
    private bool alive = true;
    private int dnaLength = 2;
    private float timeAlive;
    	
    public void Init()
    {
        dna = new DNA(2, 2);
        timeAlive = 0;
        alive = true;
    }

	void Update () {


        if(!alive)
        {
            return;
        }
        float v = 0;
        float h = 0;

        if(dna.GetGene(0) == 1)
        {
            v = 1;
        }

        if (dna.GetGene(1) == 1)
        {
            h = 1;
        }

        transform.Translate(0, 0, v * 0.1f);
        transform.Rotate(0, h, 0);
    }
}
