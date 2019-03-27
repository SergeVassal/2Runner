using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyerScript : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player") {
            GameManager.Instance.LoadLevel("002Level");
            GameManager.Instance.UnloadLevel("001Level");
			return;
		}
		if (other.gameObject.transform.parent) {
			other.gameObject.transform.parent.gameObject.SetActive (false);
		} else {
			other.gameObject.SetActive (false);
		}

	}


}
