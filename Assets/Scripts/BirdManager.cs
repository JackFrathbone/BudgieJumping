using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int _birdLimit;
    [SerializeField] float _spawnRadius = 5f;

    [Header("References")]
    [SerializeField] List<GameObject> _birdPrefabs = new();

    [Header("Data")]
    private List<GameObject> _birdSpawners = new();
    private int _currentBirds = 0;

    private void Start()
    {
        //Get all bird spawners
        foreach (GameObject birdSpawner in GameObject.FindGameObjectsWithTag("BirdSpawner"))
        {
            _birdSpawners.Add(birdSpawner);
        }
    }

    private void Update()
    {
        if (_currentBirds < _birdLimit)
        {
            _currentBirds++;
            Instantiate(GetRandomBirdPrefab(), GetRandomBirdSpawnerPosition(), Quaternion.identity);
        }
    }

    private GameObject GetRandomBirdPrefab()
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue < 10f)
        {
            return _birdPrefabs[Random.Range(0, _birdPrefabs.Count)];
        }
        else
        {
            return _birdPrefabs[Random.Range(0, 3)];
        }


    }

    public Vector3 GetRandomBirdSpawnerPosition()
    {
        Vector3 randomPosition = (_birdSpawners[Random.Range(0, _birdSpawners.Count)].transform.position) + Random.insideUnitSphere * _spawnRadius;
        return randomPosition;
    }

    public void ReportDestroyedBird()
    {
        _currentBirds--;
    }
}
