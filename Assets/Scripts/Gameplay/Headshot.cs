using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : Photon.MonoBehaviour {

    public PlayerStats myStats;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (photonView.isMine)
        {
            myStats = GameObject.Find("NewLobbyPlayer(Clone)").GetComponent<PlayerStats>();
        }
        else
        {
            myStats = GameObject.Find("NewLobbyPlayer(Clone)").GetComponent<PlayerStats>();
        }
	}

    public void OnHeadshot(float amount)
    {
        myStats.TakeDamage(amount);
        Debug.Log("Calling on headshot");
    }
}
