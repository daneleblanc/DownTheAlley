using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShooters : MonoBehaviour
{
    public GameObject shooter;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.layer == 8)
        {
            Debug.Log("By a player!");
            Debug.Log("shooter is!" + shooter.name);
            shooter.SetActive(true);
        }
    }
}