using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRotater : MonoBehaviour {

    private Vector3 newRot = new Vector3();
    IEnumerator timer;
	// Use this for initialization
	void Start ()
    {
        timer = drainHealth();
        StartCoroutine(timer);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(newRot);
    }

    public IEnumerator drainHealth()
    {
        while (true)
        {
            newRot =  new Vector3(Random.value, Random.value, Random.value);
          
            yield return new WaitForSeconds(3.0f);
        }
    }

}
