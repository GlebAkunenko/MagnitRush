using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnit : MonoBehaviour
{
    [SerializeField]
    private Polarity polarity = Polarity.North;

    public Polarity Polarity { get => polarity; set => polarity = value; }

    public void ChangePolarity()
    {
        if (polarity == Polarity.North)
            polarity = Polarity.South;
        else if (polarity == Polarity.South)
            polarity = Polarity.North;
    }


    //public Polarity Polarity => polarity;

    //public Vector3 Normal => transform.TransformDirection(Vector3.up);

    //public Vector3 MagneticLine => (int)polarity * Normal;

    //public static bool IsAttract(Magnit a, Magnit b)
    //{
    //    Vector3 lineA = a.MagneticLine;
    //    Vector3 lineB = Vector3.Project(b.MagneticLine, lineA);
    //    return Mathf.Sign(lineA.magnitude - (lineA - lineB).magnitude) == 1;
    //}


}

public enum Polarity
{
    North = 1,
    South = -1,
    Neutral = 0
}