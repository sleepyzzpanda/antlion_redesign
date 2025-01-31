using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scene_behavior : MonoBehaviour
{
    // variable parking lot -------------------------
    public float obstacle_spawn_rate, coin_spawn_rate, falling_rock_spawn_rate;
    private int coin_count; // defines how many coins to spawn
    public float obstacle_spawn_timer, coin_spawn_timer, falling_rock_spawn_timer;
    public GameObject player, antlion;
    public GameObject obstacle_1, falling_rock, thorns;
    public GameObject health_booster, stamina_booster, coin_booster, cobweb, tumbleweed, flag;
    private float speed;
    public Text HP, STAMINA, GAMEOVER, COINS, PROGRESS, WINNER, RESTART;
    private int progress_counter;
    private bool sent_flag;
    // -----------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        obstacle_spawn_timer = 0.0f;
        obstacle_spawn_rate = 2.2f;
        Time.timeScale = 1.0f;  
        coin_count = 3;
        coin_spawn_timer = 0.0f;
        coin_spawn_rate = 3.2f;
        falling_rock_spawn_rate = 16.2f;
        falling_rock_spawn_timer = 0.0f;
        progress_counter = 0;
        sent_flag = false;
        // set gameover text to inactive
        GAMEOVER.gameObject.SetActive(false);  
        WINNER.gameObject.SetActive(false); 
        RESTART.gameObject.SetActive(false);
   
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
            RESTART.gameObject.SetActive(true);
            // if player presses R, reset scene
            if(Input.GetKeyDown(KeyCode.R)){
                reset_scene();
            }
            return;
        }

        // check if player has reached the end
        if(player.GetComponent<player_behavior>().gameover == true)
        {
            Time.timeScale = 0.0f;
            // set winner text to active
            WINNER.gameObject.SetActive(true);
            RESTART.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.R)){
                reset_scene();
            }
            return;
        }

        // generate obstacles + boosters
        if(obstacle_spawn_timer > obstacle_spawn_rate)
        {
            if(progress_counter >= 100){
               // instantiate end of level flag;
               if(!sent_flag){
                   sent_flag = true;
                   // instantiate flag here
                     Instantiate(flag, new Vector3(10, -0.46f, 0), transform.rotation);
               }
               return;
            }
            else if(Random.Range(0, 4) == 0){ // 1 in 4 chance of booster
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
                        float ypos = Random.Range(-0.46f, 1.0f);
                        Instantiate(tumbleweed, new Vector3(10, ypos, 0), transform.rotation);
                    } else{
                        // cobweb
                        Instantiate(cobweb, new Vector3(10, -0.46f, 0), transform.rotation);
                    }
                } else{
                    // stamina booster
                    Instantiate(stamina_booster, new Vector3(10, -0.46f, 0), transform.rotation);
                }
                    
            } else {
                // generate random obstacle
                if(Random.Range(0, 3) != 0){
                    // generate obstacle 1
                    Instantiate(obstacle_1, new Vector3(10, -0.81f, 0), transform.rotation);
                } else{
                    // generate thorns
                    Instantiate(thorns, new Vector3(10, -0.81f, 0), transform.rotation);
                }
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
        }

        // generate falling rocks
        if(falling_rock_spawn_timer > falling_rock_spawn_rate)
        {
            if(Random.Range(0, 2) == 0){
                Instantiate(falling_rock, new Vector3(10, 8.4f, 0), transform.rotation);
                falling_rock_spawn_timer = 0.0f;
            }
            
        }
        else
        {
            falling_rock_spawn_timer += Time.deltaTime;
        }
        // update progress counter
        if(progress_counter < 100 && Time.frameCount % 100 == 0){
            progress_counter += 1;
        }

        // update UI
        HP.text = "HP: " + player.GetComponent<player_behavior>().health;
        STAMINA.text = "STAMINA: " + player.GetComponent<player_behavior>().stamina;
        COINS.text = "COINS: " + player.GetComponent<player_behavior>().coins;
        PROGRESS.text = "PROGRESS: " + progress_counter + "%";

    }

    void reset_scene(){
        // reset scene
        // reset player position
        player.transform.position = new Vector3(-4.0f, 1.0f, 0.0f);
        // reset player health
        player.GetComponent<player_behavior>().health = 3;
        // reset player stamina
        player.GetComponent<player_behavior>().stamina = 10;
        // reset player coins
        player.GetComponent<player_behavior>().coins = 0;
        // reset player alive status
        player.GetComponent<player_behavior>().alive = true;
        // reset player gameover status
        player.GetComponent<player_behavior>().gameover = false;
        // reset player slow status
        player.GetComponent<player_behavior>().slow = false;
        // reset player slow time
        player.GetComponent<player_behavior>().slow_time = 0.0f;
        // reset player progress counter
        progress_counter = 0;
        // reset player sent flag
        sent_flag = false;
        // reset gameover text
        GAMEOVER.gameObject.SetActive(false);
        // reset winner text
        WINNER.gameObject.SetActive(false);
        // reset restart text
        RESTART.gameObject.SetActive(false);
        // reset time scale
        Time.timeScale = 1.0f;

        // reset all obstacles
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(GameObject obstacle in obstacles){
            Destroy(obstacle);
        }
        // reset all boosters
        GameObject[] health_boosters = GameObject.FindGameObjectsWithTag("health_boost");
        foreach(GameObject booster in health_boosters){
            Destroy(booster);
        }
        GameObject[] stamina_boosters = GameObject.FindGameObjectsWithTag("stamina_boost");
        foreach(GameObject booster in stamina_boosters){
            Destroy(booster);
        }
        // reset all coins
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach(GameObject coin in coins){
            Destroy(coin);
        }
        // reset all flags
        GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");
        foreach(GameObject flag in flags){
            Destroy(flag);
        }

        // reset antlion
        antlion.transform.position = new Vector3(-11.08f, 0.11f, -0.0259f);
    }

}
