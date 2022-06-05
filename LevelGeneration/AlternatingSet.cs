using UnityEngine;

public class AlternatingSet : PlateSet
{
    [SerializeField] private float minPlateSize;
    [SerializeField] private float maxPlateSize;

    protected Orientation AlternatingOrientation(Orientation current)
    {
        if (current != Orientation.Mid)
            return Orientation.Mid;
        return Random.value <= 0.5f ? Orientation.Left : Orientation.Right;
    }

    protected override LevelPlate[] GenerateSet(float startDistance, int count)
    {
        TravelDirection dir = GlobalSettings.TravelDirection;
        float distance = startDistance;
        LevelPlate[] result = new LevelPlate[count];
        for(int i = 0; i < count; i++) {
            LevelPlate p = generator.CreatePlate(
                distance,
                AlternatingOrientation(generator.FrontPlate.Plate.GetOrientation()));
            result[i] = p;
            p.Init(generator, GetRandomLenght(minPlateSize, maxPlateSize), GetRandomPolarity());
            distance = dir.GetDirectionParameter(p.EndPoint);
        }
        return result;
    }
}
