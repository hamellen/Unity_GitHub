using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum boss_types { golem,death,angel,skeleton,dragon}


public class boss_status : MonoBehaviour
{
    public float[] current_valid_statetime = new float[3];//�ñ�,����,�ð�
    public int[] current_validnumber_state = new int[3]; //0=����,1=�ñ�,2=�ð�
    public IEnumerator[] enumerators = new IEnumerator[3];
    public int current_burn = 0;
   

    [Header("�ʱⰪ ����")]
    public float hp, offensive_power, defensive_power, attack_speed, move_speed;//ü��,���ݷ�,����,���ݼӵ�,�̵��ӵ�

    public float initial_hp;//�ʱ�ü��
    private float early_move_speed, early_attack_speed;//�Է��� �ʱⰪ ���� 
    private float damage_magnification;//������ ����
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

            case boss_types.death://�����긵�� óġ�� 
                player_status.portal_number = 1;
                break;

            case boss_types.golem://�� óġ�� 
                player_status.portal_number = 2;
                break;
            case boss_types.skeleton://�ذ� óġ�� 
                player_status.portal_number = 3;
                break;
            case boss_types.dragon://�� óġ�� 
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

    public void reduce_hp_1()//ȭ��,���� ���
    {
        this.hp -= 10 - (defensive_power / 10);
    }
    public void continuous_decline_hp(int i)//ȭ��,��
    {
        StartCoroutine("recover_reduce_hp", i);
    }
    IEnumerator recover_reduce_hp(int i)//ȭ��
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

    public void reduce_attack_speed(int input)//����
    {

        this.attack_speed -= input;
    }

    public void reduece_move_speed(int input)//�ñ�
    {
        this.move_speed -= input;
    }
    public void init_attack_speed()//���� �ʱ�ȭ 
    {
        this.attack_speed = early_attack_speed;
    }
    public void init_move_speed()//��ø �ʱ�ȭ 
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
            damage_magnification = 1.2f;//���������� ���� 1.2
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
        { //���������� �Ͻ� 

            hp -= player_status.offensive_power * damage_magnification;
            Debug.Log($"���������� :{player_status.offensive_power * damage_magnification}");
            Game_manager.instance.Apply_damaged_real_figure(gameObject.transform, (int)((int)player_status.offensive_power * damage_magnification));
        }
        else if (player_status.Is_Fixed_damage == false)//���������� �ƴҽ� 
        {
            float damage = player_status.offensive_power - (defensive_power / 5);
            hp -= damage * damage_magnification;
            Debug.Log($"�Ϲݵ����� :{damage * damage_magnification}");
            Game_manager.instance.Apply_damaged_real_figure(gameObject.transform, (int)((int)damage * damage_magnification));
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)//�÷��̾��� ���ݿ� ���Ұ�� 
    {
        //Debug.Log(Player + "Ž���� ");
        //Debug.Log(Player.CompareTag("Player_skilling") + " player_skilling Ž���� ");
        if (Player.CompareTag("Player_skilling"))//�÷��̾ �������϶� 
        {
            
            calculate();//������ ���� ���
            boss_animator.SetTrigger("Hited");//�ǰ� ��� Ȱ��ȭ
            real_damage_apply();//������ ��ü�� ���� 

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

            player_status.add_recovery();//�������� ���� ���߽� ȸ�� ���� 
        }


    }
}
