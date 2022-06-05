using System.Collections.Generic;
using UnityEngine;

public class PlateGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameObject leftPlatePrefab;
    [SerializeField] 
    private GameObject midPlatePrefab;
    [SerializeField] 
    private GameObject rightPlatePrefab;

    [SerializeField]
    private LevelPlate frontPlate;

    [SerializeField]
    private int updatePoolPlateIndex;
    [SerializeField]
    private float jumpSpace;
    [SerializeField]
    private float extraJumpBuffer;
    [SerializeField]
    private PlateSet[] plateSets;

    private Vector3 Offset => -GlobalSettings.MovementDirection * jumpSpace;

    public LevelPlate FrontPlate => frontPlate;

    public float JumpBuffer => extraJumpBuffer;

    public float OffsetSize => jumpSpace;

    private LevelPlate[] pool1;
    private LevelPlate[] pool2;

    private PlateSet GetRandomSet() => plateSets[Random.Range(0, plateSets.Length)];

    private void Start()
    {
        frontPlate.Init(this);
        InitSets();
        CreatePools();
    }

    private void InitSets()
    {
        foreach (PlateSet set in plateSets)
            set.Init(this);
    }

    private float GetPlateDistance(LevelPlate plate)
    {
        return GlobalSettings.TravelDirection.GetDirectionParameter(plate.EndPoint);
    }

    private void CreatePools()
    {
        pool1 = plateSets[0].GenerateSet(GetPlateDistance(frontPlate));
        pool2 = plateSets[0].GenerateSet(GetPlateDistance(pool1[pool1.Length - 1]));
    }

    private void UpdatePools()
    {
        foreach (LevelPlate plate in pool1)
            plate.Remove();
        pool1 = pool2;
        pool2 = GetRandomSet().GenerateSet(GetPlateDistance(pool1[pool1.Length - 1]));
    }

    private GameObject GetPlatePrefab(Orientation position)
    {
        return position switch {
            Orientation.Mid => midPlatePrefab,
            Orientation.Left => leftPlatePrefab,
            Orientation.Right => rightPlatePrefab,
            _ => throw new System.Exception("Wrong position"),
        };
    }

    public LevelPlate CreatePlate(float distance, Orientation orientation)
    {
        GameObject prefab = GetPlatePrefab(orientation);
        Vector3 pos = prefab.transform.position + GlobalSettings.MovementDirection * distance + Offset;
        var o = Instantiate(prefab, pos, prefab.transform.rotation);
        return frontPlate = o.GetComponent<LevelPlate>();
    }

    public void RegisterPlayerFall(LevelPlate plate)
    {
        if (pool2[updatePoolPlateIndex] == plate)
            UpdatePools();
    }

}
