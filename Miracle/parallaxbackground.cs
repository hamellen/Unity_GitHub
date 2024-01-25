using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxbackground : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float movespeed = 0.1f;
    private Material material;



    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetTextureOffset("_MainTex", Vector2.right * movespeed * Time.time);
    }
}
