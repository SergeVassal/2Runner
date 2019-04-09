using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawnable : SpawnableAbstract
{   
    private void OnDisable()
    {
        if (objectSpawner != null)
        {
            objectSpawner.DisableSpawnedObject(gameObject);
        }
    }

}
