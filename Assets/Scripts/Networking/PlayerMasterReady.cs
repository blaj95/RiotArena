﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMasterReady : MonoBehaviour {

    public bool masterReady = false;

    LobbyManager lobbyMng;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightNet")
            masterReady = true;
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "RightNet")
            masterReady = false;
    }

    [PunRPC]
    public void MasterisReady()
    {

    }
}
