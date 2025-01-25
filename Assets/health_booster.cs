using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_booster : MonoBehaviour
{
    //---variable parking lot-----------------------
    public float booster_speed;
    private float deadzone;
    public BoxCollider2D collider;
    //----------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        booster_speed = 1.0f;
        deadzone = -20;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * booster_speed * Time.deltaTime);
        if(transform.position.x < deadzone)
        {
            Destroy(gameObject);
        }
        
    }
}
