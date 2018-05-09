using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightHandTracking : Photon.MonoBehaviour {

    public GameObject tip;
    public GameObject start;
  
    public GameObject player;

    public Vector3 OffsetRotation;

    // Use this for initialization
    void Awake ()
    {
        //tip = transform.Find("Rtip").gameObject;
       
        player = gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // BYE BYE STUPID WMR TRACKING
        //---------------------------------------------------------------------------------------------------------------------
        //transform.position = InputTracking.GetLocalPosition(XRNode.RightHand) + player.transform.position;
        //transform.rotation = InputTracking.GetLocalRotation(XRNode.RightHand) * Quaternion.Euler(OffsetRotation);
        //---------------------------------------------------------------------------------------------------------------------

        transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) + player.transform.position;
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Quaternion.Euler(OffsetRotation);

        Vector3 fwd = new Vector3();

        if (tip)
        {
            fwd = tip.transform.forward;
        }
        
       
        RaycastHit hit;

        if (Physics.Raycast(tip.transform.position, fwd, out hit, Mathf.Infinity))
        {
            //Debug.Log(hit.transform.gameObject.name);

            if(hit.transform.gameObject.name == "Start")
            {
                if (PhotonNetwork.connected)
                {
                    Debug.Log("Start!");
                    Renderer rend = start.GetComponent<Renderer>();
                    rend.material.color = Color.green;

                    if (Input.GetButtonDown("RSelectTrigger"))
                    {
                        Debug.Log("Selecting Start!");
                        PhotonNetwork.LoadLevel("PlayerLoadScene");

                    }
                }
                else
                {
                    Renderer rend = start.GetComponent<Renderer>();
                    rend.material.color = Color.red;
                }
                
            }
            else
            {
                Renderer rend = start.GetComponent<Renderer>();
                rend.material.color = Color.white;
            }
        }
	}
}
