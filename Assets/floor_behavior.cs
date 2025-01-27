using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_behavior : MonoBehaviour
{
    // variable parking lot -------------------------
    private float floor_speed;
    private float deadzone;
    //public GameObject scene;
    //public BoxCollider2D collider;
    // -----------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        floor_speed = 1.5f;
        deadzone = -20;
        
    }

    // Update is called once per frame
    void Update()
    {
        // update speeds via scene_behavior
        

        transform.position = transform.position + (Vector3.left * floor_speed * Time.deltaTime);
        if(transform.position.x < deadzone)
        {
            Destroy(gameObject);
        }
    }
}
