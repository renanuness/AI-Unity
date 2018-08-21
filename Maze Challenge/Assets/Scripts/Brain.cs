using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    public LayerMask layer;
    public DNA dna;
    public float distance;

    private bool alive = true;
    private int dnaLength = 2;
    	
    public void Init()
    {
        dna = new DNA(dnaLength, 2);
        distance = 0;
        alive = true;
    }

    public void Stop(Vector3 initialPos)
    {
        distance = Vector3.Distance(transform.position, initialPos);
    }

	private void Update () {
        if(!alive)
        {
            return;
        }
        float v = 1;
        float h = 0;

        if (FindObstacle())
        {
            if (dna.GetGene(1) == 1)
            {
                h = 1;
            }
            else
            {
                h = -1;
            }
        }

        transform.Translate(0, 0, v * 0.1f);
        transform.Rotate(0, h, 0);
    }

    private bool FindObstacle()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 2f, layer, QueryTriggerInteraction.Collide))
        {
            Debug.Log(hit.collider);
            return true;
        }
        return false;
    }

}
