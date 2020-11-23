using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherController : MonoBehaviour
{

    public Transform target;
    public bool follow = true;
    public float turnSpeed = 1f;
    public bool isRolling = false;
    public float rollSpeed = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (follow && !isRolling)
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = target.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = turnSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0f);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    private void FixedUpdate()
    {
        if (isRolling)
        {
            follow = false;
            //rb.velocity = transform.forward * rollSpeed;
            rb.AddForce(new Vector3(0f, 0f, -1f) * rollSpeed);
        }
    }

}
