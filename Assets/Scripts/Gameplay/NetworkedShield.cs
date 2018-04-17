using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Riot
{

    public class NetworkedShield : Photon.MonoBehaviour
    {
        public NetworkedPlayerController playerScript;
        public NetworkedPlayerController otherPlayerScript;
        public GameObject player;
        public GameObject bullet;
        public bool reflect = true;

        public void OnCollisionEnter(Collision collision)
        {
            if (!reflect && collision.transform.tag == "Bullet")    //If Not in reflect mode
            {
                if (photonView.isMine)
                {
                    photonView.RPC("collectBullet", PhotonTargets.All, null); //RPC to send bullet info to player
                    playerScript.blist.Remove(collision.gameObject);
                }
                otherPlayerScript.blist.Remove(collision.gameObject);
                Destroy(collision.gameObject);
                PhotonNetwork.Destroy(collision.gameObject);
                PhotonNetwork.Destroy(collision.gameObject.GetPhotonView());
                Destroy(collision.gameObject);
            }
            else
            {           //If if am in reflect mode
                if (collision.transform.tag == "Bullet")     //And I collided with a bullet
                {
                   
                    BulletNeworked bullet = collision.gameObject.GetComponent<BulletNeworked>();  //Then Tell the bullet to speeed up
                    if (bullet != null)
                    {
                        bullet.LVLUpBullet();
                       
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
                otherPlayerScript = player.GetComponent<NetworkedPlayerController>();
            }
                   

            bullet = GameObject.Find("Bullet(Clone)");
        }

        [PunRPC]
        private void collectBullet() //function to change playerscript bullet stats
        {
           
            playerScript.bulletsLeft++;
            playerScript.bulletCount.text = playerScript.bulletsLeft.ToString();

        }

        [PunRPC]
        private void maxBulletPlus()
        {
            playerScript.maxBullets++;
        }
    }
}
