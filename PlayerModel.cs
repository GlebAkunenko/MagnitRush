using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour
{
    public Player Player { get; set; }

    public void Fall(float horizontalSpeed)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        rigidbody.AddForce(GlobalSettings.MovementDirection * horizontalSpeed, ForceMode.VelocityChange);
    }
}
