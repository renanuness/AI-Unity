﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    public LayerMask layer;
    [SerializeField]
    public DNA dna;
    public float distance;
    private bool alive = true;
    private int dnaLength = 100;
    private int decision = 0;
    private float timeToTurn = .5f;
    private float timeWalking = 0f;


    public void Init()
    {
        dna = new DNA(dnaLength, 3);
        distance = 0;
        decision = 0;
        alive = true;
        timeWalking = 0;
    }

    public void Stop(Vector3 initialPos)
    {
        distance = Vector3.Distance(transform.position, initialPos);
    }

    private void Update() {
        float v = 1;
        float h = 0;
        timeWalking += Time.deltaTime;
        if (decision > 99)
        {
            decision = 0;
        }

        if (decision == 0)
        {
            if(dna.GetGene(decision) == 1)
            {
                h = 1;
            }
            else
            {
                h = -1;
            }
            transform.Rotate(0, h * 90, 0);
            decision++;
            return;
        }
        if (timeWalking > timeToTurn)
        {
            timeWalking = 0;
            if(dna.GetGene(decision) == 1)
            {
                h = 1;
            }
            else if(dna.GetGene(decision) == 0)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            decision++;
        }
        else
        {
            h = 0;
        }

        if(h != 0)
        {
            transform.Rotate(0, h * 90, 0);
        }
        else
        {
            transform.Translate(0, 0, v * 0.1f);
        }
    }

    private bool FindObstacle()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, .5f, layer, QueryTriggerInteraction.Collide))
        {
            return true;
        }
        return false;
    }

}
