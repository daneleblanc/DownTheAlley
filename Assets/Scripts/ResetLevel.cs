using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public Vector2 startForce;
    public Rigidbody2D rb;

    void Start()
    {
        rb.AddForce(startForce, ForceMode2D.Impulse);
    }

    // OnCollisionEnter2D is called when this collider2D/rigidbody2D has begun touching another rigidbody2D/collider2D (2D physics only)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "wall_bottom")
            SceneManager.LoadScene("demo");
    }
}
