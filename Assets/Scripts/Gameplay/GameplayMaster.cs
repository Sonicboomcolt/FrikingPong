using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Idle,
    InPlay
}

public abstract class GameplayMaster
{
    //This is a simple way of doing a "singleton".
    //I know it is not really but it works for this case.

    public static GameState GameState { get; private set; }

    //Game events
    public static UnityAction<GameState> OnGameStateChanged;
    public static void CallGameState(GameState newState) { OnGameStateChanged?.Invoke(newState); GameState = newState; }

    public static UnityAction<int, int> OnScore;
    public static void CallAddScore(int player, int scoreAmount) { OnScore?.Invoke(player, scoreAmount); }

    public static UnityAction OnUIScoreUpdate;
    public static void CallUpdateUIScore() { OnUIScoreUpdate?.Invoke(); }

    public static UnityAction OnPaddleHit;
    public static void CallPaddleHit() { OnPaddleHit?.Invoke(); }

    //Ball events
    public static UnityAction OnResetBall;
    public static void CallResetBall() { OnResetBall?.Invoke(); }

    public static UnityAction OnLaunchBall;
    public static void CallLaunchBall() { OnLaunchBall?.Invoke(); }
}
