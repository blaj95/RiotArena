using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Riot
{

    public class ShieldScript : MonoBehaviour
    {
        public PlayerController playerScript;

        bool reflect = true;

        private void OnCollisionEnter(Collision collision)
        {
            if(!reflect)    //If Not in reflect mode
            {
                playerScript.OnChildCollisionEnter(collision);  //Then do the logic to absorb the bullet.
            }
            else
            {           //If if am in reflect mode
                if(collision.transform.tag == "Bullet")     //And I collided with a bullet
                {
                    NormalBullet bullet = collision.gameObject.GetComponent<NormalBullet>();  //Then Tell the bullet to speeed up
                    if(bullet != null)
                    {
                        bullet.LVLUpBullet();
                    }
                }
                
            }
            
        }

        private void Update()
        {
            if(Input.GetButton("LSelectTrigger" ) == true)
            {
                reflect = false;
            }
            
        }
    }
}
