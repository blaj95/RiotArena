﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReady : MonoBehaviour {

    public bool masterReady = false;
    

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
        masterReady = true;
    }

    public void OnTriggerExit(Collider other)
    {
        masterReady = false;
    }
}
