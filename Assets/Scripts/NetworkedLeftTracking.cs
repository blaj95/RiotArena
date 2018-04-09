using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NetworkedLeftTracking : Photon.MonoBehaviour, IPunObservable
{


    public GameObject leftController;
    private Vector3 correctLeftPos;//We lerp towards this
    private Vector3 onUpdateLeftPos;
    private Quaternion correctLeftRot = Quaternion.identity; //We lerp towards this
    private Quaternion onUpdateLeftRot;
    private float fraction;

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            leftController = GameObject.Find("ControllerLeft");
        }

        correctLeftPos = leftController.transform.position;
        onUpdateLeftPos = leftController.transform.position;

        correctLeftRot = leftController.transform.rotation;
        onUpdateLeftRot = leftController.transform.rotation;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = leftController.transform.position;
            Quaternion rot = leftController.transform.rotation;
            //stream.SendNext(rightController.transform.position);
            //stream.SendNext(rightController.transform.rotation);
            stream.Serialize(ref pos);
            stream.Serialize(ref rot);

        }
        else
        {
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;

            stream.Serialize(ref pos);
            stream.Serialize(ref rot);

            correctLeftPos = pos;                // save this to move towards it in FixedUpdate()
            correctLeftRot = rot;
            onUpdateLeftPos = leftController.transform.localPosition;
            onUpdateLeftRot = leftController.transform.localRotation;
            fraction = 0;

        }

    }

    void Update()
    {
        if (photonView.isMine)
        {
            leftController.transform.localPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            leftController.transform.localRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
            Vector3 curPos = InputTracking.GetLocalPosition(XRNode.LeftHand);
            Quaternion curRot = InputTracking.GetLocalRotation(XRNode.LeftHand);
            leftController.transform.position = Vector3.Lerp(curPos, correctLeftPos, Time.deltaTime * 5);
            leftController.transform.rotation = Quaternion.Lerp(curRot, correctLeftRot, Time.deltaTime * 5);

        }

        this.fraction = this.fraction + Time.deltaTime * 9;
        transform.localPosition = Vector3.Lerp(onUpdateLeftPos, correctLeftPos, fraction);
        transform.localRotation = Quaternion.Lerp(onUpdateLeftRot, correctLeftRot, fraction);
        //if (!photonView.isMine)
        //{
        //    //Update remote player 
        //    leftController.transform.position = Vector3.Lerp(transform.position, correctLeftPos, Time.deltaTime * 5);
        //    leftController.transform.rotation = Quaternion.Lerp(transform.rotation, correctLeftRot, Time.deltaTime * 5);
        //}
    }
}