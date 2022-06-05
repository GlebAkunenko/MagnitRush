using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : PlayerInput, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    private float ingoreSencority;

    public void OnBeginDrag(PointerEventData eventData)
    {
        HandleTouch(eventData);
    }

    public void OnDrag(PointerEventData eventData) { }

    private void HandleTouch(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        if (delta.magnitude <= ingoreSencority)
            return;
        float x = delta.x;
        float y = delta.y;
        if (Mathf.Abs(x) > Mathf.Abs(y)) {
            if (x < 0)
                RegisterPlayerAction(PlayerAction.Left);
            else
                RegisterPlayerAction(PlayerAction.Right);
        }
        else {
            if (y > 0)
                RegisterPlayerAction(PlayerAction.Up);
            else
                RegisterPlayerAction(PlayerAction.Down);
        }
    }
}