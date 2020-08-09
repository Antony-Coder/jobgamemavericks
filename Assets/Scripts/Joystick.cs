using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] Data data;
    [SerializeField] GameObject Gun;
    [SerializeField] Transform spaceship;
    [SerializeField] Camera cam;
    [SerializeField] AudioSource audioo;
    [SerializeField] Animator reloading;


    bool tap = false;
    Vector2 posNow;    
    Vector3 ShipPos;
    int reload = 2;
    float Xmin, Xmax;


    void Start()
    {
        posNow = Vector2.zero;
    }


    void Update()
    {
        
        float x = spaceship.position.x;
        float z = spaceship.position.z;

#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    posNow = touch.position;
                    break;

                case TouchPhase.Ended:
                    Swaipe(posNow, touch.position);
                    break;
            }



        }
#endif


#if UNITY_EDITOR

    if (Input.anyKey)
    {
           
            if (Input.GetKey(KeyCode.W)) ShipPos = Vector3.forward;
            if (Input.GetKeyDown(KeyCode.S)) ShipPos = Vector3.back;
            if (Input.GetKeyDown(KeyCode.D)) ShipPos = Vector3.right;
            if (Input.GetKeyDown(KeyCode.A)) ShipPos = Vector3.left;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (reload == 2)
                {
                    audioo.PlayOneShot(data.shot);
                    Gun.transform.position = spaceship.position + new Vector3(0, 0, 6);
                    Gun.GetComponent<ParticleSystem>().Play();
                    reload = 0;
                }
                if (reload == 0)
                {
                    Invoke("ReloadGun", 5f);
                    reloading.Play("reload");
                    reload = 1;
                }


            }
   
    }

#endif


        data.Xpos = spaceship.position.x;
        Xmin = cam.ScreenToWorldPoint(new Vector3(0, 0, z + 50)).x;
        Xmax = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, z + 50)).x;

        spaceship.position = new Vector3(Mathf.Clamp(x, Xmin+4, Xmax-4), 0, Mathf.Clamp(z, 0, 70));
        spaceship.Translate(ShipPos * Time.deltaTime * 10);

    }

    void ReloadGun() { reload = 2; }

    void Swaipe(Vector2 a, Vector2 b)
    {
        Vector2 distXY = b-a;
        Vector2 distXYabs;

        distXYabs = new Vector2(Mathf.Abs(distXY.x), Mathf.Abs(distXY.y));


        if (distXYabs.x > Screen.width / 8)
        {
            if (distXY.x > 0) ShipPos = Vector3.right;
            else ShipPos = Vector3.left;

            tap = false;

        }
        else
        {
            if (distXYabs.y > Screen.width / 8)
            {
                if (distXY.y > 0) ShipPos = Vector3.forward;
                else ShipPos = Vector3.back;


                tap = false;

            }
            else
            {

                if (reload == 2)
                {
                    audioo.PlayOneShot(data.shot);
                    Gun.transform.position = spaceship.position + new Vector3(0, 0, 6);
                    Gun.GetComponent<ParticleSystem>().Play();
                    reload = 0;
                }
                if (reload == 0)
                {
                    Invoke("ReloadGun", 5f);
                    reloading.Play("reload");
                    reload = 1;
                }
                ShipPos = Vector3.zero;

                /*  if (tap)
                  {
                      audioo.PlayOneShot(data.shot);
                      Gun.transform.position = spaceship.position + new Vector3(0, 0, 5);
                      Gun.GetComponent<ParticleSystem>().Play();
                  }
                  ShipPos = Vector3.zero;
                  tap = true;*/

            } 




        }

    }




}