using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Mode { assassin,shock,shooter}

public class playercontroller : MonoBehaviour//�ǰ� �������� 
{
    public int index_system = 0;
    public Mode mode;
    static public GameObject player_canvas;
    static public GameObject player_object;
    private Movement2D movement2d;
    private Player_Status status;
    private InventoryUI inventory_ui;
    AudioSource audiosrc;
    bool isMoving = false;
    public bool isRight;
    //bool is_Shop_open;
    GameObject fade_system, manager,smoke_creator_object;
    FadeSystem performer_fade_system;
    public Animator animator;
    public RuntimeAnimatorController[] animation_controllers= new RuntimeAnimatorController[3];
    private Rigidbody2D rigid;
    public smoke_creator smoke_script;
    public AudioClip[] weapon_sounds;


    [SerializeField]GameObject condition_applicator;
    [SerializeField] Condition_applicator applicator;



    // Start is called before the first frame update
    private void Start()
    {
        movement2d = GetComponent<Movement2D>();
        status= GetComponent<Player_Status>();
        condition_applicator = GameObject.FindWithTag("Condition_applicator");
        applicator = condition_applicator.GetComponent<Condition_applicator>();
        audiosrc = GetComponent<AudioSource>();
        player_object = GameObject.FindWithTag("Player");
        fade_system = GameObject.FindWithTag("Fade");
        performer_fade_system = fade_system.GetComponent<FadeSystem>();
        manager = GameObject.FindWithTag("GameManager");
        inventory_ui = manager.GetComponent<InventoryUI>();
        //is_Shop_open = false;
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        smoke_creator_object = GameObject.FindWithTag("Smoke_creator");
        smoke_script = smoke_creator_object.GetComponent<smoke_creator>();
        player_canvas.SetActive(false);

    }
   
   
    
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        
        if (collision.gameObject.tag.Equals("Enemy_weapon"))//���� ���Ÿ� ���ݿ� ���Ұ��,�����
        {
            if (this.CompareTag("Player"))
            {
                Destroy(collision.gameObject);
                animator.SetTrigger("Player_damaged");
                float enemy_offensive = collision.gameObject.GetComponent<EnemyWeaponStatus>().enemy_offensive_power;
                movement2d.OnDamaged(collision.transform.position);
                if (status.Is_protective_film == true)//ĳ������ ��ȣ���� �ִ� ��� 
                {
                    if (status.protective_film >= enemy_offensive)//�Ǽ�ġ ��Ƴ��� 
                    {
                        status.protective_film -= enemy_offensive;

                    }
                    else if (status.protective_film < enemy_offensive)
                    {
                        float Remaining_attack_power = enemy_offensive - status.protective_film;
                        status.protective_film = 0f;
                        status.Is_protective_film = false;
                        status.current_hp -= Remaining_attack_power - (status.defensive_power / 10);
                    }

                }
                else if (status.Is_protective_film == false)//ĳ������ ��ȣ���� ���� ��� 
                {
                    status.current_hp -= enemy_offensive - (status.defensive_power / 10);
                }
            }
            
        }
        else if (collision.gameObject.tag.Equals("Enemy"))//������ ���� �����Ұ��,�����
        {
            if (this.CompareTag("Player"))
            {
                animator.SetTrigger("Player_damaged");//�ǰ� ��� Ȱ��ȭ
                float enemy_offensive = collision.gameObject.GetComponent<EnemyStatus>().offensive_power;
                movement2d.OnDamaged(collision.transform.position);
                if (status.Is_protective_film == true)//ĳ������ ��ȣ���� �ִ� ��� 
                {
                    if (status.protective_film >= enemy_offensive)//�Ǽ�ġ ��Ƴ��� 
                    {
                        status.protective_film -= enemy_offensive;

                    }
                    else if (status.protective_film < enemy_offensive)
                    {
                        float Remaining_attack_power = enemy_offensive - status.protective_film;
                        status.protective_film = 0f;
                        status.Is_protective_film = false;
                        status.current_hp -= Remaining_attack_power - (status.defensive_power / 10);
                    }

                }
                else if (status.Is_protective_film == false)//ĳ������ ��ȣ���� ���� ��� 
                {
                    status.current_hp -= enemy_offensive - (status.defensive_power / 10);
                }
            }


        }
        else if (collision.gameObject.tag.Equals("Coin"))
        {
            Gold_Coin gold_coin = collision.gameObject.GetComponent<Gold_Coin>();
            if (gold_coin != null)
            {
                status.pocket_money += gold_coin.money;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag.Equals("Boss"))//���� ���� ���� �ǰݽ� 
        {
            if (this.CompareTag("Player"))
            {
                animator.SetTrigger("Player_damaged");//�ǰ� ��� Ȱ��ȭ
                float enemy_offensive = collision.gameObject.GetComponent<boss_status>().offensive_power;
                movement2d.OnDamaged(collision.transform.position);
                if (status.Is_protective_film == true)//ĳ������ ��ȣ���� �ִ� ��� 
                {
                    if (status.protective_film >= enemy_offensive)//�Ǽ�ġ ��Ƴ��� 
                    {
                        status.protective_film -= enemy_offensive;

                    }
                    else if (status.protective_film < enemy_offensive)
                    {
                        float Remaining_attack_power = enemy_offensive - status.protective_film;
                        status.protective_film = 0f;
                        status.Is_protective_film = false;
                        status.current_hp -= Remaining_attack_power - (status.defensive_power / 10);
                    }

                }
                else if (status.Is_protective_film == false)//ĳ������ ��ȣ���� ���� ��� 
                {
                    status.current_hp -= enemy_offensive - (status.defensive_power / 10);
                }
            }
        }
        else if (collision.gameObject.tag.Equals("Boss_long_range_attack"))//���� ���Ÿ� ���� �ǰݽ� 
        {
            if (this.CompareTag("Player"))
            {
                Destroy(collision.gameObject);//���Ÿ� �ǰ�ü �Ҹ�
                animator.SetTrigger("Player_damaged");//�ǰ� ��� Ȱ��ȭ
                float enemy_offensive = collision.gameObject.GetComponent<Boss_long_range_status>().boss_offensive_power;
                movement2d.OnDamaged(collision.transform.position);
                if (status.Is_protective_film == true)//ĳ������ ��ȣ���� �ִ� ��� 
                {
                    if (status.protective_film >= enemy_offensive)//�Ǽ�ġ ��Ƴ��� 
                    {
                        status.protective_film -= enemy_offensive;

                    }
                    else if (status.protective_film < enemy_offensive)
                    {
                        float Remaining_attack_power = enemy_offensive - status.protective_film;
                        status.protective_film = 0f;
                        status.Is_protective_film = false;
                        status.current_hp -= Remaining_attack_power - (status.defensive_power / 10);
                    }

                }
                else if (status.Is_protective_film == false)//ĳ������ ��ȣ���� ���� ��� 
                {
                    status.current_hp -= enemy_offensive - (status.defensive_power / 10);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (Input.GetKeyDown(KeyCode.F))// ��Ż�̿�� fŰ ���
        {
            
            if (collision.gameObject.tag.Equals("Teleport_Desert_normal"))
            {
                SceneManager.LoadScene("Desert_normal");
            }
            else if (collision.gameObject.tag.Equals("Teleport_Lake_normal"))
            {
                SceneManager.LoadScene("Lake_normal");
            }
            else if (collision.gameObject.tag.Equals("Teleport_Deepforest_normal"))
            {
                SceneManager.LoadScene("Deepforest_normal");
            }
            else if (collision.gameObject.tag.Equals("Teleport_Forest_normal"))
            {
                SceneManager.LoadScene("Forest_normal");
            }
            else if (collision.gameObject.tag.Equals("Teleport_Field_normal"))
            {
                SceneManager.LoadScene("Field_normal");
            }
            else if (collision.gameObject.tag.Equals("Teleport_Field_boss"))
            {
                SceneManager.LoadScene("Field_boss");
            }
            else if (collision.gameObject.tag.Equals("Teleport_Deepforest_boss"))
            {
                SceneManager.LoadScene("Deepforest_boss");
            }
            else if (collision.gameObject.tag.Equals("Teleport_Desert_boss"))
            {
                SceneManager.LoadScene("Desert_boss");
            }
            else if (collision.gameObject.tag.Equals("Teleport_Lake_boss"))
            {
                SceneManager.LoadScene("Lake_boss");
            }
            else if (collision.gameObject.tag.Equals("Teleport_Forest_boss"))
            {
                SceneManager.LoadScene("Forest_boss");
            }
            else if (collision.gameObject.tag.Equals("Normal_teleport_left"))
            {
                gameObject.transform.position = collision.gameObject.GetComponent<Teleport>().opposite_teleport.transform.position;
            }
            else if (collision.gameObject.tag.Equals("Normal_teleport_right"))
            {
                gameObject.transform.position = collision.gameObject.GetComponent<Teleport>().opposite_teleport.transform.position;
            }

            performer_fade_system.total_start();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("falling_rock"))//�� ���� ���ؽ� 
        {
            applicator.Set_state(State.deceleration);
            applicator.Apply_state();
        }
    }

    public void player_death()//�÷��̾� ���
    {
        
        
            gameObject.layer = 12;

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;//�÷��̾� ������ ����

            Invoke("Return_Village", 3.0f);//3���� ���� ������ ���ư� 
        
       

    }
    public void Set_tag_Player()
    {
        gameObject.tag = "Player";
    }
    public void Set_tag_Playerskilling()
    {
        gameObject.tag = "Player_skilling";
    }

    public void end_game() { //���� ����� ��� ���� �� ����� ���� 

        for (int i = 0; i < 12; i++) {
            applicator.Init_state(i);
        }
    
    }

    public void Return_Village()//������ ���ư��� �ý���
    {

        performer_fade_system.total_start();//���̵� �� ���̵� �ƿ� 

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;//�÷��̾� ������ ����
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        status.current_hp = status.max_hp;//ü�� ��ȸ��
        status.isDead = false;

        switch (mode)
        {
            case Mode.assassin:
                animator.Play("assassin_idle");
                break;
            case Mode.shooter:
                animator.Play("Shooter_idle");
                break;

            case Mode.shock:
                animator.Play("Shock_wake");
                break;
        }

        SceneManager.LoadScene("Village");

        

        gameObject.layer = 7;//�÷��̾� ���̾� �缳��
    }

    public void Set_ground_sound()
    {

    }

    // Update is called once per frame
    void Update()//������ ������ ��Ʈ��
    {

        float x = Input.GetAxisRaw("Horizontal");

        if (x != 0)
        {
            if (x > 0)
            {
                isRight = true;
            }
            else if (x < 0)
            {
                isRight = false;
            }
            movement2d.Move(x);
            isMoving = true;
            animator.SetBool("IsMove", isMoving);
        }
        else if (x == 0)
        {
            movement2d.Move(0);
            isMoving = false;
            animator.SetBool("IsMove", isMoving);
        }

        if (isMoving && movement2d.isGround)//������ �ְ� �����δٴ� ����
        {

            if (!audiosrc.isPlaying)
            {
                audiosrc.Play();
            }
        }
        else {
            audiosrc.Stop();
        }

        

        if (rigid.velocity.y < 0)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Ground"));

            if (raycastHit.collider != null)
            {
                if (raycastHit.distance <= 0.5f)
                {
                    animator.SetBool("IsAir", false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!inventory_ui.isStoreActive)
            {
                movement2d.Jump();
                animator.SetTrigger("jump");
                animator.SetBool("IsAir", true);
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!inventory_ui.isStoreActive)
            {
                animator.SetTrigger("Skill_attack");
               
            }
                
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!inventory_ui.isStoreActive && !inventory_ui.activeInventory)
            {
                animator.SetTrigger("Attack");

   
            }

            
        }
        

        if (Input.GetKeyDown(KeyCode.F1)&& movement2d.isGround)
        {
            mode = Mode.assassin;
            animator.runtimeAnimatorController = animation_controllers[0];
            smoke_script.activate();
        }
        if (Input.GetKeyDown(KeyCode.F2) && movement2d.isGround)
        {
            mode = Mode.shock;
            animator.runtimeAnimatorController = animation_controllers[1];
            smoke_script.activate();

        }
        if (Input.GetKeyDown(KeyCode.F3) && movement2d.isGround)
        {
            mode = Mode.shooter;
            animator.runtimeAnimatorController = animation_controllers[2];
            smoke_script.activate();
        }
    }
}
