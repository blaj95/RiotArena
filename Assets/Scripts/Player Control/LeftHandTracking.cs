using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class LeftHandTracking : MonoBehaviour
{
    public GameObject player;

    public Vector3 OffsetRotation;

    // Use this for initialization
    void Start ()
    {
        player = gameObject.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = InputTracking.GetLocalPosition(XRNode.LeftHand) + player.transform.position;
        transform.rotation = InputTracking.GetLocalRotation(XRNode.LeftHand) * Quaternion.Euler(OffsetRotation);
    }
}
