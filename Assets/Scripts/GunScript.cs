using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public GameObject bullet = null;    //Projectile that the gun fires
    public Transform muzzlePoint;       //End of the barrel of the gun

    private List<GameObject> bList = null;

    private int ownerID = -1;

    private int LastFrameShot = -1;

    public int damage = 1;



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
        else if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            gameObject.transform.Rotate(0, 5, 0); 
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            gameObject.transform.Rotate(0, -5, 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            gameObject.transform.position += Vector3.forward * Time.deltaTime * 5;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.transform.position += -Vector3.forward * Time.deltaTime * 5;
        }


    }

    void FireProjectile()
    {
        GameObject newBullet = Instantiate(bullet, muzzlePoint.position, Quaternion.Euler(muzzlePoint.transform.forward));
        newBullet.GetComponent<NormalBullet>().FireBullet(gameObject, damage, ownerID);
        if(bList == null)
        {
            bList = new List<GameObject>();
        }
        bList.Add(newBullet);

    }
}
