using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour
{
    public GameObject player;
    public GameObject MasterSpawn;
    public GameObject Spawn;
    public bool spawnable;
    public Scene currentScene;
    // Use this for initialization
    void Start ()
    {
        if (PhotonNetwork.isMasterClient)
        {
            GameObject theplayer = PhotonNetwork.Instantiate("WeaponLobbyPlayer", MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("ControllerLeftShieldNew", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("RightController", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            PhotonNetwork.Instantiate("GameHead", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
            Instantiate(player, MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0));

        }
        else
        {
            GameObject theplayer = PhotonNetwork.Instantiate("WeaponLobbyPlayer", Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            PhotonNetwork.Instantiate("ControllerLeftShieldNew", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            PhotonNetwork.Instantiate("RightController", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            PhotonNetwork.Instantiate("GameHead", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
            Instantiate(player, Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0));
        }
    }
	
	// Update is called once per frame
	void Awake ()
    {
        PhotonNetwork.automaticallySyncScene = true;
        spawnable = true;
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    //void Update()
    //{
    //    if (spawnable)
    //    {
    //        if (PhotonNetwork.isMasterClient)
    //        {
    //            GameObject theplayer = PhotonNetwork.Instantiate("WeaponLobbyPlayer", MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            PhotonNetwork.Instantiate("ControllerLeftShieldNew", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            PhotonNetwork.Instantiate("RightController", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            PhotonNetwork.Instantiate("GameHead", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            Instantiate(player, MasterSpawn.transform.position, MasterSpawn.transform.rotation * Quaternion.Euler(0, 180, 0));

    //        }
    //        else
    //        {
    //            GameObject theplayer = PhotonNetwork.Instantiate("WeaponLobbyPlayer", Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
    //            PhotonNetwork.Instantiate("ControllerLeftShieldNew", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            PhotonNetwork.Instantiate("RightController", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 180, 0), 0);
    //            PhotonNetwork.Instantiate("GameHead", player.transform.position, player.transform.rotation * Quaternion.Euler(0, 0, 0), 0);
    //            Instantiate(player, Spawn.transform.position, Spawn.transform.rotation * Quaternion.Euler(0, 0, 0));
    //        }
    //        spawnable = false;
    //    }
    //}

    //public void OnLevelWasLoaded(int level)
    //{
    //    if (level == currentScene.buildIndex)
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
