﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour
{
    public GameObject player;
    public GameObject MasterSpawn;
    public GameObject Spawn;

    // Use this for initialization
    void Start ()
    {
       
        PhotonNetwork.JoinRandomRoom();
    }
	
	// Update is called once per frame
	void Awake ()
    {
        PhotonNetwork.automaticallySyncScene = true;
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
        base.OnPhotonRandomJoinFailed(codeAndMsg);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            GameObject theplayer = PhotonNetwork.Instantiate("LobbyPlayer", MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("ControllerLeft", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("ControllerRight", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("Head", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            Instantiate(player, MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0));

        }
        else
        {
            GameObject theplayer = PhotonNetwork.Instantiate("LobbyPlayer", Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            PhotonNetwork.Instantiate("ControllerLeft", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("ControllerRight", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("Head", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            Instantiate(player, Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0));
        }

        base.OnJoinedRoom();
    }

}