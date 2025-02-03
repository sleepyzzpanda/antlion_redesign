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
    float xvel, yvel;
    private bool is_jumping;
    //public float boost_timer;
    public bool alive, slow, antlion_slow, key_held;
    private float antlion_slow_time;
    public float slow_time, hurt_time;
    public bool gameover, is_hurt;
    public SpriteRenderer sprite_renderer;
    public Sprite player_sprite;
    public Sprite player_sprite_jump;
    public Sprite p0, p1, p2, p3, p4, p5, p6, p7;
    public Sprite sp0, sp1, sp2, sp3, sp4, sp5, sp6, sp7;
    public Sprite dash, hurt;
    public int frame_count;
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
        xvel = 0.0f;
        yvel = 0.0f;
        frame_count = 1;;
        is_hurt = false;
        hurt_time = 0.0f;
        
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
            // check y pos < 0;
            if(transform.position.y < 0.0f){
                is_jumping = true;
                yvel = 10.0f;
                // play jump sound
                audio_player.PlayOneShot(jump_sound);
            }
            // change to jumping sprite
            sprite_renderer.sprite = player_sprite_jump;
        } else {
            if(is_jumping){
                // check for max y pos
                if(transform.position.y > 1.75f){
                    is_jumping = false;
                    // change to normal sprite
                    sprite_renderer.sprite = player_sprite;
                    // sprite_running(frame_count);
                }
            } else {
                // change to normal sprite
                sprite_renderer.sprite = player_sprite;
                // sprite_running(frame_count);
                // set yvel
                yvel = -5.0f;
            }
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            // keyheld
            key_held = true;
            if(slow){
                xvel = -2.0f;
            } else {
                xvel = -scroll_speed - 2.2f;
            }
            sprite_running(frame_count);
        } else if(Input.GetKey(KeyCode.RightArrow)){
            // keyheld
            key_held = true;
            if(slow){
                xvel = 2.0f;
            } else {
                xvel = 4.2f;
            }
            sprite_running(frame_count);
        } else {
            // key not held
            key_held = false;
            if(is_jumping){
                xvel = -1.0f;
            } else {
            xvel = -scroll_speed;
            }
            sprite_renderer.sprite = player_sprite;
            if(slow && !is_hurt){
                sprite_renderer.sprite = sp0;
            }
        }

        // update player velocity
        rb.velocity = new Vector2(xvel, yvel);
        // check if speed boost if keyheld
        if(key_held){
            speed_boost();
        }
        // make sure player stays in bounds
        if(transform.position.x > 7.0f){
            transform.position = new Vector3(7.0f, transform.position.y, transform.position.z);
        }

        if(transform.position.y > 1.8f){
            // max ypos
            transform.position = new Vector3(transform.position.x, 1.8f, transform.position.z);
            //
            yvel = -5.0f;
        }

        // update stamina
        if(stamina < 10 && Time.frameCount % 100 == 0){
            stamina += 1;
        }

        // check if player is still in bounds
        if(transform.position.x < -11f){
            alive = false;
        }

        if(frame_count > 35){
            frame_count = 1;
        } else {
            frame_count += 1;
        }

        if(is_hurt){
            if(hurt_time > 0.0f){
                hurt_time -= Time.deltaTime;
            } else {
                is_hurt = false;
            }
        }
        if(is_hurt){
            sprite_renderer.sprite = hurt;
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
                // set player sprite to dash
                sprite_renderer.sprite = dash;
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
            is_hurt = true;
            hurt_time = 3.0f;
            slow_time = 3.0f;
            // destroy obstacle
            Destroy(collision.gameObject);
            // play damage sound
            audio_player.PlayOneShot(damage_sound);
        } 
        if (collision.gameObject.CompareTag("Obstacle2")) 
        { 
            health -= 2;
            if(health <= 0){
                alive = false;
                // play gameover sound
                audio_player.PlayOneShot(gameover_sound);
            }
            slow = true;
            is_hurt = true;
            hurt_time = 3.0f;
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
    void sprite_running(int frame_count){
        int frame = frame_count / 5;
        if(!slow){switch(frame){
            case 0:
                sprite_renderer.sprite = p0;
                break;
            case 1:
                sprite_renderer.sprite = p1;
                break;
            case 2:
                sprite_renderer.sprite = p2;
                break;
            case 3:
                sprite_renderer.sprite = p3;
                break;
            case 4:
                sprite_renderer.sprite = p4;
                break;
            case 5:
                sprite_renderer.sprite = p5;
                break;
            case 6:
                sprite_renderer.sprite = p6;
                break;
            case 7:
                sprite_renderer.sprite = p7;
                break;
            }
        } else {
            switch(frame){
            case 0:
                sprite_renderer.sprite = sp0;
                break;
            case 1:
                sprite_renderer.sprite = sp1;
                break;
            case 2:
                sprite_renderer.sprite = sp2;
                break;
            case 3:
                sprite_renderer.sprite = sp3;
                break;
            case 4:
                sprite_renderer.sprite = sp4;
                break;
            case 5:
                sprite_renderer.sprite = sp5;
                break;
            case 6:
                sprite_renderer.sprite = sp6;
                break;
            case 7:
                sprite_renderer.sprite = sp7;
                break;
            }
        }
    }
}


