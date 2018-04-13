using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNeworked : Photon.MonoBehaviour {

    private Vector3 correctBulletPos;
    private Vector3 updatedBulletPos;
    private Quaternion correctBulletRot; 
    private Quaternion updatedBulletRot;

    bool m_SynchronizeVelocity = true;
    bool m_SynchronizeAngularVelocity = true;

    Rigidbody m_Body;

    private float fraction;

    public float bulletSpeed = 1000f;
    private Vector3 vel = new Vector3();

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
        this.m_Body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (fired)
        {
            //transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
            gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + vel * bulletSpeed * Time.deltaTime);
        }
       

        fraction = fraction + Time.deltaTime * 10;
        if (!photonView.isMine)
        {
            //Update remote player 
            transform.localPosition = Vector3.Lerp(updatedBulletPos, correctBulletPos, fraction);
            transform.localRotation = Quaternion.Lerp(updatedBulletRot, correctBulletRot, fraction);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            if (this.m_SynchronizeVelocity == true)
            {
                stream.SendNext(this.m_Body.velocity);
            }

            if (this.m_SynchronizeAngularVelocity == true)
            {
                stream.SendNext(this.m_Body.angularVelocity);
            }

            Vector3 pos = transform.localPosition;
            Quaternion rot = transform.localRotation;
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.Serialize(ref pos);
            stream.Serialize(ref rot);
        }
        else
        {
            if (this.m_SynchronizeVelocity == true)
            {
                this.m_Body.velocity = (Vector3)stream.ReceiveNext();
            }

            if (this.m_SynchronizeAngularVelocity == true)
            {
                this.m_Body.angularVelocity = (Vector3)stream.ReceiveNext();
            }

            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;

            stream.Serialize(ref pos);
            stream.Serialize(ref rot);

            correctBulletPos = pos;
            correctBulletRot = rot;
            updatedBulletPos = transform.localPosition;
            updatedBulletRot = transform.localRotation;
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
            bulletSpeed += 1;
        }
    }
}
