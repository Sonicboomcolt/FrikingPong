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

    public static UnityAction<GameState> OnGameStateChanged;
    public static void CallGameState(GameState newState) { OnGameStateChanged?.Invoke(newState); GameState = newState; }

    public static UnityAction<int, int> OnScore;
    public static void CallStartGame(int player, int scoreAmount) { OnScore?.Invoke(player, scoreAmount); }
}
