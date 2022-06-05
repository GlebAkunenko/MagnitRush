using UnityEngine;

public class KeyboardInput : PlayerInput
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            RegisterPlayerAction(PlayerAction.Left);
        else if (Input.GetKeyDown(KeyCode.D))
            RegisterPlayerAction(PlayerAction.Right);
        else if (Input.GetKeyDown(KeyCode.W))
            RegisterPlayerAction(PlayerAction.Up);
        else if (Input.GetKeyDown(KeyCode.S))
            RegisterPlayerAction(PlayerAction.Down);
    }
}
