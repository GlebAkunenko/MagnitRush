using UnityEngine;

public abstract class PlateSet : MonoBehaviour
{
    [SerializeField] private int minCount;
    [SerializeField] private int maxCount;
    protected PlateGenerator generator;

    public void Init(PlateGenerator generator)
    {
        this.generator = generator;
    }

    protected Polarity GetRandomPolarity()
    {
        if (Random.Range(0, 2) == 0)
            return Polarity.North;
        return Polarity.South;
    }

    protected float GetRandomLenght(float minPlateLenght, float maxPlateLenght)
    {
        return Random.Range(minPlateLenght, maxPlateLenght);
    }

    protected abstract LevelPlate[] GenerateSet(float startDistance, int count);

    public LevelPlate[] GenerateSet(float startDistance) => GenerateSet(startDistance, Random.Range(minCount, maxCount + 1));
}
