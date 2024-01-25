using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horror_doll_cs : MonoBehaviour
{

    public GameObject player;
    public AudioSource audioSource;
    public AudioClip kid_clip;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = kid_clip;
        player = GameObject.FindWithTag("Player");
    }

    public void interaction_complete()
    {
        
        Destroy(gameObject);
        audioSource.Play();
        player.gameObject.GetComponent<collect_quest>().current_collected_doll++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
