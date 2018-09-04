using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    public int DnaLength = 30;
    public DNA dna;
    public GameObject LeftArm;
    public GameObject RightArm;
    public GameObject Head;
    public float TimeInFloor;
    public float DistanceTraveled;
    public float TimeToChange = 1;
    public float TimeAlive = 0;
    private float timeInState = 0;
    private Vector3 startPosition;
    private int currentGene = 0;
    private bool alive;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Dead")
        {
            alive = false;
        }
    }

    private void OnEnable()
    {
        PopulationManager.OnClicked += StopDistance;
    }

    private void OnDisable()
    {
        PopulationManager.OnClicked -= StopDistance;
    }

    public void Init()
    {
        //
        dna = new DNA(1, DnaLength);
        DistanceTraveled = 0;
        transform.Translate(Random.Range(-3,3), 0, Random.Range(-3,3));
        startPosition = transform.position;
        alive = true;
    }

	private void Update ()
    {
        if (!alive)
        {
            StopDistance();
            return;
        }
        TimeAlive += Time.deltaTime;

        var rotationX = GeneToAngle(dna.GetGene(currentGene));
        var rotationY = GeneToAngle(dna.GetGene(currentGene + (int)(DnaLength*1)/3));
        var rotationZ = GeneToAngle(dna.GetGene(currentGene + (int)(DnaLength * 1) / 3));
        LeftArm.transform.Rotate(rotationX, rotationY, 0);
        RightArm.transform.Rotate(rotationX, rotationY, 0);
        timeInState += Time.deltaTime;
        if (timeInState > TimeToChange)
        {
            currentGene++;
            if(currentGene >= DnaLength/3)
            {
                currentGene = 0;
            }
        }
	}
    
    public void StopDistance()
    {
        DistanceTraveled = transform.position.x - startPosition.x;
    }

    private float GeneToAngle(float gene)
    {
        var angle = 90 * gene; 
        return angle;
    }
}
