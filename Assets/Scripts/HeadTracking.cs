using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadTracking : MonoBehaviour {

    public GameObject player; 
	// Use this for initialization
	void Start () {
        player = gameObject.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = InputTracking.GetLocalPosition(XRNode.Head) + player.transform.position;
        transform.rotation = InputTracking.GetLocalRotation(XRNode.Head);
	}
}
