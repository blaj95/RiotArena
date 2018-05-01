using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleColor : MonoBehaviour {

    Renderer myRend;
	// Use this for initialization
	void Start ()
    {
        myRend = gameObject.GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int b = 0; b <= 255; b++)
        {
            for (int g = 0; g <= 255; g++)
            {
                for (int r = 0; r <= 255; r++)
                {
                    myRend.material.color = new Color(r / 255, g / 255, b / 255);
                }
            }
        }
    }
}
