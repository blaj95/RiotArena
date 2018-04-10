using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public float bulletSpeed = 1000f;
    
    
    bool fired = false;
    int _damage = 1;
    int _ownerID = -1;

    private Vector3 vel = new Vector3();

    private Rigidbody rigidbody = null;


	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        vel = transform.forward;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(fired)
        {
            //transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
            rigidbody.MovePosition(rigidbody.position + vel * bulletSpeed * Time.deltaTime);
        }
        Debug.DrawRay(transform.position, Vector3.forward, Color.red);
        Debug.DrawRay(transform.position, Vector3.up, Color.yellow);
        
    }

    public void FireBullet(GameObject parent, int damage, int ownerID)
    {
        fired = true;
        transform.rotation = parent.transform.rotation;
        transform.Rotate(Vector3.forward);
        _ownerID = ownerID;
        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            vel = Vector3.Reflect(vel, contact.normal);
        }
        
    }
}
