using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
  ..........THIS SCRIPT WILL CHECK WHETHER MOUSE WAS CLICKED TWICE , IF YES THEN PLAYER WILL PLAY DIFFERENT ATTACK ANIMATION..........

    public float LastclickTime;
    public Player playersc;
    public float DoubleClickedTime=0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            
            float timeSincelastClick = Time.time - LastclickTime;
            if(timeSincelastClick<=DoubleClickedTime)
            {
                print("Double Clicked!!!");
                playersc.Anim.SetTrigger("ExtendAttack");
            }
            else
            {
                print("Single Click");
            }
            LastclickTime = Time.time;
        }
        
    }
    
}
