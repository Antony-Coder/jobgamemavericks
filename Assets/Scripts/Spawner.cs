using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject Meteorite;
    [SerializeField] Camera cam;
    [SerializeField] EndGame endgame;
    [SerializeField] Data data;
    float  delay, MeteoriteVelocity;
    int count = 0;
    float Xmin, Xmax;

    void Start()
    {

        Xmin = cam.ScreenToWorldPoint(new Vector3(0, 0, 120)).x;
        Xmax = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0,120)).x;

        MeteoriteVelocity = -30;
        delay = 0.5f;

        Invoke("DelayLoad", 2);
    }



    IEnumerator Spawn()
    {
        while (true)
        {


            GameObject s = Instantiate(Meteorite);
            //  s.transform.position = new Vector3(Random.Range(data.Xmin, data.Xmax), 0, 200);
            s.transform.position = new Vector3(Random.Range(Xmin,Xmax), 0, 200);

            Rigidbody sr = s.transform.GetComponent<Rigidbody>();
            sr.velocity = new Vector3(0, 0, MeteoriteVelocity);
            sr.AddTorque(new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100)));

            yield return new WaitForSeconds(delay);
        }

    }


    void ResetDelay()
    {


        if (delay < 0.17f) count++;
        else
        {
            delay -= 0.02f;
            MeteoriteVelocity -= 5;
        }

        if(count > 5) 
        {
            StopCoroutine("Spaw");
            CancelInvoke("ResetDelay");
            endgame.Win();
        }


    }

    void DelayLoad()
    {
        InvokeRepeating("ResetDelay", 0, 2);
        StartCoroutine("Spawn");
    }
}
