using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Color_mode {white,red,orange,yellow,green,blue,purple};

public class Color_Attack : MonoBehaviour
{

    public Color_mode selected_color;

    public GameObject Object_applicator;
    //public GameObject Object_enemy_applicator;

    private IEnumerator color_enumerator;

    private Condition_applicator applicator;
    public float max_color_time=60.0f;
    public float current_color_time;
    public void Set_Color_mode(Color_mode color) {

        this.selected_color = color;
       }

    public void Apply_Color()//색체 적용
    {

        StopCoroutine(color_enumerator);
        color_enumerator = start_reuse_waiting_time_color();
        StartCoroutine(color_enumerator);

        switch (selected_color) 
        {
            case Color_mode.white:

                break;
            case Color_mode.red://빨강
                applicator.Set_state(State.deceleration);
                applicator.Apply_state();
                break;
            case Color_mode.orange://주황
                applicator.Set_state(State.strength);
                applicator.Apply_state();
                applicator.Set_state(State.deceleration);
                applicator.Apply_state();
                break;
            case Color_mode.yellow://노랑
                applicator.Set_state(State.weak);
                applicator.Apply_state();
                applicator.Set_state(State.quick);
                applicator.Apply_state();
                break;
            case Color_mode.green://초록
                applicator.Set_state(State.weak);
                applicator.Apply_state();
                applicator.Set_state(State.deceleration);
                applicator.Apply_state();
                applicator.Set_state(State.recovery);
                applicator.Apply_state();
                break;
            case Color_mode.blue://파랑
                applicator.Set_state(State.destroy);
                applicator.Apply_state();
                break;
            case Color_mode.purple://보라 
                applicator.Set_state(State.weak);
                applicator.Apply_state();
                break;
        }
    }



    // Start is called before the first frame update
    void Awake()
    {
        Object_applicator = GameObject.FindWithTag("Condition_applicator");
        
        applicator = Object_applicator.GetComponent<Condition_applicator>();

        color_enumerator = start_reuse_waiting_time_color();
    }
    public void Init_color_state() {

        selected_color = Color_mode.white;
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator start_reuse_waiting_time_color()//버프 시간 시작,60초가 되면 버프 해체-색공격버전
    {
        
        current_color_time = 0.0f;

        while (true)
        {
            if (current_color_time == max_color_time)
            {
                Init_color_state();
                yield break;
            }
            yield return new WaitForSeconds(1.0f);//1초마다 유니티에게 통제권 넘기기
            current_color_time += 1.0f;
        }

    }
}
