using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Desert_enviroment : MonoBehaviour
{
    public GameObject[] left_teleports, right_teleports;
    public int left_teleport_number;
    public GameObject first_teleport, second_teleport;
    // Start is called before the first frame update
    void Start()
    {
        left_teleports = GameObject.FindGameObjectsWithTag("Normal_teleport_left");
        right_teleports = GameObject.FindGameObjectsWithTag("Normal_teleport_right");
        Set_teleport();
    }


    void Set_teleport()
    {
        List<int> selected_numbers = new List<int>();
        int selected_number;
        List<GameObject> List_left_teleports = left_teleports.ToList();
        List<GameObject> List_right_teleports = right_teleports.ToList();
        for (int i = 0; i < List_left_teleports.Count; i++)// 
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
