using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deepforest_enviroment : MonoBehaviour
{
    public GameObject[] left_teleports,right_teleports;
    public int left_teleport_number;
    

    [SerializeField] GameObject condition_applicator;
    [SerializeField] Condition_applicator applicator;


    // Start is called before the first frame update
    void Awake()
    {
        condition_applicator = GameObject.FindWithTag("Condition_applicator");
        applicator = condition_applicator.GetComponent<Condition_applicator>();
        InvokeRepeating("apply_toxin", 10f, 10f);//ó�� ������ 3���� 5�ʸ��� �ߵ�
    }

    // Update is called once per frame
     void OnDestroy()
    {
        CancelInvoke("apply_toxin");

        applicator.Init_state(9);
    }

    void Start()
    {
        left_teleports= GameObject.FindGameObjectsWithTag("Normal_teleport_left");
        right_teleports = GameObject.FindGameObjectsWithTag("Normal_teleport_right");
        
        Set_teleport();
    }

    public void apply_toxin() {
        applicator.Set_state(State.toxin);
        applicator.Apply_state();
    }

    void Set_teleport()
    {
        List<int> selected_numbers = new List<int>();
        int selected_number;
        List<GameObject> List_left_teleports = left_teleports.ToList();
        List<GameObject> List_right_teleports = right_teleports.ToList();
        for (int i = 0; i < List_left_teleports.Count; i++)//�����̱� 46ȸ �ݺ�,15���� �ʵ� �� ������ �Ա��� ���� 1���� ��Ż�� ������ �� 46��,���� ���������� �̾����� ��Ż�� ���� 
        {
            do
            {
                selected_number = Random.Range(0, List_left_teleports.Count);
            } while (selected_numbers.Contains(selected_number)|| i==selected_number);

            selected_numbers.Add(selected_number);

            left_teleports[i].GetComponent<Teleport>().opposite_teleport = right_teleports[selected_number];
            right_teleports[selected_number].GetComponent<Teleport>().opposite_teleport = left_teleports[i];

        }
    }
}
