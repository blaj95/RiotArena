﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour
{
    public GameObject player;
    public GameObject MasterSpawn;
    public GameObject Spawn;

    // Use this for initialization
    void Start ()
    {
       
       
    }
	
	// Update is called once per frame
	void Awake ()
    {
        
        PhotonNetwork.automaticallySyncScene = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {   
        if (PhotonNetwork.isMasterClient)
        {
            GameObject theplayer = PhotonNetwork.Instantiate("WeaponLobbyPlayer", MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            PhotonNetwork.Instantiate("ControllerLeftShieldNew", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            PhotonNetwork.Instantiate("ControllerRightWeapon", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            PhotonNetwork.Instantiate("GameHead", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            Instantiate(player, MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 0, 0));

        }
        else
        {
            GameObject theplayer = PhotonNetwork.Instantiate("WeaponLobbyPlayer", Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("ControllerLeftShieldNew", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("ControllerRightWeapon", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("GameHead", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            Instantiate(player, Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 180, 0));
        }
    }

    //public void OnLevelWasLoaded(int level)
    //{
    //    if(level == 2)
    //    {
    //        if (PhotonNetwork.isMasterClient)
    //        {
    //            GameObject theplayer = PhotonNetwork.Instantiate("LobbyPlayer", MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
    //            PhotonNetwork.Instantiate("ControllerLeft", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
    //            PhotonNetwork.Instantiate("ControllerRight", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
    //            PhotonNetwork.Instantiate("Head", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
    //            Instantiate(player, MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 0, 0));

    //        }
    //        else
    //        {
    //            GameObject theplayer = PhotonNetwork.Instantiate("LobbyPlayer", Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            PhotonNetwork.Instantiate("ControllerLeft", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            PhotonNetwork.Instantiate("ControllerRight", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            PhotonNetwork.Instantiate("Head", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            Instantiate(player, Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 180, 0));
    //        }
    //    }
    //}
}
