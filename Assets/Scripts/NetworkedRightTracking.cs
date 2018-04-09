using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class NetworkedRightTracking : Photon.MonoBehaviour
{


    public GameObject rightController;
    private Vector3 correctRightPos;//We lerp towards this
    private Vector3 onUpdateRightPos;
    private Quaternion correctRightRot = Quaternion.identity; //We lerp towards this
    private Quaternion onUpdateRightRot;
    private float fraction;

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            rightController = GameObject.Find("ControllerRight");
            
        }
        correctRightPos = rightController.transform.position;
        onUpdateRightPos = rightController.transform.position;
        correctRightRot = rightController.transform.rotation;
        onUpdateRightRot = rightController.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            Vector3 curPos = InputTracking.GetLocalPosition(XRNode.LeftHand);
            Quaternion curRot = InputTracking.GetLocalRotation(XRNode.LeftHand);
            rightController.transform.position = Vector3.Lerp(curPos, correctRightPos, Time.deltaTime * 5);
            rightController.transform.rotation = Quaternion.Lerp(curRot, correctRightRot, Time.deltaTime * 5);
            //rightController.transform.localPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            //rightController.transform.localRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
        }

        this.fraction = this.fraction + Time.deltaTime * 9;
        transform.localPosition = Vector3.Lerp(onUpdateRightPos, correctRightPos, fraction);
        transform.localRotation = Quaternion.Lerp(onUpdateRightRot, correctRightRot, fraction);
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
            Vector3 pos = rightController.transform.position;
            Quaternion rot = rightController.transform.rotation;
            stream.SendNext(rightController.transform.position);
            stream.SendNext(rightController.transform.rotation);
            stream.Serialize(ref pos);
            stream.Serialize(ref rot);

        }
        else
        {
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;

            stream.Serialize(ref pos);
            stream.Serialize(ref rot);

            correctRightPos = pos;                // save this to move towards it in FixedUpdate()
            correctRightRot = rot;
            onUpdateRightPos = rightController.transform.localPosition; // we interpolate from here to latestCorrectPos
            onUpdateRightRot = rightController.transform.localRotation;
            fraction = 0;                          // reset the fraction we alreay moved. see Update()

        }

    }
}
