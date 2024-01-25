using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Random_number : MonoBehaviour
{
    public List<int> lotto = new List<int>() { 1, 2, 5, 7, 8, 9, 20 };
    int iSelect;
    // Start is called before the first frame update
    void Start()
    {
        Gacha();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Gacha()
    {
        for(int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, lotto.Count);
            Debug.Log(lotto[rand]);
            lotto.RemoveAt(rand);
        }
    }
}
