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
    public List<GameObject> population = new List<GameObject>();

    private int generation = 0;
    private float elapsed;
    private float average = 0;

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
        GUI.Label(new Rect(10, 100, 200, 30), "Avarege: " + average, guiStyle);
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

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > TrialTime)
        {
            elapsed = 0;
            OnClicked();
            BreedNewPopulation();
        }
    }

    private void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o =>  -o.GetComponent<Brain>().Detour * 5 + o.GetComponent<Brain>().TimeAlive * 5 + o.GetComponent<Brain>().DistanceTraveled * 10).ToList();
        CalculateAverage(sortedList);


        int top = sortedList.Count - 1;
        population.Clear();

        for (int i = (int)(6 * sortedList.Count - 1) / 7; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i + 1]));
            population.Add(Breed(sortedList[i], sortedList[i - 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i - 1]));
            population.Add(Breed(sortedList[i - 1], sortedList[i + 1]));
            population.Add(Breed(sortedList[i - 1], sortedList[i]));
            population.Add(Breed(sortedList[top], sortedList[top]));
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
            brain.dnaX.Mutate();
            brain.dnaY.Mutate();
            brain.dnaZ.Mutate();
        }
        else
        {
            brain.Init();
            brain.dnaX.Combine(parent1.GetComponent<Brain>().dnaX, parent2.GetComponent<Brain>().dnaX);
            brain.dnaY.Combine(parent1.GetComponent<Brain>().dnaY, parent2.GetComponent<Brain>().dnaY);
            brain.dnaZ.Combine(parent1.GetComponent<Brain>().dnaZ, parent2.GetComponent<Brain>().dnaZ);

        }
        brain.RightArm.transform.rotation = new Quaternion(brain.dnaX.GetGene(0), brain.dnaY.GetGene(0), brain.dnaZ.GetGene(0),0);
        brain.LeftArm.transform.rotation = new Quaternion(brain.dnaX.GetGene(0), brain.dnaY.GetGene(0), brain.dnaZ.GetGene(0), 0);

        return go;
    }

    private void CalculateAverage(List<GameObject> list)
    {
        var distance = 0f;

        foreach(var go in list)
        {
            distance += go.GetComponent<Brain>().DistanceTraveled;
        }

        average = distance / list.Count;
    }
}
