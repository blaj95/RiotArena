using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMasterReady : Photon.MonoBehaviour {

    public bool masterReady = false;
    public GameObject buttonModel;
    LobbyManager lobbyMng;

    

    // Use this for initialization
    void Start()
    {
        lobbyMng = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        buttonModel = GetComponentInChildren<MeshRenderer>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightNet")
        {
            masterReady = true;
            photonView.RPC("MasterisReady", PhotonTargets.All);
            buttonModel.transform.Translate(Vector3.back * .07f, Space.World);
        }
            
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "RightNet")
        {
          
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "RightNet")
        {
            masterReady = false;
            photonView.RPC("MasterisNotReady", PhotonTargets.All);
            buttonModel.transform.Translate(Vector3.forward * .07f, Space.World);
        }
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
