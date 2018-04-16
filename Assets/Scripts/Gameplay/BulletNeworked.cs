using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNeworked : Photon.MonoBehaviour
{
    public PlayerStats pstats;
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
            pstats = player.GetComponent<PlayerStats>();
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
            Debug.Log("GOTCHA");
            if (photonView.isMine)
            {
                photonView.RPC("DoDamage", PhotonTargets.All, _damage);
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

    [PunRPC]
    public void DoDamage(int dmg)
    {
        pstats.playerHealth = pstats.playerHealth - dmg;
    }
}
