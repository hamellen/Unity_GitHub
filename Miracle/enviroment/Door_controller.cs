using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door_controller : MonoBehaviour
{
    public GameObject player;
    public int current_portal_number;
    // Start is called before the first frame update
    void Awake()
    {
        player= GameObject.Find("Player");
        current_portal_number=player.GetComponent<Player_Status>().portal_number;

        switch (current_portal_number)
        {
            case 0:
                gameObject.tag = "Teleport_Field_normal";
                break;

            case 1:
                gameObject.tag = "Teleport_Forest_normal";

                break;

            case 2:
                gameObject.tag = "Teleport_Desert_normal";

                break;
            case 3:
                gameObject.tag = "Teleport_Lake_normal";

                break;

            case 4:
                gameObject.tag = "Teleport_Deepforest_normal";
                break;
        }

    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
