using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource music;
    public AudioClip song;
    public GameObject lightSource;
    public Light Light;
    LightLerp lightLerp;
    LightDROP lightDROP;
    public bool isCoroutineStarted = false;
    // Use this for initialization
    void Awake ()
    {
        song = music.clip;
        lightLerp = lightSource.GetComponent<LightLerp>();
        lightDROP = lightSource.GetComponent<LightDROP>();
        Light = lightSource.GetComponent<Light>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        //Debug.Log(music.time.ToString());
        if (music.time >= 7.25f && music.time < 14)
        {
            lightLerp.color0 = Color.green;
            lightLerp.color1 = Color.magenta;
        }
        else if (music.time >= 14.35f && music.time < 44.4f)
        {
            lightLerp.color0 = Color.blue;
            lightLerp.color1 = Color.yellow;
        }
        else if (music.time >= 44.4f && music.time < 58.2f)
        {
            lightSource.GetComponent<LightDROP>().enabled = true;
            lightLerp.enabled = false;
        }
        else if (music.time >= 58.2f)
        {
            if (!isCoroutineStarted)
            {
                StartCoroutine("Strobe");
            }
        }
	}

    IEnumerator Strobe()
    {
            isCoroutineStarted = true;
            Light.enabled = false;
            yield return new WaitForSeconds(.85f);
            Light.enabled = true;
            yield return new WaitForSeconds(.85f);

        isCoroutineStarted = false;  
    }
}
