using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.PunBehaviour {

    #region Variables

    public string connectionString = "0.0";
    public Text connectingText;
    #endregion

    #region Unity Callbacks
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(connectionString);
        PhotonNetwork.autoJoinLobby = false;

    }

    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Mastah!");
        connectingText.text = "Connected!";
        
    }
    #endregion
}
