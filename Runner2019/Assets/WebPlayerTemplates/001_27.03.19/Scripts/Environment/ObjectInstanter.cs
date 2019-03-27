using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectInstanter : MonoBehaviour {

	[SerializeField] private GameObject[] objectsToPool;
    [SerializeField] private Transform instancedObjectsParent;
	[SerializeField] private int pooledAmount;
    [SerializeField] private float invokeInterval;

    private List<GameObject> listOfObjects;

	void Start () {

		listOfObjects = new List<GameObject> ();

		for (int i = 0; i < pooledAmount; i++) {
			GameObject obj = (GameObject)Instantiate (objectsToPool [Random.Range (0, objectsToPool.Length)]);
            obj.transform.parent = instancedObjectsParent;
			obj.SetActive (false);
			listOfObjects.Add (obj);
		}	

		InvokeRepeating ("Spawner", 0f, invokeInterval);	
	}
	
	public void Spawner(){

		for (int i = 0; i < pooledAmount; i++) {
			if (!listOfObjects [i].activeInHierarchy) {
				listOfObjects [i].transform.position = transform.position;
				listOfObjects [i].transform.rotation = transform.rotation;
				listOfObjects [i].SetActive (true);
				break;
			}
		}
	}

}
