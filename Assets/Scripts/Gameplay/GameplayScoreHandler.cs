using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScoreHandler : MonoBehaviour
{
    [SerializeField] private int[] scores = new int[] { 0, 0 };

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
