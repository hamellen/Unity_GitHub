using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle_Boss_Room : MonoBehaviour
{
    public GameObject field_leftdowm, field_rightup;
    public GameObject[] portals;
    public int alive_middle_monster;
    public LayerMask middle_boss_layer;
    public bool IsClear;
    public GameObject middle_boss_starting;//Ω√¿€¡°
    public GameObject middle_boss;
    // Start is called before the first frame update
    void Start()
    {

        IsClear = false;
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].SetActive(false);
        }
        Instantiate(middle_boss, middle_boss_starting.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (alive_middle_monster == 0)
        {
            Clear_middle_boss_room();
        }
        Check_middle_monster();
    }

    void Check_middle_monster()
    {
        Collider2D[] colls = Physics2D.OverlapAreaAll(field_leftdowm.transform.position, field_rightup.transform.position, middle_boss_layer);
        alive_middle_monster = colls.Length;
    }

    void Clear_middle_boss_room()
    {
        IsClear = true;
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].SetActive(true);
        }
    }
}
