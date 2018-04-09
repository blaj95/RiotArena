using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    GameObject weapon = null;
    GameObject shield = null;

    int maxBullets = -1;

    List<GameObject> blist = new List<GameObject>(); //Holds a list of all bullets for that player
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
