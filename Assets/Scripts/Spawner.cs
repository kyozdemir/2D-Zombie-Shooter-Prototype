using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    List<Transform> spawnAreas;
    [SerializeField]
    List<GameObject> zombies;
    [SerializeField]
    GameObject EnemyPool;
    float spawnTime = 5f;
    [SerializeField]
    float dropRate = 30f;
    float lastSpawnChange = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnAreas = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnAreas.Add(transform.GetChild(i));
        }
        StartCoroutine(spawnZombies());
        
        
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseSpawnTime();
    }
    IEnumerator spawnZombies()
    {
        for (; ; )
        {
            Transform spawnPoint = spawnAreas[Random.Range(0, spawnAreas.Count)];
            GameObject zombie = zombies[Random.Range(0, zombies.Count)];
            GameObject tempZombie = Instantiate(zombie, spawnPoint.position,Quaternion.identity, EnemyPool.transform);
            yield return new WaitForSeconds(spawnTime);
        }
    }
    void DecreaseSpawnTime()
    {
        if ((Time.time - lastSpawnChange) > dropRate)
        {
            lastSpawnChange = Time.time;
            print("Düşüyor");
            spawnTime = spawnTime - (spawnTime * 0.25f);
        }
    }
}
