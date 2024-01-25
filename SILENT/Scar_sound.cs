using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scar_sound : MonoBehaviour
{

    public AudioClip[] audioClips;//��ī ���� 

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip scar_sound)
    {
        //audioSource.Stop();
        audioSource.clip = scar_sound;
        audioSource.Play();
    } 


    // Update is called once per frame
    void Update()
    {
        
    }
}
