using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    public int DnaLength = 30;
    public DNA dnaX;
    public DNA dnaY;
    public DNA dnaZ;
    public GameObject LeftArm;
    public GameObject RightArm;
    public GameObject Head;
    public float Detour = 0;
    public float TimeInFloor;
    public float DistanceTraveled;
    public float TimeToChange = 1;
    public float TimeAlive = 0;
    private float timeInState = 0;
    private Vector3 startPosition;
    private int currentGene = 0;
    private bool alive;
    private bool isLeft = true;


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
        dnaX = new DNA(1, DnaLength);
        dnaY = new DNA(1, DnaLength);
        dnaZ = new DNA(1, DnaLength);

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

        var rotationX = GeneToAngle(dnaX.GetGene(currentGene));
        var rotationY = GeneToAngle(dnaY.GetGene(currentGene));
        var rotationZ = GeneToAngle(dnaZ.GetGene(currentGene));

        if (isLeft)
        {
            //LeftArm.transform.Rotate(rotationX, rotationY, rotationZ);

            LeftArm.transform.rotation = Quaternion.Lerp(LeftArm.transform.rotation, new Quaternion(rotationX, rotationY, rotationZ, 0), TimeToChange * .9f);
        }
        else
        {
            //RightArm.transform.Rotate(rotationX, rotationY, rotationZ);
            RightArm.transform.rotation = Quaternion.Lerp(RightArm.transform.rotation, new Quaternion(rotationX, rotationY, rotationZ, 0), TimeToChange * .9f);

        }

        timeInState += Time.deltaTime;
        if (timeInState > TimeToChange)
        {
            currentGene++;
            isLeft = !isLeft;
            if(currentGene >= DnaLength/3)
            {
                currentGene = 0;
            }
        }
	}
    
    public void StopDistance()
    {
        DistanceTraveled = transform.position.x - startPosition.x;
        Detour = transform.position.y - startPosition.y;
    }

    private float GeneToAngle(float gene)
    {
        var angle = 45 * gene; 
        return angle;
    }
}
