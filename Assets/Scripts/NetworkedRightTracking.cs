using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class NetworkedRightTracking : Photon.MonoBehaviour
{

    
    public GameObject rightController;
   

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            rightController = GameObject.Find("ControllerRight");
            
        }
    }

    private Vector3 correctRightPos = Vector3.zero; //We lerp towards this
    private Quaternion correctRightRot = Quaternion.identity; //We lerp towards this

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            Vector3 curPos = InputTracking.GetLocalPosition(XRNode.RightHand);
            Quaternion curRot = InputTracking.GetLocalRotation(XRNode.RightHand);
            rightController.transform.position = Vector3.Lerp(curPos, correctRightPos, Time.deltaTime * 5);
            rightController.transform.rotation = Quaternion.Lerp(curRot, correctRightRot, Time.deltaTime * 5);
            rightController.transform.localPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
            rightController.transform.localRotation = InputTracking.GetLocalRotation(XRNode.RightHand);
        }

        if (!photonView.isMine)
        {
            //Update remote player 
            rightController.transform.position = Vector3.Lerp(transform.position, correctRightPos, Time.deltaTime * 5);
            rightController.transform.rotation = Quaternion.Lerp(transform.rotation, correctRightRot, Time.deltaTime * 5);
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(rightController.transform.position);
            stream.SendNext(rightController.transform.rotation);
            //might need local and global positions
        }
        else
        {
            rightController.transform.position = (Vector3)stream.ReceiveNext();
            rightController.transform.rotation = (Quaternion)stream.ReceiveNext();

            //might need local and global positions
        }
       
    }

   
}
