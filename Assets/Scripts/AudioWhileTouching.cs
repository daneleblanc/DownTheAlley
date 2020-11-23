using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWhileTouching : MonoBehaviour
{
    private AudioSource ballsAudioSource;
    private bool isTouching = false;
    private float timeLastTouch;
    private float timeOffset = 0.75f;

    private void Start()
    {
        timeLastTouch = Time.time;
        ballsAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (isTouching)
        {
            if (!ballsAudioSource.isPlaying)
            {
                ballsAudioSource.loop = true;
                ballsAudioSource.Play();
            }
        }
        else
        {
            if (ballsAudioSource.isPlaying && Time.time > timeLastTouch + timeOffset)
            {
                ballsAudioSource.Stop();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        timeLastTouch = Time.time;
        isTouching = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        timeLastTouch = Time.time;
        isTouching = false;
    }
}
