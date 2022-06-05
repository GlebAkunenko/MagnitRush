using UnityEngine;

public struct TravelDirection
{
    private Vector3 vector;

    public TravelDirection(Enum dir)
    {
        vector = dir switch {
            Enum.Ox => Vector3.right,
            Enum.Oy => Vector3.up,
            Enum.Oz => Vector3.forward,
            _ => throw new System.NotImplementedException(),
        };
    }

    public Vector3 Direction => vector;

    /// <summary>
    /// If direction is Ox return vector.x; Oy -> vector.y; Oz -> vector.z
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public float GetDirectionParameter(Vector3 vector) => Vector3.Project(vector, Direction).magnitude;

    public static Vector3 DirectionFromEnum(Enum e) => new TravelDirection(e).Direction;
    public static TravelDirection Ox => new TravelDirection(Enum.Ox);
    public static TravelDirection Oy => new TravelDirection(Enum.Oy);
    public static TravelDirection Oz => new TravelDirection(Enum.Oz);


    public enum Enum
    {
        Ox,
        Oy,
        Oz
    }
}