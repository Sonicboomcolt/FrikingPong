using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleController : MonoBehaviour
{
    [SerializeField] protected float paddleSpeed = 5f;
    [SerializeField] private float paddleClamp;
    private float verticalPos;

    [Range(1, 2)]
    [SerializeField] protected int paddlePlayer;

    private void OnEnable()
    {
        GameplayMaster.OnPaddleHit += PaddleHitEvent;
    }

    private void OnDisable()
    {
        GameplayMaster.OnPaddleHit -= PaddleHitEvent;
    }

    /// <summary>
    /// The input needed to move the paddle up and down.
    /// </summary>
    /// <param name="inputValue"></param>
    public virtual void VerticalInput(float inputValue)
    {
        //No need to register inputs if the game state is not playing.
        if (GameplayMaster.GameState == GameState.Idle) return;
        verticalPos = transform.position.y + inputValue;
    }

    /// <summary>
    /// Call this every frame to allow the paddle to move.
    /// </summary>
    public virtual void MovePaddle()
    {
        var newPos = transform.position;
        newPos.y = Mathf.Lerp(transform.position.y, verticalPos, Time.deltaTime * paddleSpeed);

        if (Mathf.Abs(transform.position.y) >= paddleClamp)
        {
            newPos.y = Mathf.Clamp(newPos.y, -paddleClamp, paddleClamp);
        }
        
        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            GameplayMaster.CallPaddleHit();
        }
    }

    public virtual void PaddleHitEvent()
    {

    }
}
