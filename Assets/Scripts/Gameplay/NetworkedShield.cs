using System.Collections;
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
        if (photonView.isMine)
        {
            if (Input.GetButton("LSelectTrigger"))
            {
                reflect = false;
               
            }
            else
            {
                reflect = true;
            }
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {

            stream.SendNext(reflect);
        }
        else
        {
            reflect = (bool)stream.ReceiveNext();

        }
    }
}
