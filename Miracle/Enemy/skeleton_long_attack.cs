using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_long_attack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Self_destroy()//플레이어에게 피격되거나 몇 초뒤 스스로 사라짐 
    {
        Destroy(this.gameObject);
    }
}
