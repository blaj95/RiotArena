using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public GameObject bullet = null;    //Projectile that the gun fires
    public Transform muzzlePoint;       //End of the barrel of the gun

    private int LastFrameSHot = -1;



	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireProjectile();
        }
		
	}

    void FireProjectile()
    {
        GameObject newBullet = Instantiate(bullet, muzzlePoint.position, muzzlePoint.rotation);
        
    }
}
