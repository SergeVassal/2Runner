using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnable : SpawnableAbstract
{   
    private void OnDisable()
    {
        if (objectSpawner != null)
        {
            objectSpawner.DisableSpawnedObject(gameObject);
        }
    }

}
