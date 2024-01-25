using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swat_vehicle_controller : MonoBehaviour
{

    public AudioClip engine_sound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        audioSource.clip = engine_sound;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
