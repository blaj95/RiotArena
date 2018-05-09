using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class NetworkShieldTracking : Photon.MonoBehaviour, IPunObservable
{


    public GameObject player;
    public GameObject netPlayer;
    public GameObject leftHand;
    private Vector3 correctLeftPos;//We lerp towards this
    private Vector3 onUpdateLeftPos;
    private Quaternion correctLeftRot = Quaternion.identity; //We lerp towards this
    private Quaternion onUpdateLeftRot;
    private float fraction;
    public Vector3 OffsetRotation;

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

            netPlayer = GameObject.Find("NewLobbyPlayer(Clone)");
            transform.localPosition = leftHand.transform.localPosition + netPlayer.transform.localPosition;
            transform.localRotation = leftHand.transform.localRotation * Quaternion.Euler(OffsetRotation);
            transform.Rotate(0, 180, 0);
            //transform.position = Vector3.Lerp(curPos, correctRightPos, Time.deltaTime * 5);
            //transform.rotation = Quaternion.Lerp(curRot, correctRightRot, Time.deltaTime * 5);

        }

        fraction = fraction + Time.deltaTime * 10;
        //transform.localPosition = Vector3.Lerp(onUpdateRightPos, correctRightPos, fraction);
        //transform.localRotation = Quaternion.Lerp(onUpdateRightRot, correctRightRot, fraction);

        if (!photonView.isMine)
        {
            //Update remote player 
            transform.localPosition = Vector3.Lerp(onUpdateLeftPos, correctLeftPos, fraction);
            transform.localRotation = Quaternion.Lerp(onUpdateLeftRot, correctLeftRot, fraction);
        }
    }
}