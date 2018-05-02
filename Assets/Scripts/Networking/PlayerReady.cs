using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReady : Photon.MonoBehaviour {

    public bool notmasterReady = false;
    public GameObject buttonModel;
    LobbyManager lobbyMng;



	// Use this for initialization
	void Start ()
    {
        lobbyMng = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        buttonModel = GetComponentInChildren<MeshRenderer>().gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RightNet")
        {
            notmasterReady = true;
            photonView.RPC("notMasterisReady", PhotonTargets.All);
            buttonModel.transform.Translate(Vector3.back * .07f, Space.World);
        }
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "RightNet")
        {
            notmasterReady = false;
            photonView.RPC("notMasterisNotReady", PhotonTargets.All);
            buttonModel.transform.Translate(Vector3.forward * .07f, Space.World);
        }
    }


    [PunRPC]
    public void notMasterisReady()
    {
        lobbyMng.playerStart = true;
    }

    [PunRPC]
    public void notMasterisNotReady()
    {
        lobbyMng.playerStart = false;
    }
}
