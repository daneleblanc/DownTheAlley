using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{

    public float speed = 100f;
    public float jumpForce = 750f;
    public float forwardJump = 1000f;
    public LayerMask noSelfCollision = 6;
    public Rigidbody leftFoot;
    public Rigidbody rightFoot;
    public bool isRagdoll = false;
    public float ragdollDelay = .25f;
    private float ragdollLast = 0f;
    public AudioClip jumpSound;
    public GameObject mainCameraPointer;

    public GameObject debugText1;
    public string debugTextString = "test";
    TMPro.TextMeshPro debuggy;

    private bool grounded;
    private RaycastHit hit;
    private float groundDistance = 0.2f;    
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
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    private void FixedUpdate()
    {
        if (!isRagdoll)
        {

        
            // Point towards camera direction
            Quaternion targetedRotation = Quaternion.Inverse(mainCameraPointer.transform.rotation) * initialAnchorRotation;
            Vector3 v = targetedRotation.eulerAngles;

            torsoJoint.targetRotation = Quaternion.Euler(0, v.y, 0);

            // Had a problem that only occurred in the build. 
            //debuggy = debugText1.GetComponent<TMPro.TextMeshPro>();
            //debugTextString = torsoJoint.targetRotation.ToString();
            //debuggy.text = debugTextString;

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

                // Backward movement
                if (Input.GetKey(KeyCode.S))
                {
                    Vector3 back = new Vector3(0f, 0f, -1f);
                    torso.AddRelativeForce(back * speed * 1.0f);
                }

                // Left movement
                if (Input.GetKey(KeyCode.A))
                {
                    Vector3 left = new Vector3(-1f, 0f, 0f);
                    torso.AddRelativeForce(left * speed * 0.5f);
                }

                // Right movement
                if (Input.GetKey(KeyCode.D))
                {
                    Vector3 right = new Vector3(1f, 0f, 0f);
                    torso.AddRelativeForce(right * speed * 0.5f);
                }

                // Jump
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (isGrounded())
                    {
                        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                        {
                            //A bit more forward of a jump if W
                            Debug.Log("LONG Jump");
                            torso.AddRelativeForce(new Vector3(0, jumpForce, forwardJump * 1.5f));
                            if (jumpSound != null)
                            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                        }

                        else if (Input.GetKey(KeyCode.W))
                        {
                            //A bit more forward of a jump if W
                            Debug.Log("Forward Jump");
                            torso.AddRelativeForce(new Vector3(0, jumpForce, forwardJump));
                            if (jumpSound != null)
                                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                        }

                        else
                        {
                            torso.AddRelativeForce(new Vector3(0, jumpForce, 0));
                            if (jumpSound != null)
                            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                        }
                            
                    }
                }

                
            }
        }
        else
        {
            if (Time.time > ragdollDelay + ragdollLast) 
            {
                isRagdoll = false;
                ragdollLast = Time.time;
            }
                
        }
    }

    //Make sure that the player is grounded, otherwise controls are locked
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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 16)
        {
            isRagdoll = true;
        }
        // Reset level
        else if (other.gameObject.layer == 13 || other.gameObject.layer == 11)
        {
            SceneManager.LoadScene("Level1");
            Debug.Log("Object: " + other.gameObject.name);
            Debug.Log("Layer: " + other.gameObject.layer);
            Debug.Log("Reloading");
        }
        // Next level
        else if (other.gameObject.layer == 17)
        {
            SceneManager.LoadScene("DebugScene");
            Debug.Log("Object: " + other.gameObject.name);
            Debug.Log("Layer: " + other.gameObject.layer);
            Debug.Log("Reloading");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Reset level, doesn't work outside of trigger
        if (other.gameObject.layer == 13)
        {
            SceneManager.LoadScene("Level1");
            Debug.Log("Object: " + other.gameObject.name);
            Debug.Log("Layer: " + other.gameObject.layer);
            Debug.Log("Reloading");
        }
        

    }
}