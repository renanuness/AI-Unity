using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    public float TrialTime;
    public int populationSize;
    public GameObject BotPrefab;

    private int generation = 0;
    public List<GameObject> population = new List<GameObject>();
    private float elapsed;


    GUIStyle guiStyle = new GUIStyle();
    private void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
        GUI.Label(new Rect(10, 25, 200, 30), "Gen: " + generation, guiStyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:00}", elapsed), guiStyle);
        GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, guiStyle);
        GUI.EndGroup();
    }

    private void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 startingPos = new Vector3(transform.position.x + Random.Range(-3, 3),
                                  transform.position.y,
                                  transform.position.z + Random.Range(-3, 3));
            var go = Instantiate(BotPrefab, transform.position, Quaternion.identity);
            go.GetComponent<Brain>().Init();
            population.Add(go);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > TrialTime)
        {
            elapsed = 0;
            //Chama o evento
            OnClicked();
            BreedNewPopulation();
        }
    }

    private void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o =>  o.GetComponent<Brain>().DistanceTraveled * 4).ToList();

        population.Clear();
        for (int i = (int)(6*sortedList.Count - 7)/7; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i + 1]));
            population.Add(Breed(sortedList[i], sortedList[i -1]));
            population.Add(Breed(sortedList[i +  1], sortedList[i - 1]));
            population.Add(Breed(sortedList[i - 1], sortedList[i + 1]));
            population.Add(Breed(sortedList[i - 1], sortedList[i ]));

        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }

        generation++;
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        var go = Instantiate(BotPrefab, transform.position, Quaternion.identity);
        Brain brain = go.GetComponent<Brain>();

        if (Random.Range(0,100) == 1)
        {
            brain.Init();
            brain.dna.Mutate();
        }
        else
        {
            brain.Init();
            brain.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
        }
        return go;
    }
}
