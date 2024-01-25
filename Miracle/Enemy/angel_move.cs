using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Angel_state { blue, purple, red }

public class angel_move : Boss_move
{
    public int percent_angel_hp;
    public Animator angel_animator;
    public GameObject player, angel_long_attack1, angel_long_attack2, angel_long_attack3;
    Rigidbody2D rb;
    public boss_status boss_status_script;
    public Angel_state angel_state;
    public AudioClip[] angel_audio;
    public AudioSource angel_audio_source;
    public bool Is_Attacking = false;

    public float detectionRange = 10f;    // 추적을 시작할 플레이어의 거리
    public float raycastDistance = 1f;

    //public float movespeed;
    public float initial_movespeed;
    public RuntimeAnimatorController[] animation_controllers = new RuntimeAnimatorController[3];
    private bool isPlayerInRange;
    private bool isFacingRight = true;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        angel_animator = GetComponent<Animator>();
        boss_status_script = GetComponent<boss_status>();
        angel_long_attack1.GetComponent<Boss_long_range_status>().boss_offensive_power = (int)boss_status_script.offensive_power;
        angel_long_attack2.GetComponent<Boss_long_range_status>().boss_offensive_power = (int)boss_status_script.offensive_power;
        angel_long_attack3.GetComponent<Boss_long_range_status>().boss_offensive_power = (int)boss_status_script.offensive_power;
        angel_state = Angel_state.blue;
        angel_audio_source = GetComponent<AudioSource>();
        initial_movespeed = movespeed;
    }

    public void Set_true_angel_IsAttacking()
    {
        Is_Attacking = true;
    }

    public void Set_false_angel_IsAttacking()
    {
        Is_Attacking = false;
    }


    private void Start()
    {
        angel_audio_source.clip = angel_audio[0];
        angel_audio_source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        percent_angel_hp = (int)boss_status_script.remained_hp_percent;
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // 플레이어가 일정 거리 이내에 있을 때 추적 시작
            if (distanceToPlayer <= detectionRange)
            {
                angel_animator.SetBool("IsMove", true);
                isPlayerInRange = true;

                // 플레이어와의 방향을 체크
                float direction = player.transform.position.x - transform.position.x;

                //천사는 공중에서 이동 
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movespeed * Time.deltaTime);

                // 몬스터가 바라보는 방향 설정
                if (direction > 0f && isFacingRight)
                {
                    isFacingRight = false;
                    Flip();
                }
                else if (direction < 0f && !isFacingRight)
                {
                    //보스가 오른쪽에 위치 
                    isFacingRight = true;
                    Flip();
                }
            }
            else
            {
                angel_animator.SetBool("IsMove", false);
                isPlayerInRange = false;
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }

        if (percent_angel_hp == 50)//체력 50퍼센트 일시 
        {
            movespeed = 0f;
            angel_animator.SetTrigger("Next_stage");
            Set_second_stage();
        }
        else if (percent_angel_hp == 10)//체력 10퍼센트 남을시 
        {
            movespeed = 0f;
            angel_animator.SetTrigger("Next_stage");
            Set_third_stage();
        }
        
    }

    public void Flip()
    {
        //isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void FixedUpdate()
    {
        
        // 플레이어를 감지하기 위해 레이캐스트 사용
        if (isPlayerInRange)
        {
            Debug.DrawRay(new Vector3(transform.position.x-1, transform.position.y - 3, transform.position.z), !isFacingRight ? Vector2.right : Vector2.left * raycastDistance, new Color(0, 1, 0));
            RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x-1, transform.position.y-3,transform.position.z), !isFacingRight ? Vector2.right : Vector2.left, raycastDistance);

            Debug.Log(hit.collider != null);
            Debug.Log(hit.collider.CompareTag("Player")+"태그 맞춤 플레이어 감지됨");
            Debug.Log(hit.collider.gameObject.tag + "플레이어 태그 감지됨");
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {

                if (!Is_Attacking)
                {
                    angel_animator.SetTrigger("light_attack");//일반 휘두르기 공격
                    Set_true_angel_IsAttacking();
                    angel_audio_source.clip = angel_audio[2];
                    angel_audio_source.Play();
                }

            }
        }
    }

    public void Teleport_attack_ready()
    {
        angel_animator.SetTrigger("light_teleport_ready");
    }
    public void Teleport_attack_activate()
    {
        this.gameObject.transform.position = player.transform.position;
        angel_animator.SetTrigger("light_teleport_attack");
    }

    void Set_second_stage()
    {
        Invoke("angel_second_stage", 5f);
    }

    void Set_third_stage()
    {
        Invoke(" angel_third_stage", 5f);
    }

    public void angel_second_stage()
    {
        movespeed = initial_movespeed;
        angel_state = Angel_state.purple;
        angel_animator.runtimeAnimatorController = animation_controllers[1];
        boss_status_script.offensive_power += 5f;
    }

    public void angel_third_stage()
    {
        movespeed = initial_movespeed;
        angel_state = Angel_state.red;
        angel_animator.runtimeAnimatorController = animation_controllers[2];
        boss_status_script.offensive_power += 5f;
    }

    public void activate_circle_long_attack()//원거리 투사체 발산 공격 
    {
        switch (angel_state)
        {
            case Angel_state.blue://블루 상태
                
                Instantiate(angel_long_attack1, this.transform.position, Quaternion.identity);

                break;

            case Angel_state.purple://그린 상태
                Instantiate(angel_long_attack2, this.transform.position, Quaternion.identity);
                break;

                 
            case Angel_state.red://레드 상태 
                Instantiate(angel_long_attack3, this.transform.position, Quaternion.identity);
                break;
        }
        angel_audio_source.clip = angel_audio[3];
        angel_audio_source.Play();
    }
}
