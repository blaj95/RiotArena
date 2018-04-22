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
                
            }
            else
            {
                reflectID = 1;
                if (reflectID == 1)
                    reflect = true;
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting && !photonView.isMine)
        {

            stream.SendNext(reflectID);
        }
        else if(stream.isReading && !photonView.isMine)
        {
            reflectID = (int)stream.ReceiveNext();

        }
    }
}
