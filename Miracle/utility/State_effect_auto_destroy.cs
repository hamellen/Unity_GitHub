using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_effect_auto_destroy : MonoBehaviour
{
    void Destroy_state_effect()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy_state_effect", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
