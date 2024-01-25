using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss_Enemy_State { non, burn, deceleration, coldair, cooling }

public class Boss_Condition_applicator : MonoBehaviour
{
    public Boss_Enemy_State state;
    private float max_filled_time = 60.0f;//최대 버프 사용시간
    private int max_number_state = 3;
    private int input_deceleration = 5, input_coldair = 5;
    private int selected_state_index;


    public void Set_Enemy_State(Boss_Enemy_State state)
    {//상태 설정

        this.state = state;

    }

    public boss_status status;

    public void Set_status(boss_status status)//적 스테이터스 받기 
    {

        this.status = status;
        Set_enumerators();
    }

    public void Set_enumerators()
    {

        for (int i = 0; i < 3; i++)
        {//초기 enumerator 설정

            status.enumerators[i] = start_reuse_waiting_time(i);
        }
    }



    public void Apply_state()
    {
        switch (state)
        {
            case Boss_Enemy_State.burn://화상은 스테이터스에서 관리 

                if (status.current_burn < 1)
                {
                    status.continuous_decline_hp(1);
                }
                break;
            case Boss_Enemy_State.deceleration://0==감속
                selected_state_index = 0;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.reduce_attack_speed(input_deceleration);
                    status.current_validnumber_state[selected_state_index]++;
                }
                break;
            case Boss_Enemy_State.coldair://1==냉기 
                selected_state_index = 1;
                if (status.current_validnumber_state[selected_state_index] == max_number_state)//냉각 발동
                {
                    state = Boss_Enemy_State.cooling;
                    Apply_state();
                }

                else if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.reduece_move_speed(input_coldair);
                    status.current_validnumber_state[selected_state_index]++;
                }
                break;
            case Boss_Enemy_State.cooling://2==냉각 
                selected_state_index = 2;
                if (status.current_validnumber_state[selected_state_index] < 1)
                {
                    StopCoroutine(status.enumerators[selected_state_index]);//기존에 동작하던 현재 버프 사용시간 타이머 중단
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//새로운 버프 사용시간 타이머 동작
                    status.move_speed = 0;
                    status.current_validnumber_state[selected_state_index]++;
                }
                break;
        }
    }

    public void Init_state(int i)
    {
        switch (i)
        {
            case 0:
                status.init_attack_speed();//감속 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                break;
            case 1:
                status.init_move_speed();//냉기 초기화 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                break;
            case 2://냉각 초기화 
                status.init_move_speed();//초기 이동속도 값
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                break;





        }

    }
    IEnumerator start_reuse_waiting_time(int i)//버프 시간 시작,60초가 되면 버프 해체 
    {
        status.current_valid_statetime[i] = 0.0f;

        if (i == 2)//냉각 
        {
            while (true)
            {
                if (status.current_valid_statetime[i] == 10)
                {
                    Init_state(i);
                    yield break;
                }
                yield return new WaitForSeconds(1.0f);//1초마다 유니티에게 통제권 넘기기
                status.current_valid_statetime[i] += 1.0f;
            }
        }
        else if (i != 2)//냉각 이외의 상태 
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
