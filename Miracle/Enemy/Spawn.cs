using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject rock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void Spawn_activate()
    {
        Instantiate(rock, transform.position, Quaternion.identity);
        
    }

    
   
}
