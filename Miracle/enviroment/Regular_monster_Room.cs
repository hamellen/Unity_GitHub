using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_monster_Room : MonoBehaviour
{
    public GameObject  field_leftdowm, field_rightup;//���� ���� �߾�,���� ���� ���� �Ʒ�,���� ��������� ��
    public bool IsClear;
    public GameObject[] monster_starting_points;
    public GameObject[] monsters;
    public GameObject[] portals;
    public int number_monster, number_starting_point, random_monster_index, alive_number_monster, number_portal;

    [SerializeField]
    private LayerMask enemylayer;

    // Start is called before the first frame update
    void Start()
    {
        number_monster = monsters.Length;//�� �ʵ忡 ������ ���� �����ռ� 
        number_starting_point = monster_starting_points.Length;//���Ͱ� ��ȯ�� �� �ڸ��� 
        number_portal = portals.Length;
        IsClear = false;
        for (int i=0;i< number_starting_point; i++)
        {
            random_monster_index = Random.Range(0, number_monster);
            Instantiate(monsters[random_monster_index], monster_starting_points[i].transform.position, Quaternion.identity);
        }
        for(int i=0;i< number_portal; i++)
        {
            portals[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (alive_number_monster == 0)//�� ���� �������� ��� ��Ż Ȱ��ȭ 
        {
            Clear_monster_room();
        }

        Check_monsters();
    }

    void Check_monsters()
    {
        
        Collider2D[] colls = Physics2D.OverlapAreaAll(field_leftdowm.transform.position, field_rightup.transform.position, enemylayer);//������ �����ϴ� ��� �ݶ��̴� ���� 
        alive_number_monster = colls.Length;
    }

    void Clear_monster_room()//�Ϲ� ���ͷ� ����
    {
        IsClear = true;
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].SetActive(true);
        }
    }
}
