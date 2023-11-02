using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PaddleController
{
    private void Update()
    {
        VerticalInput(Input.GetAxis("Vertical"));
        MovePaddle();
    }
}
