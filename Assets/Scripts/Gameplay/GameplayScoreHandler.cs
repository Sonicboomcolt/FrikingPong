using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScoreHandler : MonoBehaviour
{
    public static GameplayScoreHandler instance;

    [SerializeField] private int[] scores = new int[] { 0, 0 };

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        GameplayMaster.OnScore += UpdateScore;
    }

    private void OnDisable()
    {
        GameplayMaster.OnScore -= UpdateScore;
    }

    private void UpdateScore(int player, int amount)
    {
        scores[player - 1] += amount;
    }

    public int ReturnScore(int player)
    {
        return scores[player - 1];
    }
}
