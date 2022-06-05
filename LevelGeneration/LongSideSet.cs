using UnityEngine;

public class LongSideSet : PlateSet
{
    [SerializeField] private float minPlateSize;
    [SerializeField] private float maxPlateSize;
    [SerializeField] private float space;
    [SerializeField] private float extraLongPlateSize;

    private Orientation GetOrientation(Orientation mainOritentation)
    {
        if (mainOritentation != Orientation.Mid)
            return Orientation.Mid;
        float test = 0;
        while (test == 0)
            test = Random.Range(-1f, 1f);
        return (Orientation)Mathf.Sign(test);
    }

    private float[] GetPlateSizes(int count)
    {
        float[] result = new float[count];
        for (int i = 0; i < count; i++)
            result[i] = GetRandomLenght(minPlateSize, maxPlateSize);
        return result;
    }

    private float SolveMainPlateSize(float[] plateSizes)
    {
        float result = 0;
        for (int i = 0; i < plateSizes.Length; i++)
            result += plateSizes[i] + space;
        return result + extraLongPlateSize;
    }

    protected override LevelPlate[] GenerateSet(float startDistance, int count)
    {
        TravelDirection dir = GlobalSettings.TravelDirection;
        LevelPlate[] result = new LevelPlate[count + 1];
        Orientation mainOrientation = GetOrientation(generator.FrontPlate.Plate.GetOrientation());
        float[] sizes = GetPlateSizes(count);
        int lastIndex = count;

        float distance = startDistance + space;
        for (int i = 0; i < count; i++) {
            LevelPlate p = generator.CreatePlate(distance, GetOrientation(mainOrientation));
            result[i] = p;
            p.Init(generator, sizes[i], GetRandomPolarity());
            distance = dir.GetDirectionParameter(p.EndPoint) + space;
        }

        result[lastIndex] = generator.CreatePlate(startDistance, mainOrientation);
        result[lastIndex].Init(generator, SolveMainPlateSize(sizes), GetRandomPolarity());

        return result;
    }

}
