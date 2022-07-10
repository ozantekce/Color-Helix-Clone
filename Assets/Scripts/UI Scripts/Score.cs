using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private Text scoreText;
    private Text bestScoreText;


    void Awake()
    {

        bestScoreText = transform.GetChild(0).GetComponent<Text>();
        scoreText = transform.GetChild(1).GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Ball.Z == 0)
        {
            bestScoreText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            bestScoreText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }


        scoreText.text = GameController.Instance.Score.ToString();

        if (GameController.Instance.Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore",GameController.Instance.Score);
        }
        bestScoreText.text = PlayerPrefs.GetInt("HighScore",0).ToString();

    }



}
