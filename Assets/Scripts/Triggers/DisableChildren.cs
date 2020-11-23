using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChildren : MonoBehaviour
{
    public GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log("Collision!");
            foreach (Transform child in parent.transform)
            {
                Debug.Log("child: " + child.gameObject.name);
                child.gameObject.SetActive(false);
            }
        }
    }
}
