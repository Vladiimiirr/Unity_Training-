using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        if (finalScoreText != null) 
        {
            finalScoreText.text = "Congratulations!\nYou got a score of " +
                                scoreKeeper.CalculateScore() + "%"; 
        }
        
    }


}
