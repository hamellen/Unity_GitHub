using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Room : MonoBehaviour
{
    public bool IsClear;//ÆÛÁñ·ë Å¬¸®¾î ¿©ºÎ 
    public GameObject[] portals;
    // Start is called before the first frame update
    void Start()
    {
        IsClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear_puzzle_Room()
    {
        IsClear = true;
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].SetActive(true);
        }
    }
}
