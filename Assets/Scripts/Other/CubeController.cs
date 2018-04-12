using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {
    [SerializeField]
    GameObject RealWalls;
    [SerializeField]
    GameObject PlaneWalls;

    private void Awake()
    {
        RealWalls.SetActive(true);
        PlaneWalls.SetActive(false);
    }
}
