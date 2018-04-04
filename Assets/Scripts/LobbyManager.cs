using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Photon.PunBehaviour
{
    public GameObject MasterSpawn;
    public GameObject Spawn;
	// Use this for initialization
	void Start ()
    {

        MasterSpawn = GameObject.Find("SpawnMaster");
        Spawn = GameObject.Find("SpawnMaster");

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
            PhotonNetwork.Instantiate("LobbyPlayer", MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0), 0);

        }
        else
        {
            PhotonNetwork.Instantiate("LobbyPlayer", Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 180, 0), 0);

        }

        base.OnJoinedRoom();
    }

}
