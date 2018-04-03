using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightHandTracking : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = InputTracking.GetLocalPosition(XRNode.RightHand);
        transform.rotation = InputTracking.GetLocalRotation(XRNode.RightHand);
	}
}
