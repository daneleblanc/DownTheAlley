using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

public class MouseInstantiate : MonoBehaviour
{
    private int objectsCreated = 0;
    public GameObject prefabToInstantiate;
    public float speed = 15.0f;
    public Camera targetCamera;
    public bool automaticFire;
    public bool insaneAutomaticFire;
    public float deathTimer = 2f;

    public void Awake()
    {
        // If you just want the target to be the main camera.
        //targetCamera = Camera.main;
    }

    public void Update()
    {
        if (prefabToInstantiate == null || targetCamera == null)
        {
            return;
        }

        // Will create new objects as long as mouse button is held down
        if (automaticFire)
        {
            if (Input.GetMouseButton(0))
            {
                Ray mouseRay = targetCamera.ScreenPointToRay(Input.mousePosition);
                GameObject newGameObject = (GameObject)Instantiate(prefabToInstantiate, mouseRay.origin, Quaternion.identity);
                Rigidbody rb = newGameObject.GetComponent<Rigidbody>();


                // destroy after 15 seconds
                Destroy(newGameObject, deathTimer);


                if (rb != null)
                {
                    rb.velocity = mouseRay.direction * speed;
                    objectsCreated++;
                    Debug.Log("Objects created by MouseInstantiate: " + objectsCreated);
                }
            }
        }

         // Single shot on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = targetCamera.ScreenPointToRay(Input.mousePosition);
            GameObject newGameObject = (GameObject)Instantiate(prefabToInstantiate, mouseRay.origin, Quaternion.identity);
            Rigidbody rb = newGameObject.GetComponent<Rigidbody>();

            // destroy after 15 seconds
            Destroy(newGameObject, deathTimer);

            if (rb != null)
            {
                rb.velocity = mouseRay.direction * speed;
                objectsCreated++;
                Debug.Log("Objects created by MouseInstantiate: " + objectsCreated);
            }
        }

        // Mostly just for testing performance with extreme amounts of rigidbodies
        // Warning: This will be GPU intensive very fast
        if (insaneAutomaticFire)
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i <= 15; i++)
                {
                    Ray mouseRay = targetCamera.ScreenPointToRay(Input.mousePosition);
                    GameObject newGameObject = (GameObject)Instantiate(prefabToInstantiate, mouseRay.origin, Quaternion.identity);
                    Rigidbody rb = newGameObject.GetComponent<Rigidbody>();

                    // destroy after 15 seconds
                    Destroy(newGameObject, deathTimer);


                    if (rb != null)
                    {
                        rb.velocity = mouseRay.direction * speed * 4;
                        objectsCreated++;
                        Debug.Log("Objects created by MouseInstantiate: " + objectsCreated);
                    }
                }
            }
        }
    }
}