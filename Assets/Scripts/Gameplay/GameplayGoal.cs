using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayGoal : MonoBehaviour
{
    [SerializeField] private int player, amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the ball touches the goal, add the score then reset the ball.
        if (collision.CompareTag("Ball"))
        {
            GameplayMaster.CallAddScore(player, amount);
            GameplayMaster.CallUpdateUIScore();
            GameplayMaster.CallResetBall();
            GameplayMaster.CallLaunchBall();

            AudioController.instance.PlayBallGoal();
        }
    }
}
