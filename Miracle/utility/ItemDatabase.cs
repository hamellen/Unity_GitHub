using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    GameObject player;//체력용
    Player_Status status;//체력용
    GameObject Applicator;//체력 이외의 이용
    Condition_applicator condition_Applicator;//체력 이외의 이용

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject fieldItemPrefab;
    public Vector3 pos;



    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        status = player.GetComponent<Player_Status>();
        Applicator = GameObject.FindWithTag("Condition_applicator");
        condition_Applicator = Applicator.GetComponent<Condition_applicator>();
        GameObject go=Instantiate(fieldItemPrefab, pos,Quaternion.identity);
        go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, 7)]);
    }

    public void Add_hp(float input)
    {
        if (status.current_hp < status.max_hp)
        {
            status.add_hp(input);
        }
        else if (status.current_hp == status.max_hp)
        {
            Debug.Log("체력이 가득입니다.");
        }
    }

    public void activate_effect(int index)//버프용
    {
        switch (index)
        {

            case 1://공격력
                condition_Applicator.Set_state(State.strength);
                condition_Applicator.Apply_state();
                Debug.Log("공격력 증가");
                break;
            case 2://공격속도 
                condition_Applicator.Set_state(State.quick);
                condition_Applicator.Apply_state();
                Debug.Log("공격속도 증가");
                break;
            case 3://방어력
                condition_Applicator.Set_state(State.solid);
                condition_Applicator.Apply_state();
                Debug.Log("방어력 증가");
                break;
            case 4://이동속도 
                condition_Applicator.Set_state(State.agility);
                condition_Applicator.Apply_state();
                Debug.Log("이동속도 증가");
                break;
            case 5://치명타율
                condition_Applicator.Set_state(State.focus);
                condition_Applicator.Apply_state();
                Debug.Log("치명타율 증가");
                break;
            case 6://회복
                condition_Applicator.Set_state(State.recovery);
                condition_Applicator.Apply_state();
                Debug.Log("회복 증가");
                break;
            case 7://고정데미지
                condition_Applicator.Set_state(State.fixed_attack);
                condition_Applicator.Apply_state();
                Debug.Log("고정데미지 적용");
                break;
        }
    }
    
}
