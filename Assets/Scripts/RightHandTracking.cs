using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightHandTracking : MonoBehaviour {

    public GameObject tip;
    public GameObject start;
    public NetworkManager networkManager;

	// Use this for initialization
	void Start ()
    {
        tip = transform.Find("Rtip").gameObject;
        start = GameObject.Find("Start");
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = InputTracking.GetLocalPosition(XRNode.RightHand);
        transform.rotation = InputTracking.GetLocalRotation(XRNode.RightHand);

        Vector3 fwd = tip.transform.forward;
       
        RaycastHit hit;

        if (Physics.Raycast(tip.transform.position, fwd, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.gameObject.name);

            if(hit.transform.gameObject.name == "Start")
            {
                Debug.Log("Start!");
                Renderer rend = start.GetComponent<Renderer>();
                rend.material.color = Color.red;

                if (Input.GetButtonDown("RSelectTrigger"))
                {
                    Debug.Log("Selecting Start!");

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
