using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {

    public GameObject personPrefab;
    public int populationSize = 10;
    public List<GameObject> population = new List<GameObject>();
    public static float TimeElapsed = 0;

    protected int _trialTime = 10;
    protected int _generation = 1;


    GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + _generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "TrialTime: " + TimeElapsed, guiStyle);
    }

	void Start () {
		for(int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 4f), 0);
            GameObject ob = Instantiate(personPrefab, pos, Quaternion.identity);


            ob.GetComponent<DNA>().R = Random.Range(0f, 1f);
            ob.GetComponent<DNA>().G = Random.Range(0f, 1f);
            ob.GetComponent<DNA>().B = Random.Range(0f, 1f);
            ob.GetComponent<DNA>().S = Random.Range(.2f, .8f);
            population.Add(ob);
        }
    }
	
	private void Update () {
        TimeElapsed += Time.deltaTime;
        if(TimeElapsed > _trialTime)
        {
            BreedNewPopulation();
            TimeElapsed = 0;
        }
	}

    private void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();

        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().TimeToDie).ToList();

        population.Clear();

        for(int i = (int) (sortedList.Count/2)-1; i < sortedList.Count - 1 ; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for(int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        _generation++;
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 4f), 0);
        GameObject offSpring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        if(Random.Range(0,40) > 38)
        {
            offSpring.GetComponent<DNA>().R = Random.Range(0f, 1f);
            offSpring.GetComponent<DNA>().G = Random.Range(0f, 1f);
            offSpring.GetComponent<DNA>().B = Random.Range(0f, 1f);
            offSpring.GetComponent<DNA>().S = Random.Range(.2f, .8f);
        }
        else
        {
            offSpring.GetComponent<DNA>().R = Random.Range(0, 10) > 5 ? dna1.R : dna2.R;
            offSpring.GetComponent<DNA>().G = Random.Range(0, 10) > 5 ? dna1.G : dna2.G;
            offSpring.GetComponent<DNA>().B = Random.Range(0, 10) > 5 ? dna1.B : dna2.B;
            offSpring.GetComponent<DNA>().S = Random.Range(0, 10) > 5 ? dna1.S : dna2.S;
        }

        return offSpring;
    }
}
