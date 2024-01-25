using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;
using static Inventory;

public class Player_Status : MonoBehaviour//�ʱ� �������ͽ� ����
{
    [Header("�ǽð� ����")]
    public float max_hp,current_hp, offensive_power, defensive_power, move_speed, protective_film, attack_speed, critical, recovery;//�ǽð� ���� ��ġ-���� 

    [Header("�÷��̾� �� �ʱⰪ")]
    public  float early_max_hp, early_offensive_power, early_defensive_power, early_move_speed,  early_attack_speed, early_critical,early_recovery;//�Է��� �ʱⰪ ���� �Ǵ� �������� �� ����

    Animator animator;
    public int portal_number=0;
    public bool isDead = false;
    public delegate void OnChangecoin();
    public OnChangecoin onChangecoin;

    private playercontroller playercontroller;

    public float[] current_valid_statetime = new float[13];//�� ���º� �ð� 
    public int[] current_validnumber_state = new int[13]; //�� ���º� Ƚ�� 
    public IEnumerator[] enumerators = new IEnumerator[13];//�ڷ�ƾ ���� ����
    public int[] permanent_index = new int[7];//���� ��ġ ���� �ε��� ����
    public int[] number_hunted_creature = new int[7];//������� ���� ������ ��
    public int current_burn=0, current_toxin=0;
    public bool Is_protective_film, Is_Fixed_damage;//��ȣ�� �� ���������� 

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
            onChangecoin.Invoke();//��������Ʈ ȣ��
        }
    }



    void Start()
    {

        for (int i = 0; i < 7; i++) {//���� ���۽� �������� �� ���� �Ǵ� ���̺��� �ٽ� ���ӽ� ���� ���������� ����
            core_init(i);
        }
        current_hp = max_hp;
        Is_Fixed_damage = false;
        //activated_Conditions= activated_condition_holder.GetComponentsInChildren<activated_condition>();
        //current_index_conditionofarray = 0;
        


    }
    public void core_init(int i) {// ���� �������ͽ� ����� ������,���� �������ͽ� ��½� �ش� ����� ���� 

        switch (i) {

            case 0://0�� ü��

                max_hp = early_max_hp + (permanent_index[i]*permanent_hp);


                break;
            case 1://1�� ���ݷ�-�Ͻ��� ���� ����
                offensive_power = early_offensive_power+ (permanent_index[i]*permanent_offensive_power)+(current_validnumber_state[0]* condition_applicator.input_offensive_power);

                break;
            case 2://2�� ����-�Ͻ��� ���� ����
                defensive_power = early_defensive_power + (permanent_index[i]*permanent_defensive_power) + (current_validnumber_state[2] * condition_applicator.input_defensive_power); 
                
                break;
            case 3://3�� �̵��ӵ�-�Ͻ��� ���� ����
                move_speed = early_move_speed + (permanent_index[i]*permanent_move_speed) + (current_validnumber_state[3] * condition_applicator.input_move_speed);
               
                break;
            case 4://4�� ���ݼӵ�-�Ͻ��� ���� ���� 
                attack_speed = early_attack_speed + (permanent_index[i]*permanent_attack_speed) + (current_validnumber_state[1] * condition_applicator.input_attack_speed);
                break;
            case 5://5�� ȸ��-�Ͻ��� ���� ����
                recovery = early_recovery + (permanent_index[i]*permanent_recovery) + (current_validnumber_state[5] * condition_applicator.input_recovery);
                
                break;
            case 6://ũ��Ƽ��-�Ͻ��� ���� ����
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
            playercontroller.player_death();//����
            isDead = true;
        }



        /*if (current_hp <= 0)
        {
            isDead = true;
       
            animator.SetTrigger("Player_death");
            playercontroller.player_death();
        }*/
    }

    public void permanent_status_add(int i)//�������� �������ͽ� ��½� ����,���� �ý���
    {
        switch (i) 
        {
            case 0://0�� ü��
                permanent_index[i]++;
                core_init(i);
                break;
            case 1://1�� ���ݷ�
                permanent_index[i]++;
                core_init(i);
                break;
            case 2://2�� ����
                permanent_index[i]++;
                core_init(i);
                break;
            case 3://3�� �̵��ӵ�
                permanent_index[i]++;
                core_init(i);
                break;
            case 4://4�� ���ݼӵ� 
                permanent_index[i]++;
                core_init(i);
                break;
            case 5://5�� ȸ��
                permanent_index[i]++;
                core_init(i);
                break;
            case 6://ũ��Ƽ��
                permanent_index[i]++;
                core_init(i);
                break;
        }
    }
    
    public void add_hp(float input) {//�Ϲ����� ü�� ���� 

        float diff = max_hp - current_hp;//�ִ� ü�°� ����ü�� ���� 

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
    public void add_recovery()//���ݽ� ȸ�� ���� 
    {

        float diff = max_hp - current_hp;//�ִ� ü�°� ����ü�� ���� 

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



    public void add_offensive_power(float input)//����
    {
        this.offensive_power += input;
    }
    public void init_offensive_power()//���� �ʱ�ȭ
    {
        this.offensive_power = early_offensive_power + permanent_index[1];
    }

    public void add_attack_speed(float input)//�ż�
    {
        this.attack_speed += input;
    }
    public void init_attack_speed()//�ż� �ʱ�ȭ 
    {
        this.attack_speed = early_attack_speed + permanent_index[4];
    }

    public void add_defensive_power(float input)//�߰�  
    {
        this.defensive_power += input;
    }
    public void init_defensive_power()//�߰� �ʱ�ȭ
    {
        this.defensive_power = early_defensive_power + permanent_index[2];
    }

    public void add_move_speed(float input)//��ø
    {
        this.move_speed += input;
    }
    public void init_move_speed()//��ø �ʱ�ȭ 
    {
        this.move_speed = early_move_speed + permanent_index[3];
    }
    public void add_critical(float input)//����
    {
        this.critical += input;
    }
    public void init_critical()//���� �ʱ�ȭ 
    {
        this.critical = early_critical + permanent_index[6];
    }
    public void add_recovery(float input)//ȸ��
    {

        this.recovery += input;
    }
    public void init_recovery()//ȸ�� �ʱ�ȭ 
    {
        this.recovery = early_recovery + permanent_index[5];
    }
    public void reduce_hp_1()//ȭ��,���� ���//ȭ�󵥹��� 10
    {
        this.current_hp -= 10.0f-(defensive_power/10);
    }
    public void continuous_decline_hp(int i)//ȭ��
    {
        StartCoroutine("recover_reduce_hp", i);
    }
    IEnumerator recover_reduce_hp(int i)//ȭ��
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
    public void reduce_offensive_power(float input)//��ȭ
    {
        this.offensive_power -= input;

    }
    public void reduce_attack_speed(float input)//����
    {

        this.attack_speed -= input;
    }
    public void reduce_defensive_power(float input)//�ı�  
    {
        this.defensive_power -= input;
    }
   
    public void reduece_move_speed(float input)//�ñ�
    {
        this.move_speed -= input;
    }
    public void Start_toxin()//
    {
        this.current_hp -= (5.0f*current_toxin);//�������� ���ô� 5��
    }
    public void InVoke_fuction()
    {
        InvokeRepeating("Start_toxin", 3f,5f);
    }
    public void InVoke_Cancel_fuction()
    {
        CancelInvoke("Start_toxin");
    }

    public void init_state(int i) {//1-���ݷ�,2-����,3-�̵��ӵ�,4-���ݼӵ�,5-ȸ��,6-ũ��Ƽ��, �Ͻ����� ���� ������ ������ ������ġ�� �ջ��� ���ռ�ġ�� ���ư��� ���

        switch (i)
        {
            case 1://1�� ���ݷ�
                offensive_power = early_offensive_power + (permanent_index[i] * permanent_offensive_power);

                break;
            case 2://2�� ����
                defensive_power = early_defensive_power + (permanent_index[i] * permanent_defensive_power);

                break;
            case 3://3�� �̵��ӵ�
                move_speed = early_move_speed + (permanent_index[i] * permanent_move_speed);

                break;
            case 4://4�� ���ݼӵ� 
                attack_speed = early_attack_speed + (permanent_index[i] * permanent_attack_speed);
                break;
            case 5://5�� ȸ��
                recovery = early_recovery + (permanent_index[i] * permanent_recovery);

                break;
            case 6://ũ��Ƽ��
                critical = early_critical + (permanent_index[i] * permanent_critical);

                break;
        }
    }
}
