using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNeworked : Photon.MonoBehaviour {

    private Vector3 correctBulletPos;
    private Vector3 updatedBulletPos;
    private Quaternion correctBulletRot; 
    private Quaternion updatedBulletRot;

    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;

    Vector3 realPosition1 = Vector3.zero;
    Vector3 realVelocity1 = Vector3.zero;
    Quaternion realRotation1 = Quaternion.identity;

    bool m_SynchronizeVelocity = true;
    bool m_SynchronizeAngularVelocity = true;

    Rigidbody rigidB;

    private float fraction;

    public float bulletSpeedIncrease;
    public float bulletSpeed = 1000f;
    private Vector3 vel = new Vector3();
    Vector3 realVelocity2 = Vector3.zero;

    bool fired = false;
    int _damage = 1;
    int _ownerID = -1;

    // Use this for initialization
    void Start ()
    {
        correctBulletPos = transform.position;
        updatedBulletPos = transform.position;
        correctBulletRot = transform.rotation;
        updatedBulletRot = transform.rotation;

        vel = transform.forward;
    }

    void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update ()
    {
      
       

        fraction = fraction + Time.deltaTime * 9;

        if (!photonView.isMine)
        {
            //Update remote player 
            transform.position = Vector3.Lerp(updatedBulletPos, realPosition, fraction);
            transform.rotation = Quaternion.Lerp(updatedBulletRot, realRotation, fraction);
        }
    }

    private void FixedUpdate()
    {
        if (fired)
        {
            //transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
            gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + vel * bulletSpeed * Time.deltaTime);
        }
        fraction = fraction + Time.deltaTime * 100;
        if (!photonView.isMine)
        {
            rigidB.position = Vector3.Lerp(rigidB.position, realPosition1, fraction);
            rigidB.velocity = Vector3.Lerp(rigidB.velocity, realVelocity1, fraction);
            rigidB.rotation = Quaternion.Lerp(rigidB.rotation, realRotation1, fraction);
            rigidB.angularVelocity = Vector3.Lerp(rigidB.angularVelocity, realVelocity2, fraction);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            stream.SendNext(rigidB.position);
            stream.SendNext(rigidB.rotation);
            stream.SendNext(rigidB.velocity);
            stream.SendNext(rigidB.angularVelocity);

            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            Vector3 rigPos = rigidB.position;
            Quaternion rigRot = rigidB.rotation;
            Vector3 rigAng = rigidB.angularVelocity;

            stream.Serialize(ref pos);
            stream.Serialize(ref rot);
            stream.Serialize(ref rigPos);
            stream.Serialize(ref rigRot);
            stream.Serialize(ref rigAng);
        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();


            realPosition1 = (Vector3)stream.ReceiveNext();
            realRotation1 = (Quaternion)stream.ReceiveNext();
            realVelocity1 = (Vector3)stream.ReceiveNext();

            realVelocity2 = (Vector3)stream.ReceiveNext();

            stream.Serialize(ref realPosition);
            stream.Serialize(ref realRotation);
            stream.Serialize(ref realPosition1);
            stream.Serialize(ref realRotation1);
            stream.Serialize(ref realVelocity1);
            stream.Serialize(ref realVelocity2);

            updatedBulletPos = realPosition;
            updatedBulletRot = realRotation;
           
            fraction = 0;

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
        foreach (ContactPoint contact in collision.contacts)
        {
            vel = Vector3.Reflect(vel, contact.normal);
        }

        if(collision.transform.tag == "Wall")
        {
            bulletSpeed += bulletSpeedIncrease;
        }
    }
}
