using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public GameObject player;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public Animator animator;
    public int number_current_skill,output_random;
    public float detectionRange = 10f;    // ������ ������ �÷��̾��� �Ÿ�
    public float raycastDistance = 1f;   // ���Ͱ� �÷��̾ �ִ��� üũ�� ����ĳ��Ʈ �Ÿ�

    public float movespeed;

    //public int nextMove;

    private bool isPlayerInRange;
    private bool isFacingRight = false;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        
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
                isPlayerInRange = true;

                // �÷��̾���� ������ üũ
                float direction = player.transform.position.x - transform.position.x;

                // �¿� �̵�
                //rb.velocity = new Vector2(direction, 0).normalized * movespeed;

                animator.SetBool("IsMove", true);

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
                isPlayerInRange = false;
                animator.SetBool("IsMove", false);
                rb.velocity = new Vector2(0f, 0);
            }
        }

   
        /*rigid.velocity = new Vector2(nextMove*movespeed,rigid.velocity.y);


        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("ground"));
        if (rayHit.collider == null)
        {
            Turn();
        }*/
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
                Random_attack();
                // �÷��̾�� �浹 ó��
                // ��: �÷��̾�� �������� ������ ���� ������ ����
            }
        }
    }

    public void Random_attack()
    {
        output_random = Random.Range(1, number_current_skill + 1);

        switch (output_random)
        {
            case 1:
                animator.SetTrigger("Attack1");
                break;

            case 2:
                animator.SetTrigger("Attack2");
                break;

            case 3:
                animator.SetTrigger("Attack3");
                break;

        }
    }


    /*void Think() {

        nextMove = Random.Range(-1, 2);

        //������ȯ
        if (nextMove != 0) {
            sprite.flipX = (nextMove == 1);
        }

        float nextThinktime = Random.Range(2f, 5f);

        Invoke("Think", nextThinktime);
    }*/

    /*void Turn() {

        nextMove *= -1;
        sprite.flipX = (nextMove == 1);
        CancelInvoke();
        Invoke("Think", 5);
    }*/
}
