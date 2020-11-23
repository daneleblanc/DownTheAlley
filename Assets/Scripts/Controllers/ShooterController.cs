using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public Transform startPosition;
    public GameObject projectile;
    public float speed = 1000f;
    public float shotsPerMinute = 60f;
    public float timedLife = 10f;
    public bool firing = true;
    public bool warmedUp = false;
    public AudioClip shootSound;


    private float lastShot;

    private void Start()
    {
        Invoke("SetWarmup", 4f);
    }

    private void Update()
    {
        if (firing && warmedUp)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > 60f / shotsPerMinute + lastShot)
        {
            if(shootSound != null)
            {
                AudioSource.PlayClipAtPoint(shootSound, transform.position);
            }

            GameObject instProj = ObjectPooler.SharedInstance.GetPooledObject(); 
            if (instProj != null) {
                instProj.transform.position = startPosition.transform.position;
                instProj.transform.rotation = startPosition.transform.rotation;
                instProj.SetActive(true);
                Rigidbody instProjRB = instProj.GetComponent<Rigidbody>();
                instProjRB.AddForce(startPosition.forward * speed * 1000);
                lastShot = Time.time;
                //Destroy(instProj, 10f);
                StartCoroutine(BackInPool(instProj));
            }
            
            //GetComponent<AudioSource>().Play();

            //GameObject instProj = Instantiate(projectile, startPosition.position, startPosition.rotation);
            //Rigidbody instProjRB = instProj.GetComponent<Rigidbody>();
            //instProjRB.AddForce(startPosition.forward * speed * 1000);
            //Destroy(instProj, 10f);
            //lastShot = Time.time;
        }
    }
    

IEnumerator BackInPool(GameObject poolObject)
{
    yield return new WaitForSeconds(timedLife);
        {
            poolObject.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
            poolObject.SetActive(false);
        }
        
    //Do Function here...
}


    private void SetWarmup()
    {
        warmedUp = true;
    }
}
