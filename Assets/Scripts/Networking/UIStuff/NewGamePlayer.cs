using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGamePlayer : Photon.MonoBehaviour
{
    public bool notmasterReady = false;

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
            notmasterReady = true;
        photonView.RPC("notMasterisReady", PhotonTargets.All);
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "RightNet")
            notmasterReady = false;
        photonView.RPC("notMasterisNotReady", PhotonTargets.All);

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


