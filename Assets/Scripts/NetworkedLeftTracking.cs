using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class NetworkedLeftTracking : Photon.MonoBehaviour, IPunObservable
{


    public GameObject player;
    public GameObject netPlayer;
    public GameObject leftHand;
    private Vector3 correctLeftPos;//We lerp towards this
    private Vector3 onUpdateLeftPos;
    private Quaternion correctLeftRot = Quaternion.identity; //We lerp towards this
    private Quaternion onUpdateLeftRot;
    private float fraction;

    // Use this for initialization
    void Start()
    {

        correctLeftPos = transform.position;
        onUpdateLeftPos = transform.position;

        correctLeftRot = transform.rotation;
        onUpdateLeftRot = transform.rotation;
        leftHand = GameObject.FindWithTag("LeftLocal");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
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
            onUpdateLeftPos = transform.localPosition;
            onUpdateLeftRot = transform.localRotation;
            fraction = 0;

        }

    }

    void Update()
    {
        if (photonView.isMine)
        {

            netPlayer = GameObject.Find("LobbyPlayer(Clone)");
            transform.localPosition = leftHand.transform.localPosition + netPlayer.transform.localPosition;
            transform.localRotation = leftHand.transform.localRotation;
            Vector3 curPos = leftHand.transform.position;
            Quaternion curRot = leftHand.transform.rotation;
            //transform.position = Vector3.Lerp(curPos, correctRightPos, Time.deltaTime * 5);
            //transform.rotation = Quaternion.Lerp(curRot, correctRightRot, Time.deltaTime * 5);

        }

        this.fraction = this.fraction + Time.deltaTime * 9;
        //transform.localPosition = Vector3.Lerp(onUpdateRightPos, correctRightPos, fraction);
        //transform.localRotation = Quaternion.Lerp(onUpdateRightRot, correctRightRot, fraction);

        //if (!photonView.isMine)
        //{
        //    //Update remote player 
        //    leftController.transform.position = Vector3.Lerp(transform.position, correctLeftPos, Time.deltaTime * 5);
        //    leftController.transform.rotation = Quaternion.Lerp(transform.rotation, correctLeftRot, Time.deltaTime * 5);
        //}
    }
}
