﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNeworked : Photon.MonoBehaviour {

    private Vector3 correctBulletPos;
    private Vector3 updatedBulletPos;

    private Vector3 correctRgbVel;
    private Vector3 updatedRbgVel;

    private Vector3 correctRgbAng;
    private Vector3 updatedRbgAng;

    private Vector3 correctRgbPos;
    private Vector3 updatedRgbPos;

    private Quaternion correctBulletRot; 
    private Quaternion updatedBulletRot;

    private Quaternion correctRgbRot;
    private Quaternion updatedRgbRot;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;


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
        if (fired)
        {

            //transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
            gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + vel * bulletSpeed * Time.deltaTime);
        }

        fraction = fraction + Time.deltaTime * 10;
        if (!photonView.isMine)
        {
            syncTime += Time.deltaTime;
            //Update remote player 
            rigidB.position = Vector3.Lerp(syncStartPosition, syncEndPosition, (syncTime/syncDelay));
            rigidB.rotation = Quaternion.Lerp(updatedRgbRot, correctBulletRot, fraction);

            transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, (syncTime/syncDelay));
            transform.rotation = Quaternion.Lerp(updatedBulletRot, correctBulletRot, fraction);
           
            rigidB.velocity = Vector3.Lerp(updatedRbgVel, correctRgbVel, fraction);
           
            rigidB.angularVelocity = Vector3.Lerp(updatedRbgAng, correctRgbAng, fraction);
        }
   
    }

    private void FixedUpdate()
    {
       
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            Vector3 rigPos = rigidB.position;
            Quaternion rigRot = rigidB.rotation;
            Vector3 rigVel = rigidB.velocity;
            Vector3 rigAng = rigidB.angularVelocity;

            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(rigidB.position);
            stream.SendNext(rigidB.rotation);
            stream.SendNext(rigidB.velocity);
            stream.SendNext(rigidB.angularVelocity);

            stream.Serialize(ref pos);
            stream.Serialize(ref rot);
            stream.Serialize(ref rigPos);
            stream.Serialize(ref rigRot);
            stream.Serialize(ref rigVel);
            stream.Serialize(ref rigAng);
        }
        else
        {
            correctBulletPos = (Vector3)stream.ReceiveNext();
            correctBulletRot = (Quaternion)stream.ReceiveNext();
            correctRgbPos = (Vector3)stream.ReceiveNext();
            correctRgbRot = (Quaternion)stream.ReceiveNext();
            correctRgbVel = (Vector3)stream.ReceiveNext();
            correctRgbAng = (Vector3)stream.ReceiveNext();

            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            Vector3 rigPos = rigidB.position;
            Quaternion rigRot = rigidB.rotation;
            Vector3 rigVel = rigidB.velocity;
            Vector3 rigAng = rigidB.angularVelocity;


            stream.Serialize(ref pos);
            stream.Serialize(ref rot);
            stream.Serialize(ref rigPos);
            stream.Serialize(ref rigRot);
            stream.Serialize(ref rigVel);
            stream.Serialize(ref rigAng);


            updatedBulletPos = transform.position;
            updatedBulletRot = transform.rotation;
            updatedRbgVel = rigidB.velocity;
            updatedRbgAng = rigidB.angularVelocity;
            updatedRgbPos = rigidB.position;
            updatedRgbRot = rigidB.rotation;

            syncTime = 0f;
            lastSynchronizationTime = Time.time;
            syncDelay = Time.time - lastSynchronizationTime;


            syncEndPosition = correctBulletPos + correctRgbVel * syncDelay;
            syncStartPosition = rigidB.position;

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
