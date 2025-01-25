using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_behavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    public int health = 5;
    public float scroll_speed;
    private bool is_jumping = false;
    public float boost_timer = 0.0f;
    public bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // moving mechanics

        // jumping
        if(Input.GetKey(KeyCode.UpArrow)){
            is_jumping = true;
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            if(Input.GetKey(KeyCode.UpArrow)){
                is_jumping = true;
            }
            if(is_jumping){
                rb.velocity = new Vector2(-2, 10);
            } else {
                rb.velocity = new Vector2(-2, -5);
                speed_boost();
            }
        } else if(Input.GetKey(KeyCode.RightArrow)){
            if(Input.GetKey(KeyCode.UpArrow)){
                is_jumping = true;
            }
            if(is_jumping){
                rb.velocity = new Vector2(2, 10);
            } else {
                rb.velocity = new Vector2(1, -5);
                speed_boost();
            }
        } else {
            if(is_jumping){
                rb.velocity = new Vector2(-scroll_speed, 10);
            } else {
                rb.velocity = new Vector2(-scroll_speed, -5);
            }
        }

        if(transform.position.y > 1.2f){
            is_jumping = false;
        }

        // countering gravity for testing, need to remove and properly implement
        if(transform.position.y < -0.46f){
            transform.position = new Vector3(transform.position.x, -0.46f, transform.position.z);
        }
        
    }

    public void speed_boost(){
        // speed boost
        // private bool is_boosting = false;
        if(Input.GetKey(KeyCode.Space)){
            boost_timer += Time.deltaTime;
            if(boost_timer > 0.3f){
                // forces cooldown
                if(boost_timer > 0.8f){
                    boost_timer = 0.0f;
                }
            } else {
                rb.velocity *= 15;
                boost_timer += Time.deltaTime;
            }  
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    { 
        if (collision.gameObject.CompareTag("Obstacle")) 
        { 
            health -= 1;
            if(health <= 0){
                alive = false;
            }
        } 
    } 
}


