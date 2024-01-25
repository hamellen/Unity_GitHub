using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsClear;
    public GameObject portal_1, portal_2, portal_3;
    void Awake()
    {
        IsClear = false;
        portal_1.SetActive(false);
        portal_2.SetActive(false);
        portal_3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Room_clear()//해당 룸의 모든 과제 클리어시 
    {
        IsClear = true;
        portal_1.SetActive(true);
        portal_2.SetActive(true);
        portal_3.SetActive(true);
    }
}
