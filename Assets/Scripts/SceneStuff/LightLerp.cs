using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLerp : MonoBehaviour {


    public float duration = 1.0F;
    public Color color0 = Color.red;
    public Color color1 = Color.blue;
    public Light thisLight;
    // Use this for initialization




    void Start ()
    {
        
    
    }

    
	
	// Update is called once per frame
	void Update ()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        thisLight.color = Color.Lerp(color0, color1, t);

    }
}
