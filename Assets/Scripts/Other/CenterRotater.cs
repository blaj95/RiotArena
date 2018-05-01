﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRotater : MonoBehaviour
{

    private Vector3 newRot = new Vector3();
    IEnumerator timer;

    public GameObject light;

    public Vector3 rotateVec = Vector3.up;
    public float speed = 45.0f;
    private float x;
    private float y;
    private float z;
    //Use this for initialization

    void Start()
    {
        timer = NewRot();
        StartCoroutine(timer);
    }


    public IEnumerator NewRot()
    {
        while (true)
        {
            rotateVec = new Vector3(Random.value, Random.value, Random.value);

            yield return new WaitForSeconds(3.0f);
        }
    }


    private void Update()
    {
        float rnd = Mathf.PingPong(speed, Time.time);
        transform.Rotate(Vector3.up, speed * Time.deltaTime);//Yaw
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);//Roll
        transform.Rotate(Vector3.left, speed * Time.deltaTime);//Pitch

    }


}