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

    int maxBullets = -1;

    bool isShooting = false;

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
            
        }
	}

    void FireWeapon()
    {
        if(isShooting)
        {
            Debug.Log("Whoa there Partner, its not like the bullets are teleporting into the chamber");
            return;
        }
        else if(blist.Count > maxBullets)
        {
            Debug.Log("You have already shot all your bullets, Catch some more with your shield");
        }
        isShooting = true;

        GameObject newBullet = Instantiate(bullet, weapon.transform.forward, weapon.transform.rotation);
        blist.Add(newBullet);
    }
}
