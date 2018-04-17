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

    public bool myShieldReflecting;
    public bool otherShieldReflecting;

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

    #region Unity CallBacks
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
            myShieldReflecting = myShield.reflect;
        }
        else
        {
            player = GameObject.Find("WeaponLobbyPlayer(Clone)");
            otherStats = player.GetComponent<PlayerStats>();
            otherPlayer = player.GetComponent<NetworkedPlayerController>();
            otherShield = GameObject.Find("ControllerLeftShieldNew(Clone)").GetComponent<NetworkedShield>();
            otherShieldReflecting = otherShield.reflect;
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
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Head")   // Checking for damaging from head collision where the players hit box is
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
        else if (photonView.isMine)
        {
            if (collision.gameObject.GetPhotonView().isMine)
            {
                if (myShieldReflecting == true)
                {
                    foreach (ContactPoint contact in collision.contacts)
                    {
                        vel = Vector3.Reflect(collision.transform.forward, contact.normal);
                    }
                }
                else
                {
                    photonView.RPC("collectBulletMe", PhotonTargets.All, null);
                    photonView.RPC("myBulletCaught", PhotonTargets.All, null);
                    if (myPlayer.bulletsLeft >= myPlayer.maxBullets)
                    {
                        photonView.RPC("MaxBulletsPlusMe", PhotonTargets.All, null);
                    }
                }
            }
            else
            {
                if(otherShieldReflecting == true)
                {
                    foreach (ContactPoint contact in collision.contacts)
                    {
                        vel = Vector3.Reflect(collision.transform.forward, contact.normal);
                    }
                }
                else
                {
                    photonView.RPC("collectBulletYou", PhotonTargets.All, null);
                    photonView.RPC("yourBulletCaught", PhotonTargets.All, null);
                    if (otherPlayer.bulletsLeft >= otherPlayer.maxBullets)
                    {
                        photonView.RPC("MaxBulletsPlusYou", PhotonTargets.All, null);
                    }
                    PhotonNetwork.Destroy(gameObject);
                    Destroy(gameObject);
                }
            }
        }
        else if (!photonView.isMine)
        {
            if (collision.gameObject.GetPhotonView().isMine)
            {
                if (myShieldReflecting == true)
                {
                    foreach (ContactPoint contact in collision.contacts)
                    {
                        vel = Vector3.Reflect(collision.transform.forward, contact.normal);
                    }
                }
                else
                {
                    photonView.RPC("collectBulletMe", PhotonTargets.All, null);
                    photonView.RPC("myBulletCaught", PhotonTargets.All, null);
                    if (myPlayer.bulletsLeft >= myPlayer.maxBullets)
                    {
                        photonView.RPC("MaxBulletsPlusMe", PhotonTargets.All, null);
                    }
                }
            }
            else
            {
                if (otherShieldReflecting == true)
                {
                    foreach (ContactPoint contact in collision.contacts)
                    {
                        vel = Vector3.Reflect(collision.transform.forward, contact.normal);
                    }
                }
                else
                {
                    photonView.RPC("collectBulletYou", PhotonTargets.All, null);
                    photonView.RPC("yourBulletCaught", PhotonTargets.All, null);
                    if (otherPlayer.bulletsLeft >= otherPlayer.maxBullets)
                    {
                        photonView.RPC("MaxBulletsPlusYou", PhotonTargets.All, null);
                    }
                    PhotonNetwork.Destroy(gameObject);
                    Destroy(gameObject);
                }
            }
        }
        //else if(collision.transform.tag == "Shield") //if the bullet hits the  local clients shield
        //{
        //    if (collision.gameObject.GetPhotonView().isMine) //if the bullet was fired from the local client
        //    {
        //        if (myShieldReflecting == true) //if the local clients shield is reflecting 
        //        {
        //            foreach (ContactPoint contact in collision.contacts)
        //            {
        //                vel = Vector3.Reflect(collision.transform.forward, contact.normal);
        //            }
        //        }
        //        else if(myShieldReflecting == false) //if the local clients shield isnt reflecting
        //        {
                    
        //                photonView.RPC("collectBulletMe", PhotonTargets.All, null); 
        //                if (photonView.isMine)// if the bullet was fired from the local client
        //                {
        //                    photonView.RPC("myBulletCaught", PhotonTargets.All, null);
        //                }
        //                else // if the bullet wasnt fired from the local client
        //                {
        //                    photonView.RPC("yourBulletCaught", PhotonTargets.All, null);
        //                }

        //                if(myPlayer.bulletsLeft >= myPlayer.maxBullets)
        //                {
        //                    photonView.RPC("MaxBulletsPlusMe", PhotonTargets.All, null);
        //                }
                       
        //                PhotonNetwork.Destroy(gameObject);
        //                Destroy(gameObject);
                    
        //        }
        //    }
        //    else // if the bullet hits the other clients shield
        //    {
        //        if (otherShieldReflecting == true) // if the other clients shield is reflecting
        //        {
        //            foreach (ContactPoint contact in collision.contacts)
        //            {
        //                vel = Vector3.Reflect(collision.transform.forward, contact.normal);
        //            }
        //        }
        //        else if (otherShieldReflecting == false) //if the other clients shield isnt reflecting
        //        {
        //                photonView.RPC("collectBulletYou", PhotonTargets.All, null); //RPC to send bullet info to player
        //                if (photonView.isMine)// if the bullet was fired from the local client
        //                {
        //                    photonView.RPC("myBulletCaught", PhotonTargets.All, null);
        //                }
        //                else // if the bullet wasnt fired from the local client
        //                {
        //                    photonView.RPC("yourBulletCaught", PhotonTargets.All, null);
        //                }
        //                if (otherPlayer.bulletsLeft >= otherPlayer.maxBullets)
        //                {
        //                  photonView.RPC("MaxBulletsPlusYou", PhotonTargets.All, null);
        //                }
                        
        //                PhotonNetwork.Destroy(gameObject);
        //                Destroy(gameObject);
                    
        //        }
        //    }
           
        //}
        else //if the bullet hits anything else
        {

            foreach (ContactPoint contact in collision.contacts)
            {
                vel = Vector3.Reflect(vel, contact.normal);
            }
            _damage = _damage + damageIncrementor;
            bulletSpeed = bulletSpeed + bulletSpeedIncrease;
        }
       
    }

    #endregion

    #region My Functions
    public void FireBullet(GameObject parent, int damage, int ownerID)
    {
        fired = true;
        transform.rotation = parent.transform.rotation;
        transform.Rotate(Vector3.forward);
        _ownerID = ownerID;
        _damage = damage;
    }

    public void LVLUpBullet()
    {
        _damage = _damage + damageIncrementor;
        bulletSpeed = bulletSpeed * speedMultiplyer;

    }
    #endregion

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
