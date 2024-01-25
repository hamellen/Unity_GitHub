using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Angel_long_attack_state { blue,green,red};

public class angel_long_attack_move : MonoBehaviour
{
    public GameObject Player;
    public bool IsPlayerRight,IsPlayerUp,Is_x_same,Is_y_same,IsMove;
    public SpriteRenderer render;
    public Animator animator;
    Vector3 direction;
    public float movespeed;
    public Angel_long_attack_state angel_long_attack_state;
    public GameObject condition_applicator_object;
    private Condition_applicator condition_applicator;

    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        condition_applicator_object = GameObject.FindWithTag("Condition_applicator");
        condition_applicator = condition_applicator_object.GetComponent<Condition_applicator>();
    }

    private void Start()
    {
        Invoke("Self_destroy", 3f);//생성 3초뒤 스스로 자멸
        
    }
    public void Self_destroy()//플레이어에게 피격되거나 몇 초뒤 스스로 사라짐 
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        direction = Player.transform.position - this.transform.position;
        this.transform.position += direction.normalized * movespeed;
        
        Check_x();
        render.flipX = IsPlayerRight;
        Check_y();

        if (Is_y_same == true)//높이가 같을경우
        {
            animator.SetTrigger("long_attack_left");
        }
        else if(Is_y_same == false)
        {
            if (IsPlayerUp == true)
            {
                if (Is_x_same == true)
                {
                    animator.SetTrigger("long_attack_up");
                }
                else if (Is_x_same == false)
                {
                    animator.SetTrigger("long_attack_leftup");
                }
            }
            else if(IsPlayerUp == false)
            {
                if (Is_x_same == true)
                {
                    animator.SetTrigger("long_attack_down");
                }
                else if (Is_x_same == false)
                {
                    animator.SetTrigger("long_attack_leftdown");
                }
            }
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))//플레이어와의 접촉 
        {
            if(angel_long_attack_state== Angel_long_attack_state.blue)
            {
                condition_applicator.Set_state(State.deceleration);
                condition_applicator.Apply_state();
            }
            else if(angel_long_attack_state == Angel_long_attack_state.green)
            {
                condition_applicator.Set_state(State.toxin);
                condition_applicator.Apply_state();
            }
            else if (angel_long_attack_state == Angel_long_attack_state.red)
            {
                condition_applicator.Set_state(State.burn);
                condition_applicator.Apply_state();
            }
        }
    }


    public void Check_x()
    {
        if (Player.transform.position.x > this.transform.position.x)
        {
            IsPlayerRight = true;
            Is_x_same = false;
        }
        else if (Player.transform.position.x < this.transform.position.x)
        {
            IsPlayerRight = false;
            Is_x_same = false;
        }
        else if (Player.transform.position.x == this.transform.position.x)
        {
            IsPlayerRight = false;
            Is_x_same = true;
        }
    }
    public void Check_y()
    {
        if (Player.transform.position.y < this.transform.position.y)
        {
            IsPlayerUp = true;
            Is_y_same = false;
        }
        else if (Player.transform.position.y > this.transform.position.y)
        {
            IsPlayerUp = false;
            Is_y_same = false;
        }
        else if (Player.transform.position.y == this.transform.position.y)
        {
            IsPlayerUp = false;
            Is_y_same = true;
        }
    }
}
