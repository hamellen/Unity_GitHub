using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;
using static Inventory;

public class Player_Status : MonoBehaviour//초기 스테이터스 설정
{
    [Header("실시간 적용")]
    public float max_hp,current_hp, offensive_power, defensive_power, move_speed, protective_film, attack_speed, critical, recovery;//실시간 적용 수치-최초 

    [Header("플레이어 극 초기값")]
    public  float early_max_hp, early_offensive_power, early_defensive_power, early_move_speed,  early_attack_speed, early_critical,early_recovery;//입력한 초기값 저장 또는 영구적인 값 저장

    Animator animator;
    public int portal_number=0;
    public bool isDead = false;
    public delegate void OnChangecoin();
    public OnChangecoin onChangecoin;

    private playercontroller playercontroller;

    public float[] current_valid_statetime = new float[13];//각 상태별 시간 
    public int[] current_validnumber_state = new int[13]; //각 상태별 횟수 
    public IEnumerator[] enumerators = new IEnumerator[13];//코루틴 상태 저장
    public int[] permanent_index = new int[7];//영구 수치 증가 인덱스 저장
    public int[] number_hunted_creature = new int[7];//잡몹별로 잡은 몬스터의 수
    public int current_burn=0, current_toxin=0;
    public bool Is_protective_film, Is_Fixed_damage;//보호막 및 고정데미지 

    public int pocket_money;
    public Movement2D movement;
    //public Transform activated_condition_holder;
    //public activated_condition[] activated_Conditions;
    public GameObject condition_applicator_object;
    private Condition_applicator condition_applicator;

    private float permanent_hp=100f, permanent_offensive_power=5f, permanent_defensive_power=5f, permanent_move_speed=2f, permanent_attack_speed=0.1f, permanent_recovery=10f, permanent_critical=5f;

    public int Pocket_money
    {
        get => pocket_money;
        set
        {
            pocket_money = value;
            onChangecoin.Invoke();//델리게이트 호출
        }
    }



    void Start()
    {

        for (int i = 0; i < 7; i++) {//최초 시작시 영구적인 값 저장 또는 세이브후 다시 접속시 이전 영구데이터 적용
            core_init(i);
        }
        current_hp = max_hp;
        Is_Fixed_damage = false;
        //activated_Conditions= activated_condition_holder.GetComponentsInChildren<activated_condition>();
        //current_index_conditionofarray = 0;
        


    }
    public void core_init(int i) {// 영구 스테이터스 상승후 저장기능,영구 스테이터스 상승시 해당 디버프 삭제 

        switch (i) {

            case 0://0은 체력

                max_hp = early_max_hp + (permanent_index[i]*permanent_hp);


                break;
            case 1://1은 공격력-일시적 버프 존재
                offensive_power = early_offensive_power+ (permanent_index[i]*permanent_offensive_power)+(current_validnumber_state[0]* condition_applicator.input_offensive_power);

                break;
            case 2://2는 방어력-일시적 버프 존재
                defensive_power = early_defensive_power + (permanent_index[i]*permanent_defensive_power) + (current_validnumber_state[2] * condition_applicator.input_defensive_power); 
                
                break;
            case 3://3은 이동속도-일시적 버프 존재
                move_speed = early_move_speed + (permanent_index[i]*permanent_move_speed) + (current_validnumber_state[3] * condition_applicator.input_move_speed);
               
                break;
            case 4://4는 공격속도-일시적 버프 존재 
                attack_speed = early_attack_speed + (permanent_index[i]*permanent_attack_speed) + (current_validnumber_state[1] * condition_applicator.input_attack_speed);
                break;
            case 5://5는 회복-일시적 버프 존재
                recovery = early_recovery + (permanent_index[i]*permanent_recovery) + (current_validnumber_state[5] * condition_applicator.input_recovery);
                
                break;
            case 6://크리티컬-일시적 버프 존재
                critical = early_critical + (permanent_index[i]*permanent_critical) + (current_validnumber_state[4] * condition_applicator.input_critical);
               
                break;

        }
        
        
        
       

    }
    public void Awake()
    {
        movement = GetComponent<Movement2D>();
        animator = GetComponent<Animator>();
        playercontroller = GetComponent<playercontroller>();
        for (int i = 0; i < current_valid_statetime.Length; i++)
        {
            current_valid_statetime[i] = 0.0f;
            current_validnumber_state[i] = 0;
        }
        condition_applicator_object = GameObject.FindWithTag("Condition_applicator");
        condition_applicator = condition_applicator_object.GetComponent<Condition_applicator>();
    }
    // Update is called once per frame
    void Update()
    {
        Is_protective_film = protective_film > 0 ? true : false;

        movement.speed = move_speed;


        if (current_hp <= 0 && isDead == false)
        {
            animator.SetTrigger("Player_death");
            playercontroller.player_death();//죽음
            isDead = true;
        }



        /*if (current_hp <= 0)
        {
            isDead = true;
       
            animator.SetTrigger("Player_death");
            playercontroller.player_death();
        }*/
    }

