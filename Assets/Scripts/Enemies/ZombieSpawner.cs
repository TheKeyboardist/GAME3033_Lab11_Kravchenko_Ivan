using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private int numberOfZombiesToSpawn;

    [SerializeField]
    private GameObject[] zombiePrefabs;

    [SerializeField]
    private SpawnVolume[] spawnVolumes;

    private GameObject followObject;

    // Start is called before the first frame update
    void Start()
    {
        followObject = GameObject.FindGameObjectWithTag("Player");

        for(int index = 0; index < numberOfZombiesToSpawn; index++)
        {
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        GameObject zombieToSpawn = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
        SpawnVolume spawner = spawnVolumes[Random.Range(0, spawnVolumes.Length)];

        if (followObject)
        {
            GameObject zombie = Instantiate(zombieToSpawn, spawner.GetPositionInBounds(), spawner.transform.rotation);

            zombie.GetComponent<ZombieComponent>().Initialize(followObject);
        }
    }
}
