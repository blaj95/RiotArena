using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNeworked : Photon.MonoBehaviour
{
    public PlayerStats myStats;
    public PlayerStats otherStats;
    public NetworkedShield myShield;
    public NetworkedShield otherShield;
    public NetworkedPlayerController myPlayer;
    public NetworkedPlayerController otherPlayer;

    Rigidbody rigidB;

    public GameObject player;

    private float fraction;

    public float bulletSpeedIncrease;
    public float bulletSpeed = 10f;
    public float speedCap;
    private Vector3 vel = new Vector3();

    [SerializeField]
    int damageIncrementor = 1;
    [SerializeField]
    float speedMultiplyer = 1;



    bool fired = false;
    public int _damage = 2;
    int _ownerID = -1;

    // Use this for initialization
    void Start ()
    {
        vel = transform.forward;  
    }

    void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            player = GameObject.Find("WeaponLobbyPlayer(Clone)");
            myStats = player.GetComponent<PlayerStats>();
            myPlayer = player.GetComponent<NetworkedPlayerController>();
            myShield = GameObject.Find("ControllerLeftShieldNew(Clone)").GetComponent<NetworkedShield>();
        }
        else
        {
            otherStats = player.GetComponent<PlayerStats>();
            otherPlayer = player.GetComponent<NetworkedPlayerController>();
            otherShield = GameObject.Find("ControllerLeftShieldNew(Clone)").GetComponent<NetworkedShield>();
        }

        if(bulletSpeed > speedCap)
        {
            bulletSpeed = speedCap;
        }
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        if (fired)
        {
            //transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
           rigidB.MovePosition(transform.localPosition + vel * bulletSpeed * Time.deltaTime);
           rigidB.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }

    public void FireBullet(GameObject parent, int damage, int ownerID)
    {
        fired = true;
        transform.rotation = parent.transform.rotation;
        transform.Rotate(Vector3.forward);
        _ownerID = ownerID;
        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Head")
        {
           
            if (photonView.isMine)
            { 
                photonView.RPC("DamageMe", PhotonTargets.All, _damage);
            }
            else
            {
                photonView.RPC("DamageYou", PhotonTargets.All, _damage);
            }
        }
        else if(collision.transform.tag == "Shield")
        {
            if (collision.gameObject.GetPhotonView().isMine)
            {
                if (myShield.reflect == true)
                {
                    foreach (ContactPoint contact in collision.contacts)
                    {
                        vel = Vector3.Reflect(collision.transform.forward, contact.normal);
                    }
                }
                else if(myShield.reflect == false)
                {
                    if (collision.gameObject.GetPhotonView().isMine)
                    {
                        photonView.RPC("collectBulletMe", PhotonTargets.All, null); //RPC to send bullet info to player
                        if (photonView.isMine)
                        {
                            photonView.RPC("myBulletCaught", PhotonTargets.All, null);
                        }
                        else
                        {
                            photonView.RPC("yourBulletCaught", PhotonTargets.All, null);
                        }

                        if(myPlayer.bulletsLeft >= myPlayer.maxBullets)
                        {
                            photonView.RPC("MaxBulletsPlusMe", PhotonTargets.All, null);
                        }
                       
                        PhotonNetwork.Destroy(gameObject);
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                if (otherShield.reflect == true)
                {
                    foreach (ContactPoint contact in collision.contacts)
                    {
                        vel = Vector3.Reflect(collision.transform.forward, contact.normal);
                    }
                }
                else if (otherShield.reflect == false)
                {
                    if (collision.gameObject.GetPhotonView().isMine)
                    {
                        photonView.RPC("collectBulletYou", PhotonTargets.All, null); //RPC to send bullet info to player
                        if (photonView.isMine)
                        {
                            photonView.RPC("myBulletCaught", PhotonTargets.All, null);
                        }
                        else
                        {
                            photonView.RPC("yourBulletCaught", PhotonTargets.All, null);
                        }
                        if (otherPlayer.bulletsLeft >= otherPlayer.maxBullets)
                        {
                          photonView.RPC("MaxBulletsPlusYou", PhotonTargets.All, null);
                        }
                        
                        PhotonNetwork.Destroy(gameObject);
                        Destroy(gameObject);
                    }
                }
            }
           
        }
        else
        {

            foreach (ContactPoint contact in collision.contacts)
            {
                vel = Vector3.Reflect(vel, contact.normal);
            }
            _damage = _damage + damageIncrementor;
            bulletSpeed = bulletSpeed + bulletSpeedIncrease;
        }
       
    }

    public void LVLUpBullet()
    {
        _damage = _damage + damageIncrementor;
        bulletSpeed = bulletSpeed * speedMultiplyer;

    }

    #region PUN RPCs
    [PunRPC]
    public void DamageMe(int dmg)
    {
        myStats.playerHealth = myStats.playerHealth - dmg;
    }

    [PunRPC]
    public void DamageYou(int dmg)
    {
        otherStats.playerHealth = otherStats.playerHealth - dmg;
    }

    [PunRPC]
    private void collectBulletMe() //function to change playerscript bullet stats
    {
        myPlayer.bulletsLeft++;
    }

    [PunRPC]
    private void collectBulletYou() //function to change playerscript bullet stats
    {
        otherPlayer.bulletsLeft++;
    }

    [PunRPC]
    private void maxBulletsPlusMe()
    {
        myPlayer.maxBullets++;
    }

    [PunRPC]
    private void maxBulletsPlusYou()
    {
        otherPlayer.maxBullets++;
    }

    [PunRPC]
    private void myBulletCaught()
    {
        myPlayer.currentBulletsOut--;
    }

    private void yourBulletCaught()
    {
        otherPlayer.currentBulletsOut--;
    }
    #endregion
}
