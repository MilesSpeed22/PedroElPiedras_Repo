using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [Header("MeteorSettings")]
    public GameObject meteorPrefab;

    public float spawnInterval = 3f;
    public float spawnRadius = 30f;
    public float spawnHeight = 25f;

    void Start()
    {
        StartCoroutine(SpawnMeteors());
    }

    IEnumerator SpawnMeteors()
    {
        while (true)
        {
            SpawnMeteor();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnMeteor()
    {
        Vector3 randomPosition = transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), spawnHeight, Random.Range(-spawnRadius, spawnRadius));
        Instantiate(meteorPrefab, randomPosition, Quaternion.identity);
    }
}
