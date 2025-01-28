using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_behavior : MonoBehaviour
{
    // variable parking lot -------------------------
    public Rigidbody2D rb;
    public BoxCollider2D bcollider;
    public int health;
    private int max_health;
    public int stamina;
    public int coins;
    private float scroll_speed;
    private bool is_jumping;
    //public float boost_timer;
    public bool alive, slow, antlion_slow;
    private float antlion_slow_time;
    public float slow_time;
    public bool gameover;
    public SpriteRenderer sprite_renderer;
    public Sprite player_sprite;
    public Sprite player_sprite_jump;
    public AudioSource audio_player;
    public AudioClip coin_sound, powerup_sound, damage_sound, gameover_sound, antlion_sound, win_sound, jump_sound;
    //public Text key;
    //string key_text = "KEY: ";
    // -----------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bcollider = GetComponent<BoxCollider2D>();
        health = 3;
        stamina = 10;
        scroll_speed = 5.0f;
        is_jumping = false;
        max_health = 5;
        alive = true;        
        slow = false;
        gameover = false;
        slow_time = 0.0f;
        antlion_slow = false;
        antlion_slow_time = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        // antlion slow
        if(antlion_slow){
            if(antlion_slow_time > 0.0f){
                antlion_slow_time -= Time.deltaTime;
            } else {
                antlion_slow = false;
            }
        }
        
        //key.text = key_text;
        //key_text = "KEY: ";
        // moving mechanics
        // check if player slow
        if(slow){
            if(slow_time > 0.0f){
                slow_time -= Time.deltaTime;
            } else {
                slow = false;
            }
        }
        // jumping
        if(Input.GetKey(KeyCode.UpArrow)){
            // key text
            //key_text += "UP ";
            // check y pos < 0;
            if(transform.position.y < 0.0f){
                is_jumping = true;
                // play jump sound
                audio_player.PlayOneShot(jump_sound);
            }
            // change to jumping sprite
            sprite_renderer.sprite = player_sprite_jump;
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            // key text
            //key_text += "LEFT ";
            if(Input.GetKey(KeyCode.UpArrow)){
                // check y pos < 0;
                if(transform.position.y < -0.46f){
                    is_jumping = true;
                }
            }
            if(is_jumping){
                if(slow){
                    rb.velocity = new Vector2(-1.0f, 10);
                } else {
                    rb.velocity = new Vector2(-4.2f, 10);
                }
                // jumping sprite
                sprite_renderer.sprite = player_sprite_jump;
            } else {
                if(slow){
                    rb.velocity = new Vector2(-1.0f, -5);
                } else {
                    rb.velocity = new Vector2(-4.2f, -5);
                    speed_boost();
                }
                // change to normal sprite
                sprite_renderer.sprite = player_sprite;                
            }
        } else if(Input.GetKey(KeyCode.RightArrow)){
            // key text
            //key_text += "RIGHT ";
            if(Input.GetKey(KeyCode.UpArrow)){
                // check y pos < 0;
                if(transform.position.y < -0.46f){
                    is_jumping = true;
                }
            }
            if(is_jumping){
                if(slow){
                    rb.velocity = new Vector2(1, 10);
                } else {
                    rb.velocity = new Vector2(4.2f, 10);
                }
                // jumping sprite
                sprite_renderer.sprite = player_sprite_jump;
            } else {
                if(slow){
                    rb.velocity = new Vector2(1, -5);
                } else {
                    rb.velocity = new Vector2(4.2f, -5);
                    speed_boost();
                }
                // change to normal sprite
                sprite_renderer.sprite = player_sprite;
            }
            // make sure player stays in bounds
            if(transform.position.x > 9.66f){
                transform.position = new Vector3(9.66f, transform.position.y, transform.position.z);
            }
        } else {
            if(is_jumping){
                rb.velocity = new Vector2(-scroll_speed, 10);
                // jumping sprite
                sprite_renderer.sprite = player_sprite_jump;
            } else {
                rb.velocity = new Vector2(-scroll_speed, -5);
                // change to normal sprite
                sprite_renderer.sprite = player_sprite;
            }
        }

        if(transform.position.y > 1.2f){
            is_jumping = false;
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
                Vector2 new_vel = rb.velocity;
                new_vel.x *= 10;
                rb.velocity = new_vel;
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
                // play gameover sound
                audio_player.PlayOneShot(gameover_sound);
            }
            slow = true;
            slow_time = 3.0f;
            // destroy obstacle
            Destroy(collision.gameObject);
            // play damage sound
            audio_player.PlayOneShot(damage_sound);
        } 
        if (collision.gameObject.CompareTag("health_boost")) 
        { 
            // destroy health booster
            Destroy(collision.gameObject);
            if(health < max_health){
                health += 1;
            }
            // play powerup sound
            audio_player.PlayOneShot(powerup_sound);
        }
        if(collision.gameObject.CompareTag("stamina_boost")){
            // destroy stamina booster
            Destroy(collision.gameObject);
            stamina = 10;
            // play powerup sound
            audio_player.PlayOneShot(powerup_sound);
        }
        // coins
        if(collision.gameObject.CompareTag("Coin")){
            // destroy coin
            Destroy(collision.gameObject);
            // add to score
            coins += 1;
            // play coin sound
            audio_player.PlayOneShot(coin_sound);
        }
        // cobweb
        if(collision.gameObject.CompareTag("Cobweb")){
            // todo
            // destroy cobweb
            Destroy(collision.gameObject);
            antlion_slow = true;
            antlion_slow_time = 3.0f;
            // play antlion sound
            audio_player.PlayOneShot(antlion_sound);
        }
        // tumbleweed slows player
        if(collision.gameObject.CompareTag("Tumbleweed")){
            slow = true;
            // destroy tumbleweed
            Destroy(collision.gameObject);
            slow_time = 3.0f;
        }
        // collided with end of level flag
        if(collision.gameObject.CompareTag("Flag")){
            gameover = true;
            // play win sound
            audio_player.PlayOneShot(win_sound);
        }

        // antlion
        if(collision.gameObject.CompareTag("Antlion")){
            // game over
            alive = false;
            // play gameover sound
            audio_player.PlayOneShot(gameover_sound);
        }
    } 
}


