using UnityEngine;

public class GameplayStartGame : MonoBehaviour
{
    [SerializeField] private GameObject StartUI;

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(GameplayMaster.GameState == GameState.Idle)
            {
                GameplayMaster.CallGameState(GameState.InPlay);
                StartUI.SetActive(false);
            }
        }
    }
}
