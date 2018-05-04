using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSwitcher : Photon.MonoBehaviour {

    public GameObject bHead;
    public GameObject rHead;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PhotonNetwork.isMasterClient)
        {
            bHead.SetActive(true);
            rHead.SetActive(false);
        }
        else
        {
            bHead.SetActive(false);
            rHead.SetActive(true);
        }
	}
}
