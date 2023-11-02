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
    [SerializeField] private float middleDistanceRange;

    private float inaccuateRNG;
    private float inputValueRNG;
    private bool isInaccuate;
    private bool playerIsWinning;

    private float movementRNG;

    private void Start()
    {
        //FindFirstObjectByType can be slow, but for this instance this is fine due to it calling once.
        ball = FindFirstObjectByType<BallController>();
    }

    private void Update()
    {
        if (!ball) return;


        float distanceFromBall = Vector2.Distance(transform.position, ball.transform.position);

        if(distanceFromBall >= ballDetectDistance)
        {
            if(movementRNG >= 50)
            {
                if (ball.transform.position.y >= middleDistanceRange)
                {
                    SetVerticalInputValue(-1);
                }
                else if (ball.transform.position.y <= middleDistanceRange)
                {
                    SetVerticalInputValue(1);
                }
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

        VerticalInput(verticalInput);
        MovePaddle();
    }

    private void AITrackBallMovement()
    {
        if ((ball.transform.position.y + inputValueRNG) > transform.position.y) { SetVerticalInputValue(1); }    //Move up.
        if (ball.transform.position.y == transform.position.y) { verticalInput = 0; }   //Don't move.
        if ((ball.transform.position.y - inputValueRNG) < transform.position.y) { SetVerticalInputValue(-1); }   //Move Down.
    }

    private void SetVerticalInputValue(float newValue)
    {
        verticalInput = Mathf.Lerp(verticalInput, newValue, Time.deltaTime * paddleSpeed);
    }

    public override void PaddleHitEvent()
    {
        base.PaddleHitEvent();

        playerIsWinning = GameplayScoreHandler.instance.ReturnScore(1) > GameplayScoreHandler.instance.ReturnScore(2);

        movementRNG = Random.Range(0, 100);

        if (!playerIsWinning)
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
            inputValueRNG = isInaccuate ? ball.ReturnBallSpeed() : 0;
            isInaccuate = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ballDetectDistance);
    }
}
