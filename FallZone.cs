using UnityEngine;

public class FallZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerModel playerModel)) {
            if (!playerModel.Player.IsFly)
                playerModel.Player.Lose();
        }
    }
}
