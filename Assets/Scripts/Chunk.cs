using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public int numberOfObjects = 10;
    public float spawnHeight = 5f;
    public GameObject chunkPrefab;
    private Vector3 chunkSize;

    void Start()
    {
        CalculateChunkSize();
        SpawnObjects();
    }

    void CalculateChunkSize()
    {
        Renderer renderer = GetComponent<Renderer>();
        chunkSize = renderer.bounds.size;
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-chunkSize.x / 2, chunkSize.x / 2) * 0.9f,
                                                spawnHeight,
                                                Random.Range(-chunkSize.z / 2, chunkSize.z / 2) * 0.9f)
                                     + transform.position;

            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            float randomYRotation = Random.Range(0f, 360f);
            GameObject oldObject = Instantiate(objectToSpawn, spawnPosition, objectToSpawn.transform.rotation, transform);
            oldObject.transform.rotation = Quaternion.Euler(oldObject.transform.rotation.eulerAngles.x, randomYRotation, oldObject.transform.rotation.eulerAngles.z);

        }
    }


    public void SpawnNewChunk()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 829f);
        Instantiate(chunkPrefab, targetPos, Quaternion.identity);
    }
}
