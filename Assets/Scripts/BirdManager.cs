using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int _birdLimit;

    [Header("References")]
    [SerializeField] List<GameObject> _birdPrefabs = new();
    [SerializeField] List<GameObject> _birdSpawners = new();

    [Header("Data")]
    private List<GameObject> _currentBirds = new();

    private void Update()
    {
        if (_currentBirds.Count < _birdLimit)
        {
            Instantiate(GetRandomBirdPrefab(), GetRandomBirdSpawnerPosition(), Quaternion.identity);
        }
    }

    private GameObject GetRandomBirdPrefab()
    {
        return _birdPrefabs[Random.Range(0, _birdPrefabs.Count)];
    }

    private Vector3 GetRandomBirdSpawnerPosition()
    {
        return _birdSpawners[Random.Range(0, _birdSpawners.Count)].transform.position;
    }
}
