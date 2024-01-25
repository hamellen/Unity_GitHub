using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunship_controller : MonoBehaviour
{

    public GameObject Airdrop_prefab;
    public Transform first_position, second_position;
    public Vector3 movedirection,middle_point;
    public float gunship_speed;
    AudioSource audioSource;
    // Start is called before the first frame update

    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();
    }

    void Start()//해당 방향을 보도록 회전 조치 
    {
        Invoke("Auto_destroy", 20f);
        Invoke("create_airdrop", 5f);
        audioSource.Play();
        transform.position = first_position.position;
        movedirection = second_position.position - first_position.position;
        //movedirection.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(movedirection);
        GameObject.FindWithTag("hellicopter_mainbody").transform.rotation = targetRotation;
        middle_point = calculate_middle();
    }

    // Update is called once per frame
    void Update()//이동 
    {
       

        if (transform.position == middle_point)
        {
            Instantiate(Airdrop_prefab, transform.position, Quaternion.identity);
        }

        
       
        transform.position += Time.deltaTime * gunship_speed * movedirection;

    }

    public void Auto_destroy()
    {
        Destroy(gameObject);
    }

    public void create_airdrop()
    {
        Instantiate(Airdrop_prefab, transform.position, Quaternion.identity);
    }

    public Vector3 calculate_middle()
    {
        Vector3 distance = second_position.position - first_position.position;
        return first_position.position + (distance / 2);

    }
}
