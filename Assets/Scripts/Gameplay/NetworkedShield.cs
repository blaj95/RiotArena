using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class NetworkedShield : Photon.MonoBehaviour
    {
        public NetworkedPlayerController playerScript;
        public NetworkedPlayerController otherPlayerScript;
        public BulletNeworked bulletNet;
        public GameObject player;
       
        public bool reflect = true;

        public void OnCollisionEnter(Collision collision)
        {
            
        }

        private void Update()
        {
            if (Input.GetButton("LSelectTrigger"))
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
        }
    }

