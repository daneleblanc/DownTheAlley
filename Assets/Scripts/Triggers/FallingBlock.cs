using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public float fallTimer = .5f;
    private Rigidbody rb;
    private Material mat;
    private float intensity = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<Renderer>().material;

        // Another thing to note is that Unity 5 uses the concept of shader keywords extensively.
        // So if your material is initially configured to be without emission, then in order to enable emission, you need to enable // the keyword.
        mat.EnableKeyword("_EMISSION");
    }

    void BlockFall()
    {
        rb.isKinematic = false;
        
        Destroy(gameObject, 3f);
    }

    private void OnCollisionExit(Collision collision)
    {
        mat.SetColor("_EmissionColor", Color.green * intensity);

        Invoke("BlockFall",fallTimer);
    }
}
