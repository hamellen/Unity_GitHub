using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Select : MonoBehaviour
{

    public GameObject create;//새 캐릭 생성 창
    public Text[] slotText;
    public Text newPlayerName;




    bool[] savefile=new bool[3];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++) 
        {
            if (File.Exists(Game_manager.instance.path + $"{i}"))
            {
                savefile[i] = true;
                Game_manager.instance.nowSlot = i;
                Game_manager.instance.LoadData();
                slotText[i].text = Game_manager.instance.now_player.player_name;
               
            }
            else
            {
                slotText[i].text = "비어있음";

            }
        }
        Game_manager.instance.DataClear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slot(int selected_number)
    {
        Game_manager.instance.nowSlot = selected_number;

        if (savefile[selected_number])
        {
            Game_manager.instance.LoadData();
            link_game();
        }
        else
        {
            Create();
        }
        

    }

    public void Create() {
        create.gameObject.SetActive(true);//이름입력창 활성화 
    }

    public void link_game() {

        if (!savefile[Game_manager.instance.nowSlot])
        {
            Game_manager.instance.now_player.player_name = newPlayerName.text;//이름 입력하고 게임 접속 
            Game_manager.instance.now_player.player_status = new Player_Status();
            Game_manager.instance.SaveData();
        }

        
        SceneManager.LoadScene("Village");
    }
}
