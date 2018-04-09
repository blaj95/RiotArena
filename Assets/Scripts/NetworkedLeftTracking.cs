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
            leftController = GameObject.Find("ControllerLeft");

        }
    }

    private Vector3 correctLeftPos = Vector3.zero; //We lerp towards this
    private Quaternion correctLeftRot = Quaternion.identity; //We lerp towards this


    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            Vector3 curPos = InputTracking.GetLocalPosition(XRNode.LeftHand);
            Quaternion curRot = InputTracking.GetLocalRotation(XRNode.LeftHand);
            leftController.transform.position = Vector3.Lerp(curPos, correctLeftPos, Time.deltaTime * 5);
            leftController.transform.rotation = Quaternion.Lerp(curRot, correctLeftRot, Time.deltaTime * 5);
            leftController.transform.localPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            leftController.transform.localRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
        }
        if (!photonView.isMine)
        {
            //Update remote player 
            leftController.transform.position = Vector3.Lerp(transform.position, correctLeftPos, Time.deltaTime * 5);
            leftController.transform.rotation = Quaternion.Lerp(transform.rotation, correctLeftRot, Time.deltaTime * 5);
        }
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