    public void permanent_status_add(int i)//영구적인 스테이터스 상승시 저장,도감 시스템
    {
        switch (i) 
        {
            case 0://0은 체력
                permanent_index[i]++;
                core_init(i);
                break;
            case 1://1은 공격력
                permanent_index[i]++;
                core_init(i);
                break;
            case 2://2는 방어력
                permanent_index[i]++;
                core_init(i);
                break;
            case 3://3은 이동속도
                permanent_index[i]++;
                core_init(i);
                break;
            case 4://4는 공격속도 
                permanent_index[i]++;
                core_init(i);
                break;
            case 5://5는 회복
                permanent_index[i]++;
                core_init(i);
                break;
            case 6://크리티컬
                permanent_index[i]++;
                core_init(i);
                break;
        }
    }
    
    public void add_hp(float input) {//일반적인 체력 증가 

        float diff = max_hp - current_hp;//최대 체력과 현재체력 차이 

        if (current_hp < max_hp) {

            if (diff <= input)
            {
                current_hp += diff;
            }
            else if (diff > input) {
                current_hp += input;
            }
        }
        
    }
    public void add_recovery()//공격시 회복 적용 
    {

        float diff = max_hp - current_hp;//최대 체력과 현재체력 차이 

        if (current_hp < max_hp)
        {

            if (diff <= recovery)
            {
                current_hp += diff;
            }
            else if (diff > recovery)
            {
                current_hp += recovery;
            }
        }

    }
    public void add_protective_film(float input)
    {
        this.protective_film += input;
    }



    public void add_offensive_power(float input)//괴력
    {
        this.offensive_power += input;
    }
    public void init_offensive_power()//괴력 초기화
    {
        this.offensive_power = early_offensive_power + permanent_index[1];
    }

    public void add_attack_speed(float input)//신속
    {
        this.attack_speed += input;
    }
    public void init_attack_speed()//신속 초기화 
    {
        this.attack_speed = early_attack_speed + permanent_index[4];
    }

    public void add_defensive_power(float input)//견고  
    {
        this.defensive_power += input;
    }
    public void init_defensive_power()//견고 초기화
    {
        this.defensive_power = early_defensive_power + permanent_index[2];
    }

    public void add_move_speed(float input)//민첩
    {
        this.move_speed += input;
    }
    public void init_move_speed()//민첩 초기화 
    {
        this.move_speed = early_move_speed + permanent_index[3];
    }
    public void add_critical(float input)//집중
    {
        this.critical += input;
    }
    public void init_critical()//집중 초기화 
    {
        this.critical = early_critical + permanent_index[6];
    }
    public void add_recovery(float input)//회복
    {

        this.recovery += input;
    }
    public void init_recovery()//회복 초기화 
    {
        this.recovery = early_recovery + permanent_index[5];
    }
    public void reduce_hp_1()//화상,방어력 비례//화상데미지 10
    {
        this.current_hp -= 10.0f-(defensive_power/10);
    }
    public void continuous_decline_hp(int i)//화상
    {
        StartCoroutine("recover_reduce_hp", i);
    }
    IEnumerator recover_reduce_hp(int i)//화상
    {
        if (i == 1)
        {
            current_burn=1;
            InvokeRepeating("reduce_hp_1", 3f,5f);
            yield return new WaitForSeconds(60.0f);
            current_burn=0;
            CancelInvoke("reduce_hp_1");
            condition_applicator.activated_condition_holder.GetChild(13).gameObject.SetActive(false);
        }
    }
    public void reduce_offensive_power(float input)//약화
    {
        this.offensive_power -= input;

    }
    public void reduce_attack_speed(float input)//감속
    {

        this.attack_speed -= input;
    }
    public void reduce_defensive_power(float input)//파괴  
    {
        this.defensive_power -= input;
    }
   
    public void reduece_move_speed(float input)//냉기
    {
        this.move_speed -= input;
    }
    public void Start_toxin()//
    {
        this.current_hp -= (5.0f*current_toxin);//독데미지 스택당 5씩
    }
    public void InVoke_fuction()
    {
        InvokeRepeating("Start_toxin", 3f,5f);
    }
    public void InVoke_Cancel_fuction()
    {
        CancelInvoke("Start_toxin");
    }

    public void init_state(int i) {//1-공격력,2-방어력,3-이동속도,4-공격속도,5-회복,6-크리티컬, 일시적인 버프 적용이 끝난후 영구수치를 합산한 종합수치로 돌아가는 모습

        switch (i)
        {
            case 1://1은 공격력
                offensive_power = early_offensive_power + (permanent_index[i] * permanent_offensive_power);

                break;
            case 2://2는 방어력
                defensive_power = early_defensive_power + (permanent_index[i] * permanent_defensive_power);

                break;
            case 3://3은 이동속도
                move_speed = early_move_speed + (permanent_index[i] * permanent_move_speed);

                break;
            case 4://4는 공격속도 
                attack_speed = early_attack_speed + (permanent_index[i] * permanent_attack_speed);
                break;
            case 5://5는 회복
                recovery = early_recovery + (permanent_index[i] * permanent_recovery);

                break;
            case 6://크리티컬
                critical = early_critical + (permanent_index[i] * permanent_critical);

                break;
        }
    }
}
