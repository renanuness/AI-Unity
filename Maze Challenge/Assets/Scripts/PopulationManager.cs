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
    public static Vector3 GetInitialPosition()
    {
        Vector3 pos = new Vector3(361.01f,34.8f,84.1f);
        return pos;
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
            for(int i = 0; i < population.Count; i++)
            {
                population[i].GetComponent<Brain>().Stop(InitialPoint.transform.position);
            }
            BreedNewPopulation();
            elapsed = 0;
        }
	}

    private void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().distance).ToList();
        int j = 1;
        population.Clear();
        for(int i = (int) (sortedList.Count / 2f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for(int i = 0; i < sortedList.Count; i++)
        {

            Destroy(sortedList[i]);
        }
        generation++;
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        GameObject offSpring = InstantiateBot();
        Brain brain  = offSpring.GetComponent<Brain>();

        if (Random.Range(0,100) == 10)
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
