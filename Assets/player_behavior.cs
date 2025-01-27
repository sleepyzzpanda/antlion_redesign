using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_behavior : MonoBehaviour
{
    // variable parking lot -------------------------
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    public int health;
    private int max_health;
    public int stamina;
    public int coins;
    private float scroll_speed;
    private bool is_jumping;
    //public float boost_timer;
    public bool alive;
    // -----------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        health = 3;
        stamina = 10;
        scroll_speed = 1.5f;
        is_jumping = false;
        max_health = 5;
        alive = true;        
    }

    // Update is called once per frame
    void Update()
    {
        // moving mechanics

        // jumping
        if(Input.GetKey(KeyCode.UpArrow)){
            // check y pos < 0;
            if(transform.position.y < -0.46f){
                is_jumping = true;
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            if(Input.GetKey(KeyCode.UpArrow)){
                // check y pos < 0;
                if(transform.position.y < -0.46f){
                    is_jumping = true;
                }
            }
            if(is_jumping){
                rb.velocity = new Vector2(-2, 10);
            } else {
                rb.velocity = new Vector2(-2, -5);
                speed_boost();
            }
        } else if(Input.GetKey(KeyCode.RightArrow)){
            if(Input.GetKey(KeyCode.UpArrow)){
                // check y pos < 0;
                if(transform.position.y < -0.46f){
                    is_jumping = true;
                }
            }
            if(is_jumping){
                rb.velocity = new Vector2(3, 10);
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

        // update stamina
        if(stamina < 10 && Time.frameCount % 100 == 0){
            stamina += 1;
        }

        // check if player is still in bounds
        if(transform.position.x < -11f){
            alive = false;
        }
        
    }

    public void speed_boost(){
        // speed boost
        // based on stamina
        if(Input.GetKey(KeyCode.Space)){
            if(stamina > 0){
                rb.velocity *= 15;
                stamina -= 1;
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
        if (collision.gameObject.CompareTag("health_boost")) 
        { 
            // destroy health booster
            Destroy(collision.gameObject);
            if(health < max_health){
                health += 1;
            }
        }
        if(collision.gameObject.CompareTag("stamina_boost")){
            // destroy stamina booster
            Destroy(collision.gameObject);
            stamina = 10;
        }
        // coins
        if(collision.gameObject.CompareTag("Coin")){
            // destroy coin
            Destroy(collision.gameObject);
            // add to score
            coins += 1;
        }
    } 
}


