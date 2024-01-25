


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyStatus : MonoBehaviour
{

    public float[] current_valid_statetime = new float[3];//냉기,감속,냉각
    public int[] current_validnumber_state = new int[3]; //0=감속,1=냉기,2=냉각
    public IEnumerator[] enumerators = new IEnumerator[3];
    public int current_burn = 0;
    public int minimal_enemy_number;//영구 속성 능력치 함수를 위한 숫자 

    [Header("초기값 적용")]
    public float hp, offensive_power, defensive_power,attack_speed, move_speed;//체력,공격력,방어력,공격속도,이동속도
    
    private  float initial_hp;//초기체력
    private float  early_move_speed, early_attack_speed;//입력한 초기값 저장 
    private float damage_magnification;//데미지 배율

    private Material Enemy_material;
    private SpriteRenderer render;
    private Player_Status player_status;
    private EnemyMove enemy_move;
    private GameObject Object_enemy_applicator,Player,Color_selector;
    private Enemy_Condition_applicator enemy_applicator;
    private Rito.WeightedRandomPicker<bool> wrPicker;
    private Color_Attack color_attack;
    // Start is called before the first frame update

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        
        enemy_move = GetComponent<EnemyMove>();
        Object_enemy_applicator = GameObject.FindWithTag("Enemy_Condition_applicator");
        enemy_applicator = Object_enemy_applicator.GetComponent<Enemy_Condition_applicator>();
        Player= GameObject.FindWithTag("Player");
        player_status = Player.GetComponent<Player_Status>();
        Color_selector= GameObject.FindWithTag("Color_Selector");
        color_attack = Color_selector.GetComponent<Color_Attack>();
    }
    void Start()
    {
        initial_hp = hp;
        early_move_speed = move_speed;
        early_attack_speed = attack_speed;
        //render.material.color= new Color(0/255f, 0/255f, 0/255f);

        wrPicker = new Rito.WeightedRandomPicker<bool>();
    }

    // Update is called once per frame
    void Update()
    {
        float percent = (initial_hp - hp / initial_hp)*100;

        render.material.color = new Color(percent*255f / 255f, percent * 255f / 255f, percent * 255f/ 255f);

        enemy_move.movespeed = this.move_speed;

        if (hp <= 0)
        {
            Destroy(gameObject);
            //player_status.number_hunted_creature[minimal_enemy_number]++;
        }
    }
    
    public void Dead()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
            //player_status.number_hunted_creature[minimal_enemy_number]++;
        }
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
            InvokeRepeating("reduce_hp_1", 3f,5f);
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
    public void calculate() {

        

        wrPicker.Add(
            
            (true,player_status.critical+1),
            (false,100- player_status.critical)
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
        if (Player.CompareTag("Player_skilling"))//플레이어가 공격중일때 
        {
            calculate();//데미지 배율 계산
            real_damage_apply();//데미지 실체력 적용 

            if (color_attack.selected_color == Color_mode.red)
            {
                enemy_applicator.Set_status(this);
                enemy_applicator.Set_Enemy_State(Enemy_State.burn);
                enemy_applicator.Apply_state();
            }
            else if (color_attack.selected_color == Color_mode.blue)
            {
                enemy_applicator.Set_status(this);
                enemy_applicator.Set_Enemy_State(Enemy_State.cooling);
                enemy_applicator.Apply_state();
            }
            else if (color_attack.selected_color == Color_mode.purple)
            {
                enemy_applicator.Set_status(this);
                enemy_applicator.Set_Enemy_State(Enemy_State.deceleration);
                enemy_applicator.Apply_state();
            }

            player_status.add_recovery();//몬스터한테 공격 적중시 회복 적용 
        }
        

    }
}
