using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class NetworkedRightTrackingOffset : Photon.MonoBehaviour, IPunObservable
{


    public GameObject player;
    public GameObject netPlayer;
    public GameObject rightHand;
    private Vector3 correctRightPos;//We lerp towards this
    private Vector3 onUpdateRightPos;
    private Quaternion correctRightRot = Quaternion.identity; //We lerp towards this
    private Quaternion onUpdateRightRot;
    private float fraction;

    public Vector3 OffsetRotation;
    public GameObject weaponTip;


    // Use this for initialization
    void Start()
    {

        correctRightPos = transform.position;
        onUpdateRightPos = transform.position;

        correctRightRot = transform.rotation;
        onUpdateRightRot = transform.rotation;
        rightHand = GameObject.FindWithTag("RightLocal");
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

            correctRightPos = pos;
            correctRightRot = rot;
            onUpdateRightPos = transform.localPosition;
            onUpdateRightRot = transform.localRotation;
            fraction = 0;

        }

    }

    void Update()
    {
        if (photonView.isMine)
        {

            netPlayer = GameObject.Find("WeaponLobbyPlayer(Clone)");
            weaponTip = GameObject.Find("ControllerRightWeapon(Clone)/Rtip");
            transform.localPosition = rightHand.transform.localPosition + netPlayer.transform.localPosition;
            transform.localRotation = rightHand.transform.localRotation * Quaternion.Euler(OffsetRotation);
            Vector3 curPos = rightHand.transform.position;
            Quaternion curRot = rightHand.transform.rotation;
            //transform.position = Vector3.Lerp(curPos, correctRightPos, Time.deltaTime * 5);
            //transform.rotation = Quaternion.Lerp(curRot, correctRightRot, Time.deltaTime * 5);

        }

        fraction = fraction + Time.deltaTime * 10;


        if (!photonView.isMine)
        {
            //Update remote player 
            transform.localPosition = Vector3.Lerp(onUpdateRightPos, correctRightPos, fraction);
            transform.localRotation = Quaternion.Lerp(onUpdateRightRot, correctRightRot, fraction);
        }

        RaycastHit hit;
        Debug.DrawRay(weaponTip.transform.position, weaponTip.transform.forward,  Color.green);
        if (Physics.Raycast(weaponTip.transform.position, weaponTip.transform.forward, out hit, Mathf.Infinity))
        {
            
            //Debug.Log(hit.transform.gameObject.name);
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                if (Input.GetKeyUp("joystick button 17"))
                {
                    Debug.Log("Teleport!");
                    netPlayer.transform.position = hit.point;
                }
              
            }
        }

    }
}

