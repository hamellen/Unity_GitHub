using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem_move : Boss_move
{
    public Animator golem_animator;
    public GameObject player;
    public GameObject[] rock_spawners;
    public AudioClip[] golem_audio;
    public AudioSource golem_audio_source;
    Rigidbody2D rb;

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
        golem_animator = GetComponent<Animator>();
        golem_audio_source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InvokeRepeating("Random_rock_attack", 4, 4);
        golem_audio_source.clip = golem_audio[0];
        golem_audio_source.Play();
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
                golem_animator.SetBool("IsMove", true);
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
                golem_animator.SetBool("IsMove", false);
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
                golem_animator.SetTrigger("Golem_skill_1");
                golem_audio_source.clip = golem_audio[1];
                golem_audio_source.Play();
            }
        }
    }
    //높이차는 9정도 
    public void Random_rock_attack()
    {
        rock_spawners[0].transform.position = new Vector3(transform.position.x-6, transform.position.y+9,0);
        rock_spawners[1].transform.position = new Vector3(transform.position.x - 3, transform.position.y + 9, 0);
        rock_spawners[2].transform.position = new Vector3(transform.position.x , transform.position.y + 9, 0);
        rock_spawners[3].transform.position = new Vector3(transform.position.x +3, transform.position.y + 9, 0);
        rock_spawners[4].transform.position = new Vector3(transform.position.x + 6, transform.position.y + 9, 0);

        golem_animator.SetTrigger("Golem_skill_2");
        golem_audio_source.clip = golem_audio[1];
        golem_audio_source.Play();

        foreach (GameObject spawner in rock_spawners)
        {
            spawner.GetComponent<Spawn>().Spawn_activate();
        }
    }
}
