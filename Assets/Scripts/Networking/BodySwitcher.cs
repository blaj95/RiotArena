using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySwitcher : MonoBehaviour {

    public GameObject bBody;
    public GameObject rBody;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            bBody.SetActive(true);
            rBody.SetActive(false);
        }
        else
        {
            bBody.SetActive(false);
            rBody.SetActive(true);
        }
    }
}
