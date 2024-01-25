using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster_patrolmove : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public int index_to_patrol;
    public GameObject central_point;
    public int from_central;
    public int count=0;
    public bool Is_patrol=true;
    public Vector3[] move_to_positions;
    public Animator animator;
    Monster_controller monster_ct;
    GameObject status_ui, end_ui;
    //public Rigidbody rb;
    // Start is called before the first frame update

    private void Start()
    {
        Monster_controller.patrol_start += Re_start_patrol;
        Monster_controller.patrol_end += Stop_patrol;
        InvokeRepeating("MoveToNextPoint", 0f, 3f);
    }
    void Awake()
    {
        status_ui = GameObject.FindWithTag("Normal_status_ui");
        end_ui = GameObject.FindWithTag("End_game_ui");
        //rb = GetComponent<Rigidbody>();
        count = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
        move_to_positions = new Vector3[index_to_patrol];
        animator = GetComponent<Animator>();
        monster_ct = GetComponent<Monster_controller>();
    }

    public void Re_start_patrol()
    {
        //rb.constraints = RigidbodyConstraints.None;
        navMeshAgent.isStopped = false;
        Is_patrol = true;
        InvokeRepeating("MoveToNextPoint", 0f, 3f);
    }

    public void MoveToNextPoint()
    {

        if (central_point == null)
        {
            GameObject[] remained_dolls = GameObject.FindGameObjectsWithTag("Horror_doll");

            if (remained_dolls.Length == 0)//모든 인형 다 흡수 할시 게임 종료 
            {
                //monster_ct.monster_speed = 0;
                //Stop_patrol();
                //navMeshAgent.speed = 0;
                //monster_ct.Is_monster_down = true;
                // animator.SetTrigger("Monster_down");
                Destroy(gameObject);
                SceneManager.LoadScene("end_scene");
            }

            else//남아 있는 경우 
            {
                central_point = remained_dolls[0].gameObject;
            }
        }

        else if (central_point != null)
        {
            move_to_positions[0] = new Vector3(0, 0, 1) * from_central + central_point.transform.position;
            move_to_positions[1] = new Vector3(1, 0, 0) * from_central + central_point.transform.position;
            move_to_positions[2] = new Vector3(0, 0, -1) * from_central + central_point.transform.position;
            move_to_positions[3] = new Vector3(-1, 0, 0) * from_central + central_point.transform.position;

            animator.SetBool("Is_Move", true);
            //transform.LookAt(move_to_positions[count]);
            navMeshAgent.SetDestination(move_to_positions[count]);
        }


       



    }

    public void Stop_patrol()
    {
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        navMeshAgent.isStopped = true;
        Is_patrol = false;
        CancelInvoke(" MoveToNextPoint");

    }

    private void Update()
    {
        if (transform.position.x== move_to_positions[count].x && transform.position.z == move_to_positions[count].z)
        {
            navMeshAgent.velocity = Vector3.zero;
            count+=1;
            animator.SetBool("Is_Move", false);
            if (count >= move_to_positions.Length)
            {
                count = 0;
            }
        }

    }


    // Update is called once per frame

}
