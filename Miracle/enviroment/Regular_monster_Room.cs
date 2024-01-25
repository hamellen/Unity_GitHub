using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_monster_Room : MonoBehaviour
{
    public GameObject  field_leftdowm, field_rightup;//룸의 센터 중앙,룸의 가장 왼쪽 아래,룸의 가장오른쪽 위
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
        number_monster = monsters.Length;//각 필드에 생성될 몬스터 프리팹수 
        number_starting_point = monster_starting_points.Length;//몬스터가 소환될 각 자리수 
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
        if (alive_number_monster == 0)//룸 돌파 성공으로 모든 포탈 활성화 
        {
            Clear_monster_room();
        }

        Check_monsters();
    }

    void Check_monsters()
    {
        
        Collider2D[] colls = Physics2D.OverlapAreaAll(field_leftdowm.transform.position, field_rightup.transform.position, enemylayer);//범위에 존재하는 모든 콜라이더 감지 
        alive_number_monster = colls.Length;
    }

    void Clear_monster_room()//일반 몬스터룸 성공
    {
        IsClear = true;
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].SetActive(true);
        }
    }
}
