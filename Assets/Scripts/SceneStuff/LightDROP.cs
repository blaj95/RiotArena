using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDROP : MonoBehaviour {

    public Light thisLight;
    public float Speed = 1;
    public Material rend;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rend.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * Speed, 1), 1, 1)));
        thisLight.color = rend.color;
    }
}
