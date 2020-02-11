using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed,SwingForce;
    public float JumpHeight;
    public float gravity;
    public bool Grounded;
    public bool Running;
    public Animator Anim;
    SpriteRenderer sr;
    GrappleHook GrappleScript;
    Cammov CM;
    BoxCollider2D col;
    public GameObject BulletPrefab;
    public Transform BulPos;
    SpriteRenderer BulletSR;
    BulletMovement BulletSC;
    public Transform AttackPos;
    public float AttackRange;
    public LayerMask OnlyEnemies;
    private float timedelay;
    public float Timestamp;
    public float jumpcounter,JumpCounts;
    private float _jumpC;
    public float DamageForce;
    public bool PlayerInAir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        GrappleScript = GetComponent<GrappleHook>();
        //InvokeRepeating("Shoot", 0f, 0.5f);
        BulletSR = BulletPrefab.GetComponent<SpriteRenderer>();
        BulletSC = BulletPrefab.GetComponent<BulletMovement>();
        col = GetComponent<BoxCollider2D>();
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            Grounded = true;
            _jumpC  = jumpcounter;
            Anim.SetBool("JumpAnim", false);
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = false;
            Anim.SetBool("RunAnim", false);
        }
        PlayerInAir = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boss1")
        {
            rb.AddRelativeForce(new Vector3(-1, 0, 0) * DamageForce * Time.deltaTime);
            Anim.SetTrigger("Blink");
        }
        JumpCounts = 0;
    }


    // Update is called once per frame
    void Update()
    {

        rb.AddForce(new Vector2(0, -gravity * rb.mass));
        if(Grounded)
        {
            Mov();
        }
        OnAirMechanics();
        Jump();
        Attack();
        Slide();
        Shoot();
        OnFlip();
    }

    public void Mov()
    {
        float movx = Input.GetAxis("Horizontal");
        
        //float movy = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(movx * speed, 0);
        Anim.SetBool("RunAnim", true);
        
        if (movx==0)
        {
            Anim.SetBool("RunAnim", false);
            Running = false;
        }
        if(movx<0)
        {
            sr.flipX = true;
            Running = true;
        }
        if (movx > 0)
        {
            sr.flipX = false;
            Running = true;
        }
       
    }

    public void Jump()
    {
        if (Input.GetButton("Jump"))
        {

            if (Grounded || _jumpC > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, CalculateJump());
                Anim.SetBool("JumpAnim", true);
                _jumpC -= Time.deltaTime;
                JumpCounts += 1;
            }
            else
            {
                Summersault();
            }
        }
       

           // rb.AddForce(new Vector2(rb.velocity.x,20));
     
    }

    float CalculateJump()
    {
        float Jump = Mathf.Sqrt(2 * JumpHeight*gravity);
        return Jump;
    }
    void OnAirMechanics()//Players Movement and jump when on air like during Grappling
    {
        if (!Grounded && GrappleScript.Grappling1)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("Force Added To The Right");
                sr.flipX = false;
                rb.AddForce(Vector2.right * SwingForce);

            }
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("Force Added To The Left");
                sr.flipX = true;
                rb.AddForce(Vector2.left * SwingForce);

            }
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, CalculateJump());
                Anim.SetBool("JumpAnim", true);

                GrappleScript.GrappleRope.enabled = false;
                GrappleScript.RopeLine.enabled = false;
                GrappleScript.Grappling1 = false;
            }
        }
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Anim.SetTrigger("AttackAnim");
            Collider2D[] EnemiesInRange = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, OnlyEnemies);
            for (int i = 0; i < EnemiesInRange.Length; i++)
            {
                EnemiesInRange[i].GetComponent<Enemy>().TakeDamage();
            }

        }
        if(Input.GetMouseButtonUp(0))
        {
            Anim.ResetTrigger("AttackAnim");
        }

        /*{
            if(Timestamp<=0)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Anim.SetTrigger("AttackAnim");
                    Collider2D[] EnemiesInRange = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, OnlyEnemies);
                    for (int i = 0; i < EnemiesInRange.Length; i++)
                    {
                        EnemiesInRange[i].GetComponent<Enemy>().TakeDamage();
                    }
                    
                }
                Timestamp = timedelay;
            }
            else
            {
                Timestamp -= Time.deltaTime;
            }
        }*/
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRange);
    }
    void Slide()
    {
        if(Input.GetKey(KeyCode.S)&&Running==true)
        {
            Anim.SetBool("SlideAnim",true);
            col.size = new Vector2(0.64f, 0.22f);
            col.offset = new Vector2(-0.01f, -0.41f);
        }
        if(Input.GetKeyUp(KeyCode.S)||Running==false)
        {
            Anim.SetBool("SlideAnim", false);
            col.size = new Vector2(0.64f, 0.93f);
            col.offset = new Vector2(-0.01f, -0.11f);
        }
    }
    

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(BulletPrefab, BulPos.position, Quaternion.identity);
            Anim.SetBool("CastAnim", true);
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            Anim.SetBool("CastAnim", false);
        }
        
    }
    
    void OnFlip()
    {
        if(sr.flipX)
        {
            BulletSR.flipX = true;
            BulletSC.BulSpeed = -0.2f;
            BulPos.localPosition = new Vector3(-0.9f, 0, 0);
        }
        else
        {
            BulletSR.flipX = false;
            BulletSC.BulSpeed = 0.2f;
            BulPos.localPosition = new Vector3(0.9f, 0, 0);
        }
    }
    void Summersault()
    {
        if(PlayerInAir&&JumpCounts>1)
        {
            Anim.SetTrigger("SumSault");
        }
    }
    
}
