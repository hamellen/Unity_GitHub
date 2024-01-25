using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum boss_types { golem,death,angel,skeleton,dragon}


public class boss_status : MonoBehaviour
{
    public float[] current_valid_statetime = new float[3];//냉기,감속,냉각
    public int[] current_validnumber_state = new int[3]; //0=감속,1=냉기,2=냉각
    public IEnumerator[] enumerators = new IEnumerator[3];
    public int current_burn = 0;
   

    [Header("초기값 적용")]
    public float hp, offensive_power, defensive_power, attack_speed, move_speed;//체력,공격력,방어력,공격속도,이동속도

    public float initial_hp;//초기체력
    private float early_move_speed, early_attack_speed;//입력한 초기값 저장 
    private float damage_magnification;//데미지 배율
    public float remained_hp_percent;
   
    private Player_Status player_status;
    private Boss_move enemy_move;
    private GameObject Object_enemy_applicator, Player, Color_selector;
    private Boss_Condition_applicator boss_applicator;
    private Rito.WeightedRandomPicker<bool> wrPicker;
    private Color_Attack color_attack;
    public Animator boss_animator;
    public boss_types boss_type;

    private void Awake()
    {
       

        enemy_move = GetComponent<Boss_move>();
        Object_enemy_applicator = GameObject.FindWithTag("Boss_Condition_applicator");
        boss_applicator = Object_enemy_applicator.GetComponent<Boss_Condition_applicator>();
        Player = GameObject.FindWithTag("Player");
        player_status = Player.GetComponent<Player_Status>();
        Color_selector = GameObject.FindWithTag("Color_Selector");
        color_attack = Color_selector.GetComponent<Color_Attack>();
        boss_animator = GetComponent<Animator>();
    }
    void Start()
    {
        initial_hp = hp;
        early_move_speed = move_speed;
        early_attack_speed = attack_speed;

        
        wrPicker = new Rito.WeightedRandomPicker<bool>();
    }

    // Update is called once per frame
    void Update()
    {
        remained_hp_percent = ( hp / initial_hp) * 100;
        enemy_move.movespeed = (int)this.move_speed;

        if (hp <= 0)
        {
            boss_Dead_animation();
        }
    }
    public void boss_Dead_animation()
    {
        
        boss_animator.SetTrigger("Death");

        switch (boss_type)
        {

            case boss_types.death://데스브링거 처치시 
                player_status.portal_number = 1;
                break;

            case boss_types.golem://골렘 처치시 
                player_status.portal_number = 2;
                break;
            case boss_types.skeleton://해골 처치시 
                player_status.portal_number = 3;
                break;
            case boss_types.dragon://용 처치시 
                player_status.portal_number = 4;
                break;
        }

        Invoke("Set_Village", 2f);
        
    }

    public void Set_Village()
    {
        SceneManager.LoadScene("Village");
    }

    public void bringer_Dead()
    {
        Destroy(gameObject);
    }

    public void reduce_hp_1()//화상,방어력 비례
    {
        this.hp -= 10 - (defensive_power / 10);
    }
    public void continuous_decline_hp(int i)//화상,독
    {
        StartCoroutine("recover_reduce_hp", i);
    }
    IEnumerator recover_reduce_hp(int i)//화상
    {
        if (i == 1)
        {
            current_burn = 1;
            InvokeRepeating("reduce_hp_1", 3f, 5f);
            yield return new WaitForSeconds(60.0f);
            current_burn = 0;
            CancelInvoke("reduce_hp_1");
        }
    }

    public void reduce_attack_speed(int input)//감속
    {

        this.attack_speed -= input;
    }

    public void reduece_move_speed(int input)//냉기
    {
        this.move_speed -= input;
    }
    public void init_attack_speed()//감속 초기화 
    {
        this.attack_speed = early_attack_speed;
    }
    public void init_move_speed()//민첩 초기화 
    {
        this.move_speed = early_move_speed;
    }
    public void calculate()
    {



        wrPicker.Add(

            (true, player_status.critical + 1),
            (false, 100 - player_status.critical)
            );

        if (wrPicker.GetRandomPick())
        {
            damage_magnification = 1.2f;//최종데미지 배율 1.2
        }
        else if (!wrPicker.GetRandomPick())
        {
            damage_magnification = 1.0f;
        }

        wrPicker.Remove(true);
        wrPicker.Remove(false);
    }

    public void real_damage_apply()
    {

        if (player_status.Is_Fixed_damage == true)
        { //고정데미지 일시 

            hp -= player_status.offensive_power * damage_magnification;
            Debug.Log($"고정데미지 :{player_status.offensive_power * damage_magnification}");
            Game_manager.instance.Apply_damaged_real_figure(gameObject.transform, (int)((int)player_status.offensive_power * damage_magnification));
        }
        else if (player_status.Is_Fixed_damage == false)//고정데미지 아닐시 
        {
            float damage = player_status.offensive_power - (defensive_power / 5);
            hp -= damage * damage_magnification;
            Debug.Log($"일반데미지 :{damage * damage_magnification}");
            Game_manager.instance.Apply_damaged_real_figure(gameObject.transform, (int)((int)damage * damage_magnification));
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)//플레이어의 공격에 당할경우 
    {
        //Debug.Log(Player + "탐지됨 ");
        //Debug.Log(Player.CompareTag("Player_skilling") + " player_skilling 탐지됨 ");
        if (Player.CompareTag("Player_skilling"))//플레이어가 공격중일때 
        {
            
            calculate();//데미지 배율 계산
            boss_animator.SetTrigger("Hited");//피격 모션 활성화
            real_damage_apply();//데미지 실체력 적용 

            if (color_attack.selected_color == Color_mode.red)
            {
                boss_applicator.Set_status(this);
                boss_applicator.Set_Enemy_State(Boss_Enemy_State.burn);
                boss_applicator.Apply_state();
            }
            else if (color_attack.selected_color == Color_mode.blue)
            {
                boss_applicator.Set_status(this);
                boss_applicator.Set_Enemy_State(Boss_Enemy_State.cooling);
                boss_applicator.Apply_state();
            }
            else if (color_attack.selected_color == Color_mode.purple)
            {
                boss_applicator.Set_status(this);
                boss_applicator.Set_Enemy_State(Boss_Enemy_State.deceleration);
                boss_applicator.Apply_state();
            }

            player_status.add_recovery();//몬스터한테 공격 적중시 회복 적용 
        }


    }
}
