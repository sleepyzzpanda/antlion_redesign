using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_rock : MonoBehaviour
{
    // variable parking lot -------------------------
    public Collider2D c_collider;

    void OnCollisionEnter2D(Collision2D other)
    {
        // destroy self
        Destroy(gameObject);
    }
}
