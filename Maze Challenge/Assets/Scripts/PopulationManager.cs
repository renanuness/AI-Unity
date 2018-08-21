using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {

    public Transform InitialPoint;
    public GameObject botPrefab;
    public float trialTime = 10f;

    private int populationSize = 50;
    private List<GameObject> population = new List<GameObject>();
    private int generation = 0;
    private float elapsed = 0;


    GUIStyle guiStyle = new GUIStyle();
    private void OnGUI()
    {
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.red;
        GUI.BeginGroup(new Rect(10,10,250,150));
        GUI.Label(new Rect(10, 10, 200, 30), string.Format("Time: {0:00}", elapsed), guiStyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Generation: {0}", generation), guiStyle);
        GUI.EndGroup();
    }

	private void Start () {
		for(int i = 0; i < populationSize; i++)
        {
            GameObject offSpring =InstantiateBot();
            offSpring.GetComponent<Brain>().Init();
            population.Add(offSpring);
        }
	}
	
	private void Update () {
        elapsed += Time.deltaTime;
        if(elapsed >= trialTime)
        {
            //stop
            Time.timeScale = 0;
            for(int i = 0; i < population.Count; i++)
            {
                population[i].GetComponent<Brain>().Stop(InitialPoint.transform.position);
            }
            BreedNewPopulation();
            elapsed = 0;
            Time.timeScale = 1;
        }
	}

    private void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().distance).ToList();

        population.Clear();
        for(int i = 0; i < sortedList.Count; i++)
        {
            if(i < sortedList.Count/2)
            {
                population.Add(Breed(sortedList[i], sortedList[i - 1], false));
            }
            else
            {
                population.Add(Breed(sortedList[i], sortedList[i - 1], true));

            }
        }

        for(int i = 0; i < sortedList.Count; i++)
        {
            Debug.Log("das");
            Destroy(sortedList[i]);
        }
        generation++;
    }

    private GameObject Breed(GameObject parent1, GameObject parent2, bool shouldMutate)
    {
        GameObject offSpring = InstantiateBot();
        Debug.Log(offSpring);
        Brain brain  = offSpring.GetComponent<Brain>();

        if (shouldMutate)
        {
            brain.Init();
            brain.dna.Mutate();
        }
        else
        {
            brain.Init();
            brain.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
        }
        return offSpring;
    }

    private GameObject InstantiateBot()
    {
        GameObject obj= Instantiate(botPrefab, InitialPoint.position, Quaternion.identity);
        return obj;
    }
}
