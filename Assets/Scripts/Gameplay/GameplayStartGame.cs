using UnityEngine;

public class GameplayStartGame : MonoBehaviour
{
    [SerializeField] private GameObject StartUI;

    private void Start()
    {
        StartUI.SetActive(true);
    }

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
