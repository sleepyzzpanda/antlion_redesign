using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_booster : MonoBehaviour
{
    //---variable parking lot-----------------------
    private float booster_speed;
    private float deadzone;
    public BoxCollider2D bcollider;
    //----------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        booster_speed = 3.0f;
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
