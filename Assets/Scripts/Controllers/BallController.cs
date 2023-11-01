using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    private Rigidbody2D ballRigidbody; //The Rigidbody on the ball.
    private Vector2 startingDir; //The starting direction of the ball.

    [Tooltip("The speed that the ball will apply force.")]
    [SerializeField] private float ballSpeed; //The speed that the ball will apply force.

    private void Start()
    {
        //Grab the Rigidbody of the ball.
        ballRigidbody = GetComponent<Rigidbody2D>();

        GameplayMaster.instance.OnStartGame += BallStart;
    }

    private void OnDisable()
    {
        GameplayMaster.instance.OnStartGame -= BallStart;
    }

    /// <summary>
    /// Force the ball in a direction to start the game.
    /// </summary>
    private void BallStart()
    {
        //Just a sanity check.
        if (!ballRigidbody) Debug.LogError("Failed to grab the Rigidbody off of the ball.");
        if (!ballRigidbody) return;

        //Set a Random number with a range of 0 - 100.
        var RNG = Random.Range(0, 100);

        if (RNG > 50) { startingDir = (Vector2.right + Vector2.down).normalized; } else { startingDir = (Vector2.left + Vector2.down).normalized; }

        PushBall(startingDir);
    }

    /// <summary>
    /// Push the ball in a set direction, this will use the ballSpeed for how hard it will be pushed.
    /// </summary>
    /// <param name="pushDirection"></param>
    public void PushBall(Vector2 pushDirection)
    {
        ballRigidbody.AddForce(pushDirection * ballSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Make the ball "bounce" off of stuff.
        var reflectDir = Vector2.Reflect(ballRigidbody.velocity.normalized, collision.contacts[0].point);
        PushBall(reflectDir);
    }
}
