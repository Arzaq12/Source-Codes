using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
                    .......//THIS WILL HANDLE BEHAVIOUR OF ENEMY MOVEMENT AND ATTACK ON THE PLAYER..........

    Vector3 target;
   public  float dist;
    public float speed;
    
    Animator anim;
    public int health=100;
    public int destroyDelay;
    [Header("Player Attributes")]
    PlayerHealth PH;
    public bool takingDamage;
    public float T2;
    float T;
    UIscripts UI;
    // Start is called before the first frame update
    void Start()
    {
        PH = GameObject.Find("Arm").GetComponent<PlayerHealth>();
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        anim = GetComponent<Animator>();
        T = 1.9f;
        takingDamage = false;
        UI = GameObject.Find("Canvas").GetComponent<UIscripts>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
        if(Input.GetMouseButtonUp(0))
        {
            anim.ResetTrigger("damage");
        }
        if (takingDamage)
        {
            if (T < 0)
            {
                PH.P_health -= 10;
                T = T2;
            }
            else
            {
                T -= Time.deltaTime;
            }

        }
    }
    public void TakeDamage()
    {
        health -= 10;
        if(health==0)
        {
            Destroy(gameObject, destroyDelay);
            anim.SetTrigger("death");
            speed = 0;
            UI.Kills += 1;
        }
        else if(health>0)
        {
            anim.SetTrigger("damage");
            
        }
    }
    public void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, target) < dist)
        {
            print("Enemy near");
            anim.SetTrigger("attack");
            takingDamage = true;
        }
        else
        {

            takingDamage = false;
            transform.LookAt(new Vector3(target.x, 0f, target.z));
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(0f, 0f, target.z), speed * Time.deltaTime);
        }
    }
   
}
