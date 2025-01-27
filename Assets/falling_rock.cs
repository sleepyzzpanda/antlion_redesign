using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_rock : MonoBehaviour
{
    // variable parking lot -------------------------
    private float scroll_speed;
    private float deadzone;
    bool drop;
    public GameObject rock;
    // ---------------------------------------------- 
    // Start is called before the first frame update
    void Start()
    {
        scroll_speed = 1.5f;
        deadzone = Random.Range(-1, 7);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * scroll_speed * Time.deltaTime);
        if(transform.position.x < deadzone)
        {
            Instantiate(rock, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
