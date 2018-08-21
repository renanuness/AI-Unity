using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour {

    public Transform InitialPoint;
    public GameObject botPrefab;
    private int populationSize = 3;
    private List<GameObject> population = new List<GameObject>();

	private void Start () {
		for(int i = 0; i < populationSize; i++)
        {
            GameObject offSpring = Instantiate(botPrefab, InitialPoint.position, Quaternion.identity);
            offSpring.GetComponent<Brain>().Init();
            population.Add(offSpring);
        }
	}
	
	private void Update () {
		
	}

    private void BreedNewPopulation()
    {

    }
}
