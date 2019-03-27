using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	[SerializeField] private Transform player;
    [SerializeField] private float distanceAhead;

	void Update ()
    {
		transform.position = new Vector3 (player.position.x + distanceAhead, 0f,-10f);	
	}
}
