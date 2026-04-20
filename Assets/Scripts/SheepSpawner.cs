using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SheepSpawner : MonoBehaviour
{
   

    private float timer = 0f;
    public bool canSpawn = true;
    public GameObject sheepPrefab;
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float spawnRateMin = 1.5f;
    public float spawnRateMax = 2.5f;
    public float speedIncreaseInterval = 10f;
    private List<GameObject> sheepList = new List<GameObject>();


    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= speedIncreaseInterval)
        {
            timer = 0f;
            spawnRateMin = Mathf.Max(0.4f, spawnRateMin - 0.2f);
            spawnRateMax = Mathf.Max(0.8f, spawnRateMax - 0.2f);
        }
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position;

        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);

        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));
        }
    }
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }
        sheepList.Clear();
    }
}
