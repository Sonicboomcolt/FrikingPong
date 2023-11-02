using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PaddleController
{
    private BallController ball;
    private float verticalInput;

    [Header("AI Data")]
    [SerializeField] private Vector2 falseMovementAmount;

    private float RNG;
    private float inputValueRNG;
    private bool isInaccuate;

    private void Start()
    {
        //FindFirstObjectByType can be slow, but for this instance this is fine due to it calling once.
        ball = FindFirstObjectByType<BallController>();
    }

    private void Update()
    {
        if (!ball) return;
        if ((ball.transform.position.y + inputValueRNG) > transform.position.y) { verticalInput = Mathf.Lerp(verticalInput, 1, Time.deltaTime * paddleSpeed); }    //Move up.
        //if (ball.transform.position.y == transform.position.y) { verticalInput = 0; }   //Don't move.
        if ((ball.transform.position.y - inputValueRNG) < transform.position.y) { verticalInput = Mathf.Lerp(verticalInput, -1, Time.deltaTime * paddleSpeed); }   //Move Down.

        VerticalInput(verticalInput);
        MovePaddle();
    }

    public override void PaddleHitEvent()
    {
        base.PaddleHitEvent();

        RNG = Random.Range(0, 100);

        if (RNG >= 50)
        {
            isInaccuate = true;
        }
        else
        {
            isInaccuate = false;
        }

        inputValueRNG = isInaccuate ? Random.Range(falseMovementAmount.x, falseMovementAmount.y) : 0;
    }
}
