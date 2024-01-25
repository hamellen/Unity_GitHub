using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;



public class PlayerData //플레이어 이름,플레이어스테이터스,도감상황,무기
{
    public string player_name;
    public Player_Status player_status;
    public Inventory inventory;
}


public class Game_manager : MonoBehaviour
{
    //RingMenu
    public RingMenu MainMenuPrefab;
    protected RingMenu MainMenuInstance;




    //single 
    public static Game_manager instance;

    public PlayerData now_player = new PlayerData();

    public string path;
    public int nowSlot;


    public GameObject damaged_text;//데미지 출력 text

    public void Awake()
    {
        path = Application.persistentDataPath+ "/save_file";

        if (instance == null)
        {
            instance = this;
        }
        /*else if (instance != this)
        {
            Destroy(instance.gameObject);
        }*/


        

        
    }

    // Start is called before the first frame update
    void Start()
    {
        //inventoryPanel.SetActive(activeInventory);
    }

    public void Apply_damaged_real_figure(Transform input_transform, int input_figure)
    {
        damaged_text.SetActive(true);
        damaged_text.GetComponent<monster_hited_damaged>().apply_figure(input_transform, input_figure);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            MainMenuInstance = Instantiate(MainMenuPrefab, FindAnyObjectByType<Canvas>().transform);
            Time.timeScale = 0.3f;
            Time.timeScale = 0.3f;
        }
    }

    

    public void SaveData() {
        
        
        string data = JsonUtility.ToJson(now_player);

        File.WriteAllText(path+nowSlot.ToString(), data);
    }

    public void LoadData() 
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        now_player=JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        now_player = new PlayerData();

        
    }

   
}
