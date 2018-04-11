﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerCountUI : Photon.PunBehaviour {

    public Text countText;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(PhotonNetwork.room.PlayerCount == 1)
        {
            countText.text = "1/2" + Environment.NewLine + "Players";
        }
        else if(PhotonNetwork.countOfPlayers == 2)
        {
            countText.text = "2/2" + Environment.NewLine + "Players";
        }
	}
}
