using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    GameObject player;//ü�¿�
    Player_Status status;//ü�¿�
    GameObject Applicator;//ü�� �̿��� �̿�
    Condition_applicator condition_Applicator;//ü�� �̿��� �̿�

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
            Debug.Log("ü���� �����Դϴ�.");
        }
    }

    public void activate_effect(int index)//������
    {
        switch (index)
        {

            case 1://���ݷ�
                condition_Applicator.Set_state(State.strength);
                condition_Applicator.Apply_state();
                Debug.Log("���ݷ� ����");
                break;
            case 2://���ݼӵ� 
                condition_Applicator.Set_state(State.quick);
                condition_Applicator.Apply_state();
                Debug.Log("���ݼӵ� ����");
                break;
            case 3://����
                condition_Applicator.Set_state(State.solid);
                condition_Applicator.Apply_state();
                Debug.Log("���� ����");
                break;
            case 4://�̵��ӵ� 
                condition_Applicator.Set_state(State.agility);
                condition_Applicator.Apply_state();
                Debug.Log("�̵��ӵ� ����");
                break;
            case 5://ġ��Ÿ��
                condition_Applicator.Set_state(State.focus);
                condition_Applicator.Apply_state();
                Debug.Log("ġ��Ÿ�� ����");
                break;
            case 6://ȸ��
                condition_Applicator.Set_state(State.recovery);
                condition_Applicator.Apply_state();
                Debug.Log("ȸ�� ����");
                break;
            case 7://����������
                condition_Applicator.Set_state(State.fixed_attack);
                condition_Applicator.Apply_state();
                Debug.Log("���������� ����");
                break;
        }
    }
    
}
