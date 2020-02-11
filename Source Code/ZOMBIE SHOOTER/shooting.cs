using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{
    public Button shoootButton;
    public Camera fpscam;
    public float shootrange;
    Animator anim;
    public bool Fire;
    // Start is called before the first frame update
    void Start()
    {
       anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Fire)
        {
            shoot();
        }
    }
    public void shoot()
    {
        anim.SetBool("Recoil",true);
        {
            RaycastHit hit;
            if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, shootrange))
            {
                print("raycast hit");
                Debug.Log(hit.transform.name);
                if (hit.transform.gameObject.tag == "zombie")
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    enemy.TakeDamage();
                    
                }

            }
        }

    }
    void Mouse_shoot()
    {
        if (Input.GetMouseButton(0))
        {
            shoot();

        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Recoil", false);

        }
    }
    public void DontShoot()
    {
        anim.SetBool("Recoil", false);
        Fire = false;
    }
   public void buttonShoot()
   {
        Fire = true;
   }
}
