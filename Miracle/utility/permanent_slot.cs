using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class permanent_slot : MonoBehaviour
{

    public Text captured_monster;
    public GameObject player;
    public Player_Status status;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        status = player.GetComponent<Player_Status>();
        captured_monster = GetComponent<Text>();
    }

    public void set_captured_monster_number(int index)
    {
        captured_monster.text = status.number_hunted_creature[index].ToString();
    }
}
