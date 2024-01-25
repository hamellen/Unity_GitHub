using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public enum State {  non,strength, quick, solid, agility, focus, recovery,  burn, weak, deceleration, destroy, toxin, coldair, cooling,fixed_attack }



public class Condition_applicator : MonoBehaviour
{

    public State state;

    private float max_filled_time = 60.0f;//최대 버프 사용시간
    private int max_number_state = 3;
    public float input_offensive_power = 5.0f, input_attack_speed = 5.0f, input_defensive_power = 5.0f, input_move_speed = 2.0f, input_critical = 5.0f, input_recovery = 5.0f;//중첩당 적용버프 수치
    private float input_weak = 5.0f, input_deceleration = 5.0f, input_destroy = 5.0f,input_coldair=5.0f;//중첩다 적용 디버프 수치 
    private int selected_state_index;

    public Transform activated_condition_holder;
    public GameObject player,particle_object;
    public Particle_manager particle_manager;
    public ParticleSystem particle_system;
    private Player_Status status;
    private Image[] Images;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        status = player.GetComponent<Player_Status>();//플레이어 오브젝트의 status 컴포넌트 접근 
        particle_manager = player.GetComponent<Particle_manager>();
        for (int i = 0; i < 13; i++) {//초기 enumerator 설정

            status.enumerators[i] = start_reuse_waiting_time(i);
        }
    }
    
    private void Update()
    {
        if (particle_system != null)
        {
            particle_system.transform.position = player.transform.position;
        }
    }
    public void Set_state(State state) {

        this.state = state;
    }
    public void Apply_state()//상태 적용 
    {
        switch (state)
        {
            case State.strength://0==괴력
                selected_state_index=0;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index]==0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.add_offensive_power(input_offensive_power);
                    
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[selected_state_index], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                    
                   
                }
                break;
            case State.quick://1==신속
                selected_state_index = 1;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.add_attack_speed(input_attack_speed);
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[selected_state_index], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.solid://2==견고 
                selected_state_index = 2;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.add_defensive_power(input_defensive_power);
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[selected_state_index], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.agility://3==민첩
                selected_state_index = 3;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.add_move_speed(input_move_speed);
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[selected_state_index], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;

            case State.focus://4==집중
                selected_state_index = 4;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.add_critical(input_critical);
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[selected_state_index], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.recovery://5==회복
                selected_state_index = 5;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.add_recovery(input_recovery);
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[selected_state_index], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.burn://화상은 스테이터스에서 관리 
                
                if (status.current_burn < 1)
                {
                    Images = activated_condition_holder.GetChild(13).gameObject.GetComponentsInChildren<Image>();
                    Images[0].sprite = ConditionDatabase.Instance.conditionDB[13].condition_Image;//상태아이콘 
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                    activated_condition_holder.GetChild(13).gameObject.SetActive(true);
                    status.continuous_decline_hp(1);

                    particle_object = Instantiate(particle_manager.particles[6], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.weak://6== 약화 
                selected_state_index = 6;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.reduce_offensive_power(input_weak);
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[7], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.deceleration://7==감속
                selected_state_index = 7;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.reduce_attack_speed(input_deceleration);
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[8], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.destroy://8==파괴 
                selected_state_index = 8;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.reduce_defensive_power(input_destroy);
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[9], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.toxin://9==독
                selected_state_index = 9;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.current_toxin++;
                    status.InVoke_fuction();
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[10], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.coldair://10==냉기 
                selected_state_index = 10;
                if (status.current_validnumber_state[selected_state_index] ==max_number_state)//냉각 발동
                {
                    state = State.cooling;
                    Apply_state();
                }

                else if(status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.reduece_move_speed(input_coldair);
                    status.current_validnumber_state[selected_state_index]++;
                    Images[1].sprite = ConditionDatabase.Instance.indexDB[status.current_validnumber_state[selected_state_index] - 1].index_Image;

                    particle_object = Instantiate(particle_manager.particles[11], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.cooling://11==냉각 
                selected_state_index = 11;
                if (status.current_validnumber_state[selected_state_index] < 1)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.movement.jumpforce = 0.0f;
                    status.move_speed = 0.0f;

                    particle_object = Instantiate(particle_manager.particles[11], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();
                }
                break;
            case State.fixed_attack://고정데미지 여부 
                selected_state_index = 12;
                if (status.current_validnumber_state[selected_state_index] < 1)
                {
                    if (status.current_validnumber_state[selected_state_index] == 0)//처음 버프 적용시 
                    {
                        Images = activated_condition_holder.GetChild(selected_state_index).gameObject.GetComponentsInChildren<Image>();
                        Images[0].sprite = ConditionDatabase.Instance.conditionDB[selected_state_index].condition_Image;//상태아이콘 
                        Images[1].sprite = ConditionDatabase.Instance.indexDB[0].index_Image;//인덱스 아이콘
                        activated_condition_holder.GetChild(selected_state_index).gameObject.SetActive(true);
                    }
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.Is_Fixed_damage = true;

                    particle_object = Instantiate(particle_manager.particles[12], player.transform.position, Quaternion.identity);
                    particle_system = particle_object.GetComponentInChildren<ParticleSystem>();
                    particle_system.Play();

                }

                break;
        }
    }
    public void Init_state(int i)//상태 초기화 
    {
        switch (i)
        {
            case 0:
                status.init_state(1);//괴력 초기화 
                status.current_validnumber_state[i]=0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(0).gameObject.SetActive(false);
                break;
            case 1:
                status.init_state(4);//신속 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(1).gameObject.SetActive(false);
                break;
            case 2:
                status.init_state(2);//견고 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(2).gameObject.SetActive(false);
                break;
            case 3:
                status.init_state(3);//민첩 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(3).gameObject.SetActive(false);
                break;
            case 4:
                status.init_state(6);//집중 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(4).gameObject.SetActive(false);
                break;
            case 5:
                status.init_state(5);//회복 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(5).gameObject.SetActive(false);
                break;
            case 6:
                status.init_state(1);//약화 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(6).gameObject.SetActive(false);
                break;
            case 7:
                status.init_state(4);//감속 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(7).gameObject.SetActive(false);
                break;
            case 8:
                status.init_state(2);//파괴 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(8).gameObject.SetActive(false);
                break;
            case 9:
                status.InVoke_Cancel_fuction();//독 초기화 
                status.current_toxin = 0;
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(9).gameObject.SetActive(false);
                break;
            case 10:
                status.init_state(3);//냉기 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(10).gameObject.SetActive(false);
                break;
            case 11://냉각 초기화 
                status.movement.jumpforce = 8.0f;//점프 초기값
                status.init_state(3);//초기 이동속도 값
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(11).gameObject.SetActive(false);
                break;
            case 12://플레이어 고정데미지 
                status.Is_Fixed_damage = false;
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                activated_condition_holder.GetChild(12).gameObject.SetActive(false);
                break;
        }

    }
    IEnumerator start_reuse_waiting_time(int i)//버프 시간 시작,60초가 되면 버프 해체 
    {
        status.current_valid_statetime[i] = 0.0f;

        if (i == 11)//냉각 
        {
            while (true)
            {
                if (status.current_valid_statetime[i] == 10)//냉각상태 해제시 냉기상태도 같이 해재 
                {
                    Init_state(i - 1);//냉기상태 초기화 
                    Init_state(i);
                    yield break;
                }
                yield return new WaitForSeconds(1.0f);//1초마다 유니티에게 통제권 넘기기
                status.current_valid_statetime[i] += 1.0f;
            }
        }
        else if (i != 11)//냉각 이외의 상태 
        {
            while (true)
            {
                if (status.current_valid_statetime[i] == max_filled_time)
                {
                    Init_state(i);
                    yield break;
                }
                yield return new WaitForSeconds(1.0f);//1초마다 유니티에게 통제권 넘기기
                status.current_valid_statetime[i] += 1.0f;
            }


        }
    }
}
