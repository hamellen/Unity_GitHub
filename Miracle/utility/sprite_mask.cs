using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite_mask : MonoBehaviour
{

    private GameObject player;
    Transform mask_transform;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        mask_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
    }
}
