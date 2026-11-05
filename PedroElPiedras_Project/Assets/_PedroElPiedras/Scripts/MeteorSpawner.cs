using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [Header("MeteorSettings")]
    public GameObject meteorPrefab;
    public LayerMask groundLayer;

    public float spawnInterval = 3f;
    public float spawnRadius = 30f;
    public float spawnHeight = 25f;
    public float meteorHeight = 20f;

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
        for (int i = 0; i < 10; i++)
        {
            float randomX = Random.Range(-spawnRadius, spawnRadius);
            float randomZ = Random.Range(-spawnRadius, spawnRadius);

            Vector3 rayOrigin = transform.position + new Vector3(randomX, spawnHeight, randomZ);

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, spawnHeight * 2f, groundLayer))
            {
                Vector3 spawnPosition = hit.point + Vector3.up * meteorHeight;

                Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

                return;
            }
        }
    }
}
