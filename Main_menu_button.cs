using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Main_menu_button : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject main_menu, howtoplay;
   


   
    public void START_GAME()
    {
        SceneManager.LoadScene("game_start");

    }

    public void END_GAME()
    {
        
        Application.Quit(); // 어플리케이션 종료

    }
    
    public void change_to_second_canvas()
    {
        
        main_menu.SetActive(false);
        howtoplay.SetActive(true);
    }

    public void back_to_first_canvas()
    {
        
        main_menu.SetActive(true);
        howtoplay.SetActive(false);
    }

}
