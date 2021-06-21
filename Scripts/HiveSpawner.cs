using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveSpawner : MonoBehaviour
{
    public List<GameObject> EnemiesPrefab;
    public List<GameObject> SpawnPoints;
    public bool IsLaserSpawnAllowed;
    public bool IsWolfSpawnAllowed;
    public bool IsBearSpawnAllowed;
    [Range(1, 3)] public int EnemiesSpawnedPerCharge = 1;
    public float SpawnTime;
    private float CurrentTime;

    public bool IsActivated;
    public RoomEvents RoomEvents;

    // Start is called before the first frame update
    void Start()
    {
        RoomEvents.RoomEnter += ActivateSpawner;
        RoomEvents.RoomExit += DeactivateSpwaner;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsActivated) return;
        CurrentTime += Time.deltaTime;
        if (CurrentTime > SpawnTime)
        {
            SpawnEnemy();
            CurrentTime = 0f;
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < EnemiesSpawnedPerCharge; i++)
        {
            Transform Spot = SpawnPoints[EnemiesSpawnedPerCharge - 1].transform;
            int Random = UnityEngine.Random.Range(0, EnemiesPrefab.Count);
            Instantiate(EnemiesPrefab[Random], Spot.position, Quaternion.identity);
        }
    }

    public void ActivateSpawner() => IsActivated = true;
    public void DeactivateSpwaner()
    {
        IsActivated = false;
        RoomEvents.RoomEnter -= ActivateSpawner;
        this.enabled = false;
    } 
}