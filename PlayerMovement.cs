using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float startSpeed;
    [SerializeField]
    private float acceleration;

    private float speed;

    private new Transform transform;

    public bool Move { get; set; }
    public float Speed => speed;

    public void StartMove() => Move = true;
    public void Stop() => Move = false;

    private void Start()
    {
        transform = GetComponent<Transform>();
        speed = startSpeed;
    }

    private void Update()
    {
        if (!Move)
            return;

        Vector3 move = speed * Time.deltaTime * GlobalSettings.MovementDirection;
        transform.position += move;
        speed += acceleration * Time.deltaTime;

        DistanceTraveled?.Invoke(move.magnitude);
        SpeedChanged?.Invoke(speed / startSpeed);
    }

    public event Action<float> DistanceTraveled;
    public event Action<float> SpeedChanged;

}