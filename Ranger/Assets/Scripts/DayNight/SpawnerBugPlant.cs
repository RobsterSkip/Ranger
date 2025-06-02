using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBugPlant : MonoBehaviour
{
    public GameObject CampArea;

    public GameObject[] DayBugPrefabs;
    public GameObject[] NightBugPrefabs;
   
    public GameObject PlantPrefab1;
    public GameObject PlantPrefab2;
    public GameObject PlantPrefab3;
    public GameObject PlantPrefab4;
    public GameObject PlantPrefab5;
    public GameObject PlantPrefab6;

    public GameObject DayNight;
    public TimeManager TimeManager;

    private readonly int _maxBug = 30;
    private readonly int _maxPlant = 40;

    private readonly int _maxBugPerSpawner = 5;
    private readonly int _maxPlantPerSpawner = 10;

    public Vector2 Bounds;

    float currentTime;

    IEnumerator WaitForTimeService()
    {
        while (TimeManager.service == null || TimeManager.service.isDayTime == null)
        {
            yield return null;
        }

        TimeManager.service.isDayTime.ValueChanged += OnDayNightChanged;

        SpawnPlants(_maxPlantPerSpawner);
        SpawnBugs(DayBugPrefabs, _maxBugPerSpawner, "BugDay");
    }

    void Start()
    {
        DayNight = GameObject.FindGameObjectWithTag("TimeManager");
        TimeManager = DayNight.GetComponent<TimeManager>();

        StartCoroutine(WaitForTimeService());
    }

    void OnDayNightChanged(bool isDay)
    {
        if (isDay)
        {
            SpawnDayContent();
            DestroyBugsWithTag("BugNight");
            BooleanManager.IsDay = true;
            BooleanManager.IsNight = false;
        }
        else
        {
            SpawnNightContent();
            DestroyBugsWithTag("BugDay");
            BooleanManager.IsDay = false;
            BooleanManager.IsNight = true;
        }
    }

    void SpawnDayContent()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("PlantDropped");
        GameObject[] bugs = GameObject.FindGameObjectsWithTag("bug");

        int plantShortage = _maxPlant - plants.Length;
        int bugShortage = _maxBug - bugs.Length;


        if (plantShortage > 0)
        {
            SpawnPlants(Mathf.Min(_maxPlantPerSpawner, plantShortage));
        }
        Debug.Log($"[Spawner] Current plant count: {plants.Length}, max: {_maxPlant}");

        if (bugShortage > 0)
        {
            SpawnBugs(DayBugPrefabs, Mathf.Min(bugShortage, _maxBugPerSpawner), "BugDay");
        }
    }

    void SpawnNightContent()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("PlantDropped");
        GameObject[] bugs = GameObject.FindGameObjectsWithTag("bug");

        int plantShortage = _maxPlant - plants.Length;
        int bugShortage = _maxBug - bugs.Length;

        if (plantShortage > 0)
        {
            SpawnPlants(Mathf.Min(_maxPlantPerSpawner, plantShortage));
        }

        if (bugShortage > 0)
        {
           SpawnBugs(NightBugPrefabs, Mathf.Min(bugShortage, _maxBugPerSpawner), "BugNight");
        }
    }

    void OnDestroy()
    {
        if (TimeManager != null)
        {
            TimeManager.service.isDayTime.ValueChanged -= OnDayNightChanged;
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

            float number = Random.Range(1, 7);

            if (number == 1)
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

            if (number == 4)
            {
                Instantiate(PlantPrefab4, destination, PlantPrefab3.transform.rotation);
            }

            if (number == 5)
            {
                Instantiate(PlantPrefab5, destination, PlantPrefab3.transform.rotation);
            }

            if (number == 6)
            {
                Instantiate(PlantPrefab6, destination, PlantPrefab3.transform.rotation);
            }
        }
    }

    void SpawnBugs(GameObject[] bugPrefabs, int bugsAmount, string bugTag)
    {
        for (int i = 0; i < bugsAmount; i++)
        {
            Vector3 destination = new Vector3(
                Random.Range(transform.position.x - Bounds.x, transform.position.x + Bounds.x),
                transform.position.y + 0.25f,
                Random.Range(transform.position.z - Bounds.y, transform.position.z + Bounds.y)
            );

            int index = Random.Range(0, bugPrefabs.Length);
            GameObject bug = Instantiate(bugPrefabs[index], destination, bugPrefabs[index].transform.rotation);
            bug.tag = bugTag; 
        }
    }

    void DestroyBugsWithTag(string tag)
    {
        GameObject[] bugs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var bug in bugs)
        {
            Destroy(bug);
        }
    }

}