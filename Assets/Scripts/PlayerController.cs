using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    public LayerMask noSelfCollision = 6;
    public Rigidbody leftFoot;
    public Rigidbody rightFoot;


    private bool grounded;
    private RaycastHit hit;
    private float groundDistance = 0.5f;    
    private Vector3 downV;
    private Rigidbody torso;
    private ConfigurableJoint torsoJoint;

    Quaternion initialAnchorRotation = new Quaternion();

    public void Awake()
    {
        downV = new Vector3(0, -1, 0);
        torso = GetComponent<Rigidbody>();
    }
    void Start()
    {
        torsoJoint = myJoint;
        
        initialAnchorRotation = torso.transform.rotation;
    }

    public ConfigurableJoint myJoint
    {
        get
        {
            if (torsoJoint == null)
            {
                torsoJoint = this.gameObject.GetComponent<ConfigurableJoint>();
            }
            return torsoJoint;
        }
    }

    private void Update()
    {
        //transform.parent.position = transform.position - transform.localPosition;
    }

    private void FixedUpdate()
    {
        float Angle = AngleCalculator.GetAngle(this.transform.forward.x, this.transform.forward.z);
        

        Quaternion targetedRotation = Quaternion.Inverse(Camera.main.transform.rotation) * initialAnchorRotation;
        Vector3 v = targetedRotation.eulerAngles;
        torsoJoint.targetRotation = Quaternion.Euler(0, v.y, 0);

        Color color = (!isGrounded()) ? Color.red : Color.green;
        Debug.DrawRay(new Vector3(0, 0.1f, 0) + leftFoot.transform.position, downV * groundDistance, color);
        if (Input.anyKey)
        {
            if (!isGrounded())
            {
                return;
            }
            // Forward movement
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 forward = new Vector3(0f, 0f, 1f);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    torso.AddRelativeForce(forward * speed * 1.5f);
                }
                else
                {
                    torso.AddRelativeForce(forward * speed);
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                Vector3 back = new Vector3(0f, 0f, -1f);
                torso.AddRelativeForce(back * speed * 1.5f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Vector3 left = new Vector3(-1f, 0f, 0f);
                torso.AddRelativeForce(left * speed * 1.5f);
            }

            

            if (Input.GetKey(KeyCode.D))
            {
                Vector3 right = new Vector3(1f, 0f, 0f);
                torso.AddRelativeForce(right * speed * 1.5f);
            }

            if (Input.GetAxis("Jump") > 0)
            {
                if (isGrounded())
                {
                    torso.AddForce(new Vector3(0, jumpForce, 0));
                }
            }
        }
    }

    private bool isGrounded()
    {
        Vector3 leftFootCenter = new Vector3(0, 0.1f, 0) + leftFoot.transform.position;
        

        if (Physics.Raycast(new Ray(leftFootCenter, downV), out hit, groundDistance, ~noSelfCollision.value)) 
        {
            grounded = true;
            return grounded;
        }

        Vector3 rightFootCenter = new Vector3(0, 0.1f, 0) + rightFoot.transform.position;
        grounded = Physics.Raycast(new Ray(rightFootCenter, downV), out hit, groundDistance, ~noSelfCollision.value);
        return grounded;

    }
}