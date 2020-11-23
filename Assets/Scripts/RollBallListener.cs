using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBallListener : MonoBehaviour
{

    public AudioClip ballsSound;

    public float launchSpeed = 500;
    public float defaultSpeed;
    public float currentSpeed;
    public float defaultPitch;
    private bool hasLaunched = false;
    private bool isTouching = false;

    private Rigidbody rb;
    private AudioSource ballsAudioSource;
    private float minPitch = 0;
    private float maxPitch = 2;
    private float maxSpeed = 20;
    private float volume;
    private float pitch;
    private float pitchModifier;
    const float c = 0.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballsAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var currentSpeed = rb.velocity.magnitude;
        pitchModifier = maxPitch - minPitch;
        ballsAudioSource.pitch = minPitch + (currentSpeed / maxSpeed) * pitchModifier;

        currentSpeed = Mathf.Abs(rb.velocity.magnitude);
        volume = currentSpeed * c;
        //Debug.Log("Velocity: " + rb.velocity.magnitude);

        if(GetComponent<Rigidbody>().velocity.magnitude > 0.05)
        {
            if (!ballsAudioSource.isPlaying)
            {
                ballsAudioSource.loop = true;
                ballsAudioSource.Play();
            }
        }
        else if (GetComponent<Rigidbody>().velocity.magnitude <= 0.04f)
        {
            ballsAudioSource.Stop();
        }

    }

    private void FixedUpdate()
    {
        if (!hasLaunched) {
        Vector3 direction = new Vector3(0f, 0.0f, -1f);
        rb.AddForce(direction * launchSpeed * 100);
            hasLaunched = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
                isTouching = true;
        }
        else if(GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            //ballsAudioSource.Stop();
        }
    }

    void OnCollisionExit(Collision col)
    {
        //ballsAudioSource.Stop();
    }


}
