using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public float bulletSpeed = 1.0f;
    
    
    bool fired = false;
    int _damage = 1;
    int _ownerID = -1;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(fired)
        {
            transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
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
}
