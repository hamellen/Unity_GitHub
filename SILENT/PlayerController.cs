using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject dialogue;
    public TextMeshProUGUI bullet_text, hp_text;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public Rigidbody rb;
    RotateToMouse rotateToMouse;
    public float movespeed, bulletspeed;
    public float walkspeed=10, runspeed=15;
    Vector3 movedirection;
    CharacterController characterController;
    public float current_hp;//ü�� 
    public Animator animator;
    public GameObject bullet_prefab;
    public Transform bulletSpawnPoint;
    public int initial_bullet_total, loaded_bullet, appropriate_bullet;//źâ�� �ִ� �Ѿ� ������ �� ����,źâ���� �Ѿ�,źâ�� ������ źâ�� 
    //Quaternion add_rotation;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //add_rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        rotateToMouse = GetComponent<RotateToMouse>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

   

    void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")//���Ϳ� �浹�ҽ� 
        {
            current_hp -= 10;
            audioSource.clip = audioClips[3];
            audioSource.Play();
        }
    }

    


    public void MoveTo(Vector3 direction)
    {

        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);
        movedirection = new Vector3(movespeed * direction.x, movedirection.y, movespeed * direction.z);
    }
    // Update is called once per frame
    void Update()
    {
        if (current_hp == 0)
        {
            SceneManager.LoadScene("dead_scene");
        }


        if (GameObject.FindGameObjectsWithTag("Horror_doll").Length==0)
        {
            SceneManager.LoadScene("end_scene");
        }
        hp_text.text = current_hp.ToString();
        bullet_text.text = loaded_bullet.ToString() + "  /  " + initial_bullet_total.ToString();
        movedirection.y += -9.8f * Time.deltaTime;//�߷� �ۿ�
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            int required_bullet = appropriate_bullet - loaded_bullet;
            if(required_bullet<= initial_bullet_total)
            {
                initial_bullet_total -= required_bullet;
                loaded_bullet = appropriate_bullet;
            }
            else if(required_bullet > initial_bullet_total)
            {
                loaded_bullet += initial_bullet_total;
                initial_bullet_total = 0;
            }
            //audioSource.Stop();
            audioSource.clip = audioClips[0];
            //audioSource.loop = false;
            audioSource.Play();
            animator.SetTrigger("Reload");

        }

        if (Input.GetKeyDown(KeyCode.F))//��ȣ�ۿ� 
        {
            RaycastHit hit;

            Vector3 fwd = bulletSpawnPoint.forward;

            if(Physics.Raycast(bulletSpawnPoint.position,fwd,out hit, 10) == false)
            {
                return;
            }

            if(hit.collider.gameObject.tag == "Horror_doll")
            {
                hit.collider.gameObject.GetComponent<Horror_doll_cs>().interaction_complete();

            }


            if(hit.collider.gameObject.tag == "Airdrop")
            {
                //���� �������� ��ȣ�ۿ�
                initial_bullet_total += hit.collider.gameObject.GetComponent<Airdrop_controller>().additional_bullet;
                current_hp += hit.collider.gameObject.GetComponent<Airdrop_controller>().additional_hp;
                hit.collider.gameObject.GetComponent<Airdrop_controller>().Play_item_sound();
                Destroy(hit.collider.gameObject);//������ ���� 
            }

            if(hit.collider.gameObject.tag == "Swat")
            {
                
                dialogue.GetComponent<Dialogue>().start_dialogue();

            }
          
        }

       
        if (Input.GetButtonDown("Fire1"))
        {
            if (loaded_bullet > 0)
            {
                //audioSource.Stop();
                animator.SetTrigger("Fire");
                audioSource.clip = audioClips[1];
                //audioSource.loop = false;
                audioSource.Play();
                loaded_bullet--;
                var bullet = Instantiate(bullet_prefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                //bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * bulletspeed);
            }
            
            if(loaded_bullet == 0)
            {
                //������ �ʿ� ���� �ϰ� ���� ��� 
            }

        }


        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))//�ٱ� 
        {
            movespeed = runspeed;
            animator.SetFloat("Movespeed", 1 * Input.GetAxis("Run"));
            //animator.SetBool("IsRun", true);//�ִϸ��̼� ����
            if (audioSource.isPlaying==false)
            {
                audioSource.clip = audioClips[2];
                audioSource.Play();
            }
        }
        else //�ȱ� 
        {
            //audioSource.clip = null;
            movespeed = walkspeed;
            animator.SetFloat("Movespeed", -1 * Input.GetAxis("Run"));
            //animator.SetBool("IsRun", false);//�ִϸ��̼� ����


        }


       

        MoveTo(new Vector3(x,0,y));
        characterController.Move(movedirection * Time.deltaTime);
        UpdateRotate();
    }

   
}
