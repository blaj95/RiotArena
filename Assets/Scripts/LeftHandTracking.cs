using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class LeftHandTracking : MonoBehaviour
{
    public GameObject player;


	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = InputTracking.GetLocalPosition(XRNode.LeftHand) + player.transform.position;
        transform.rotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
    }
}
