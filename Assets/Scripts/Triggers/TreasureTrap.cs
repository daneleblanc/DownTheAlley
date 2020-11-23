using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureTrap : MonoBehaviour
{
    public GameObject watcher;
    public GameObject signLight;
    public GameObject floorActivate;
    public GameObject floorDeactivate;
    public GameObject wallActivate;
    public GameObject lightDeactivate;
    public AudioClip spookySound;


    private Rigidbody rigidBody;
    private Material watcherMaterial;
    private Light watcherLight;
    private float triggerDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = watcher.GetComponent<Rigidbody>();
        watcherMaterial = watcher.GetComponentInChildren<Renderer>().material;
        watcherLight = watcher.GetComponent<Light>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            signLight.SetActive(true);
            floorActivate.SetActive(true);
            floorDeactivate.SetActive(false);
            lightDeactivate.SetActive(false);
            wallActivate.SetActive(true);
            Invoke("WatcherTrigger", triggerDelay);
        }
    }

    void WatcherTrigger()
    {
        AudioSource.PlayClipAtPoint(spookySound, transform.position);
        Debug.Log("WatcherTrigger!");
        rigidBody.isKinematic = false;
        //risingFloor.transform.position = floorTarget;
        // Changes the variable inside of the Watcher WatcherController script
        watcher.GetComponent<WatcherController>().isRolling = true;
        watcherMaterial.SetColor("_Color", Color.red);
        watcherLight.enabled = true;
        Destroy(gameObject);
    }
}