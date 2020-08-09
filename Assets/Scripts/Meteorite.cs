using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] GameObject mesh;
    bool first = false;


    private void OnParticleCollision(GameObject other)
    {
        DestroyMeteorite();
    }

    public void DestroyMeteorite()
    {
        if (!first)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(mesh);
            transform.eulerAngles = Vector3.zero;
            gameObject.GetComponent<ParticleSystem>().Play();
            first = true;
        }
    }

}
