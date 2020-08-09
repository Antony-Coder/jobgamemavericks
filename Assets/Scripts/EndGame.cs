using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    [SerializeField] GameObject[] hearts = new GameObject[3];
    [SerializeField] GameObject WinSprite;
    [SerializeField] GameObject LoseSprite;
    [SerializeField] Data data;
    bool IsEnd = false;


    private void Start()
    {
        data.health = 3;
    }

    public void Win()
    {
        if (!IsEnd)
        {
            IsEnd = true;

            int MaxLvl = PlayerPrefs.GetInt("lvl");
            if(data.selected ==  MaxLvl) PlayerPrefs.SetInt("lvl", MaxLvl + 1);

            WinSprite.SetActive(true);
            Invoke("End", 0.6f);
            Time.timeScale = 0.3f;
        }

    }

    void Lose()
    {
        LoseSprite.SetActive(true);
        Invoke("End",0.6f);
        Time.timeScale = 0.3f;
    }

    void End()
    {
        LoseSprite.SetActive(false);
        WinSprite.SetActive(false);
        SceneManager.LoadScene("Menu");

    }


    public void Damage()
    {
        if (!IsEnd)
        {
            data.health--;
            hearts[data.health].SetActive(false);
            if (data.health <= 0) { IsEnd = true; Lose(); }
        }


    }
}
