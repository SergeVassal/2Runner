using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawnerAbstract : MonoBehaviour
{
    [SerializeField] protected Transform poolParentGO;
    [SerializeField] protected GameObject[] gameObjectArray;
    [SerializeField] protected int objectPoolSize;

    protected List<GameObject> activeObjectsPool=new List<GameObject>();
    protected List<GameObject> inactiveObjectsPool = new List<GameObject>();
    
    



    private void Start()
    {
        FillPool();
        StartSpawning();
    }

    private void FillPool()
    {
        for (int i = 0; i < objectPoolSize; i++)
        {
            GameObject goToAdd = GetRandomGameObjectFromArray();
            if (goToAdd != null)
            {
                GameObject go = (GameObject)Instantiate(goToAdd, transform.position, Quaternion.identity, poolParentGO);
                go.GetComponent<SpawnableAbstract>().SetObjectSpawner(this);
                go.SetActive(false);
            }
        }
    }
    private GameObject GetRandomGameObjectFromArray()
    {
        if (gameObjectArray.Length > 0)
        {
            GameObject newGO = gameObjectArray[Random.Range(0, gameObjectArray.Length)];
            return newGO;
        }
        return null;
    }

    protected abstract void StartSpawning();

    protected void SpawnGO()
    {
        if (inactiveObjectsPool.Count > 0)
        {
            GameObject go = inactiveObjectsPool[0];
            go.transform.position = transform.position;
            go.SetActive(true);
            activeObjectsPool.Add(go);
            inactiveObjectsPool.Remove(go);

            Debug.Log("inactiveObjectsPool   " + inactiveObjectsPool.Count);
            Debug.Log("activeObjectsPool   " + activeObjectsPool.Count);
        }
    } 

    public void DisableSpawnedObject(GameObject objToDisable)
    {
        inactiveObjectsPool.Add(objToDisable);
        activeObjectsPool.Remove(objToDisable);
        objToDisable.gameObject.SetActive(false);
        Debug.Log("inactiveObjectsPool   " + inactiveObjectsPool.Count);
        Debug.Log("activeObjectsPool   " + activeObjectsPool.Count);
    }
}
