using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data", order = 1)]
public class Data : ScriptableObject
{

    public AudioClip shot;
    public AudioClip explosion;

    public int lvl;
    public int selected = 0;
    public int lastselected = 0;
    public int health;
    public float Xpos;

}
