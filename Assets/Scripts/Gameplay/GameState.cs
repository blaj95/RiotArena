﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    public GameObject[] players;
    public GameObject master;
    public GameObject notMaster;
    public NetworkedPlayerController masterCon;
    public NetworkedPlayerController notMasterCon;
    public PlayerStats masterStats;
    public PlayerStats notMasterStats;
    public Scene currentScene;

    public GameObject MasterSpawn;
    public GameObject Spawn;
    public GameObject player;
    public bool spawnable;

    public bool masterStart = false;
    public bool playerStart = false;

    public string winnerName;

    public Text winText;

    public bool masterDead = false;
    public bool nonMasterDead = false;

    public static GameState instance;
    // Use this for initialization
    void Start ()
    {
       
	}

    private void Awake()
    {
         instance = this;
         DontDestroyOnLoad(gameObject);
        
        if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

        PhotonNetwork.automaticallySyncScene = true;

        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Arena")
        {
            players = (GameObject.FindGameObjectsWithTag("Player"));
            foreach (GameObject p in players)
            {
              if (PhotonNetwork.isMasterClient)
              {
                    master = p;
                    masterCon = p.GetComponent<NetworkedPlayerController>();
                    masterStats = p.GetComponent<PlayerStats>();
                    if(p.GetPhotonView().isMine == false)
                    {
                        notMaster = p;
                        notMasterCon = p.GetComponent<NetworkedPlayerController>();
                        notMasterStats = p.GetComponent<PlayerStats>();
                    }
              }
              
            }

            
            if (masterDead == true || nonMasterDead == true)
            {
                if (masterDead == true)
                    winnerName = "Player 2";
                else if (nonMasterDead == true)
                    winnerName = "Player 1";

                GameOver();
            }
        }
        else if (currentScene.name == "GameOverScene")
        {
            winText = GameObject.Find("EndUI/Text").GetComponent<Text>();
            winText.text = "Winner" + Environment.NewLine + winnerName;
            if (spawnable)
            {

                MasterSpawn = GameObject.Find("SpawnMaster");
                Spawn = GameObject.Find("Spawn");
                if (PhotonNetwork.isMasterClient)
                {
                    Instantiate(player, MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0));
                    GameObject theplayer = PhotonNetwork.Instantiate("NewLobbyPlayer", MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("ControllerLeft", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("ControllerRight", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("GameHead", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    

                }
                else
                {
                    Instantiate(player, Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0));
                    GameObject theplayer = PhotonNetwork.Instantiate("NewLobbyPlayer", Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
                    PhotonNetwork.Instantiate("ControllerLeft", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("ControllerRight", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("GameHead", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
                   // Instantiate(player, Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0));
                }

                spawnable = false;
            }
        }
        if (masterStart == true && playerStart == true)
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel(2);
            }
        }
    }
    
    public void GameOver()
    {

        spawnable = true;
        if(PhotonNetwork.isMasterClient)
        PhotonNetwork.LoadLevel(3);
    }
}
