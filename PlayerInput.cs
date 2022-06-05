using System;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{
    public event Action<PlayerAction> PlayerSwaped;

    protected void RegisterPlayerAction(PlayerAction action) => PlayerSwaped?.Invoke(action);
}

public enum PlayerAction
{
    Nothing = 0,
    Right,
    Up,
    Left,
    Down
}
