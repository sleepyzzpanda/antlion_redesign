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
    // tutorial objects
    public GameObject tutorial_1, tutorial_2, tutorial_3, tutorial_4, tutorial_5, tutorial_6, tutorial_7, START_screen;
    public GameObject obstacle_1, falling_rock, thorns;
    public GameObject health_booster, stamina_booster, coin_booster, cobweb, tumbleweed, flag;
    private float speed;
    public Text HP, STAMINA, COINS, PROGRESS;
    public GameObject GAMEOVER, WINNER, RESTART, progress_icon;
    public GameObject hp1, hp2, hp3, hp4, hp5;
    public GameObject stam1, stam2, stam3, stam4, stam5;
    public Sprite HPACTIVE, HPINACTIVE, STAMACTIVE, STAMINACTIVE;
    private int progress_counter, tutorial_counter;
    private bool sent_flag, tutorial;
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

        // set all UI text to inactive
        HP.gameObject.SetActive(false);
        STAMINA.gameObject.SetActive(false);
        COINS.gameObject.SetActive(false);
        PROGRESS.gameObject.SetActive(false);
                
        // pause all game components
        Time.timeScale = 0.0f;
        tutorial = true;   
        tutorial_counter = 0;
        // init ant icon
        progress_icon.transform.position = new Vector3(-8.31f, 6.41f, -1.14f);
    }

    // Update is called once per frame
    void Update()
    {
        // check if tutorial is playing
        if(tutorial){
            switch(tutorial_counter){
                case 0:
                    START_screen.gameObject.SetActive(true);
                    break;
                case 1:
                    START_screen.gameObject.SetActive(false);
                    tutorial_1.gameObject.SetActive(true);
                    break;
                case 2:
                    tutorial_1.gameObject.SetActive(false);
                    tutorial_2.gameObject.SetActive(true);
                    break;
                case 3:
                    tutorial_2.gameObject.SetActive(false);
                    tutorial_3.gameObject.SetActive(true);
                    break;
                case 4:
                    tutorial_3.gameObject.SetActive(false);
                    tutorial_4.gameObject.SetActive(true);
                    break;
                case 5:
                    tutorial_4.gameObject.SetActive(false);
                    tutorial_5.gameObject.SetActive(true);
                    break;
                case 6:
                    tutorial_5.gameObject.SetActive(false);
                    tutorial_6.gameObject.SetActive(true);
                    break;
                case 7:
                    tutorial_6.gameObject.SetActive(false);
                    tutorial_7.gameObject.SetActive(true);
                    break;
                case 8:
                    tutorial_7.gameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                    tutorial = false;
                    // HP.gameObject.SetActive(true);
                    // STAMINA.gameObject.SetActive(true);
                    COINS.gameObject.SetActive(true);
                    // PROGRESS.gameObject.SetActive(true);
                    reset_scene();
                    break;
            }
            // increase counter when player presses C
            if(Input.GetKeyDown(KeyCode.C)){
                tutorial_counter += 1;
            }
        }
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

        // update player HP and stamina
        updateHP();
        updateSP();

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
                    // random y value between -0.46 and 1.0
                    float ypos = Random.Range(-0.46f, 1.0f);
                    for(int i = 0; i < coin_count; i++)
                    {
                        // offset the xpos by 0.5
                        Instantiate(coin_booster, new Vector3(12 + (i * 0.5f), ypos, 0), transform.rotation);
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
                Instantiate(falling_rock, new Vector3(10, 6.75f, 0), transform.rotation);
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

        // update progress icon
        progress_icon.transform.position = new Vector3(-8.31f + (progress_counter * 0.0292f), 6.41f, -1.14f);
        

        // update UI
        // HP.text = "HP: " + player.GetComponent<player_behavior>().health;
        // STAMINA.text = "STAMINA: " + player.GetComponent<player_behavior>().stamina;
        COINS.text = "COINS: " + player.GetComponent<player_behavior>().coins;
        // PROGRESS.text = "PROGRESS: " + progress_counter + "%";

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
        antlion.transform.position = new Vector3(-16.0f, 0.9f, 0.0f);
        // reset progress icon
        progress_icon.transform.position = new Vector3(-8.31f, 6.41f, -1.14f);

    }

    void updateHP(){
        // set sprite to active ver or inactive ver based on player health
        switch(player.GetComponent<player_behavior>().health){
            case 1:
                hp1.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp2.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                hp3.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                hp4.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                hp5.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                break;
            case 2:
                hp1.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp2.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp3.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                hp4.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                hp5.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                break;
            case 3:
                hp1.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp2.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp3.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp4.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                hp5.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                break;
            case 4:
                hp1.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp2.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp3.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp4.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp5.GetComponent<SpriteRenderer>().sprite = HPINACTIVE;
                break;
            case 5:
                hp1.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp2.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp3.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp4.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                hp5.GetComponent<SpriteRenderer>().sprite = HPACTIVE;
                break;
        }
    }

    void updateSP(){
        // max 10 stamina, 5 icons
        int sp = player.GetComponent<player_behavior>().stamina;
        sp = sp / 2;
        // set sprite to active ver or inactive ver based on player stamina
        switch(sp){
            case 1:
                stam1.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam2.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam3.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam4.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam5.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                break;
            case 2:
                stam1.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam2.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam3.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam4.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam5.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                break;
            case 3:
                stam1.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam2.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam3.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam4.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam5.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                break;
            case 4:
                stam1.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam2.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam3.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam4.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam5.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                break;
            case 5:
                stam1.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam2.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam3.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam4.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                stam5.GetComponent<SpriteRenderer>().sprite = STAMACTIVE;
                break;

            case 0:
                stam1.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam2.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam3.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam4.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                stam5.GetComponent<SpriteRenderer>().sprite = STAMINACTIVE;
                break;
        }
    }        
}

