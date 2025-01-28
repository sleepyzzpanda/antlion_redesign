using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_behavior : MonoBehaviour
{
    private float deadzone;
    private float bg_speed;
    // Start is called before the first frame update
    void Start()
    {
        deadzone = -33;
        bg_speed = 3.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        // scroll left
        transform.position = transform.position + (Vector3.left * bg_speed * Time.deltaTime);
        // when hits deadzone, reset position to x = 34
        if(transform.position.x < deadzone)
        {
            transform.position = new Vector3(34, transform.position.y, transform.position.z);
        }
        
    }
}
