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

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            rightController.transform.position = InputTracking.GetLocalPosition(XRNode.RightHand);
            rightController.transform.rotation = InputTracking.GetLocalRotation(XRNode.RightHand);
            rightController.transform.localPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
            rightController.transform.localRotation = InputTracking.GetLocalRotation(XRNode.RightHand);
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
