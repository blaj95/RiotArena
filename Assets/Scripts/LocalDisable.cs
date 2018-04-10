﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDisable : Photon.MonoBehaviour {

    public GameObject[] disableGO;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine)
        {
            foreach(GameObject go in disableGO)
            {
                go.SetActive(false);
            }
        }
	}
}
