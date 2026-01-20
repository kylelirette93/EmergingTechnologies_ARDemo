using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject bladePrefab;
    private Transform cat;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Spawn Settings")]
    [SerializeField] ObjectPlacer catPlacer;
    bool canStartSpawning = false;
    float timer = 0f;
    float spawnInterval = 3f;
    private float spawnDistance = 3f;
    private float spawnY;

    private void OnEnable()
    {
        catPlacer.OnCatPlaced += StartSpawning;
    }

    private void OnDisable()
    {
        catPlacer.OnCatPlaced -= StartSpawning;
    }

    private void StartSpawning()
    {
        cat = GameObject.FindWithTag("Cat").transform;
        spawnY = cat.position.y;
        canStartSpawning = true;
    }

    public void SetSpawning(bool value)
    {
        canStartSpawning = value;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (canStartSpawning && timer >= spawnInterval)
        {
            SpawnBlade();
            timer = 0f;
        }
    }

    private void SpawnBlade()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * 5f;
        spawnPosition.y = spawnY;
        if (Vector3.Distance(cat.position, spawnPosition) <= spawnDistance) 
        {
            Instantiate(bladePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            spawnPosition = Random.insideUnitSphere * 5f;
        }
    }
}
