using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with" + other.gameObject.name + " layer: " + other.gameObject.layer);
        if (other.gameObject.layer == 16)  
        {
            Debug.Log("Collider");
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.layer != 8)
        {
            Debug.Log("Collider");
            Destroy(other.gameObject);
        }
            
    }
}
