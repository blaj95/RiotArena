using System;
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

    public static GameState instance = null;
    // Use this for initialization
    void Start ()
    {
       
	}

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance!= this)
        {
            Destroy(gameObject);
        }
       
    }

    // Update is called once per frame
    void Update()
    {

        PhotonNetwork.automaticallySyncScene = true;

        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TestGame")
        {
            players = (GameObject.FindGameObjectsWithTag("Head"));
            foreach (GameObject p in players)
            {
                if (p.GetPhotonView().isMine)
                {
                    if (PhotonNetwork.isMasterClient)
                    {
                        master = p;
                        masterCon = p.GetComponent<NetworkedPlayerController>();
                        masterStats = p.GetComponent<PlayerStats>();
                    }
                    else
                    {
                        notMaster = p;
                        notMasterCon = p.GetComponent<NetworkedPlayerController>();
                    }
                }
                else
                {
                    if (PhotonNetwork.isMasterClient)
                    {
                        master = p;
                        masterCon = p.GetComponent<NetworkedPlayerController>();
                    }
                    else
                    {
                        notMaster = p;
                        notMasterCon = p.GetComponent<NetworkedPlayerController>();
                        notMasterStats = p.GetComponent<PlayerStats>();
                    }
                }
            }

            if (masterStats.playerHealth <= 0 || notMasterStats.playerHealth <= 0)
            {
                if (masterStats.playerHealth <= 0)
                    winnerName = "Player 2";
                else if (notMasterStats.playerHealth <= 0)
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
                    GameObject theplayer = PhotonNetwork.Instantiate("LobbyPlayer", MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("ControllerLeft", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("ControllerRight", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("Head", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    Instantiate(player, MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0));

                }
                else
                {
                    GameObject theplayer = PhotonNetwork.Instantiate("LobbyPlayer", Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
                    PhotonNetwork.Instantiate("ControllerLeft", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("ControllerRight", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
                    PhotonNetwork.Instantiate("Head", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
                    Instantiate(player, Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0));
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
        PhotonNetwork.LoadLevel(3);
    }
}
