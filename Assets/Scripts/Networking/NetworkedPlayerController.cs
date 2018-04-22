using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class NetworkedPlayerController : Photon.MonoBehaviour
{

    [SerializeField]
    GameObject weapon = null;

    public GameObject rightHand;
    public GameObject weaponTip;

    [SerializeField]
    GameObject shield = null;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    public GameObject bulletPrefab;

    public Text bulletCount;

    public int currentBulletsOut;
    public int bulletsLeft =-1;
    public int bulletsFired;
    public int maxBullets;
    public int damage =1;

    


    public int playerID = 1;

    bool isShooting = false;

    public float rateOfFire = -1.0f;
    float nextFire = 1.0f;
    
    void Start()
    {

    }

    private void Awake()
    {
        bulletsLeft = maxBullets;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            rightHand = GameObject.Find("ControllerRightWeapon(Clone)");

            weaponTip = GameObject.Find("ControllerRightWeapon(Clone)/Rtip");

            bulletCount = GameObject.Find("ControllerRightWeapon(Clone)/Canvas/Text").gameObject.GetComponent<Text>();


        }
        if (Input.GetButtonDown("RSelectTrigger") && photonView.isMine)
        {
            photonView.RPC("FireWeapon", PhotonTargets.All, null);
        }

        bulletCount.text = bulletsLeft.ToString();
        bullet = GameObject.Find("Bullet(Clone)");

        if(bulletsLeft <= 0)
        {
            bulletsLeft = 0;
        }
    }


    [PunRPC]
    void FireWeapon()
    {
            if (Time.time < nextFire)
            {
                Debug.Log("Whoa there Partner, its not like the bullets are teleporting into the chamber");
                return;
            }
            else if (bulletsLeft == 0)
            {
                Debug.Log("You have already shot all your bullets, Catch some more with your shield");
                return;
            }
            isShooting = true;

            nextFire = Time.time + rateOfFire;
            GameObject newBullet = Instantiate(bulletPrefab, weaponTip.transform.position + weaponTip.transform.forward, weaponTip.transform.rotation);
            newBullet.GetComponent<BulletNeworked>().FireBullet(weaponTip, damage, playerID);
            newBullet.GetComponent<BulletNeworked>().GetShooter(gameObject);

            bulletsFired++;
            currentBulletsOut++;
            bulletsLeft = maxBullets - currentBulletsOut;
        
    }
}
