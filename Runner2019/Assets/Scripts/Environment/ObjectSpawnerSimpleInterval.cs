using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerSimpleInterval : ObjectSpawnerAbstract
{ 
    [SerializeField] private float spawnIntervalInSeconds;



    protected override void StartSpawning()
    {
        InvokeRepeating("SpawnGO", 0f, spawnIntervalInSeconds);
    } 

    
}
