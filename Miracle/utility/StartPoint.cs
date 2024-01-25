using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

   
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //player= GameObject.FindWithTag("Player");
        player = playercontroller.player_object;
        Debug.Log(player);
        if (player != null)//시작 위치 설정
        {
            player.transform.position = this.transform.position;
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
