using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMasterReady : Photon.MonoBehaviour {

    public bool masterReady = false;

    LobbyManager lobbyMng;

    

    // Use this for initialization
    void Start()
    {
        lobbyMng = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MasterLever")
            masterReady = true;
        photonView.RPC("MasterisReady", PhotonTargets.All);
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "MasterLever")
            masterReady = false;
        photonView.RPC("MasterisNotReady", PhotonTargets.All);
    }

    [PunRPC]
    public void MasterisReady()
    {
        lobbyMng.masterStart = true;
    }

    [PunRPC]
    public void MasterisNotReady()
    {
        lobbyMng.masterStart = false;
    }
}
