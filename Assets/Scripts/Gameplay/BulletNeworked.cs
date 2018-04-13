using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNeworked : Photon.MonoBehaviour
{

    Rigidbody rigidB;

    private float fraction;

    public float bulletSpeedIncrease;
    public float bulletSpeed = 10f;

    private Vector3 vel = new Vector3();
   

    bool fired = false;
    int _damage = 1;
    int _ownerID = -1;

    // Use this for initialization
    void Start ()
    {
        vel = transform.forward;  
    }

    void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
  

    private void FixedUpdate()
    {
        if (fired)
        {
            //transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
           rigidB.MovePosition(transform.localPosition + vel * bulletSpeed * Time.deltaTime);
           rigidB.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
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
        foreach (ContactPoint contact in collision.contacts)
        {
            vel = Vector3.Reflect(vel, contact.normal);
        }

        if(collision.transform.tag == "Wall")
        {
            bulletSpeed += bulletSpeedIncrease;
        }

        if(collision.transform.tag == "Shield")
        {
            Debug.Log("ADD BULLET COLLECT LOGIC");
        }

        
    }
}
