using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    public LayerMask layer;
    [SerializeField]
    public DNA dna;
    public float distance;

    private bool seeWall;
    private bool alive = true;
    private int dnaLength = 100;
    
    public void Init()
    {
        dna = new DNA(dnaLength, 360);
        distance = 0;
        alive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bounds")
        {
            alive = false;
            Debug.Log("das");
        }
    }
    public void Stop(Vector3 initialPos)
    {
        distance = Vector3.Distance(transform.position, initialPos);
    }

    private void Update() {
        if (!alive)
        {
            Stop(PopulationManager.GetInitialPosition());
            return;
        }
        seeWall = false;
        RaycastHit hit;
        seeWall = (Physics.Raycast(transform.position, transform.forward, out hit, .5f, layer));

    }

    private void FixedUpdate()
    {
        if (!alive)
            return;
        float h = 0;
        float v = dna.GetGene(0);

        if (seeWall)
        {
            h = dna.GetGene(1);
        }

        transform.Translate(0, 0, v * .001f);
        transform.Rotate(0, h, 0);
    }


}
