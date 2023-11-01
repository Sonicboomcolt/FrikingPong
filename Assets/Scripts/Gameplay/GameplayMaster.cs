using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayMaster : MonoBehaviour
{
    //This is a simple way of doing a "singleton".
    //I know it is not really but it works for this case.
    public static GameplayMaster instance;

    private void Awake()
    {
        instance = this;
    }

    public UnityAction OnStartGame;
    public void CallStartGame() { OnStartGame?.Invoke(); }

    public UnityAction<int, int> OnScore;
    public void CallStartGame(int player, int scoreAmount) { OnScore?.Invoke(player, scoreAmount); }
}
