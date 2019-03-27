using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text scoreText;



    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }


}
