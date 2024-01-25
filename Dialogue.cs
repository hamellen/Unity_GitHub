using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text_mesh;
    int index=0;
    public string[] dialogue_sentence = new string[4];
    void Start()
    {
        
    }

    public void start_dialogue()
    {
        if (index == 4)
        {
            text_mesh.text ="";
            index = 0;
            return;
        }


        text_mesh.text = dialogue_sentence[index];
        index++;



    }

    // Update is called once per frame
    void Update()
    {
       


    }
}
