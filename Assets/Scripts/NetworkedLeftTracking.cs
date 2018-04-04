﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NetworkedLeftTracking : Photon.MonoBehaviour{

    // Use this for initialization
    
    public GameObject leftController;

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            leftController = GameObject.Find("ControllerRight");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            player = GameObject.Find("LobbyPlayer(Clone)");
        }
        leftController.transform.position = InputTracking.GetLocalPosition(XRNode.LeftHand) + transform.position;
        leftController.transform.rotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
        leftController.transform.localPosition = InputTracking.GetLocalPosition(XRNode.LeftHand) + transform.position;
        leftController.transform.localRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(leftController.transform.position);
            stream.SendNext(leftController.transform.rotation);
            //might need local and global positions
        }
        else
        {
            leftController.transform.position = (Vector3)stream.ReceiveNext();
            leftController.transform.rotation = (Quaternion)stream.ReceiveNext();

            //might need local and global positions
        }

    }
}
