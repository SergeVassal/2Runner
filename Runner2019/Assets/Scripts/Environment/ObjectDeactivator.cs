using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("GAME OVER");            
        }
        else
        {
            if (collision.gameObject.transform.parent.tag == "Ground")
            {
                collision.gameObject.transform.parent.gameObject.SetActive(false);
            }            
            else 
            {
                collision.gameObject.SetActive(false);
            }
        }


    }

}
