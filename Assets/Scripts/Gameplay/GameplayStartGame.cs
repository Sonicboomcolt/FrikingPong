using UnityEngine;

public class GameplayStartGame : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(GameplayMaster.GameState == GameState.Idle)
                GameplayMaster.CallGameState(GameState.InPlay);
        }
    }
}
