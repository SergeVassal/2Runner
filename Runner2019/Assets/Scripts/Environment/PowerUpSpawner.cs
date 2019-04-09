using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private ObjectSpawnerRandomInterval objectSpawner;
    [SerializeField] private float checkForGroundDistance;


    private void Start()
    {
        objectSpawner.OnSpawn += ObjectSpawner_OnSpawn;
    }

    private void ObjectSpawner_OnSpawn()
    {
        bool isGroundBelow = CheckIfGroundBelow();
        if (isGroundBelow)
        {
            objectSpawner.SetSpawnAllowed(true);            
        }
        else
        {
            objectSpawner.SetSpawnAllowed(false);
        }
    }

    private bool CheckIfGroundBelow()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, checkForGroundDistance);
        if (hitInfo.collider == null)
        {
            Debug.Log("Collider below  " + hitInfo.collider);
            return false;
        }
        else
        {
            return true;            
        }
    }
}
