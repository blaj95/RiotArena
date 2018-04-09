using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDisable : Photon.MonoBehaviour {

    public GameObject[] disableGO;
    public Renderer[] disableRend;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!photonView.isMine)
        {
            foreach(GameObject go in disableGO)
            {
                go.SetActive(false);
            }
            
            foreach(Renderer rnd in disableRend)
            {
                rnd.enabled = false;
            }
        }
	}
}
