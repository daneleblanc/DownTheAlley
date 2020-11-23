using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{

    public GameObject projectile;
    public Transform spawnTransform;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, spawnTransform.position, spawnTransform.rotation);
        }
    }
}