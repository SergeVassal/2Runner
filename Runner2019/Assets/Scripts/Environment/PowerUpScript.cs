using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player") {
			GameManager.Instance.IncreaseScore (1000);
			this.gameObject.SetActive (false);
		}
	}
}
