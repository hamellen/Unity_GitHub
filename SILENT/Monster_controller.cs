using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Monster_controller;

public class Monster_controller : MonoBehaviour
{
    public Rigidbody rb;
    public float view_angle, view_distance;
    public LayerMask view_layermask;

    public float initial_navagent_speed;
    public  AudioClip[] monster_clips;
    public AudioSource audioSource;
    public bool Is_Chased=false;
    public int monster_speed,initial_movespeed;
    //public Transform target;
    public int required_down_bullet,current_hited_bullet;//기절하는데 필요한 총알 갯수, 현재 맞은 총알 갯수 
    public bool Is_monster_down=false;
    NavMeshAgent navMeshAgent;
    public Animator animator;
    // Start is called before the first frame update

    public delegate void Patrol_start();
    public static event Patrol_start patrol_start;

    public delegate void Patrol_end();
    public static event Patrol_start patrol_end;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        initial_movespeed = monster_speed;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            audioSource.clip = monster_clips[0];
            audioSource.Play();
            current_hited_bullet++;
        }
    }



    // Update is called once per frame
    void Update()
    {

        Sight();



        if (current_hited_bullet == required_down_bullet)
        {
            if (!Is_monster_down)
            {
                monster_speed = 0;
                patrol_end();
                initial_navagent_speed = navMeshAgent.speed;
                navMeshAgent.speed = 0;
                audioSource.clip = monster_clips[3];
                audioSource.Play();
                Is_monster_down = true;
                animator.SetTrigger("Monster_down");
                Invoke("Revive", 20f);//30초후에 재 부활
            }
        }

    }

    public void Revive()
    {
        monster_speed = initial_movespeed;
        navMeshAgent.speed = initial_navagent_speed;
        current_hited_bullet = 0;
        animator.SetTrigger("Monster_revive");//몬스터 재부활
        Is_monster_down = false;
    }

    void Sight()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, view_distance, view_layermask);

        if (cols.Length == 0)//순환움직임 
        {
            patrol_start();
            Is_Chased = false;
        }

        if (cols.Length > 0)//플레이어 탐지 
        {
           
            Transform spotted_player = cols[0].transform;

            Vector3 spotted_direction = (spotted_player.position - transform.position).normalized;
            float spotted_angle = Vector3.Angle(spotted_direction, transform.forward);

            if(spotted_angle < view_angle * 0.5f)
            {
                if(Physics.Raycast(transform.position,spotted_direction,out RaycastHit hit, view_distance))
                {
                    if (hit.transform.gameObject.tag == "Player")//추적시작
                    {
                        patrol_end();
                        Is_Chased = true;
                        animator.SetBool("Is_Move", true);
                        audioSource.clip= monster_clips[2];//걷기 사운드 
                        audioSource.Play();
                        Debug.Log("몬스터 추격중");
                        transform.LookAt(hit.transform.position);
                        transform.position += spotted_direction * Time.deltaTime * monster_speed;
                        //navMeshAgent.SetDestination(hit.transform.position);
                    }
                }
            }
        }
    }
}
