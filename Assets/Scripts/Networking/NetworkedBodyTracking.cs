using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedBodyTracking : Photon.MonoBehaviour {

    public GameObject player;
    public GameObject netPlayer;
    public GameObject rightHand;
    private Vector3 correctRightPos;//We lerp towards this
    private Vector3 onUpdateRightPos;
    private Quaternion correctRightRot = Quaternion.identity; //We lerp towards this
    private Quaternion onUpdateRightRot;
    private float fraction;

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
        else
        {
            transform.localPosition = Vector3.Lerp(onUpdateRightPos, correctRightPos, fraction);
            transform.localRotation = Quaternion.Lerp(onUpdateRightRot, correctRightRot, fraction);

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = transform.localPosition;
            // Quaternion rot = transform.localRotation;
            stream.SendNext(transform.position);
            // stream.SendNext(transform.rotation);
            stream.Serialize(ref pos);
            // stream.Serialize(ref rot);

        }
        else
        {
            Vector3 pos = Vector3.zero;
            //Quaternion rot = Quaternion.identity;

            stream.Serialize(ref pos);
            // stream.Serialize(ref rot);

            correctRightPos = pos;
            // correctRightRot = rot;
            onUpdateRightPos = transform.localPosition;
            // onUpdateRightRot = transform.localRotation;
            fraction = 0;

        }

    }
}
