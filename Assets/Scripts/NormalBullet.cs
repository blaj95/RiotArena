using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public float bulletSpeed = 1.0f;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        Debug.DrawRay(transform.position, Vector3.forward);
	}
}
