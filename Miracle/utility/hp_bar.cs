using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_bar : MonoBehaviour
{
    public GameObject player;
    float hp_percent;
    public Image bar;

    void Start()
    {
        

    }

    void Update()
    {
        hp_percent = player.GetComponent<Player_Status>().current_hp / player.GetComponent<Player_Status>().max_hp;
        bar.fillAmount = hp_percent;
        
    }
}
