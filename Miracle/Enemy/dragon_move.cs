using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon_move : Boss_move
{

    public Animator dragon_animator;
    public GameObject player, dragon_long_attack;
    Rigidbody2D rb;
    public boss_status boss_status_script;
    public AudioClip[] dragon_audio;
    public AudioSource dragon_audio_source;
    public float detectionRange = 10f;    // ������ ������ �÷��̾��� �Ÿ�
    public float raycastDistance = 1f;

    //public float movespeed;

    private bool isPlayerInRange;
    private bool isFacingRight = true;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        dragon_animator = GetComponent<Animator>();
        boss_status_script = GetComponent<boss_status>();
        dragon_long_attack.GetComponent<Boss_long_range_status>().boss_offensive_power = (int)boss_status_script.offensive_power;
        dragon_audio_source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        dragon_audio_source.clip = dragon_audio[0];
        dragon_audio_source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // �÷��̾ ���� �Ÿ� �̳��� ���� �� ���� ����
            if (distanceToPlayer <= detectionRange)
            {
                dragon_animator.SetBool("IsMove", true);
                isPlayerInRange = true;

                // �÷��̾���� ������ üũ
                float direction = player.transform.position.x - transform.position.x;

                // �¿� �̵�
                rb.velocity = new Vector2(direction, rb.velocity.y).normalized * movespeed;

                // ���Ͱ� �ٶ󺸴� ���� ����
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
                dragon_animator.SetBool("IsMove", false);
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
        // �÷��̾ �����ϱ� ���� ����ĳ��Ʈ ���
        if (isPlayerInRange)
        {
            Debug.DrawRay(rb.position, Vector2.left, new Color(0, 1, 0));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, isFacingRight ? Vector2.right : Vector2.left, raycastDistance);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                dragon_animator.SetTrigger("Claw_attack");//�Ϲ� �ֵθ��� ����
                dragon_audio_source.clip = dragon_audio[2];
                dragon_audio_source.Play();
            }
        }
    }

    public void dragon_attack()
    {
        Instantiate(dragon_long_attack, this.gameObject.transform.position, Quaternion.identity);
        dragon_audio_source.clip = dragon_audio[3];
        dragon_audio_source.Play();
    }
}
