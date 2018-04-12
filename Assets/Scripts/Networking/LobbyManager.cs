using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Photon.PunBehaviour
{
    public GameObject MasterSpawn;
    public GameObject Spawn;
    public GameObject player;
    public bool masterStart = false;
    public bool playerStart = false;
  
	// Use this for initialization
	void Start ()
    {

        //MasterSpawn = GameObject.Find("SpawnMaster");
        //Spawn = GameObject.Find("SpawnMaster");

        PhotonNetwork.JoinRandomRoom();
        
    
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Awake()
    {
        PhotonNetwork.automaticallySyncScene = true;
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("MEE");
        }
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
