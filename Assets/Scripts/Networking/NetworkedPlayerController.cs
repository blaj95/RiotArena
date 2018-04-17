﻿using System.Collections;
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
    GameObject bullet = null;

    public Text bulletCount;
   

    public int bulletsLeft;
    public int bulletsFired;
    public int maxBullets;
    public int damage =1;


    public int playerID = 1;

    bool isShooting = false;

    public float rateOfFire = -1.0f;
    float nextFire = 1.0f;



    public List<GameObject> blist = new List<GameObject>(); //Holds a list of all bullets for that player
                                                    
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

            bulletCount = GameObject.Find("ControllerRightWeapon(Clone)/gun/Canvas/Text").gameObject.GetComponent<Text>();


        }

        if (Input.GetButtonDown("RSelectTrigger") && photonView.isMine)
        {
            photonView.RPC("FireWeapon", PhotonTargets.All, null);
        }

        bulletCount.text = bulletsLeft.ToString();
        bullet = GameObject.Find("Bullet(Clone)");

        if(maxBullets < 0)
        {
            maxBullets = 0;
        }
    }


    [PunRPC]
    void FireWeapon()
    {
        if (photonView.isMine)
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
            GameObject newBullet = PhotonNetwork.Instantiate("Bullet", weaponTip.transform.position + weaponTip.transform.forward, weaponTip.transform.rotation, 0);
            newBullet.GetComponent<BulletNeworked>().FireBullet(weaponTip, damage, playerID);
            blist.Add(newBullet);
            bulletsFired++;
            bulletsLeft = maxBullets - blist.Count;
            bulletCount.text = bulletsLeft.ToString();
        }
    }

    public void OnChildCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            if (blist.Contains(collision.gameObject))
            {
                blist.Remove(collision.gameObject);
                bulletsLeft++;
                bulletCount.text = bulletsLeft.ToString();
                Destroy(collision.gameObject);
                PhotonNetwork.Destroy(collision.gameObject);
                PhotonNetwork.Destroy(collision.gameObject.GetPhotonView());
            }
            else
            {
                maxBullets++;
                Destroy(collision.gameObject);
            }
        }
    }
    }
