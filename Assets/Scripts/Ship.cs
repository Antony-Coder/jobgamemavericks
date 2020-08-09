using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{


    [SerializeField] EndGame endgame;


    private void OnCollisionEnter(Collision other)
    {
        endgame.Damage();
        other.gameObject.GetComponent<Meteorite>().DestroyMeteorite();
    }


}
