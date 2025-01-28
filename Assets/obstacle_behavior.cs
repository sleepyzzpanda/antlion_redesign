using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_behavior : MonoBehaviour
{
    private float obstacle_speed;
    private float deadzone = -20;
    public BoxCollider2D bcollider;
    // Start is called before the first frame update
    void Start()
    {
        obstacle_speed = 3.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * obstacle_speed * Time.deltaTime);
        if(transform.position.x < deadzone)
        {
            Destroy(gameObject);
        }
    }
}
