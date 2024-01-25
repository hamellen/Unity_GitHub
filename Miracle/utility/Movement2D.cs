using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement2D : MonoBehaviour
{

    
    public float speed;

    public float Invincible_time;//피격시 무적시간
    public float jumpforce = 8.0f;

    public bool is_attacked;
    private Rigidbody2D rigid2d;

    [SerializeField]
    private LayerMask groundLayer;
    private PolygonCollider2D polygoncollider2d;
    public  bool isGround;
    private Vector3 footposition;
    private SpriteRenderer sprite;
   




    // Start is called before the first frame update
    private void Awake()
    {
        rigid2d=GetComponent<Rigidbody2D>();
        polygoncollider2d = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Bounds bounds = polygoncollider2d.bounds;
        footposition = new Vector2(bounds.center.x, bounds.min.y);
        isGround = Physics2D.OverlapCircle(footposition, 0.1f, groundLayer);

        if (Input.GetButton("Horizontal"))
        {
            sprite.flipX = rigid2d.velocity.x < 0 ? true : false;
        }

        

    }

   

    public void Move(float x)
    {
        rigid2d.velocity = new Vector2(x * speed, rigid2d.velocity.y);
    }

    public void Jump() {

        if (isGround==true) {
            rigid2d.velocity = Vector2.up * jumpforce;
        }
        
    }
    public void OnDamaged(Vector2 damagecposition) {
        gameObject.layer = 9;
        sprite.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - damagecposition.x > 0 ?  1 : -1;
        rigid2d.AddForce(new Vector2(dirc, 1)*3, ForceMode2D.Impulse);//피격시 튕김
        is_attacked = true;

        Invoke("OffDamaged", Invincible_time);//무적시간 설정

    }

    public void OffDamaged()
    {
        is_attacked = false;
        gameObject.layer = 7;
        sprite.color = new Color(1, 1, 1, 1);


    }
}
