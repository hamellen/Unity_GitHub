using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lake_enviroment : MonoBehaviour
{
    public GameObject[] left_teleports, right_teleports;
    public int left_teleport_number;


    [SerializeField] GameObject condition_applicator;

    // Start is called before the first frame update
    void Awake()
    {
        condition_applicator = GameObject.FindWithTag("Condition_applicator");
        condition_applicator.SetActive(false);//플레이어 상태적용기 비활성화 
    }
    void Start()
    {
        left_teleports = GameObject.FindGameObjectsWithTag("Normal_teleport_left");
        right_teleports = GameObject.FindGameObjectsWithTag("Normal_teleport_right");
        Set_teleport();
    }

    void OnDestroy()
    {
        condition_applicator.SetActive(true);//플레이어 상태적용기 활성화 
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void Set_teleport()
    {
        List<int> selected_numbers = new List<int>();
        int selected_number;
        List<GameObject> List_left_teleports = left_teleports.ToList();
        List<GameObject> List_right_teleports = right_teleports.ToList();
        for (int i = 0; i < List_left_teleports.Count; i++)//랜덤뽑기 46회 반복,15개의 필드 랑 보스방 입구로 들어서는 1개의 포탈을 포함해 총 46개,실질 보스방으로 이어지는 포탈은 예외 
        {
            do
            {
                selected_number = Random.Range(0, List_left_teleports.Count);
            } while (selected_numbers.Contains(selected_number) || i == selected_number);

            selected_numbers.Add(selected_number);

            left_teleports[i].GetComponent<Teleport>().opposite_teleport = right_teleports[selected_number];
            right_teleports[selected_number].GetComponent<Teleport>().opposite_teleport = left_teleports[i];

        }
    }
}
