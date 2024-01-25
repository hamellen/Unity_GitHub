using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dead_event : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void player_dead()
    {
        SceneManager.LoadScene("Start_scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
