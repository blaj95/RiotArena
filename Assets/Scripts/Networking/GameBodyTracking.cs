using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBodyTracking : Photon.MonoBehaviour {

    public GameObject myhead;
    public Vector3 bodyOffset;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            myhead = GameObject.Find("GameHead(Clone)");

            transform.position = myhead.transform.position + bodyOffset;
        }
    }
}
