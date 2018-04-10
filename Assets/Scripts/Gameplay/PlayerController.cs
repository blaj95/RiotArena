using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Riot
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField]
        GameObject weapon = null;

        [SerializeField]
        GameObject shield = null;

        [SerializeField]
        GameObject bullet = null;


        int bulletsLeft = -1;
        int bulletsFired = -1;
        public int maxBullets = -1;
        public int damage = -1;


        public int playerID = 1;

        bool isShooting = false;

        public float rateOfFire = -1.0f;
        float nextFire = 1.0f;



        List<GameObject> blist = new List<GameObject>(); //Holds a list of all bullets for that player
                                                         // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            updateControllers();
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);

            if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.GetButtonDown("Fire1")))
            {
                FireWeapon();
            }
        }

        void FireWeapon()
        {
            if (Time.time < nextFire)
            {
                Debug.Log("Whoa there Partner, its not like the bullets are teleporting into the chamber");
                return;
            }
            else if (bulletsLeft == 0)
            {
                Debug.Log("You have already shot all your bullets, Catch some more with your shield");
                return;
            }
            isShooting = true;

            nextFire = Time.time + rateOfFire;
            GameObject newBullet = Instantiate(bullet, weapon.transform.position + weapon.transform.forward, weapon.transform.rotation);
            newBullet.GetComponent<NormalBullet>().FireBullet(weapon, damage, playerID);
            blist.Add(newBullet);
            bulletsFired++;
            bulletsLeft = maxBullets - blist.Count;
        }

        public void OnChildCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Bullet")
            {
                if (blist.Contains(collision.gameObject))
                {
                    blist.Remove(collision.gameObject);
                    bulletsLeft++;
                    Destroy(collision.gameObject);

                }
                else
                {
                    maxBullets++;
                    Destroy(collision.gameObject);
                }
            }
        }

        public void updateControllers()
        {
            weapon.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            weapon.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

            shield.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            shield.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
    }
}
 