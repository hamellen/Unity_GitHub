using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_move : Boss_move

{
    public GameObject player;
    public boss_status boss_status_script;
    public GameObject[] flame_spawners;
    public Animator skeleton_animator;
    public float detectionRange = 10f;    // 추적을 시작할 플레이어의 거리
    public float raycastDistance = 1f;
    Rigidbody2D rb;
    //public float movespeed;
    private bool isPlayerInRange;
    private bool isFacingRight = true;
    public AudioClip[] skeleton_audio;
    public AudioSource skeleton_audio_source;


    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        boss_status_script = GetComponent<boss_status>();
        skeleton_animator = GetComponent<Animator>();
        skeleton_audio_source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        flame_spawners[0].GetComponent<Boss_long_range_status>().boss_offensive_power= (int)boss_status_script.offensive_power;
        flame_spawners[1].GetComponent<Boss_long_range_status>().boss_offensive_power = (int)boss_status_script.offensive_power;
        flame_spawners[2].GetComponent<Boss_long_range_status>().boss_offensive_power = (int)boss_status_script.offensive_power;
        flame_spawners[3].GetComponent<Boss_long_range_status>().boss_offensive_power = (int)boss_status_script.offensive_power;
    }

    private void Start()
    {
        skeleton_audio_source.clip = skeleton_audio[0];
        skeleton_audio_source.Play();
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
                skeleton_animator.SetBool("IsMove", true);
                isPlayerInRange = true;

                // 플레이어와의 방향을 체크
                float direction = player.transform.position.x - transform.position.x;

                // 좌우 이동
                rb.velocity = new Vector2(direction, rb.velocity.y).normalized * movespeed;

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
                skeleton_animator.SetBool("IsMove", false);
                isPlayerInRange = false;
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
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
                skeleton_animator.SetTrigger("Skeleton_attack");//일반 휘두르기 공격
                skeleton_audio_source.clip = skeleton_audio[2];
                skeleton_audio_source.Play();
            }
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Skeleton_random_attack()
    {
        flame_spawners[0].transform.position=new Vector3(transform.position.x - 6, transform.position.y , 0);
        flame_spawners[1].transform.position = new Vector3(transform.position.x - 3, transform.position.y, 0);
        flame_spawners[2].transform.position = new Vector3(transform.position.x +3, transform.position.y, 0);
        flame_spawners[3].transform.position = new Vector3(transform.position.x + 6, transform.position.y, 0);


        skeleton_animator.SetTrigger("Skeleton_casting");
        skeleton_audio_source.clip = skeleton_audio[2];
        skeleton_audio_source.Play();

        foreach (GameObject spawner in flame_spawners)
        {
            Instantiate(spawner, transform.position, Quaternion.identity);
        }
    }
}
