using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerStats : Photon.MonoBehaviour {

    public float playerHealth;
    public float maxHealth;
    public Text health;
    public Text liveText;
    public int lives;
    public  bool atZero;
    public bool isDead;
    public GameState state;


    // Use this for initialization
    void Start ()
    {
		
	}

    private void Awake()
    {
        playerHealth = maxHealth;
        state = GameObject.Find("GameState").GetComponent<GameState>();
        photonView.RPC("RPC_IsDeadReset", PhotonTargets.All);
    }

    // Update is called once per frame
    void Update ()
    {
        if (photonView.isMine)
        {
            state = GameObject.Find("GameState").GetComponent<GameState>();
            health = GameObject.Find("ControllerLeftShieldNew(Clone)/Canvas/HealthText").gameObject.GetComponent<Text>();
            liveText = GameObject.Find("ControllerLeftShieldNew(Clone)/Canvas/LivesText").gameObject.GetComponent<Text>();
            health.text = playerHealth.ToString();
            liveText.text = lives.ToString();

            

            if (playerHealth <= 0)
            {
                atZero = true;
                if (atZero == true && lives > 0)
                {
                    playerHealth = maxHealth;
                    lives = lives - 1;
                    atZero = false;
                    //teleport logic
                }

            }
            else if (lives == 0)
            {
                isDead = true;
                
            }

            if (isDead == true)
            {
                photonView.RPC("RPC_IsDead", PhotonTargets.All);
            }
        }  
    }

    public void TakeDamage(float amount)
    {
        print("In take damage function. Damage to be taken: " + amount);
        photonView.RPC("RPC_TakeDamage", PhotonTargets.All, amount);
    }

    [PunRPC]
    private void RPC_TakeDamage(float amount)
    {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot take negative damage.");
                return;
            }
            playerHealth -= amount;

            if (playerHealth <= 0)
            {
                Debug.Log(gameObject.name + " hp <= 0");
                //Die();
            }
            Debug.Log("Player hp: " + playerHealth);   
    }

    [PunRPC]
    private void RPC_IsDead()
    {
        if (PhotonNetwork.isMasterClient)
        {
            state.masterDead = true;
        }
        else
        {
            state.nonMasterDead = true;
        }
    }

    [PunRPC]
    private void RPC_IsDeadReset()
    {
        if (PhotonNetwork.isMasterClient)
        {
            state.masterDead = false;
        }
        else
        {
            state.nonMasterDead = false;
        }
    }
}
