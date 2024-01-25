using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cooltime_manager : MonoBehaviour
{

    GameObject icon, player,cooltime_object;
    Player_Status status;

    private TextMeshPro cooltime;

    

    // Start is called before the first frame update
    void Start()
    {
        icon = GameObject.FindGameObjectWithTag("icon");
        player = GameObject.FindGameObjectWithTag("Player");
        status = player.GetComponent<Player_Status>();
        cooltime_object = icon.transform.GetChild(0).gameObject;
        cooltime = cooltime_object.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
