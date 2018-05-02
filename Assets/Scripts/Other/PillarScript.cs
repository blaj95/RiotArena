using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour {

    public float myRand;
	// Use this for initialization
	void Start ()
    {
        myRand = Random.Range(1, 10);
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.localScale = new Vector3(transform.localScale.x, Mathf.PingPong(Time.time, myRand), transform.localScale.z);
	}
}
