using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class END_BUTTON : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Back_to_menu()
    {
        SceneManager.LoadScene("Start_scene");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
