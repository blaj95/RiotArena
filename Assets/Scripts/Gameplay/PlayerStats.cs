using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerStats : Photon.MonoBehaviour {

    public float playerHealth;
    public float maxHealth;
    public Text health;
    public int lives;
    public  bool atZero;
    public bool isDead;


    // Use this for initialization
    void Start ()
    {
		
	}

    private void Awake()
    {
        playerHealth = maxHealth;
    }

    // Update is called once per frame
    void Update ()
    {
        if (photonView.isMine)
        {
            health = GameObject.Find("ControllerLeftShieldNew(Clone)/Canvas/Text").gameObject.GetComponent<Text>();
            health.text = playerHealth.ToString();

            if (playerHealth <= 0)
            {
                atZero = true;
                if (atZero == true && lives > 0)
                {
                    playerHealth = maxHealth;
                    lives = lives - 1;
                    atZero =false;
                    //teleport logic
                }
                
            }
            else if (lives == 0)
            {
                isDead = true;
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
}
