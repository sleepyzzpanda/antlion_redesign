using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scene_behavior : MonoBehaviour
{
    // vairaible parking lot -------------------------
    public float floor_spawn_rate;
    private float floor_spawn_timer;
    // for obstacle 1
    public float obstacle_spawn_rate, coin_spawn_rate, falling_rock_spawn_rate;
    private int coin_count; // defines how many coins to spawn
    private float obstacle_spawn_timer, coin_spawn_timer, falling_rock_spawn_timer;
    public GameObject floor_tile;
    public GameObject player;
    public GameObject obstacle_1, falling_rock;
    public GameObject health_booster, stamina_booster, coin_booster, cobweb, tumbleweed;
    private float speed;
    public Text HP, STAMINA, GAMEOVER, COINS;
    // -----------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        floor_spawn_rate = 0.7f;
        floor_spawn_timer = 0.0f;
        obstacle_spawn_timer = 0.0f;
        speed = 1.5f;
        Time.timeScale = 1.0f;  
        coin_count = 3;
        coin_spawn_timer = 0.0f;
        coin_spawn_rate = 3.2f;
        falling_rock_spawn_rate = 10.0f;
        falling_rock_spawn_timer = 0.0f;

        // set gameover text to inactive
        GAMEOVER.gameObject.SetActive(false);   
   
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is alive
        if(player.GetComponent<player_behavior>().alive == false)
        {
            Time.timeScale = 0.0f;
            // update hp text
            HP.text = "HP: 0"; 

            // set gameover text to active
            GAMEOVER.gameObject.SetActive(true);
            return;
        }

        // generate floor tiles
        if(floor_spawn_timer > floor_spawn_rate)
        {
            Instantiate(floor_tile, transform.position, transform.rotation);
            floor_spawn_timer = 0.0f;
        }
        else
        {
            floor_spawn_timer += Time.deltaTime;
        } 

        // generate obstacles + boosters
        if(obstacle_spawn_timer > obstacle_spawn_rate)
        {
            if(Random.Range(0, 4) == 0){ // 1 in 4 chance of booster
                int selection = Random.Range(0, 3);
                // generate random booster
                if(selection == 0){
                    // health booster
                    Instantiate(health_booster, new Vector3(10, -0.46f, 0), transform.rotation);
                }
                else if(selection == 1){
                    // random between tumbleweed and cobweb
                    int selection2 = Random.Range(0, 2);
                    if(selection2 == 0){
                        // tumbleweed
                        Instantiate(tumbleweed, new Vector3(10, -0.46f, 0), transform.rotation);
                    } else{
                        // cobweb
                        Instantiate(cobweb, new Vector3(10, -0.46f, 0), transform.rotation);
                    }
                } else{
                    // stamina booster
                    Instantiate(stamina_booster, new Vector3(10, -0.46f, 0), transform.rotation);
                }
                    
            } else {
                // generate obstacle 1
                Instantiate(obstacle_1, new Vector3(10, -0.81f, 0), transform.rotation);
                // instantiate coins based on probability
                if(Random.Range(0, 3) == 0){
                    for(int i = 0; i < coin_count; i++)
                    {
                        // offset the xpos by 0.5
                        Instantiate(coin_booster, new Vector3(12 + (i * 0.5f), -0.46f, 0), transform.rotation);
                    }
                }
            }

            // reset timer
            obstacle_spawn_timer = 0.0f;
        }
        else
        {
            obstacle_spawn_timer += Time.deltaTime;
            /*COME BACK TO THIS*/
            // // no obstacle or booster so deploy coin booster
            // // in multiple of coin count
            // if(coin_spawn_timer > coin_spawn_rate)
            // {
            //     for(int i = 0; i < coin_count; i++)
            //     {
            //         // offset the xpos by 0.8
            //         Instantiate(coin_booster, new Vector3(11 + (i * 0.5f), -0.46f, 0), transform.rotation);
            //     }
            //     coin_spawn_timer = 0.0f;
            // }
            // else
            // {
            //     coin_spawn_timer += Time.deltaTime;
            // }

        }

        // generate falling rocks
        if(falling_rock_spawn_timer > falling_rock_spawn_rate)
        {
            Instantiate(falling_rock, new Vector3(10, 8.4f, 0), transform.rotation);
            falling_rock_spawn_timer = 0.0f;
        }
        else
        {
            falling_rock_spawn_timer += Time.deltaTime;
        }

        // update UI
        HP.text = "HP: " + player.GetComponent<player_behavior>().health;
        STAMINA.text = "STAMINA: " + player.GetComponent<player_behavior>().stamina;
        COINS.text = "COINS: " + player.GetComponent<player_behavior>().coins;

    }
}
