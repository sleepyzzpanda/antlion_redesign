using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antlion_behavior : MonoBehaviour
{
    public float speed;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.065f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<player_behavior>().antlion_slow){
            speed = -0.15f;
        } else {
            speed = 0.085f;
        }
        transform.position = transform.position + (Vector3.right * speed * Time.deltaTime);   
    }
}
