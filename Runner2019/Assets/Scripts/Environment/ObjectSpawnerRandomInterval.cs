using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerRandomInterval : ObjectSpawnerAbstract
{
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;


    protected override void StartSpawning()
    {
        InvokeRepeating("SpawnGO", 0f, Random.Range(minSpawnInterval,maxSpawnInterval));
    }
}
