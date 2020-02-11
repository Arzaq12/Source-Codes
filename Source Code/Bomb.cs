using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
                                                      ......//EXPLOSION OF A BOMB....


    public float delay=3f;
    float countdown;
    bool explode;
    public GameObject Explosion;
    float radius = 8f;

	// Use this for initialization
	void Start () {
        countdown = delay;
        explode = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(!explode&&ThrowBomb.clicked)
        {
            //countdown -= Time.deltaTime;
            StartCoroutine(wait());
            
            explode = true;
            
        }
		
	}
    void Explode()
    {
        Debug.Log("BOOM GOES THE DYNAMITE");
        Instantiate(Explosion, transform.position, transform.rotation);// this will instantiate a particle effect,explosion effect
       Collider[] colliders= Physics.OverlapSphere(transform.position, radius);//stores the colliders in the array near by the the bomb within given radius
        foreach (Collider nearbox in colliders)
        {
            Rigidbody rb = nearbox.GetComponent<Rigidbody>();//gets rigidbody and adds explosion force to all those objects
            if(rb!=null)
            {
                rb.AddExplosionForce(1000f, transform.position, radius);
            }
        }
        AudioSource audi = GameObject.Find("CUBES").GetComponent<AudioSource>();
        audi.Play();
        Destroy(gameObject);
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        Explode();
    }
}
