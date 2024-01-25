using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke_creator : MonoBehaviour
{
    GameObject player;
    Animator animator;
    public int smoke_vertical_figure;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        
    }
    private void Update()
    {
       
    }
    public void activate()
    {
        this.transform.position = player.transform.position+new Vector3(0, smoke_vertical_figure, 0);
        animator.SetTrigger("Change_class");
    }
}
