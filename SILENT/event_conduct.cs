using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class event_conduct : MonoBehaviour
{
    public AudioSource audio;
    public GameObject hellicopter_supply, first_supply, second_supply;
    // Start is called before the first frame update
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        InvokeRepeating("Repeat_hellicopter_supply", 0f, 120f);


    }

    public void Repeat_hellicopter_supply()
    {
        hellicopter_supply.GetComponent<gunship_controller>().first_position = first_supply.transform;
        hellicopter_supply.GetComponent<gunship_controller>().second_position = second_supply.transform;
        Instantiate(hellicopter_supply, first_supply.transform.position, Quaternion.identity);
    }

}
