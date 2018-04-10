using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkDisable : Photon.MonoBehaviour {

    public GameObject[] gos;
    public Renderer[] rnd;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (photonView.isMine)
        {
            foreach(GameObject go in gos)
            {
                go.SetActive(false);
            }
            foreach(Renderer rend in rnd)
            {
                rend.enabled = false;
            }
        }
	}
}
