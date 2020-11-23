using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeOnContact : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 13)
        Destroy(gameObject);
    }
}
