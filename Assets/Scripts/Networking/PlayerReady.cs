using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReady : Photon.MonoBehaviour {

    public bool notmasterReady = false;

    LobbyManager lobbyMng;



	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RightNet")
        notmasterReady = true;
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "RightNet")
            notmasterReady = false;
        
    }
}
