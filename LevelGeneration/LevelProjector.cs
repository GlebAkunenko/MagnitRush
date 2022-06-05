using UnityEngine;

public class LevelProjector : LevelElement
{
    private void SetScale(int index)
    {
        transform.localScale = Vector3.one - 2 * Vector3.Cross(direction, Vector3.up) * (index % 2);
    }

    public override void Init(CommonGenerator generator, int index)
    {
        base.Init(generator, index);
        SetScale(index);
    }

    public override void Regen(Vector3 position, int index)
    {
        base.Regen(position, index);
        SetScale(index);
    }
}
