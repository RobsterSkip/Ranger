using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject CampArea;

    public GameObject FishPrefab1;
    public GameObject FishPrefab2;
    public GameObject FishPrefab3;
    public GameObject FishPrefab4;
    public GameObject FishPrefab5;
    public GameObject FishPrefab6;

    public GameObject DayNight;
    public TimeManager TimeManager;

    private readonly int _maxFish = 35;
    private readonly int _maxFishPerSpawner = 5;

    public Vector2 Bounds;

    void Start()
    {
        DayNight = GameObject.FindGameObjectWithTag("TimeManager");
        TimeManager = DayNight.GetComponent<TimeManager>();

        DayNight = GameObject.FindGameObjectWithTag("NPC");

        SpawnFish(_maxFishPerSpawner);
    }

    void Update()
    {
        GameObject[] fish = GameObject.FindGameObjectsWithTag("fish");

        if ((String.Compare(TimeManager.service.CurrentTime.ToString("hh:mm"), "06:00") == 0))
        {
            if (fish.Length < _maxFish)
            {
                SpawnFish((int)((_maxFish - fish.Length)/ _maxFishPerSpawner));
            }
        }
    }

    void SpawnFish(int fishAmount)
    {
        for (int i = 0; i < fishAmount; i++)
        {
            Vector3 destination = new Vector3(Random.Range(transform.position.x - Bounds.x, transform.position.x + Bounds.x),
                                    transform.position.y + 0.25f,
                                    Random.Range(transform.position.z - Bounds.y, transform.position.z + Bounds.y));


            float number = Random.Range(1, 7);

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

            if (number == 4)
            {
                Instantiate(FishPrefab4, destination, FishPrefab3.transform.rotation);
            }

            if (number == 5)
            {
                Instantiate(FishPrefab5, destination, FishPrefab3.transform.rotation);
            }

            if (number == 6)
            {
                Instantiate(FishPrefab6, destination, FishPrefab3.transform.rotation);
            }
        }
    }
}