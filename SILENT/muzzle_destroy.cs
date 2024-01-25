using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzzle_destroy : MonoBehaviour
{

    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Auto_destroy", lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        //Destroy(collision.gameObject);
    }

    public void Auto_destroy()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
