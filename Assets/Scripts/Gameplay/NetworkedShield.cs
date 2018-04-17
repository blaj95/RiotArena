using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class NetworkedShield : Photon.MonoBehaviour
    {
        public NetworkedPlayerController playerScript;
        public NetworkedPlayerController otherPlayerScript;
        public BulletNeworked bulletNet;
        public GameObject player;
        public GameObject bullet;
        public bool reflect = true;

        public void OnCollisionEnter(Collision collision)
        {

            if (collision.transform.tag == "Bullet" && reflect)     //And I collided with a bullet
            {
                bulletNet = collision.gameObject.GetComponent<BulletNeworked>();

                BulletNeworked bullet = collision.gameObject.GetComponent<BulletNeworked>(); 
                foreach (ContactPoint contact in collision.contacts)
                {
                    bulletNet.vel = Vector3.Reflect(transform.forward, contact.normal);
                }
                    if (bullet != null)
                    {
                        bullet.LVLUpBullet();
                       
                    }
            }
            else if(collision.transform.tag == "Bullet" && !reflect)
            {
                if (photonView.isMine)
                {
                    playerScript.bulletsLeft++;
                    if (playerScript.bulletsLeft > playerScript.maxBullets)
                    {
                        playerScript.maxBullets++;
                    }

                }
                if (collision.gameObject.GetPhotonView().isMine)
                {
                    playerScript.currentBulletsOut--;
                    if (playerScript.currentBulletsOut <0)
                    {
                        playerScript.currentBulletsOut = 0;
                    }
                }
                else
                {
                    if (playerScript.currentBulletsOut < 0)
                    {
                        playerScript.currentBulletsOut = 0;
                    }
                }
            }
        }

        private void Update()
        {
            if (Input.GetButton("LSelectTrigger") == true)
            {
                reflect = false;
            }
            else
            {
                reflect = true;
            }

            if (photonView.isMine)
            {
                player = GameObject.Find("WeaponLobbyPlayer(Clone)");
                playerScript = player.GetComponent<NetworkedPlayerController>();
                
            }
            else
            {
                playerScript = player.GetComponent<NetworkedPlayerController>();
            }
                   
            
            bullet = GameObject.Find("Bullet(Clone)");
        }
    

       
    }

