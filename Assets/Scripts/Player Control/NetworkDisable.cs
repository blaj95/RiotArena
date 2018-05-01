using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDisable : Photon.MonoBehaviour {

    public GameObject[] disableGO;
    public Renderer[] disableRend;
    public GameObject[] disableMyGO;
    public MeshRenderer[] myMesh;

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
            foreach (MeshRenderer mr in myMesh)
            {
                mr.enabled = true;
            }
        }
        else
        {
            foreach (GameObject go in disableMyGO)
            {
                go.SetActive(false);
            }

            foreach (MeshRenderer mr in myMesh)
            {
                
            }
        }
	}
}
