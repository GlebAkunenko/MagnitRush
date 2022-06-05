using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlate : MonoBehaviour
{
    [SerializeField]
    private Plate plate;
    [SerializeField]
    private Animator armAnimator;

    private new Transform transform;
    private PlateGenerator generator;

    public Plate Plate => plate;

    public Vector3 Position => transform.position;
    public Vector3 EndPoint => Plate.Scale.GetEndPoint();
    public Quaternion Rotation => transform.rotation;

    public void Init(PlateGenerator generator)
    {
        transform = GetComponent<Transform>();
        armAnimator = GetComponent<Animator>();
        this.generator = generator;
    }

    public void Init(PlateGenerator generator, float lenght, Polarity polarity)
    {
        transform = GetComponent<Transform>();
        armAnimator = GetComponent<Animator>();
        this.generator = generator;
        plate.SetUpSize(transform.position, lenght + generator.OffsetSize, generator.JumpBuffer);
        plate.SetUpPolarity(polarity);
    }

    public void OnPlayerFall()
    {
        if (generator != null)
            generator.RegisterPlayerFall(this);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}