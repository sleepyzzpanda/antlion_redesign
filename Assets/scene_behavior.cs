using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene_behavior : MonoBehaviour
{
    
    public float floor_spawn_rate = 0.6f;
    private float floor_spawn_timer = 0.0f;
    // for obstacle 1
    public float obstacle_spawn_rate;
    private float obstacle_spawn_timer = 0.0f;
    public GameObject floor_tile;
    public GameObject player;
    public GameObject obstacle_1;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        // update speeds
        floor_tile.GetComponent<floor_behavior>().floor_speed = speed;
        obstacle_1.GetComponent<obstacle_behavior>().obstacle_speed = speed;
        player.GetComponent<player_behavior>().scroll_speed = speed;
        // check if player is alive
        if(player.GetComponent<player_behavior>().alive == false)
        {
            Time.timeScale = 0.0f;
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

        // generate obstacle 1
        if(obstacle_spawn_timer > obstacle_spawn_rate)
        {
            Instantiate(obstacle_1, new Vector3(10, -0.46f, 0), transform.rotation);
            obstacle_spawn_timer = 0.0f;
        }
        else
        {
            obstacle_spawn_timer += Time.deltaTime;
        }

    }
}
