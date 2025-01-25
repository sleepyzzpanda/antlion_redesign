using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_behavior : MonoBehaviour
{
    public float floor_speed = 1.0f;
    private float deadzone = -20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * floor_speed * Time.deltaTime);
        if(transform.position.x < deadzone)
        {
            Destroy(gameObject);
        }
    }
}
