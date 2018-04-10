using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    GameObject weapon = null;

    [SerializeField]
    GameObject shield = null;

    [SerializeField]
    GameObject bullet = null;


    int bulletsLeft = -1;
    int bulletsFired = -1;
    public int maxBullets = -1;
    public int damage = -1;


    public int playerID = 1;

    bool isShooting = false;

    public float rateOfFire = -1.0f;
    float nextFire = 1.0f;

    

    List<GameObject> blist = new List<GameObject>(); //Holds a list of all bullets for that player
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
	}

    void FireWeapon()
    {
        if(Time.time < nextFire)
        {
            Debug.Log("Whoa there Partner, its not like the bullets are teleporting into the chamber");
            return;
        }
        else if(bulletsLeft == 0)
        {
            Debug.Log("You have already shot all your bullets, Catch some more with your shield");
            return;
        }
        isShooting = true;

        nextFire = Time.time + rateOfFire;
        GameObject newBullet = Instantiate(bullet, weapon.transform.position + weapon.transform.forward, weapon.transform.rotation);
        newBullet.GetComponent<NormalBullet>().FireBullet(weapon, damage, playerID);
        blist.Add(newBullet);
        bulletsFired++;
        bulletsLeft = maxBullets - bulletsFired;
        
        
    }
}
 