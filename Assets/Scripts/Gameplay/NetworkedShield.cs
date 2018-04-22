using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedShield : Photon.MonoBehaviour
{
    public NetworkedPlayerController playerScript;
    public GameObject player;
    public int reflectID;
    public bool reflect;

    public void OnCollisionEnter(Collision collision)
    {

    }

    private void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetButton("LSelectTrigger"))
            {
                reflectID = 0;
                if(reflectID == 0)
                reflect = false;
                if (photonView.isMine)
                {
                    photonView.RPC("ReflectFalse", PhotonTargets.Others);
                }
            }
            else
            {
                reflectID = 1;
                if (reflectID == 1)
                    reflect = true;
                if (photonView.isMine)
                {
                    photonView.RPC("ReflectTrue", PhotonTargets.Others);
                }
            }
        }
        else
        {

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
    void ReflectTrue()
    {
        reflect = true;
    }
    [PunRPC]
    void ReflectFalse()
    {
        reflect = false;
    }

}
