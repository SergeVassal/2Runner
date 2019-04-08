using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCharacter : MonoBehaviour
{
    [SerializeField] private Transform characterToFollow;
    [SerializeField] private float horizontalOffset;
    [SerializeField] private float verticalPosition;
    [SerializeField] private float ZOffset;



    private void LateUpdate()
    {
        Vector3 newPosition = new Vector3(characterToFollow.position.x + horizontalOffset, verticalPosition, characterToFollow.position.z + ZOffset);
        gameObject.transform.position = newPosition;
    }
}
