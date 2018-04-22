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
            if (Time.time < nextFire)
            {
                Debug.Log("Whoa there Partner, its not like the bullets are teleporting into the chamber");
                return;
            }
            if (Input.GetButtonDown("RSelectTrigger"))
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
                else
                {
                    photonView.RPC("FireWeapon", PhotonTargets.All, weaponTip.transform.forward, weaponTip.transform.rotation);
                    isShooting = true;
                    nextFire = Time.time + rateOfFire;
                    bulletsFired++;
                    currentBulletsOut++;
                    
                }
                
            }
            bulletsLeft = maxBullets - currentBulletsOut;
            bulletCount.text = bulletsLeft.ToString();
            if (bulletsLeft <= 0)
            {
                bulletsLeft = 0;
            }
        }
      

        
    }


    [PunRPC]
    void FireWeapon(Vector3 shootPos, Vector3 shootRot)
    {
            GameObject newBullet = Instantiate(bulletPrefab, shootPos, Quaternion.Euler(shootRot));
            newBullet.GetComponent<BulletNeworked>().FireBullet(weaponTip, damage, playerID);
            newBullet.GetComponent<BulletNeworked>().GetShooter(gameObject);   
    }
}
