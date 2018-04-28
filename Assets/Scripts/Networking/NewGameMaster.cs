using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameMaster : Photon.MonoBehaviour
{

    public bool masterReady = false;

    GameState lobbyMng;



    // Use this for initialization
    void Start()
    {
        lobbyMng = GameObject.Find("GameHandler").GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightNet")
            masterReady = true;
        photonView.RPC("MasterisReady", PhotonTargets.All);
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "RightNet")
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
