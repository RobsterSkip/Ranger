using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBugPlant : MonoBehaviour
{
    public GameObject CampArea;

    public GameObject BugPrefab1;
    public GameObject BugPrefab2;
    public GameObject BugPrefab3;

    public GameObject PlantPrefab1;
    public GameObject PlantPrefab2;
    public GameObject PlantPrefab3;

    public GameObject DayNight;
    public TimeManager TimeManager;

    private readonly int _maxBug = 30;
    private readonly int _maxPlant = 40;

    private readonly int _maxBugPerSpawner = 5;
    private readonly int _maxPlantPerSpawner = 10;

    public Vector2 Bounds;

    [SerializeField] private TextMeshProUGUI timeText;

    float currentTime;

    void Start()
    {
        DayNight = GameObject.FindGameObjectWithTag("TimeManager");
        TimeManager = DayNight.GetComponent<TimeManager>();

        DayNight = GameObject.FindGameObjectWithTag("NPC");

        SpawnPlants(_maxPlantPerSpawner);
        SpawnBugs(_maxBugPerSpawner);
    }

    void Update()
    {
        currentTime = 12 + Time.deltaTime;
        Debug.Log(currentTime);

        if (currentTime > 12)
        {
            Debug.Log("It is now day");
        }
        else
        {
            Debug.Log("It is now night");
        }

        GameObject[] plants = GameObject.FindGameObjectsWithTag("PlantDropped");
        GameObject[] bugs = GameObject.FindGameObjectsWithTag("bug");

        if ((String.Compare(TimeManager.service.CurrentTime.ToString("hh:mm"), "06:00") == 0))
        {
            if (plants.Length < _maxPlant)
            {
                SpawnPlants((int)((_maxPlant - plants.Length)/_maxPlantPerSpawner));
            }

            if (bugs.Length < _maxBug)
            {
                SpawnBugs((int)((_maxBug - bugs.Length) / _maxBugPerSpawner));
            }
        }
    }

    void SpawnPlants(int plantAmount)
    {
        for(int i = 0; i < plantAmount; i++)
        {
            Vector3 destination = new Vector3(Random.Range(transform.position.x - Bounds.x, transform.position.x + Bounds.x), transform.position.y + 1,
            Random.Range(transform.position.z - Bounds.y, transform.position.z + Bounds.y));

            if (CampArea.GetComponent<Collider>().bounds.Contains(destination))
            {
                continue;
            }

            float number = Random.Range(1, 4);

            if(number == 1)
            {
                Instantiate(PlantPrefab1, destination, PlantPrefab1.transform.rotation);
            }

            if (number == 2)
            {
                Instantiate(PlantPrefab2, destination, PlantPrefab2.transform.rotation);
            }

            if (number == 3)
            {
                Instantiate(PlantPrefab3, destination, PlantPrefab3.transform.rotation);
            }
        }
    }

    void SpawnBugs(int bugsAmount)
    {
        for (int i = 0; i < bugsAmount; i++)
        {
            Vector3 destination = new Vector3(Random.Range(transform.position.x - Bounds.x, transform.position.x + Bounds.x),
                                    transform.position.y + 0.25f,
                                    Random.Range(transform.position.z - Bounds.y, transform.position.z + Bounds.y));


            float number = Random.Range(1, 4);

            if (number == 1)
            {
                Instantiate(BugPrefab1, destination, BugPrefab1.transform.rotation);
            }

            if (number == 2)
            {
                Instantiate(BugPrefab2, destination, BugPrefab2.transform.rotation);
            }

            if (number == 3)
            {
                Instantiate(BugPrefab3, destination, BugPrefab3.transform.rotation);
            }
        }
    }
}