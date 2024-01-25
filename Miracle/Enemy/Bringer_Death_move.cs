using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bringer_Death_move : Boss_move
{
    public Animator bringer_animator;
    public GameObject player, spell_attack_object;
    Rigidbody2D rb;
    public boss_status boss_status_script;
    public SpriteRenderer render;
    public AudioClip[] bringer_audio;
    public AudioSource bringer_audio_source;
    public float detectionRange = 10f;    // 추적을 시작할 플레이어의 거리
    public float raycastDistance = 1f;

    //public float movespeed;

    private bool isPlayerInRange;
    private bool isFacingRight = true;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        bringer_animator = GetComponent<Animator>();
        boss_status_script = GetComponent<boss_status>();
        spell_attack_object.GetComponent<Boss_long_range_status>().boss_offensive_power = (int)boss_status_script.offensive_power;
        render = GetComponent<SpriteRenderer>();
        bringer_audio_source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InvokeRepeating("Bringer_Death_random_attack",5f,5f);
        bringer_audio_source.clip = bringer_audio[0];
        bringer_audio_source.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // 플레이어가 일정 거리 이내에 있을 때 추적 시작
            if (distanceToPlayer <= detectionRange)
            {
                bringer_animator.SetBool("IsMove", true);
                isPlayerInRange = true;

                // 플레이어와의 방향을 체크
                float direction = player.transform.position.x - transform.position.x;

                // 좌우 이동
                rb.velocity = new Vector2(direction, 0).normalized * movespeed;

                // 몬스터가 바라보는 방향 설정
                if (direction > 0f && isFacingRight)
                {
                    Flip();
                }
                else if (direction < 0f && !isFacingRight)
                {
                    Flip();
                }
            }
            else
            {
                bringer_animator.SetBool("IsMove", false);
                isPlayerInRange = false;
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void FixedUpdate()
    {
        // 플레이어를 감지하기 위해 레이캐스트 사용
        if (isPlayerInRange)
        {
            Debug.DrawRay(rb.position, Vector2.left, new Color(0, 1, 0));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, isFacingRight ? Vector2.right : Vector2.left, raycastDistance);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                bringer_animator.SetTrigger("Death_Attack");//일반 휘두르기 공격
                bringer_audio_source.clip = bringer_audio[2];
                bringer_audio_source.Play();
            }
        }
    }

    public void Spell_attack()//데스 브링거 원거리 공격 
    {
        bringer_animator.SetTrigger("Casting");//원거리 공격시 데스브링거의 시전동작 
        Instantiate(spell_attack_object,new Vector3(player.transform.position.x, player.transform.position.y+9, player.transform.position.z),Quaternion.identity);
    }

    public void Behind_attack_ready()
    {
        bringer_animator.SetTrigger("Shadowmove");// 뒤치기 준비모션
        
    }

    public void Bringer_Death_random_attack()
    {
        int index= Random.Range(1, 3);

        switch (index)
        {
            case 1:
                Behind_attack_ready();
                break;
            case 2:
                Spell_attack();
                break;
        }
        bringer_audio_source.clip = bringer_audio[3];
        bringer_audio_source.Play();
    }

    public void Behind_attack_start()
    {
        if (player.GetComponent<playercontroller>().isRight == true)
        {
            this.transform.position = player.transform.position + new Vector3(-1, 0, 0);//플레이어 왼쪽 
        }
        else if (player.GetComponent<playercontroller>().isRight == false)
        {
            this.transform.position = player.transform.position + new Vector3(1, 0, 0);//플레이어 오른쪽
        }
        bringer_animator.SetTrigger("Shadowmove_reverse");//플레이어 뒤로 완벽히 이동
    }

    
}
