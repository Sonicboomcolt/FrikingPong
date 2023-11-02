using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    private Rigidbody2D ballRigidbody; //The Rigidbody on the ball.
    private Vector2 startingDir; //The starting direction of the ball.
    private Collider2D ourCollider;

    [Tooltip("The speed that the ball will apply force.")]
    private float baseBallSpeed;
    [SerializeField] private float ballSpeed; //The speed that the ball will apply force.
    [SerializeField] private float ballSpeedAdd; //How much we add to the balls speed every bounce.

    private void Start()
    {
        //Grab the Rigidbody of the ball.
        ballRigidbody = GetComponent<Rigidbody2D>();
        ourCollider = GetComponent<Collider2D>();

        GameplayMaster.OnGameStateChanged += BallStart;
        GameplayMaster.OnLaunchBall += LauchBall;
        GameplayMaster.OnResetBall += ResetBall;

        //Set the base speed so we can reset back to it.
        baseBallSpeed = ballSpeed;
    }

    private void OnDisable()
    {
        GameplayMaster.OnGameStateChanged -= BallStart;
        GameplayMaster.OnLaunchBall -= LauchBall;
        GameplayMaster.OnResetBall -= ResetBall;
    }

    public float ReturnBallSpeed()
    {
        return ballSpeed;
    }

    private void LauchBall()
    {
        BallStart(GameplayMaster.GameState);
    }

    /// <summary>
    /// Force the ball in a direction to start the game.
    /// </summary>
    private void BallStart(GameState newState)
    {
        //Just a sanity check.
        if (newState == GameState.Idle) return; //If the game is idle, no need to do anything.
        if (!ballRigidbody) Debug.LogError("Failed to grab the Rigidbody off of the ball.");
        if (!ballRigidbody) return;

        //Set a Random number with a range of 0 - 100.
        var RNG = Random.Range(0, 100);

        if (RNG > 50) { startingDir = (Vector2.right + Vector2.down).normalized; } else { startingDir = (Vector2.left + Vector2.down).normalized; }

        PushBall(startingDir);
    }

    /// <summary>
    /// Reset the ball back to the starting point.
    /// </summary>
    private void ResetBall()
    {
        //This might not be the best way of doing it, but since the ball starts at 0,0,0. Might as well.
        transform.position = Vector3.zero;
        ballRigidbody.velocity = Vector3.zero;
        ballSpeed = baseBallSpeed;
    }

    /// <summary>
    /// Push the ball in a set direction, this will use the ballSpeed for how hard it will be pushed.
    /// </summary>
    /// <param name="pushDirection"></param>
    public void PushBall(Vector2 pushDirection)
    {
        ballRigidbody.AddForce(pushDirection.normalized * ballSpeed, ForceMode2D.Impulse);

        Vector2 clampVel = Vector2.ClampMagnitude(ballRigidbody.velocity, ballSpeed);
        ballRigidbody.velocity = clampVel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballSpeed += ballSpeedAdd;
        AudioController.instance.PlayBallHit();


        ContactPoint2D contact = collision.GetContact(0);
        Vector2 contactPoint = contact.point;
        Vector2 ownCenter = ourCollider.bounds.center;

        if (contactPoint.y > ownCenter.y)
        {
            PushBall(Vector2.down);
        }
        else
        {
            PushBall(Vector2.up);
        }

        if (contactPoint.x > ownCenter.x)
        {
            PushBall(Vector2.left);
        }
        else
        {
            PushBall(Vector2.right);
        }
    }
}
