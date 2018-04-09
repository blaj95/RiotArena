using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NetworkedLeftTracking : Photon.MonoBehaviour{

    // Use this for initialization
    
    public GameObject leftController;
    private Vector3 correctLeftPos;//We lerp towards this
    private Vector3 onUpdateLeftPos; 
    private Quaternion correctLeftRot = Quaternion.identity; //We lerp towards this
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
    }

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
        //if (!photonView.isMine)
        //{
        //    //Update remote player 
        //    leftController.transform.position = Vector3.Lerp(transform.position, correctLeftPos, Time.deltaTime * 5);
        //    leftController.transform.rotation = Quaternion.Lerp(transform.rotation, correctLeftRot, Time.deltaTime * 5);
        //}
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = leftController.transform.position;
            Quaternion rot = leftController.transform.rotation;
            stream.SendNext(leftController.transform.position);
            stream.SendNext(leftController.transform.rotation);
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
            onUpdateLeftPos = leftController.transform.localPosition; // we interpolate from here to latestCorrectPos
            fraction = 0;                          // reset the fraction we alreay moved. see Update()

            transform.localRotation = rot;              // this sample doesn't smooth rotation

            //might need local and global positions
        }

    }
}
