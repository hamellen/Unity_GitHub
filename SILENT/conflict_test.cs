using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conflict_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("�浹����");
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
