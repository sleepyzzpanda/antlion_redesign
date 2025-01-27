using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_behavior : MonoBehaviour
{
    private float obstacle_speed = 1.5f;
    private float deadzone = -20;
    public BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        
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
