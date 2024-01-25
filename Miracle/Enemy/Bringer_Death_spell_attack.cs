using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bringer_Death_spell_attack : MonoBehaviour
{
    public Animator spell_animator;

    private void Awake()
    {
        spell_animator = GetComponent<Animator>();
    }
    void Start()
    {
        spell_animator.SetTrigger("Spell_attack");
        Invoke("Auto_destroy_spell", 10f);//10�ʵ� �ڵ� ���� 
    }

    public void Auto_destroy_spell()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
