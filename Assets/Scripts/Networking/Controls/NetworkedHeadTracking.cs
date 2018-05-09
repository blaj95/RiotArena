using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NetworkedHeadTracking : Photon.MonoBehaviour
{


    public GameObject player;
    public GameObject netPlayer;
    public GameObject head;
    private Vector3 correctHeadPos;//We lerp towards this
    private Vector3 onUpdateHeadPos;
    private Quaternion correctHeadRot; //We lerp towards this
    private Quaternion onUpdateHeadRot;
    private float fraction;

    // Use this for initialization
    void Start()
    {

        correctHeadPos = transform.position;
        onUpdateHeadPos = transform.position;

        correctHeadRot = transform.rotation;
        onUpdateHeadRot = transform.rotation;
        head = GameObject.FindWithTag("HeadLocal");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = transform.localPosition;
            Quaternion rot = transform.localRotation;
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

            correctHeadPos = pos;
            correctHeadRot = rot;
            onUpdateHeadPos = transform.localPosition;
            onUpdateHeadRot = transform.localRotation;
            fraction = 0;

        }

    }

    void Update()
    {
        if (photonView.isMine)
        {

            netPlayer = GameObject.Find("LobbyPlayer(Clone)");

            transform.localPosition = head.transform.localPosition + netPlayer.transform.localPosition;
            transform.localRotation = head.transform.localRotation;
            Vector3 curPos = head.transform.position;
            Quaternion curRot = head.transform.rotation;
            //transform.position = Vector3.Lerp(curPos, correctRightPos, Time.deltaTime * 5);
            //transform.rotation = Quaternion.Lerp(curRot, correctRightRot, Time.deltaTime * 5);

        }

        fraction = fraction + Time.deltaTime * 10;


        if (!photonView.isMine)
        {
            //Update remote player 
            transform.localPosition = Vector3.Lerp(onUpdateHeadPos, correctHeadPos, fraction);
            transform.localRotation = Quaternion.Lerp(onUpdateHeadRot, correctHeadRot, fraction);
        }
    }
}