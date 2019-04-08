using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableAbstract : MonoBehaviour
{
    protected ObjectSpawnerAbstract objectSpawner;    



    public void SetObjectSpawner(ObjectSpawnerAbstract objectSpawnerParam)
    {
        objectSpawner = objectSpawnerParam;
        Debug.Log(objectSpawner);
    }    
}
