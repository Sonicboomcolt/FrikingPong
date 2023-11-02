using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(GameplayScoreHandler))]
public class UIScores : MonoBehaviour
{
    private GameplayScoreHandler scoreHandler;

    [SerializeField] private TextMeshProUGUI Player01ScoreText;
    [SerializeField] private TextMeshProUGUI Player02ScoreText;

    private void OnEnable()
    {
        GameplayMaster.OnUIScoreUpdate += UpdateUIScore;
        scoreHandler = GetComponent<GameplayScoreHandler>();
    }

    private void OnDisable()
    {
        GameplayMaster.OnUIScoreUpdate -= UpdateUIScore;
    }

    private void UpdateUIScore()
    {
        Player01ScoreText.text = scoreHandler.ReturnScore(1).ToString();
        Player02ScoreText.text = scoreHandler.ReturnScore(2).ToString();
    }
}
