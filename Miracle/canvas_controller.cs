using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas_controller : MonoBehaviour
{
    public GameObject _canvas;
    // Start is called before the first frame update
    void Awake()
    {
        playercontroller.player_canvas = _canvas;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
