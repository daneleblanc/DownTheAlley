using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInstantiate : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public float speed = 15.0f;
    public Camera targetCamera;

    public void Awake()
    {
        //targetCamera = Camera.main;
    }

    public void Update()
    {
        if (prefabToInstantiate == null || targetCamera == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = targetCamera.ScreenPointToRay(Input.mousePosition);
            GameObject newGameObject = (GameObject)Instantiate(prefabToInstantiate, mouseRay.origin, Quaternion.identity);
            Rigidbody rb = newGameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = mouseRay.direction * speed;
            }
        }
    }
}