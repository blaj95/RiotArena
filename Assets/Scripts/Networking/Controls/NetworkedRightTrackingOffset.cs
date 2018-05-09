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
    public Vector3 offsetPos;

    public LineRenderer laser;

    public GameObject laserBase;
    public GameObject gslidePointA;
    public GameObject gslidePointB;

    public float laserMoveSpeed;

    // Use this for initialization
    void Start()
    {
        correctRightPos = transform.position;
        onUpdateRightPos = transform.position;

        correctRightRot = transform.rotation;
        onUpdateRightRot = transform.rotation;
        if (photonView.isMine)
        {
            rightHand = GameObject.FindWithTag("RightLocal");
            laserBase = GameObject.FindWithTag("LaserPoint");
            gslidePointA = GameObject.FindWithTag("SlideA");
            gslidePointB = GameObject.FindWithTag("SlideB");
        }



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
            netPlayer = GameObject.Find("NewLobbyPlayer(Clone)");
            weaponTip = GameObject.Find("RightController(Clone)/Rtip");
            transform.localPosition = rightHand.transform.localPosition + netPlayer.transform.localPosition + offsetPos;
            transform.localRotation = rightHand.transform.localRotation * Quaternion.Euler(OffsetRotation);

            laser = GetComponentInChildren<LineRenderer>();

            //if (Input.GetKeyDown("joystick button 17"))
            if(OVRInput.GetDown(OVRInput.Button.One))
            {
                laser.enabled = true;
                float step = laserMoveSpeed * Time.deltaTime;
                laserBase.transform.position = Vector3.MoveTowards(laserBase.transform.position, gslidePointA.transform.position, step);
            }
            // else if (Input.GetKeyUp("joystick button 17"))
            if(OVRInput.GetUp(OVRInput.Button.One))
            {
                laser.enabled = false;
                float step = laserMoveSpeed * Time.deltaTime;
                laserBase.transform.position = Vector3.MoveTowards(laserBase.transform.position, gslidePointB.transform.position, step);
            }
        }

        fraction = fraction + Time.deltaTime * 10;


        if (!photonView.isMine)
        {
            //Update remote player 
            transform.localPosition = Vector3.Lerp(onUpdateRightPos, correctRightPos, fraction);
            transform.localRotation = Quaternion.Lerp(onUpdateRightRot, correctRightRot, fraction);
        }

        RaycastHit hit;
        //Debug.DrawRay(weaponTip.transform.position, weaponTip.transform.forward,  Color.green);
        if (Physics.Raycast(weaponTip.transform.position, weaponTip.transform.forward, out hit, Mathf.Infinity))
        {
            
            //Debug.Log(hit.transform.gameObject.name);
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                
                if (OVRInput.GetDown(OVRInput.Button.One))
                { 

                    Debug.Log("Teleport!");
                    netPlayer.transform.position = hit.point;
                }
              
            }
        }

    }
}

