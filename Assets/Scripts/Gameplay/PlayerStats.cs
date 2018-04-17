﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerStats : Photon.MonoBehaviour {

    public float playerHealth;
    public Text health;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (photonView.isMine)
        {
            health = GameObject.Find("ControllerLeftShieldNew(Clone)/Canvas/Text").gameObject.GetComponent<Text>();
            health.text = playerHealth.ToString();
        }
         
    }

   




}
