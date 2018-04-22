﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class NetworkedShield : Photon.MonoBehaviour
 {
        public NetworkedPlayerController playerScript;
        public GameObject player;
       
        public bool reflect;

        public void OnCollisionEnter(Collision collision)
        {
            
        }

        private void Update()
        {
            if (Input.GetButton("LSelectTrigger"))
            {
                reflect = false;
                if(photonView.isMine)
                photonView.RPC("SetReflectFalse",PhotonTargets.All);
            }
            else
            {
                reflect = true;
                
                photonView.RPC("SetReflectTrue", PhotonTargets.All);
        }

            if (photonView.isMine)
            {
                player = GameObject.Find("WeaponLobbyPlayer(Clone)");
                playerScript = player.GetComponent<NetworkedPlayerController>();
                
            }
           
                
        }

    public void AddBullet()
    {
        playerScript.currentBulletsOut--;
    }

    [PunRPC]
    void SetReflectTrue()
    {
        if (photonView.isMine)
            reflect = true;
    }

    [PunRPC]
    void SetReflectFalse()
    {
        if (photonView.isMine)
            reflect = false;
    }
}

