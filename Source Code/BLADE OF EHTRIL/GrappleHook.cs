using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    [HideInInspector] 
    public DistanceJoint2D GrappleRope;
    
    Vector3 targetPos;
    RaycastHit2D hit;
    public float MaxDist;
    public LayerMask mask;
    public LineRenderer RopeLine;
    public float step = 0.2f;
    public bool Grappling1;
    public bool Grappling2;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        GrappleRope = GetComponent<DistanceJoint2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Grapple();//Grapples a point on the object and Swings
        Grapple2();// Grapples an object and moves towards the Grappled Point
    }
    public void Grapple()
    {
        
        if(Input.GetMouseButtonDown(1))
        {
            Grappling1 = true;
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            anim.SetBool("CastAnim", true);
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position,MaxDist,mask);

            if(hit.collider!=null&&hit.collider.gameObject.GetComponent<Rigidbody2D>()!=null)
            {
                GrappleRope.enabled = true;
                GrappleRope.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                GrappleRope.distance = Vector2.Distance(transform.position, hit.point);
                //GrappleRope.connectedAnchor = hit.collider.transform.position;
                GrappleRope.connectedAnchor=hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);

                RopeLine.enabled = true;
                //RopeLine.SetPosition(0, transform.position);
                RopeLine.SetPosition(1, hit.collider.transform.position);
            }
        }
        
        if(Input.GetMouseButtonUp(1))
        {
            GrappleRope.enabled = false;
            RopeLine.enabled = false;
            Grappling1 = false;
            anim.SetBool("CastAnim", false);
        }
    
    }
    void Grapple2()
    {
       
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Grappling2 = true;
            print("Grapple2 Working");

        }
        if (GrappleRope.distance > 0f && Grappling2)
        {

            GrappleRope.distance -= step;

        }
        if(Input.GetKeyUp(KeyCode.LeftControl) || Input.GetMouseButtonUp(0))
        {
            GrappleRope.enabled = false;
            RopeLine.enabled = false;
            Grappling2 = false;
        }
        
        RopeLine.SetPosition(0, transform.position);


    }
  

}
