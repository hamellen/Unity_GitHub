using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airdrop_controller : MonoBehaviour
{
    public int additional_bullet;
    public float additional_hp;
    public AudioClip item_sound;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = item_sound;
    }

    public void Play_item_sound()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
