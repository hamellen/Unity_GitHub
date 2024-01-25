using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Updating_coin : MonoBehaviour
{
    GameObject player;
    Player_Status status;
    public TextMeshProUGUI current_coin;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        status = player.GetComponent<Player_Status>();
        status.onChangecoin += Change_coin;
    }

    public void Change_coin()
    {
        current_coin.text = status.pocket_money.ToString();
    }
}
