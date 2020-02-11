using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int health=100;
    public Transform RayOrigin;
    public float Patrolspeed;
    public bool Movingright = true;
    public float rayDist;
    Transform Target;
    public Vector2 PlayerCheckBoxSize;
    public static bool Alert;
    Blink alert;
    Health healthSC;
    
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Izan").GetComponent<Transform>();
        alert = GetComponentInChildren<Blink>();
        healthSC = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        Death();
        EnemyPatrol();
        EnemyFollow();
        healthSC.healthbar.transform.eulerAngles = new Vector3(0, 180, 0);
    }
        
    public void TakeDamage()
    {
        health -= 10;
        healthSC.healthbar.fillAmount -= 0.1f;
        
    }
    void Death()
    {
        if(health<=0)
        {
            Destroy(this.gameObject);
        }
    }
    void EnemyPatrol()
    {
        RaycastHit2D EdgeDetection = Physics2D.Raycast(RayOrigin.position, Vector2.down,rayDist);
        
        if (EdgeDetection.collider == false)
        {
            if (Movingright)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);

                Movingright = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                Movingright = true;
            }
        }
        
    }
    void EnemyMove()
    {
        transform.Translate(Vector2.right * Patrolspeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            //Patrolspeed = 0;
            GameObject.Find("Izan").GetComponent<Player>().Grounded = true; 
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject.Find("Izan").GetComponent<Player>().Grounded = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Bullet")
        {
            TakeDamage();
        }
    }
    void EnemyFollow()
    {
        Collider2D checkForPlayer = Physics2D.OverlapBox(transform.position, PlayerCheckBoxSize, 0);
        if (checkForPlayer.gameObject.tag == "Player")
        {
            
            if ((transform.position.x - Target.position.x) < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);//player is right of enemy
                
            }
            else if ((transform.position.x - Target.position.x) > 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);//player is left of enemy
                
            }
            Alert = true;
            alert.BlinkOn();
            print("Player Insight");
        }
        else
        {
            print("PlayerOutOfSight");
            Alert = false;
            alert.BlinkOff();
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, PlayerCheckBoxSize);
        Gizmos.color = Color.red;
    }
}
