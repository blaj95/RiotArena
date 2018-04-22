using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNeworked : Photon.MonoBehaviour
{
    [SerializeField]
    protected PlayerStats myStats;
    // public PlayerStats otherStats;
 
   
    [SerializeField]
    protected NetworkedPlayerController myPlayer;
    //public NetworkedPlayerController otherPlayer;

    [SerializeField]
    protected NetworkedShield shieldScript;

    Rigidbody rigidB;

    
   // protected GameObject player;

    [SerializeField]
    protected GameObject _shooter;

    [SerializeField]
    protected GameObject _Shield;


    private float fraction;

    public float bulletSpeedIncrease;
    public float bulletSpeed = 10f;
    public float speedCap;
    public Vector3 vel = new Vector3();

    [SerializeField]
    int damageIncrementor = 1;
    [SerializeField]
    float speedMultiplyer = 1;



    bool fired = false;
    public int _damage = 2;
    int _ownerID;

    #region Unity CallBacks
    // Use this for initialization
    void Start()
    {
        vel = transform.forward;
    }

    void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
        if (bulletSpeed > speedCap)
        {
            bulletSpeed = speedCap;
        }
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        if (fired)
        {
            rigidB.MovePosition(transform.localPosition + vel * bulletSpeed * Time.deltaTime);
            rigidB.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Head")   // Checking for damaging from head collision where the players hit box is
        {

            //if (collision.gameObject.GetPhotonView())
            //{
            //    photonView.RPC("DamageMe", PhotonTargets.All, _damage);
            //}
           
        }
        else if(collision.transform.tag == "Shield")
        {
           shieldScript = collision.gameObject.GetComponent<NetworkedShield>();
           if(shieldScript.reflect == true)
           {
                foreach (ContactPoint contact in collision.contacts)
                {
                    vel = Vector3.Reflect(vel, contact.normal);
                }
            }
           else if(shieldScript.reflect == false)
           {
                shieldScript.AddBullet();
                Destroy(gameObject);
           }
           else
           {
                Destroy(gameObject);
           }
                   
        }
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
    
    public virtual void GetShooter(GameObject shooter, GameObject shooterShield)
    {
        _shooter = shooter;
        _Shield = shooterShield;
        myStats = shooter.GetComponent<PlayerStats>();
        myPlayer = _shooter.GetComponent<NetworkedPlayerController>();
      
    }
    #endregion

    #region PUN RPCs
    [PunRPC]
    public void DamageMe(int dmg)
    {
        myStats.playerHealth = myStats.playerHealth - dmg;
    }

  

    [PunRPC]
    private void collectBulletMe() //function to change playerscript bullet stats
    {
        myPlayer.bulletsLeft++;
    }

    //[PunRPC]
    //private void collectBulletYou() //function to change playerscript bullet stats
    //{
    //    otherPlayer.bulletsLeft++;
    //}

    [PunRPC]
    private void maxBulletsPlusMe()
    {
        myPlayer.maxBullets++;
    }

    //[PunRPC]
    //private void maxBulletsPlusYou()
    //{
    //    otherPlayer.maxBullets++;
    //}

    [PunRPC]
    private void myBulletCaught()
    {
        myPlayer.currentBulletsOut--;
    }

    //private void yourBulletCaught()
    //{
    //    otherPlayer.currentBulletsOut--;
    //}
    #endregion
}