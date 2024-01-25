using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss_Enemy_State { non, burn, deceleration, coldair, cooling }

public class Boss_Condition_applicator : MonoBehaviour
{
    public Boss_Enemy_State state;
    private float max_filled_time = 60.0f;//�ִ� ���� ���ð�
    private int max_number_state = 3;
    private int input_deceleration = 5, input_coldair = 5;
    private int selected_state_index;


    public void Set_Enemy_State(Boss_Enemy_State state)
    {//���� ����

        this.state = state;

    }

    public boss_status status;

    public void Set_status(boss_status status)//�� �������ͽ� �ޱ� 
    {

        this.status = status;
        Set_enumerators();
    }

    public void Set_enumerators()
    {

        for (int i = 0; i < 3; i++)
        {//�ʱ� enumerator ����

            status.enumerators[i] = start_reuse_waiting_time(i);
        }
    }



    public void Apply_state()
    {
        switch (state)
        {
            case Boss_Enemy_State.burn://ȭ���� �������ͽ����� ���� 

                if (status.current_burn < 1)
                {
                    status.continuous_decline_hp(1);
                }
                break;
            case Boss_Enemy_State.deceleration://0==����
                selected_state_index = 0;
                if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    StopCoroutine(status.enumerators[selected_state_index]);//������ �����ϴ� ���� ���� ���ð� Ÿ�̸� �ߴ�
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//���ο� ���� ���ð� Ÿ�̸� ����
                    status.reduce_attack_speed(input_deceleration);
                    status.current_validnumber_state[selected_state_index]++;
                }
                break;
            case Boss_Enemy_State.coldair://1==�ñ� 
                selected_state_index = 1;
                if (status.current_validnumber_state[selected_state_index] == max_number_state)//�ð� �ߵ�
                {
                    state = Boss_Enemy_State.cooling;
                    Apply_state();
                }

                else if (status.current_validnumber_state[selected_state_index] < max_number_state)
                {
                    StopCoroutine(status.enumerators[selected_state_index]);//������ �����ϴ� ���� ���� ���ð� Ÿ�̸� �ߴ�
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//���ο� ���� ���ð� Ÿ�̸� ����
                    status.reduece_move_speed(input_coldair);
                    status.current_validnumber_state[selected_state_index]++;
                }
                break;
            case Boss_Enemy_State.cooling://2==�ð� 
                selected_state_index = 2;
                if (status.current_validnumber_state[selected_state_index] < 1)
                {
                    StopCoroutine(status.enumerators[selected_state_index]);//������ �����ϴ� ���� ���� ���ð� Ÿ�̸� �ߴ�
                    status.enumerators[selected_state_index] = start_reuse_waiting_time(selected_state_index);
                    StartCoroutine(status.enumerators[selected_state_index]);//���ο� ���� ���ð� Ÿ�̸� ����
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
                status.init_attack_speed();//���� �ʱ�ȭ 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                break;
            case 1:
                status.init_move_speed();//�ñ� �ʱ�ȭ 
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                break;
            case 2://�ð� �ʱ�ȭ 
                status.init_move_speed();//�ʱ� �̵��ӵ� ��
                status.current_validnumber_state[i] = 0;
                status.current_valid_statetime[i] = 0.0f;
                break;





        }

    }
    IEnumerator start_reuse_waiting_time(int i)//���� �ð� ����,60�ʰ� �Ǹ� ���� ��ü 
    {
        status.current_valid_statetime[i] = 0.0f;

        if (i == 2)//�ð� 
        {
            while (true)
            {
                if (status.current_valid_statetime[i] == 10)
                {
                    Init_state(i);
                    yield break;
                }
                yield return new WaitForSeconds(1.0f);//1�ʸ��� ����Ƽ���� ������ �ѱ��
                status.current_valid_statetime[i] += 1.0f;
            }
        }
        else if (i != 2)//�ð� �̿��� ���� 
        {
            while (true)
            {
                if (status.current_valid_statetime[i] == max_filled_time)
                {
                    Init_state(i);
                    yield break;
                }
                yield return new WaitForSeconds(1.0f);//1�ʸ��� ����Ƽ���� ������ �ѱ��
                status.current_valid_statetime[i] += 1.0f;
            }


        }
    }
}
