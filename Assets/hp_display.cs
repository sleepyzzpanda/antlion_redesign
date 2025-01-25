using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_display : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = "HP: " + player.GetComponent<player_behavior>().health;
        
    }
}
