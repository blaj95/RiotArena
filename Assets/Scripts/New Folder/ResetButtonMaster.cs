using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonMaster : Photon.MonoBehaviour {

    public bool masterReady = false;
    public GameObject buttonModel;
    GameState state;
    // Use this for initialization
    void Start () {
		state = GameObject.Find("GameState").GetComponent<GameState>();
        buttonModel = GetComponentInChildren<MeshRenderer>().gameObject;
    }

	// Update is called once per frame
	void Update ()
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
        state.masterStart = true;
    }

    [PunRPC]
    public void MasterisNotReady()
    {
        state.masterStart = false;
    }
}
