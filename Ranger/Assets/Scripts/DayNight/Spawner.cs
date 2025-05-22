using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject FishArea;
    public GameObject Ground;

    public GameObject FishPrefab1;
    public GameObject FishPrefab2;
    public GameObject FishPrefab3;

    public GameObject BugPrefab1;
    public GameObject BugPrefab2;
    public GameObject BugPrefab3;

    public GameObject PlantPrefab1;
    public GameObject PlantPrefab2;
    public GameObject PlantPrefab3;

    public GameObject DayNight;
    public TimeManager TimeManager;

    private readonly int _maxFish = 5;
    private readonly int _maxBug = 5;
    private readonly int _maxPlant = 15;

    public Vector2 BoundsGroundX;
    public Vector2 BoundsGroundZ;
    public Vector2 BoundsWaterX;
    public Vector2 BoundsWaterZ;

    void Start()
    {
        DayNight = GameObject.FindGameObjectWithTag("TimeManager");
        TimeManager = DayNight.GetComponent<TimeManager>();

        SpawnPlants(_maxPlant);
        SpawnBugs(_maxBug);
        SpawnFish(_maxFish);
    }

    void Update()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("PlantDropped");
        GameObject[] bugs = GameObject.FindGameObjectsWithTag("bug");
        GameObject[] fish = GameObject.FindGameObjectsWithTag("fish");

        //Debug.Log(bugs.Length);

        if ((String.Compare(TimeManager.service.CurrentTime.ToString("hh:mm"), "06:00") == 0))
        {
            if (plants.Length < _maxPlant)
            {
                SpawnPlants(_maxPlant - plants.Length);
            }

            if (bugs.Length < _maxBug)
            {
                SpawnBugs(_maxBug - bugs.Length);
            }

            if (fish.Length < _maxFish)
            {
                SpawnFish(_maxFish - fish.Length);
            }
        }
    }

    void SpawnPlants(int plantAmount)
    {
        for(int i = 0; i < plantAmount; i++)
        {
            Vector3 destination = new Vector3(Random.Range(BoundsGroundX.x, BoundsGroundX.y), 1,
            Random.Range(BoundsGroundZ.x, BoundsGroundZ.y));

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
            Vector3 destination = new Vector3(Random.Range(BoundsGroundX.x, BoundsGroundX.y),
                                    0.25f,
                                    Random.Range(BoundsGroundZ.x, BoundsGroundZ.y));


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

    void SpawnFish(int fishAmount)
    {
        for (int i = 0; i < fishAmount; i++)
        {
            Vector3 destination = new Vector3(Random.Range(BoundsWaterX.x, BoundsWaterX.y),
                                    0.25f,
                                    Random.Range(BoundsWaterZ.x, BoundsWaterZ.y));


            float number = Random.Range(1, 4);

            if (number == 1)
            {
                Instantiate(FishPrefab1, destination, FishPrefab1.transform.rotation);
            }

            if (number == 2)
            {
                Instantiate(FishPrefab2, destination, FishPrefab2.transform.rotation);
            }

            if (number == 3)
            {
                Instantiate(FishPrefab3, destination, FishPrefab3.transform.rotation);
            }
        }
    }
}