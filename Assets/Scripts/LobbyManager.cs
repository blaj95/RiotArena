using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Photon.PunBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        PhotonNetwork.automaticallySyncScene = true;
    }
}
