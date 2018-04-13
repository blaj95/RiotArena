using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestBullet : Photon.MonoBehaviour {

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    private Vector3 rigVel;

    private float fraction;
    Rigidbody rigidB;
    Vector3 vel = new Vector3();
    bool fired = false;
    int _damage = 1;
    int _ownerID = -1;
    public float bulletSpeed;
    public float bulletSpeedIncrease;

    // Use this for initialization
    void Start () {
        vel = transform.forward;
    }

    void Awake()
    {
        rigidB = GetComponent<Rigidbody>();

    }
    // Update is called once per frame
    void Update ()
    {

        if (fired)
        {
            //transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
            gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + vel * bulletSpeed * Time.deltaTime);
        }

        if (!photonView.isMine)
        {
            syncTime += Time.deltaTime;
            rigidB.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);

        }
    }

  

    private void FixedUpdate()
    {
    

    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(rigidB.position);
            stream.SendNext(rigidB.velocity);
            stream.SendNext(rigidB.angularVelocity);
        }                                 
          		
        else                                                  
        {
            Vector3 syncPosition = (Vector3)stream.ReceiveNext();
            Vector3 syncVelocity = (Vector3)stream.ReceiveNext();
            Vector3 syncAng = (Vector3)stream.ReceiveNext();

           syncTime = 0f;
           lastSynchronizationTime = Time.time;
           syncDelay = Time.time - lastSynchronizationTime;
           

            syncEndPosition = syncPosition + syncVelocity * syncDelay;
            syncStartPosition = rigidB.position;

        }
    }

    [PunRPC]
    public void MoveBullet()
    {

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
        foreach (ContactPoint contact in collision.contacts)
        {
            vel = Vector3.Reflect(vel, contact.normal);
        }

        if (collision.transform.tag == "Wall")
        {
            bulletSpeed += bulletSpeedIncrease;
        }
    }
}
