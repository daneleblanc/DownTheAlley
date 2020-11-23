using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celebrate : MonoBehaviour
{

    public float startDelay = 0f;

    private bool timeToStart = false;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        Invoke("TimeToStart", startDelay);
    }

    void TimeToStart()
    {
        anim.Play("Celebration");
        timeToStart = true;
    }
}
