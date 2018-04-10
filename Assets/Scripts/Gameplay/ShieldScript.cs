using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Riot
{

    public class ShieldScript : MonoBehaviour
    {
        public PlayerController playerScript;

        private void OnCollisionEnter(Collision collision)
        {
            playerScript.OnChildCollisionEnter(collision);
        }
    }
}
