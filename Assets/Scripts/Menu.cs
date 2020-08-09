using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Data data;
    [SerializeField] Animator LockNote;
    [SerializeField] Animator Load;
    [SerializeField] GameObject[] LockImg = new GameObject[3];
    [SerializeField] GameObject[] CompliteImg = new GameObject[3];
    [SerializeField] GameObject[] SelectImg = new GameObject[3];



    void Start()
    {
        Time.timeScale = 1;

        if (!PlayerPrefs.HasKey("lvl")) PlayerPrefs.SetInt("lvl", 0);
        data.lvl = PlayerPrefs.GetInt("lvl");

        SelectImg[data.selected].SetActive(true);

        for (int i=0; i<3; i++)
        {

            if (i > data.lvl)
            {
                LockImg[i].SetActive(true);
                CompliteImg[i].SetActive(false);
            } 

            if (i < data.lvl)
            {
                CompliteImg[i].SetActive(true);
                LockImg[i].SetActive(false);
            }

            if (i == data.lvl)
            {
                CompliteImg[i].SetActive(false);
                LockImg[i].SetActive(false);
            }
        }

    }


    public void PlayButton()
    {

        if (data.selected <= PlayerPrefs.GetInt("lvl"))
        {           
            Load.Play("load1");
            Invoke("LoadLvl", 1);
            
        } 
        else LockNote.Play("locked");
    }

    void LoadLvl()
    {
        SceneManager.LoadScene(data.selected + 1);
    }


    public void Select(int s)
    {
        data.selected = s;
        SelectImg[data.lastselected].SetActive(false);
        SelectImg[s].SetActive(true);
        data.lastselected = s;
    }
}
