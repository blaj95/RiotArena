using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class NetworkedShield : Photon.MonoBehaviour
    {
        public NetworkedPlayerController playerScript;
        public NetworkedPlayerController otherPlayerScript;
        public GameObject player;
        public GameObject bullet;
        public bool reflect = true;

        public void OnCollisionEnter(Collision collision)
        {
                if (collision.transform.tag == "Bullet" && reflect)     //And I collided with a bullet
                {
                   
                    BulletNeworked bullet = collision.gameObject.GetComponent<BulletNeworked>();  //Then Tell the bullet to speeed up
                    if (bullet != null)
                    {
                        bullet.LVLUpBullet();
                       
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
                otherPlayerScript = player.GetComponent<NetworkedPlayerController>();
            }
                   

            bullet = GameObject.Find("Bullet(Clone)");
        }
    

       
    }

