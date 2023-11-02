using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PaddleController
{
    private BallController ball;
    private float verticalInput;

    [Header("AI Data")]
    [SerializeField] private Vector2 falseMovementAmount;
    [SerializeField] private float ballDetectDistance;
    [SerializeField] private float ballDetectMoveOffset;

    private float inaccuateRNG;
    private float inputValueRNG;
    private bool isInaccuate;
    private bool otherPlayerIsWinning;

    private float movementRNG;

    [Header("AI Data")]
    public bool ALWAYS_PREDICT;

    private void Start()
    {
        //FindFirstObjectByType can be slow, but for this instance this is fine due to it calling once.
        ball = FindFirstObjectByType<BallController>();
    }

    private void Update()
    {
        if (!ball) return;
        if (!ALWAYS_PREDICT)
        {
            float distanceFromBall = Vector2.Distance(transform.position, ball.transform.position);

            if (distanceFromBall >= ballDetectDistance)
            {
                //Sometimes predect the movment of the ball.
                if (movementRNG >= 50)
                {
                    AIPredectBallMovement();
                }
                else
                {
                    AITrackBallMovement();
                }
            }
            else
            {
                AITrackBallMovement();
            }
        }
        else
        {
            AIPredectBallMovement();
        }

        VerticalInput(verticalInput);
        MovePaddle();
    }

    private void AIPredectBallMovement()
    {
        var ballPredectedValue = ball.ReturnPredectedData();

        if (ballPredectedValue.y > transform.position.y)
        {
            SetVerticalInputValue(1);
        }
        else if (ballPredectedValue.y < transform.position.y)
        {
            SetVerticalInputValue(-1);
        }
    }

    private void AITrackBallMovement()
    {
        if ((ball.transform.position.y + inputValueRNG) > transform.position.y + ballDetectMoveOffset) { SetVerticalInputValue(1); }    //Move up.
        if ((ball.transform.position.y - inputValueRNG) < transform.position.y - ballDetectMoveOffset) { SetVerticalInputValue(-1); }   //Move Down.

        if (ball.transform.position.y < transform.position.y + ballDetectMoveOffset && ball.transform.position.y > transform.position.y - ballDetectMoveOffset) { SetVerticalInputValue(0); }    //Move None.
    }

    private void SetVerticalInputValue(float newValue)
    {
        verticalInput = Mathf.Clamp(Mathf.Lerp(verticalInput, newValue, Time.deltaTime * paddleSpeed), -1, 1);
        //verticalInput = newValue;
    }

    public override void PaddleHitEvent()
    {
        base.PaddleHitEvent();

        var otherPlayer = 0;

        //This is a very hard coded way of doing it, but it works for now.
        if(paddlePlayer == 1) { otherPlayer = 2; }
        if(paddlePlayer == 2) { otherPlayer = 1; }

        otherPlayerIsWinning = GameplayScoreHandler.instance.ReturnScore(paddlePlayer) > GameplayScoreHandler.instance.ReturnScore(otherPlayer);

        movementRNG = Random.Range(0, 100);

        if (!otherPlayerIsWinning)
        {
            inaccuateRNG = Random.Range(0, 100);

            if (inaccuateRNG >= 50)
            {
                isInaccuate = true;
            }
            else
            {
                isInaccuate = false;
            }

            inputValueRNG = isInaccuate ? Random.Range(falseMovementAmount.x, falseMovementAmount.y) : 0;
        }
        else
        {
            inputValueRNG = isInaccuate ? ball.ReturnPredectedData().y : 0;
            isInaccuate = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ballDetectDistance);
    }
}
